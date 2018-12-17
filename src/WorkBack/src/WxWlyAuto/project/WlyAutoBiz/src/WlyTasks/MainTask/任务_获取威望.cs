// *******************************************************************
// * 文件名称： 任务_获取威望.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-08 23:48:30
// *******************************************************************

using System;

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
    /// 获取威望
    /// </summary>
    public class 任务_获取威望 : WlyMainTask
    {
        #region Fields

        private readonly int m_amount;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount">获取威望的数量</param>
        /// <param name="depends">依赖的任务</param>
        public 任务_获取威望(string id, int amount, params string[] depends) : base(id, depends)
        {
            m_amount = amount;
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
            entity.AccountInfo.Credit = WlyUtilityBiz.GetCreditAmount(dmGuid);
            if (entity.AccountInfo.Credit >= m_amount)
            {
                if (m_amount < 2000000)
                {
                    return new WlyTaskInfo(ID, true);
                }

                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            // 尝试从竞技场获取
            if (entity.AccountInfo.FightPoint >= 300)
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_竞技场);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(621, 560));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 327));

                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(576, 437, 607, 457), "兑换", "e9e7cf-000000"))
                    {
                        return true;
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(686, 559));
                    return false;
                }, TimeSpan.FromSeconds(10));

                if (!wait)
                {
                    throw new InvalidOperationException("无法打开兑换界面");
                }

                // 开始兑换
                wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(745, 336, 780, 358), "威望", "f3f3da-000000"))
                    {
                        return true;
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(389, 229));
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException("无法兑换威望");
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(602, 471));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(602, 471));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(602, 471));
            }

            return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
        }

        #endregion
    }
}