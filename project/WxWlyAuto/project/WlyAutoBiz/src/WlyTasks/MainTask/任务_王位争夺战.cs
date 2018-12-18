// *******************************************************************
// * 文件名称： 任务_王位争夺战.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-15 10:59:43
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 王位争夺战
    /// </summary>
    public class 任务_王位争夺战 : WlyDailyTask
    {
        #region Constructors

        public 任务_王位争夺战(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 过滤检测
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool InternalFilter(WlyEntity entity)
        {
            return DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            //var start = DateTime.Today.AddHours(18);
            //var end = DateTime.Today.AddHours(19).AddMinutes(30);

            //if (DateTime.Now < start)
            //{
            //    return new WlyTaskInfo(ID, start);
            //}

            //if (DateTime.Now > end)
            //{
            //    return new WlyTaskInfo(ID, true);
            //}

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_王位争夺战);

            // 报名
            DMService.Instance.LeftClick(dmGuid, new WxPoint(396, 557));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}