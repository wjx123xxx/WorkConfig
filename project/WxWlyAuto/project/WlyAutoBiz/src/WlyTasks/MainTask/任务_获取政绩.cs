// *******************************************************************
// * 文件名称： 任务_获取政绩.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-13 00:08:21
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

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 获取政绩
    /// </summary>
    public class 任务_获取政绩 : WlyMainTask
    {
        #region Constructors

        public 任务_获取政绩(string id, params string[] depends) : base(id, depends)
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
            var amount = 0;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                if (DateTime.Now.Hour < 20)
                {
                    return new WlyTaskInfo(ID, DateTime.Today.AddHours(20).AddMinutes(30));
                }
            }

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);

            // 获取银子数量
            var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(133, 26, 217, 47), "f3f3da-000000", out amount);
            if (!result)
            {
                throw new InvalidOperationException("Cannot Get Money Amount");
            }

            if (amount < 30000000)
            {
                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            // 捐款2600万获取100政绩
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_国政);

            // 点开内政
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(95, 42));
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

            // 捐款
            DMService.Instance.LeftClick(dmGuid, new WxPoint(613, 438));
            DMService.Instance.SendString(dmGuid, entity.WndHwnd, "13000000");

            wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(577, 429, 671, 447), "e7e7fd-000000", out amount))
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

            DMService.Instance.LeftClick(dmGuid, new WxPoint(739, 438));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}