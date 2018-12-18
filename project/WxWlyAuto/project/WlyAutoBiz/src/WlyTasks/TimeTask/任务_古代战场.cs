// *******************************************************************
// * 文件名称： 任务_古代战场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 22:09:33
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 古代战场
    /// </summary>
    public class 任务_古代战场 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_古代战场(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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

            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_古代战场);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(493, 444));

            var wait = FlowLogicHelper.RepeatRun(() => DMService.Instance.FindStr(dmGuid, new WxRect(449, 77, 504, 94), "剩余时间", "00fc39-000000"),
                TimeSpan.FromSeconds(60));
            if (!wait)
            {
                throw new InvalidOperationException("无法进入古代战场");
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(125, 500), TimeSpan.FromSeconds(2));
            if (DMService.Instance.FindStr(dmGuid, new WxRect(331, 373, 360, 393), "战", WlyColor.Normal))
            {
                DMService.Instance.RepeatLeftClick(dmGuid, new WxPoint(344, 382), 6, 10);
                var view = WlyViewMgr.GetView(WlyViewType.场景_战斗);

                FlowLogicHelper.RepeatRun(() =>
                {
                    if (view.IsCurrentView(dmGuid))
                    {
                        view.Exit(dmGuid);
                        return true;
                    }

                    return false;
                }, TimeSpan.FromSeconds(30));
                Thread.Sleep(5000);
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(913, 560));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}