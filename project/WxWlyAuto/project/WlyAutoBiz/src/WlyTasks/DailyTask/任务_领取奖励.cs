// *******************************************************************
// * 文件名称： 任务_领取奖励.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:24:29
// *******************************************************************

using System;
using System.Threading;

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
    /// 领取奖励
    /// </summary>
    [WlyTask(WlyTaskType.领取奖励)]
    public class 任务_领取奖励 : WlyDailyTask
    {
        #region Fields

        private readonly TimeSpan m_interval;

        #endregion

        #region Constructors

        public 任务_领取奖励(string id, TimeSpan interval, params string[] depends) : base(id, depends)
        {
            m_interval = interval;
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

            var rect = new WxRect(373, 5, 819, 269);
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);

            Thread.Sleep(1000);

            // 首先跳转到主界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.导航_日常按钮栏);

            // 寻找封赏
            if (DMService.Instance.FindPic(dmGuid, WlyPicType.封赏, rect, out var px, out var py))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
            }

            // 寻找VIP奖励
            if (DMService.Instance.FindPic(dmGuid, WlyPicType.Vip奖励, rect, out px, out py))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
            }

            // 寻找日常奖励
            if (DMService.Instance.FindPic(dmGuid, WlyPicType.每日奖励, rect, out px, out py))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(378, 357));
            }

            // 寻找开服礼包
            while (DMService.Instance.FindPic(dmGuid, WlyPicType.开服礼包, rect, out px, out py))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(470, 419, 511, 449), "领取", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(503, 435));
                }
                else
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(606, 145));
                    break;
                }
            }

            // 寻找离线奖励
            FlowLogicHelper.RepeatRun(() =>
            {
                if (!DMService.Instance.FindPic(dmGuid, WlyPicType.离线奖励, rect, out px, out py))
                {
                    return true;
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(470, 419, 511, 449), "领取", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(503, 435));
                }
                else
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(606, 145));
                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(30));

            // 领取补偿
            if (DMService.Instance.FindStr(dmGuid, rect, "补偿", WlyColor.Normal, out px, out py))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(px + 5, py + 5));
            }

            // 确认有活动图标
            if (DMService.Instance.FindPic(dmGuid, WlyPicType.活动一览按钮, rect))
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.活动界面);

                // 寻找周年送礼
                if (DMService.Instance.FindStr(dmGuid, new WxRect(136, 110, 299, 493), "周年送礼", "ffffff-000000", out px, out py))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(px, py));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(771, 334), TimeSpan.FromSeconds(2));
                }

                // 寻找登录奖励
                if (DMService.Instance.FindStr(dmGuid, new WxRect(136, 110, 299, 493), "登录送礼", "ffffff-000000", out px, out py))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(px, py));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(769, 334), TimeSpan.FromSeconds(2));
                }

                // 寻找节日活动页
                if (DMService.Instance.FindStr(dmGuid, new WxRect(136, 110, 299, 493), "节日活动", "ffffff-000000", out px, out py))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(px, py));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(774, 475));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(856, 84));
                }
            }

            // 获取军令数量
            entity.AccountInfo.Point = WlyUtilityBiz.GetPoint(dmGuid);

            // 30分钟打开一次
            return new WlyTaskInfo(ID)
            {
                NextRunTime = DateTime.Now.Add(m_interval)
            };
        }

        #endregion
    }
}