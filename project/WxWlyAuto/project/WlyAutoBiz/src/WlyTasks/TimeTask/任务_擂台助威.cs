// *******************************************************************
// * 文件名称： 任务_擂台助威.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-11 18:58:54
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 擂台助威
    /// </summary>
    [WlyTask(WlyTaskType.擂台助威)]
    public class 任务_擂台助威 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_擂台助威(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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

            // 首先跳转到擂台赛界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.日常_擂台赛);
            Thread.Sleep(5000);

            // 计算助威人数
            WlyUtilityBiz.GetAmount(dmGuid, new WxRect(330, 369, 365, 383), "ebe9cf-101010", out var count1);
            WlyUtilityBiz.GetAmount(dmGuid, new WxRect(340, 421, 364, 434), "ebe9cf-101010", out var count2);

            // 上面数字大就助威上面，下面大助威下面
            var target = count1 >= count2 ? new WxPoint(404, 371) : new WxPoint(405, 423);
            DMService.Instance.LeftClick(dmGuid, target);
            DMService.Instance.LeftClick(dmGuid, target);
            DMService.Instance.LeftClick(dmGuid, target);
            DMService.Instance.LeftClick(dmGuid, target);

            // 助威
            DMService.Instance.LeftClick(dmGuid, new WxPoint(515, 553));

            // 确定
            DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 332));

            // 退出
            DMService.Instance.LeftClick(dmGuid, new WxPoint(911, 549));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}