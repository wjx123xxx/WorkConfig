// *******************************************************************
// * 文件名称： WlyEntityBiz.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-26 16:38:15
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyUtility
{
    /// <summary>
    /// 实体具体业务
    /// </summary>
    public static class WlyEntityBiz
    {
        #region Public Methods

        /// <summary>
        /// 清理仓库里的装备
        /// </summary>
        /// <param name="entity"></param>
        public static void ClearEquipments(WlyEntity entity)
        {
            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_仓库);

            // 如果花费0金币则 开启
            var result = DMService.Instance.FindStr(dmGuid, new WxRect(390, 464, 434, 490), "0金币", "ffff00-000000");
            if (result)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(411, 456));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(451, 332));
            }

            var startX = 215;
            var startY = 233;
            var column = 8;
            var row = 3;
            var width = 56;
            var height = 56;

            // 清理仓库
            IList<Tuple<WlyStaffType, WlyEquipType>> list = new List<Tuple<WlyStaffType, WlyEquipType>>();
            var sell = false;
            var key = true;

            WlyUtilityBiz.GetAmount(dmGuid, new WxRect(302, 443, 341, 465), "f3f3da-000000", out var cap);
            var amount = 0;

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                for (var r = 0; r < row; r++)
                {
                    if (amount > cap)
                    {
                        return true;
                    }

                    for (var c = 0; c < column; c++)
                    {
                        amount++;
                        if (amount > cap)
                        {
                            return true;
                        }

                        var x = startX + (c * width);
                        var y = startY + (r * height);

                        if (DMService.Instance.FindColor(dmGuid, "17201d-000000", new WxRect(new WxPoint(x, y), 10, 10)))
                        {
                            continue;
                        }

                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                        if (DMService.Instance.FindStr(dmGuid, new WxRect(532, 345, 563, 365), "取消", WlyColor.Normal))
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(547, 356));
                            key = false;
                            r = row;
                            break;
                        }

                        // 使用物品
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            if (DMService.Instance.FindStr(dmGuid, new WxRect(484, 408, 519, 433), "使用", WlyColor.Normal, WlyColor.White))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 417));
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(20)))
                        {
                            throw new InvalidOperationException("使用物品失败");
                        }

                        // 出售装备
                        while (WlyEquipMgr.IsEquipment(dmGuid, new WxRect(712, 339, 808, 358)))
                        {
                            var equipInfo = WlyEquipMgr.GetEquipInfo(dmGuid);
                            var flag = false;
                            foreach (var staff in entity.AccountInfo.StaffInfoDict.Values)
                            {
                                var currentInfo = staff.GetEquipInfo(equipInfo.Type);
                                if ((equipInfo.Quality > currentInfo.Quality)
                                    || ((equipInfo.Quality == currentInfo.Quality) && (equipInfo.EquipLevel > currentInfo.EquipLevel)))
                                {
                                    flag = true;
                                    if ((equipInfo.EquipLevel <= staff.Level)
                                        && (list.FirstOrDefault(o => (o.Item1 == staff.Name) && (o.Item2 == equipInfo.Type)) == null))
                                    {
                                        list.Add(new Tuple<WlyStaffType, WlyEquipType>(staff.Name, equipInfo.Type));
                                    }

                                    break;
                                }
                            }

                            if (flag)
                            {
                                break;
                            }

                            sell = true;
                            if (DMService.Instance.FindStr(dmGuid, new WxRect(473, 406, 536, 430), "一键降级", WlyColor.Normal))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 416));
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));
                            }

                            DMService.Instance.LeftClick(dmGuid, new WxPoint(412, 419));
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));
                        }

                        // 出售将器
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            if (DMService.Instance.FindStr(dmGuid, new WxRect(761, 361, 796, 384), "单挑", "ff0000-000000"))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(412, 419));
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(20)))
                        {
                            throw new InvalidOperationException("出售将器失败");
                        }
                    }
                }

                if (key)
                {
                    for (var count = 0; count < 10; count++)
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(653, 378), TimeSpan.FromMilliseconds(100));
                    }
                }

                return !key;
            }, TimeSpan.FromSeconds(30));

            if (!wait)
            {
                throw new InvalidOperationException("清理仓库超时");
            }

            if (!sell && !list.Any())
            {
                return;
            }

            // 退出，重新进入，进行一键领取
            DMService.Instance.LeftClick(dmGuid, new WxPoint(startX, startY));

            // 尝试领取等级礼包
            if (DMService.Instance.FindStr(dmGuid, new WxRect(713, 315, 807, 334), "主城", "f3f3da-000000"))
            {
                FlowLogicHelper.RunToTarget(() =>
                {
                    var levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(713, 315, 807, 334), "e9e7cf-000000");
                    return int.Parse(levelStr.Substring(0, levelStr.Length - 1));
                }, current => current > entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level, () =>
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(497, 417));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 424), TimeSpan.FromSeconds(2));
                });
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 452));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 452));

            if (list.Any())
            {
                foreach (var group in list.GroupBy(o => o.Item1))
                {
                    var staff = entity.AccountInfo.GetStaffInfo(group.Key);
                    staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, group.Key);
                    foreach (var tuple in group)
                    {
                        WlyEquipMgr.ChangeEquipmenet(dmGuid, tuple.Item1, tuple.Item2, staff.Level);
                        staff.SaveEquipInfo(WlyEquipMgr.GetStaffEquipInfo(dmGuid, staff.Name, tuple.Item2));
                    }
                }

                entity.AccountInfo.Save();

                // 进行递归
                ClearEquipments(entity);
            }
        }

        /// <summary>
        /// 强化所有的装备
        /// </summary>
        /// <param name="entity"></param>
        public static void UpgradeAllEquipmenets(WlyEntity entity)
        {
            // 清理装备
            var dmGuid = entity.DMGuid;
            var cityLevel = entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level;
            foreach (var staff in entity.AccountInfo.StaffInfoDict.Values)
            {
                if ((staff.Level == cityLevel) && (staff.GrowLevel > cityLevel))
                {
                    continue;
                }

                staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, staff.Name);
                if ((cityLevel >= 100) && (staff.Level < 80))
                {
                    WlyStaffMgr.Upgrade(staff.Name, 80, dmGuid);
                    staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, staff.Name);
                }
            }

            ClearEquipments(entity);

            var r = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(274, 47, 321, 72), "f3f3da-000000", out var aa);
            if (r && (aa >= 500000))
            {
                // 遍历进行装备交换
                var staffs = entity.AccountInfo.StaffInfoDict.Values;
                foreach (var a in staffs.ToList())
                {
                    foreach (var b in staffs.ToList())
                    {
                        if (a.Name == b.Name)
                        {
                            continue;
                        }

                        // 遍历装备
                        foreach (var e1 in a.EquipInfoDict.Values)
                        {
                            var e2 = b.GetEquipInfo(e1.Type);
                            if (a.EquipmentRequestDict[e1.Type] && !b.EquipmentRequestDict[e2.Type]
                                                                && ((e1.Quality < e2.Quality)
                                                                    || ((e1.Quality == e2.Quality) && (e1.EquipLevel < e2.EquipLevel))))
                            {
                                WlyStaffMgr.SwitchEquipment(dmGuid, a, b, e1.Type);
                            }
                        }
                    }
                }

                entity.AccountInfo.Save();
            }

            if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(133, 26, 217, 47), "f3f3da-000000", out var amount))
            {
                if ((amount < 10000000) && (entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level >= 100))
                {
                    return;
                }
            }

            var shopLevel = entity.AccountInfo.GetBuildingInfo(WlyBuildingType.商铺).Level;
            var ss = new List<WlyStaffType>();
            foreach (var s in entity.AccountInfo.StaffInfoDict.Values)
            {
                foreach (var e in s.EquipInfoDict.Values)
                {
                    if (s.EquipmentRequestDict[e.Type] && (e.Level < shopLevel) && (e.Quality != WlyQualityType.Unknow))
                    {
                        ss.Add(s.Name);
                        break;
                    }
                }
            }

            if (!ss.Any())
            {
                return;
            }

            var staffstr = string.Join("|", ss.Select(o => o.ToString()));

            // 切换到功能
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_强化装备);

            for (int i = 0; i < 3; i++)
            {
                var result = DMService.Instance.FindStrEx(dmGuid, new WxRect(188, 201, 417, 439), staffstr, WlyColor.White);
                var list = result.Split('|');
                foreach (var pos in list)
                {
                    var ps = pos.Split(',');
                    var index = int.Parse(ps[0]);
                    var point = new WxPoint(int.Parse(ps[1]), int.Parse(ps[2]));
                    var info = entity.AccountInfo.GetStaffInfo(ss[index]);

                    // 点击辨别装备
                    DMService.Instance.LeftClick(dmGuid, point);
                    var equipInfo = WlyEquipMgr.GetEquipInfo(dmGuid);
                    if (info.EquipmentRequestDict[equipInfo.Type] && (equipInfo.Level < shopLevel))
                    {
                        // 开始强化
                        DMService.Instance.LeftDown(dmGuid, new WxPoint(563, 354));
                        var res = FlowLogicHelper.RunToTarget(() => GetEquipLevelCore(dmGuid), current => current >= shopLevel, () => Thread.Sleep(500));
                        DMService.Instance.LeftUp(dmGuid, new WxPoint(563, 354));
                        info.SaveEquipInfo(WlyEquipMgr.GetEquipInfo(dmGuid));
                        if (!res)
                        {
                            // 银币耗尽，直接返回
                            entity.AccountInfo.Save();
                            return;
                        }
                    }
                }

                // 往下翻一页
                DMService.Instance.RepeatLeftClick(dmGuid, new WxPoint(426, 427), 14, 100);
            }

            entity.AccountInfo.Save();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取装备等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        private static int GetEquipLevelCore(string dmGuid)
        {
            var levelStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(713, 289, 812, 313), "e9e7cf-000000");
                return !string.IsNullOrEmpty(levelStr);
            }, TimeSpan.FromSeconds(10));

            if (!wait || !levelStr.Contains("级") || !int.TryParse(levelStr.Substring(0, levelStr.Length - 1), out var currentLevel))
            {
                throw new InvalidOperationException("无法解析装备等级");
            }

            return currentLevel;
        }

        #endregion
    }
}