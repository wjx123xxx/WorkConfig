// *******************************************************************
// * 文件名称： 任务_古城探险.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:16:44
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
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
    /// 古城探险
    /// </summary>
    [WlyTask(WlyTaskType.古城探险)]
    public class 任务_古城探险 : WlyDailyTask
    {
        #region Constructors

        public 任务_古城探险(string id) : base(id)
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

            // 进入古城探险界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.日常_古城探秘);
            Thread.Sleep(400);

            // 获取剩余次数
            var countRect = new WxRect(614, 424, 634, 442);
            if (WlyUtilityBiz.GetAmount(dmGuid, countRect, WlyColor.Normal, out var count) && (count == 0))
            {
                WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(30));
                return new WlyTaskInfo(ID, true);
            }

            // 开始一键探险
            DMService.Instance.LeftClick(dmGuid, new WxPoint(567, 392));
            var wait = SpinWait.SpinUntil(() =>
            {
                Thread.Sleep(1000);
                return DMService.Instance.FindStr(dmGuid, new WxRect(549, 487, 587, 513), "扫荡", WlyColor.Normal);
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(1));
            }

            // 勾上选项
            DMService.Instance.LeftClick(dmGuid, new WxPoint(422, 214));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(439, 237));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(423, 420));
            //DMService.Instance.LeftClick(dmGuid, new WxPoint(421, 444));

            // 开始扫荡
            DMService.Instance.LeftClick(dmGuid, new WxPoint(557, 501));

            // 等待扫荡结束
            wait = SpinWait.SpinUntil(() =>
            {
                Thread.Sleep(1000);
                return DMService.Instance.FindStr(dmGuid, new WxRect(447, 477, 515, 501), "已完成全部", "ff3300-000000");
            }, TimeSpan.FromSeconds(60));
            if (!wait)
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(1));
            }

            // 完成古城探秘
            DMService.Instance.LeftClick(dmGuid, new WxPoint(763, 126));
            Thread.Sleep(400);
            WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(30));
            return new WlyTaskInfo(ID, DateTime.Now);
        }

        #endregion
    }
}