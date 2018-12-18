// *******************************************************************
// * 文件名称： 任务_游戏助手.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-10-17 22:04:37
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
    /// Comment
    /// </summary>
    public class 任务_游戏助手 : WlyDailyTask
    {
        #region Constructors

        public 任务_游戏助手(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_游戏助手);

            // 点击开始
            DMService.Instance.LeftClick(dmGuid, new WxPoint(597, 473));

            // 等待结束
            var wait = SpinWait.SpinUntil(() =>
            {
                Thread.Sleep(1000);
                return DMService.Instance.FindStr(dmGuid, new WxRect(400, 83, 455, 521), "停止执行", WlyColor.White);
            }, TimeSpan.FromMinutes(2));

            DMService.Instance.LeftClick(dmGuid, new WxPoint(496, 519));
            if (!wait)
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(1));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}