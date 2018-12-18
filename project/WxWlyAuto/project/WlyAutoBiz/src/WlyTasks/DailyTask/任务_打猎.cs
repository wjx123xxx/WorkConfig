// *******************************************************************
// * 文件名称： 任务_打猎.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:26:40
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 打猎
    /// </summary>
    public class 任务_打猎 : WlyDailyTask
    {
        #region Constructors

        public 任务_打猎(string id) : base(id)
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

            // 进入打猎界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_打猎);

            Thread.Sleep(1000);
            while (true)
            {
                if (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(324, 498, 339, 520), "e9e7cf-000000", out var amount) && (amount == 0))
                {
                    break;
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 253));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 352), TimeSpan.FromSeconds(5));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}