// *******************************************************************
// * 文件名称： 任务_委派.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 08:26:57
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
    /// 委派
    /// </summary>
    public class 任务_委派 : WlyDailyTask
    {
        #region Constructors

        public 任务_委派(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_委派);

            // 主城等级大于100时自动卖出
            if (entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level >= 100)
            {
                // 设置自动卖出
                DMService.Instance.LeftClick(dmGuid, new WxPoint(251, 473));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(442, 341));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(493, 395));
            }

            // 开始委派
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(294, 429, 312, 449), "fce69a-000000", out var count) && (count == 0))
                {
                    return true;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(524, 338, 569, 367), "取消", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(548, 351));
                }

                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(257, 368), TimeSpan.FromMilliseconds(200)), count);
                Thread.Sleep(1000);
                return false;
            }, TimeSpan.FromSeconds(30));

            if (DMService.Instance.FindStr(dmGuid, new WxRect(483, 384, 517, 409), "免费", "ffff00-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(496, 367));
            }

            if (wait)
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID, DateTime.Now.Add(TimeSpan.FromMinutes(30)));
        }

        #endregion
    }
}