// *******************************************************************
// * 文件名称： 任务_生产.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 23:30:28
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
    /// 生产
    /// </summary>
    public class 任务_生产 : WlyDailyTask
    {
        #region Constructors

        public 任务_生产(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_生产);
            Thread.Sleep(1000);

            // 勾选使用3金币进行暴击
            if (!DMService.Instance.FindColor(dmGuid, WlyColor.Normal, new WxRect(534, 236, 566, 255)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(520, 245));
            }

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(575, 420, 599, 437), "f3f3da-000000", out var amount);
                if (result && (amount == 0))
                {
                    return true;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(458, 260, 521, 294), "金币", "ffff00-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(545, 357));
                }

                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(554, 457), TimeSpan.FromMilliseconds(200)), amount);
                Thread.Sleep(1000);
                return false;
            }, TimeSpan.FromSeconds(30));

            if (wait)
            {
                Thread.Sleep(1000);
                if (DMService.Instance.FindStr(dmGuid, new WxRect(531, 342, 561, 364), "取消", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(548, 353));
                }

                // 卖出商品
                DMService.Instance.LeftClick(dmGuid, new WxPoint(710, 456));

                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID, DateTime.Now.AddHours(1));
        }

        #endregion
    }
}