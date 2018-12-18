// *******************************************************************
// * 文件名称： 任务_虎牢关战役.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 21:32:24
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 吕布boss
    /// </summary>
    public class 任务_虎牢关战役 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_虎牢关战役(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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

            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_虎牢关战役);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(496, 483), TimeSpan.FromSeconds(10));
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_虎牢关战役);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(50, 230));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}