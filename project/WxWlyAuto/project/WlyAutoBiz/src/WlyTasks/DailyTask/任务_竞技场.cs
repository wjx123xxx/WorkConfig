// *******************************************************************
// * 文件名称： 任务_竞技场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-27 23:21:06
// *******************************************************************

using System;
using System.Reflection.Emit;
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
    /// 日常竞技场
    /// </summary>
    public class 任务_竞技场 : WlyDailyTask
    {
        #region Constructors

        public 任务_竞技场(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_竞技场);
            Thread.Sleep(1000);
            if (DMService.Instance.FindColor(dmGuid, "ff0000-000000", new WxRect(436, 470, 486, 484)))
            {
                return new WlyTaskInfo(ID, true);
            }

            FlowLogicHelper.RepeatRun(() =>
            {
                if (!DMService.Instance.FindStr(dmGuid, new WxRect(108, 207, 174, 229), "查看奖励", "ffcc00-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(141, 214));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 330));
                    return false;
                }

                return true;
            }, TimeSpan.FromSeconds(10));

            if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(167, 156, 219, 175), "ff9900-000000", out var amount))
            {
                entity.AccountInfo.FightPoint = amount;
            }

            var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(366, 268, 401, 289), WlyColor.White, out var targetLevel);
            if (result && targetLevel > entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level)
            {
                return new WlyTaskInfo(ID, true);
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(621, 558));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 330));

            DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 386));
            var enemy = DMService.Instance.GetWords(dmGuid, new WxRect(323, 247, 383, 269), "ffffff-000000");
            if (enemy.Contains("海潮"))
            {
                return new WlyTaskInfo(ID, true);
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 325));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));

            // 直接结束，不再等待
            throw new InvalidOperationException();
        }

        #endregion
    }
}