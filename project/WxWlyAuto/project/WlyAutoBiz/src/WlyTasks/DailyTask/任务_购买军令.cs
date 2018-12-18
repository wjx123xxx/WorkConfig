// *******************************************************************
// * 文件名称： 任务_购买军令.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-11 19:00:55
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 购买军令
    /// </summary>
    public class 任务_购买军令 : WlyDailyTask
    {
        #region Constructors

        public 任务_购买军令(string id, params string[] depends) : base(id, depends)
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

            // 跳转到主界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);

            // 点击购买
            DMService.Instance.LeftClick(dmGuid, new WxPoint(179, 111));

            // 识别花费
            var payStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                payStr = DMService.Instance.GetWords(dmGuid, new WxRect(528, 249, 579, 284), "ff6600-101010");
                if (DMService.Instance.FindStr(dmGuid, new WxRect(485, 317, 518, 342), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(495, 330));
                    return true;
                }

                return payStr.Contains("金币");
            }, TimeSpan.FromSeconds(30));
            if (!wait)
            {
                throw new InvalidOperationException("无法识别购买花费");
            }

            // 点击购买或取消
            DMService.Instance.LeftClick(dmGuid, payStr == "0金币" ? new WxPoint(454, 355) : new WxPoint(548, 352));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}