// *******************************************************************
// * 文件名称： 任务_卖粮.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:28:10
// *******************************************************************

using System;

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
    /// 卖粮
    /// </summary>
    [WlyTask(WlyTaskType.卖粮)]
    public class 任务_卖粮 : WlyDailyTask
    {
        #region Constructors

        public 任务_卖粮(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_卖粮);

            // 读取粮价
            var priceRect = new WxRect(386, 211, 479, 232);
            var priceStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                priceStr = DMService.Instance.GetWords(dmGuid, priceRect, "e9e7cf-000000");
                return !string.IsNullOrEmpty(priceStr);
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                throw new InvalidOperationException("无法读取当前粮价");
            }

            var info = WlyUtilityBiz.SystemInfo;
            info.FoodPriceDescription = $"{priceStr} [{DateTime.Now:yyyy-MM-dd HH:mm:ss}]";
            if (double.TryParse(priceStr.Split('(')[0], out var price))
            {
                info.FoodPrice = price;
            }

            // 如果达到最高价则卖粮
            if (price >= 1.8)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(760, 343));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(762, 377));
            }

            if (price <= 0.65)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(759, 421));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(761, 454));
                return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(9));
            }

            var now = DateTime.Now;
            var flag = DateTime.Today.AddHours(now.Hour).AddMinutes(30);
            var nextTime = now < flag ? flag.AddMinutes(1) : flag.AddMinutes(31);
            info.NextFoodTime = nextTime;
            return new WlyTaskInfo(ID, nextTime);
        }

        #endregion
    }
}