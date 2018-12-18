// *******************************************************************
// * 文件名称： 任务_兵种重修.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-01 17:54:27
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 重修
    /// </summary>
    public class 任务_兵种重修 : WlyDailyTask
    {
        #region Constructors

        public 任务_兵种重修(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            var dmGuid = entity.DMGuid;
            foreach (var staff in entity.AccountInfo.StaffInfoDict.Values)
            {
                if (staff.CurrentType != staff.TargetType)
                {
                    WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_武将);
                    WlyUtilityBiz.SelectStaffInList(dmGuid, staff.Name);
                    staff.CurrentType = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(322, 283, 414, 307));
                    if (staff.CurrentType == staff.TargetType)
                    {
                        continue;
                    }

                    staff.CurrentType = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(430, 283, 496, 307));
                    if (staff.CurrentType == staff.TargetType)
                    {
                        continue;
                    }

                    if (staff.CurrentType <= WlySoldierType.巫术师 && staff.TargetType > WlySoldierType.巫术师 && staff.CurrentType != WlySoldierType.Unkonwn)
                    {
                        WlyStaffMgr.Evolve(staff.Name, dmGuid);
                        WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_武将);
                        WlyUtilityBiz.SelectStaffInList(dmGuid, staff.Name);
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(368, 342));

                    // 等待界面打开
                    var wait = FlowLogicHelper.RepeatRun(() =>
                    {
                        if (DMService.Instance.FindStr(dmGuid, new WxRect(498, 139, 529, 162), "兵种", WlyColor.Normal))
                        {
                            return true;
                        }

                        Thread.Sleep(500);
                        return false;
                    }, TimeSpan.FromSeconds(5));

                    if (!wait)
                    {
                        throw new InvalidOperationException("无法打开重修界面");
                    }

                    while (true)
                    {
                        var nt = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(348, 399, 417, 421));
                        if (nt == staff.TargetType)
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(385, 471));
                            break;
                        }

                        nt = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(436, 399, 513, 426));
                        if (nt == staff.TargetType)
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(385, 471));
                            break;
                        }

                        // 点击重修
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(507, 469));

                        // 确认重修花费
                        var cost = 100;

                        wait = FlowLogicHelper.RepeatRun(() =>
                        {
                            if (DMService.Instance.FindStr(dmGuid, new WxRect(495, 249, 530, 281), "免费", "ff6600-000000"))
                            {
                                cost = 0;
                            }
                            else
                            {
                                var w = FlowLogicHelper.RepeatRun(() =>
                                {
                                    var costStr = DMService.Instance.GetWords(dmGuid, new WxRect(510, 253, 574, 283), "ff6600-000000");
                                    if (string.IsNullOrEmpty(costStr) || !costStr.Contains("金币"))
                                    {
                                        Thread.Sleep(500);
                                        return false;
                                    }

                                    var index = costStr.IndexOf("金币", StringComparison.Ordinal);
                                    cost = int.Parse(costStr.Substring(0, index));
                                    return true;
                                }, TimeSpan.FromSeconds(2));

                                if (!w)
                                {
                                    return false;
                                }
                            }

                            return true;
                        }, TimeSpan.FromSeconds(10));
                        if (!wait)
                        {
                            throw new InvalidOperationException();
                        }

                        if (cost <= 10)
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 353));
                        }
                        else
                        {
                            // 今日重修完成
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(546, 353));
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(627, 467));
                            return new WlyTaskInfo(ID, true);
                        }
                    }

                    // 点击退出
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(627, 467));

                    // 追加确认兵种重修结果
                    WlyUtilityBiz.SelectStaffInList(dmGuid, staff.Name);
                    staff.CurrentType = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(322, 283, 414, 307));
                    if (staff.CurrentType == staff.TargetType)
                    {
                        continue;
                    }

                    staff.CurrentType = WlyStaffMgr.GetStaffType(dmGuid, new WxRect(430, 283, 496, 307));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}