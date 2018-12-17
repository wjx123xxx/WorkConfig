// *******************************************************************
// * 文件名称： WlyProgressMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-11 23:49:46
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 推图管理器
    /// </summary>
    public static class WlyProgressMgr
    {
        #region Fields

        private static readonly WxRect _mainRect = new WxRect(439, 567, 562, 590);

        private static readonly string m_filePath;

        /// <summary>
        /// 节点字典
        /// </summary>
        private static ConcurrentDictionary<string, WlyProgressNode> m_progressDict = new ConcurrentDictionary<string, WlyProgressNode>();

        private static object m_saveLocker = new object();

        #endregion

        #region Constructors

        static WlyProgressMgr()
        {
            m_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "global", "progress.data");
            if (File.Exists(m_filePath))
            {
                m_progressDict = JsonHelper.LoadFromXmlFile<ConcurrentDictionary<string, WlyProgressNode>>(m_filePath);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 推图
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        public static bool Attack(WlyEntity entity, int index)
        {
            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_副本);
            var view = WlyViewMgr.GetView(WlyViewType.导航_日常按钮栏);
            if (view.IsCurrentView(dmGuid))
            {
                view.Exit(dmGuid);
            }

            WlyProgressNode current = null;
            WlyProgressNode previous = null;
            if (!string.IsNullOrEmpty(entity.AccountInfo.ProgressNode))
            {
                m_progressDict.TryGetValue(entity.AccountInfo.ProgressNode, out previous);
            }

            if ((previous != null) && !string.IsNullOrEmpty(previous.Next))
            {
                current = m_progressDict[previous.Next];
            }

            while (true)
            {
                GoToMain(index, dmGuid);
                if (DMService.Instance.FindPic(dmGuid, WlyPicType.下一个副本, new WxRect(567, 567, 592, 589)))
                {
                    return true;
                }

                if (DMService.Instance.FindPic(dmGuid, WlyPicType.首攻军团, WlyUtilityBiz.GameWndRect))
                {
                    return true;
                }

                // 寻找下一节点
                if (current == null)
                {
                    var node = GetSubPoint(dmGuid, index, previous?.ID);
                    if (!m_progressDict.TryGetValue(node.ID, out current))
                    {
                        current = node;
                    }
                }

                bool? result;
                try
                {
                    result = Attack(current.Location, dmGuid);
                    if (result == false)
                    {
                        return false;
                    }
                }
                catch
                {
                    entity.AccountInfo.ProgressNode = string.Empty;
                    entity.AccountInfo.Save();
                    throw;
                }

                entity.AccountInfo.ProgressNode = current.ID;
                entity.AccountInfo.Save();

                if (result == null)
                {
                    if (!string.IsNullOrEmpty(current.Next))
                    {
                        current = m_progressDict[current.Next];
                    }
                    else
                    {
                        current = null;
                    }
                }
                else
                {
                    var key = false;
                    if ((previous != null) && (previous.Next == null))
                    {
                        previous.Next = current.ID;
                        key = true;
                    }

                    if ((current.Previous == null) && (previous != null))
                    {
                        current.Previous = previous.ID;
                        key = true;
                    }

                    if (!m_progressDict.ContainsKey(current.ID))
                    {
                        current = m_progressDict.GetOrAdd(current.ID, current);
                        key = true;
                    }

                    if (key)
                    {
                        Save();
                    }

                    if (current.IsEnd)
                    {
                        return true;
                    }

                    previous = current;
                    if (string.IsNullOrEmpty(current.Next))
                    {
                        current = null;
                    }
                    else
                    {
                        m_progressDict.TryGetValue(current.Next, out current);
                    }
                }
            }
        }

        /// <summary>
        /// 提供首攻给大号
        /// </summary>
        /// <param name="entity"></param>
        public static void FreeAttack(WlyEntity entity)
        {
            return;

            // 检测大号是否处于首攻检测中
            if (!WlyUtilityBiz.FreeAttack)
            {
                return;
            }

            // 检测免费首攻是否已经用完
            var free = Math.Max(entity.AccountInfo.FreeProgress, 4);
            var last = entity.AccountInfo.Progress;
            if (free > last)
            {
                return;
            }

            var dmGuid = entity.DMGuid;
            GoToMain(free, dmGuid);

            var key = false;
            int x = 0, y = 0;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindPic(dmGuid, WlyPicType.首攻军团, WlyUtilityBiz.GameWndRect, out x, out y))
                {
                    key = true;
                    return true;
                }

                if (DMService.Instance.FindPic(dmGuid, WlyPicType.收费军团, WlyUtilityBiz.GameWndRect, out x, out y))
                {
                    key = false;
                    return true;
                }

                Thread.Sleep(500);
                return false;
            }, TimeSpan.FromSeconds(10));

            if (!wait)
            {
                entity.AccountInfo.ResetLoginTime();
                throw new InvalidOperationException();
            }

            if (key)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));

                // 等待攻击按钮可用
                wait = FlowLogicHelper.RepeatRun(() =>
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(584, 304));
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(477, 95, 528, 124), "军团战", WlyColor.Normal))
                    {
                        return true;
                    }

                    Thread.Sleep(500);
                    return false;
                }, TimeSpan.FromSeconds(10));

                if (!wait)
                {
                    entity.AccountInfo.ResetLoginTime();
                    throw new InvalidOperationException();
                }

                // 上锁后开始攻击
                lock (WlyUtilityBiz.FreeAttackLocker)
                {
                    if (!WlyUtilityBiz.FreeAttack)
                    {
                        return;
                    }

                    // 选取相同国家
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(722, 312, 780, 344), "无限制", "e9e7cf-000000"))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(791, 325));
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(755, 361));
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(725, 477), TimeSpan.FromSeconds(1));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(322, 475), TimeSpan.FromSeconds(5));

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(634, 230, 809, 455), "海潮", "e9e7cf-000000", WlyColor.Normal))
                    {
                        // 开战
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(758, 479), TimeSpan.FromSeconds(10));
                        entity.AccountInfo.ResetLoginTime();
                        return;
                        throw new InvalidOperationException();
                    }
                }
            }
            else
            {
                entity.AccountInfo.FreeProgress++;
                entity.AccountInfo.Save();
                FreeAttack(entity);
            }
        }

        public static void GoTo(string dmGuid, int i)
        {
            GoToMain(i, dmGuid);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 攻击指定位置的npc，获取攻击结果
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        private static bool? Attack(WxPoint point, string dmGuid)
        {
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                var v = WlyViewMgr.GetView(WlyViewType.导航_日常按钮栏);
                if (v.IsCurrentView(dmGuid))
                {
                    v.Exit(dmGuid);
                }

                v = WlyViewMgr.GetView(WlyViewType.右侧提示);
                if (v.IsCurrentView(dmGuid))
                {
                    v.Exit(dmGuid);
                }

                v = WlyViewMgr.GetView(WlyViewType.略_点击继续);
                if (v.IsCurrentView(dmGuid))
                {
                    v.Exit(dmGuid);
                }

                Thread.Sleep(100);
                WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_副本);
                DMService.Instance.LeftClick(dmGuid, point);
                return DMService.Instance.FindStr(dmGuid, new WxRect(556, 290, 608, 318), "攻击", WlyColor.Normal);
            }, TimeSpan.FromSeconds(5));
            if (!wait)
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(566, 297, 605, 314), "攻击", "cccccc-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(638, 183));
                    return null;
                }

                throw new InvalidOperationException("无法点开NPC");
            }

            // 判断NPC是否已经完成攻击
            if (DMService.Instance.FindStr(dmGuid, new WxRect(587, 401, 624, 422), "战斗", "66ff00-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(638, 183));
                return null;
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(584, 304));
            var view = WlyViewMgr.GetView(WlyViewType.场景_战斗);
            wait = FlowLogicHelper.RepeatRun(() =>
            {
                Thread.Sleep(2000);
                return view.IsCurrentView(dmGuid);
            }, TimeSpan.FromSeconds(30));

            if (!wait)
            {
                throw new InvalidOperationException("无法进去战斗画面");
            }

            //if (!DMService.Instance.FindStr(dmGuid, new WxRect(791, 566, 826, 591), "关闭", "f3f3da-000000"))
            //{
            //    DMService.Instance.LeftClick(dmGuid, new WxPoint(817, 576));
            //}

            // 等待战斗结果
            bool key = true;
            wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(723, 560, 768, 591), "结果", "f3f3da-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(751, 576));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(439, 205, 560, 238), "获得胜利", "e9e7cf-000000"))
                {
                    return true;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(517, 211, 552, 234), "失败", "e9e7cf-000000"))
                {
                    key = false;
                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(60));
            if (!wait)
            {
                throw new InvalidOperationException("等待不到战斗结果");
            }

            WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(10));
            return key;
        }

        private static int GetIndex(string name)
        {
            return int.Parse(name.Substring(5));
        }

        private static string GetMainName(int main)
        {
            return $"Wly副本{main:D2}";
        }

        private static WlyProgressNode GetSubPoint(string dmGuid, int index, string previousID)
        {
            var location = new WxPoint(0, 0);

            if (previousID == "wly17_347_127")
            {
                location = new WxPoint(394, 94);
            }
            else
            {
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    var find = DMService.Instance.FindPic(dmGuid, WlyPicType.可攻击目标, WlyUtilityBiz.GameWndRect, out var x, out var y);
                    if (find)
                    {
                        location = new WxPoint(x + 7, y + 14);
                        return true;
                    }

                    Thread.Sleep(500);
                    var view = WlyViewMgr.GetView(WlyViewType.导航_日常按钮栏);
                    if (view.IsCurrentView(dmGuid))
                    {
                        view.Exit(dmGuid);
                    }

                    view = WlyViewMgr.GetView(WlyViewType.右侧提示);
                    if (view.IsCurrentView(dmGuid))
                    {
                        view.Exit(dmGuid);
                    }

                    view = WlyViewMgr.GetView(WlyViewType.略_点击继续);
                    if (view.IsCurrentView(dmGuid))
                    {
                        view.Exit(dmGuid);
                    }

                    Thread.Sleep(100);
                    return false;
                }, TimeSpan.FromSeconds(5));

                if (!wait)
                {
                    if (index == 11)
                    {
                        location = new WxPoint(909, 213);
                    }

                    if (index == 17)
                    {
                        location = new WxPoint(347, 127);
                    }
                }
            }

            // 判断是否寻到攻击目标
            if ((location.X == 0) && (location.Y == 0))
            {
                throw new InvalidOperationException("找不到攻击目标");
            }

            // 构建攻击节点
            var node = new WlyProgressNode
            {
                Location = location,
                Progress = index
            };
            return node;
        }

        private static void GoToMain(int main, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_副本);
            var name = GetMainName(main);
            if (DMService.Instance.FindStr(dmGuid, _mainRect, name, "ffffcc-000000"))
            {
                return;
            }

            var current = DMService.Instance.GetWords(dmGuid, _mainRect, "ffffcc-000000");
            var index = GetIndex(current);
            var direct = index < main;

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(468, 356, 508, 381), "副本", WlyColor.Normal))
                {
                    return true;
                }

                var view = WlyViewMgr.GetView(WlyViewType.功能_军团战);
                if (view.IsCurrentView(dmGuid))
                {
                    view.Exit(dmGuid);
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 578));
                return false;
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                throw new InvalidOperationException($"未能找到副本{name}");
            }

            wait = FlowLogicHelper.RepeatRun(() =>
            {
                var result = DMService.Instance.FindStr(dmGuid, new WxRect(420, 377, 567, 568), name, "e9e7cf-000000", out var x, out var y);
                if (result)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    return true;
                }

                if (direct)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(574, 554));
                }
                else
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(576, 393));
                }

                return false;
            }, TimeSpan.FromSeconds(10));

            if (wait)
            {
                GoToMain(main, dmGuid);
            }
            else
            {
                throw new InvalidOperationException($"未能找到副本{name}");
            }
        }

        private static void Save()
        {
            lock (m_saveLocker)
            {
                var nodeList = m_progressDict.Values.ToList();
                foreach (var node in nodeList)
                {
                    if (string.IsNullOrEmpty(node.Previous))
                    {
                        node.Index = 1;
                        var current = node;
                        while (!string.IsNullOrEmpty(current.Next))
                        {
                            var next = m_progressDict[current.Next];
                            next.Index = current.Index + 1;
                            if (current.Progress < next.Progress)
                            {
                                current.IsEnd = true;
                            }

                            current = next;
                        }
                    }
                }

                JsonHelper.SaveToXmlFile(m_progressDict, m_filePath, "ProgressData");
            }
        }

        #endregion
    }
}