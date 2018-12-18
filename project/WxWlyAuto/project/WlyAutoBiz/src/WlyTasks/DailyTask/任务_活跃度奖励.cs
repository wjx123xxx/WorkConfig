// *******************************************************************
// * 文件名称： 任务_活跃度奖励.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 15:22:43
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 活跃度奖励
    /// </summary>
    public class 任务_活跃度奖励 : WlyFinalTask
    {
        #region Constructors

        public 任务_活跃度奖励(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_活跃);

            Thread.Sleep(1000);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(756, 361));

            var startPoint = new WxPoint(543, 302);
            for (var i = 0; i < 6; i++)
            {
                DMService.Instance.LeftClick(dmGuid, startPoint.Shift(52 * i, 0), TimeSpan.FromMilliseconds(150));
            }

            startPoint = new WxPoint(270, 441);
            for (var i = 0; i < 7; i++)
            {
                DMService.Instance.LeftClick(dmGuid, startPoint.Shift(86 * i, 0), TimeSpan.FromMilliseconds(100));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}