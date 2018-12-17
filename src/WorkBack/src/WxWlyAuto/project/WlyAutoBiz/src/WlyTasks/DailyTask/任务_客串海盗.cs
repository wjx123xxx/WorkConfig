// *******************************************************************
// * 文件名称： 任务_客串海盗.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 08:41:44
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 打劫
    /// </summary>
    [WlyTask(WlyTaskType.客串海盗)]
    public class 任务_客串海盗 : WlyDailyTask
    {
        #region Constructors

        public 任务_客串海盗(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_出航贸易);
            Thread.Sleep(1000);
            if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(642, 561, 663, 588), "31f101-000000", out var count) && (count == 0))
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(30));
            }

            var targets = DMService.Instance.FindPics(dmGuid, WlyPicType.打劫目标, new WxRect(110, 179, 931, 536));
            foreach (var target in targets)
            {
                DMService.Instance.LeftClick(dmGuid, target);
                var wait = DMService.Instance.FindStr(dmGuid, new WxRect(110, 179, 931, 536), "名称", WlyColor.Normal, out var x, out var y);
                if (!wait)
                {
                    continue;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(x - 5, y - 5, x + 150, y + 50), "墨君", WlyColor.White))
                {
                    var nextTime = WlyUtilityBiz.GetTime(dmGuid, new WxRect(x, y, x + 100, y + 100), WlyColor.Selected);
                    if (nextTime == TimeSpan.Zero)
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(706, 577));
                        nextTime = TimeSpan.FromMinutes(10);
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(480, 318, 519, 342), "确定", WlyColor.Normal))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 331));
                    }

                    return new WlyTaskInfo(ID, DateTime.Now.Add(nextTime));
                }
            }

            return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(30));
        }

        #endregion
    }
}