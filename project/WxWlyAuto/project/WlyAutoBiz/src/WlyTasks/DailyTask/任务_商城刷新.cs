// *******************************************************************
// * 文件名称： 任务_商城刷新.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 00:11:50
// *******************************************************************

using System;
using System.Collections.Generic;

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
    /// 商城刷新
    /// </summary>
    public class 任务_商城刷新 : WlyDailyTask
    {
        #region Constructors

        public 任务_商城刷新(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_商城);

            DMService.Instance.LeftClick(dmGuid, new WxPoint(559, 468));
            var costStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                costStr = DMService.Instance.GetWords(dmGuid, new WxRect(473, 265, 518, 285), "ffff00-101010");
                return costStr.Contains("金币");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法确认商城刷新的花费");
            }

            if (costStr == "10金币")
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(460, 357), TimeSpan.FromSeconds(2));

                // 购买所有可以使用粮食和银币购买的东西
                var itemRects = new List<WxRect>
                {
                    new WxRect(262, 301, 332, 327),
                    new WxRect(411, 304, 472, 325),
                    new WxRect(551, 304, 619, 326),
                    new WxRect(266, 418, 329, 439),
                    new WxRect(408, 417, 474, 440),
                    new WxRect(556, 417, 617, 442)
                };

                foreach (var rect in itemRects)
                {
                    if (WlyUtilityBiz.GetAmount(dmGuid, rect, "ffffff-000000", out var _))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(rect.Left, rect.Top));
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(415, 470));

                        if (DMService.Instance.FindStr(dmGuid, new WxRect(434, 338, 473, 366), "确定", WlyColor.Normal))
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(459, 311));
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(457, 351));
                        }
                    }
                }
            }
            else
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(547, 353));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}