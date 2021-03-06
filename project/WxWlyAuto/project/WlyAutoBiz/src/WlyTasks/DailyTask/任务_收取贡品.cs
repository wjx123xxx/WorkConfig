﻿// *******************************************************************
// * 文件名称： 任务_收取贡品.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 08:31:30
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 收取贡品
    /// </summary>
    public class 任务_收取贡品 : WlyDailyTask
    {
        #region Constructors

        public 任务_收取贡品(string id) : base(id)
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

            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_属臣);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(592, 469));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}