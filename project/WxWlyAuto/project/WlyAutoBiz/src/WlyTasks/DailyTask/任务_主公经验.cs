// *******************************************************************
// * 文件名称： 任务_主公经验.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 15:21:04
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 领取主公经验
    /// </summary>
    public class 任务_主公经验 : WlyFinalTask
    {
        #region Fields

        private readonly TimeSpan m_interval;

        #endregion

        #region Constructors

        public 任务_主公经验(string id, TimeSpan interval, params string[] depends) : base(id, depends)
        {
            m_interval = interval;
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_主线任务);
            FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(550, 418, 634, 446), "领取|接受",
                    $"{WlyColor.Normal}|{WlyColor.White}"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(592, 436));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(550, 418, 634, 446), "取消",
                    $"{WlyColor.Normal}|{WlyColor.White}"))
                {
                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(20));

            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_每日任务);
            var key = false;
            FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindColor(dmGuid, "4bd8b0-000000", new WxRect(765, 244, 784, 261)))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(721, 220));
                }

                if (DMService.Instance.FindColor(dmGuid, "418674-000000", new WxRect(765, 244, 784, 261)))
                {
                    return true;
                }

                if (DMService.Instance.FindColor(dmGuid, "101b19-000000", new WxRect(new WxPoint(682, 192), 10, 10)))
                {
                    key = true;
                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(20));

            return key ? new WlyTaskInfo(ID, true) : new WlyTaskInfo(ID, DateTime.Now.Add(m_interval));
        }

        #endregion
    }
}