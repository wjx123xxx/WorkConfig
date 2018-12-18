// *******************************************************************
// * 文件名称： 任务_群雄逐鹿.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:36:38
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 群雄逐鹿
    /// </summary>
    [WlyTask(WlyTaskType.群雄逐鹿)]
    public class 任务_群雄逐鹿 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_群雄逐鹿(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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
            var todayDayOfWeek = DateTime.Today.DayOfWeek;
            var timeCheck = (todayDayOfWeek == DayOfWeek.Saturday) || (todayDayOfWeek == DayOfWeek.Sunday);
            return entity.AccountInfo.GetSwitchInfo(WlySwitchType.群雄逐鹿).Enable && timeCheck;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            if (!entity.AccountInfo.GetSwitchInfo(WlySwitchType.群雄逐鹿).Enable)
            {
                return new WlyTaskInfo(ID, true);
            }

            var dmGuid = entity.DMGuid;

            // 计算时间
            WlyViewMgr.GoTo(dmGuid, WlyViewType.日常_群雄逐鹿);

            var result = DMService.Instance.FindStr(dmGuid, new WxRect(462, 360, 530, 391), "开始匹配", WlyColor.Normal);
            if (result)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(491, 376));
            }

            return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(2));
        }

        #endregion
    }
}