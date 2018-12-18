// *******************************************************************
// * 文件名称： 任务_主线奖励.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 22:46:59
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 主线奖励
    /// </summary>
    public class 任务_主线奖励 : WlyMainTask
    {
        #region Constructors

        public 任务_主线奖励(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_主线任务);

            var wait = FlowLogicHelper.RepeatRun(() =>
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
            }, TimeSpan.FromSeconds(120));

            if (!wait)
            {
                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}