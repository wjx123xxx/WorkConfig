// *******************************************************************
// * 文件名称： 任务_手动打猎.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 11:37:17
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
    /// 手动打猎
    /// </summary>
    public class 任务_手动打猎 : WlyDailyTask
    {
        #region Constructors

        public 任务_手动打猎(string id, params string[] depends) : base(id, depends)
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
            var cityLevel = entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level;

            if (cityLevel <= 80)
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(102, 510));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(491, 235));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(746, 405));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(813, 176));
                WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(10));
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);
                Thread.Sleep(1000);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(102, 510));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(491, 235));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(746, 405));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(813, 176));

                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(324, 498, 339, 520), "e9e7cf-000000", out var amount) && (amount == 0))
                    {
                        return true;
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 253));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 352), TimeSpan.FromSeconds(5));

                    WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);
                    return false;
                }, TimeSpan.FromSeconds(30));

                if (!wait)
                {
                    throw new InvalidOperationException("打猎未正常完成");
                }

                return new WlyTaskInfo(ID, true);
            }

            if (cityLevel < 110)
            {
                // 战功大于50的话 直接返回
                var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(274, 47, 321, 72), "f3f3da-000000", out var amount);
                if (!result || (amount >= 500000))
                {
                    return new WlyTaskInfo(ID, true);
                }
            }

            // 开始打猎
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);
            Thread.Sleep(1000);
            var ww = FlowLogicHelper.RepeatRun(() =>
            {
                if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(324, 498, 339, 520), "e9e7cf-000000", out var amount) && (amount == 0))
                {
                    return true;
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 253));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 352), TimeSpan.FromSeconds(5));

                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);
                return false;
            }, TimeSpan.FromSeconds(30));

            if (!ww)
            {
                throw new InvalidOperationException("打猎未正常完成");
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}