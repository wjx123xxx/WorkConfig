// *******************************************************************
// * 文件名称： 任务_收粮.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 09:19:59
// *******************************************************************

using System;

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
    /// 收粮
    /// </summary>
    public class 任务_收粮 : WlyDailyTask
    {
        #region Constructors

        public 任务_收粮(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);
            WlyUtilityBiz.GetPercent(dmGuid, new WxRect(137, 47, 238, 68), "e9e7cf-000000", out var persent);
            if (persent > 0.8)
            {
                return new WlyTaskInfo(ID, true);
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_卖粮);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(760, 343));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(762, 377));
            }

            WlyViewMgr.GoTo(dmGuid, WlyViewType.建筑_农田);

            FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindColor(dmGuid, "aaaaaa-000000", new WxRect(341, 402, 367, 413)))
                {
                    return true;
                }

                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(350, 407), TimeSpan.FromMilliseconds(100)), 5);
                return false;
            }, TimeSpan.FromSeconds(10));

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}