// *******************************************************************
// * 文件名称： 任务_酒馆.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 11:40:57
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
    /// 酒馆任务
    /// </summary>
    public class 任务_酒馆 : WlyDailyTask
    {
        #region Constructors

        public 任务_酒馆(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_酒馆);
            Thread.Sleep(1000);
            FlowLogicHelper.RepeatRun(() =>
            {
                // 计算次数
                if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(466, 526, 477, 541), WlyColor.Normal, out var count) && (count == 0))
                {
                    return true;
                }

                // 首先检测是否有特殊人物
                if (Special(entity))
                {
                }
                else if (DMService.Instance.FindStr(dmGuid, new WxRect(554, 576, 617, 599), "免费使用", "ff6600-000000"))
                {
                    // 检测是否可以免费喝酒
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(584, 565));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 331), TimeSpan.FromSeconds(1));
                }
                else if (DMService.Instance.FindStr(dmGuid, new WxRect(414, 575, 472, 599), "免费使用", "ff6600-000000"))
                {
                    // 检测是否可以免费另寻他城
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(445, 563));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 331), TimeSpan.FromSeconds(1));
                }
                else
                {
                    // 直接点击
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(89, 116));
                }

                return false;
            }, TimeSpan.FromSeconds(30));
            return new WlyTaskInfo(ID, true);
        }

        #endregion

        #region Private Methods

        private bool Special(WlyEntity entity)
        {
            var dmGuid = entity.DMGuid;
            var target = "cc00ff-000000|ff0000-000000|ff9933-000000";
            var key = false;

            if (DMService.Instance.FindColor(dmGuid, target, new WxRect(45, 152, 139, 179)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(88, 113), TimeSpan.FromSeconds(1));
                key = true;
            }
            else if (DMService.Instance.FindColor(dmGuid, target, new WxRect(186, 151, 258, 179)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(229, 115), TimeSpan.FromSeconds(1));
                key = true;
            }
            else if (DMService.Instance.FindColor(dmGuid, target, new WxRect(326, 151, 398, 179)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(369, 115), TimeSpan.FromSeconds(1));
                key = true;
            }
            else if (DMService.Instance.FindColor(dmGuid, "ffffb0-000000", new WxRect(654, 474, 689, 504)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(674, 491));
                key = true;
            }

            if (DMService.Instance.FindStr(dmGuid, new WxRect(661, 345, 696, 368), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(676, 353));
            }

            if (DMService.Instance.FindStr(dmGuid, new WxRect(672, 341, 708, 367), "武将", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(676, 355), TimeSpan.FromSeconds(1));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(497, 473), TimeSpan.FromSeconds(1));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 330), TimeSpan.FromSeconds(1));
            }

            return key;
        }

        #endregion
    }
}