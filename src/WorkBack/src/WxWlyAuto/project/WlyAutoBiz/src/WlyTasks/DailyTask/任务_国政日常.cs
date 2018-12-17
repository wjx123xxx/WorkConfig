// *******************************************************************
// * 文件名称： 任务_国政日常.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-15 21:57:17
// *******************************************************************

using System;
using System.Threading;

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
    /// 国政日常
    /// </summary>
    public class 任务_国政日常 : WlyDailyTask
    {
        #region Constructors

        public 任务_国政日常(string id, params string[] depends) : base(id, depends)
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
            var now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Sunday && now.Hour >= 4 && now.Hour < 21)
            {
                return new WlyTaskInfo(ID, DateTime.Now.Add(DateTime.Today.AddHours(21) - now));
            }

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_国政);

            // 点开国库
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(97, 194));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(483, 100, 516, 120), "国政", WlyColor.Normal))
                {
                    return true;
                }

                Thread.Sleep(500);
                return false;
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                throw new InvalidOperationException();
            }

            Thread.Sleep(1000);
            var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(635, 433, 672, 450), "ffff99-000000", out var amount);
            if (!result)
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddHours(1));
            }

            var wish = (WlyUtilityBiz.GetRefreshTime() - DateTime.Now).Hours * 20;
            var count = Math.Min((1000 - amount - wish) / 2, 10);
            count = Math.Max(0, count);

            // 点击加号开始捐献
            for (var i = 0; i < count; i++)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(653, 470));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(531, 278, 562, 304), "不足", "e9e7cf-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(501, 332));
                    break;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(437, 343, 473, 364), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(459, 312));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 357));
                }
            }

            // 领取官职奖励
            DMService.Instance.LeftClick(dmGuid, new WxPoint(373, 162));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(746, 477));

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}