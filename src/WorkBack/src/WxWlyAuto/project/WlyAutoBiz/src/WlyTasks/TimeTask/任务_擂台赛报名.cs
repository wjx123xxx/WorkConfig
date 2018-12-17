// *******************************************************************
// * 文件名称： 任务_擂台赛报名.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:35:16
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 擂台赛报名
    /// </summary>
    [WlyTask(WlyTaskType.擂台赛报名)]
    public class 任务_擂台赛报名 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_擂台赛报名(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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

            // 找一个位置
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(329, 112, 953, 496),"擂", "feff99-000000", out var x, out var y))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    return true;
                }

                Thread.Sleep(1000);
                return false;
            }, TimeSpan.FromSeconds(10));

            if (!wait)
            {
                throw new InvalidOperationException();
            }

            // 点击报名
            var signupPoint = new WxPoint(393, 551);
            DMService.Instance.LeftClick(dmGuid, signupPoint);

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}