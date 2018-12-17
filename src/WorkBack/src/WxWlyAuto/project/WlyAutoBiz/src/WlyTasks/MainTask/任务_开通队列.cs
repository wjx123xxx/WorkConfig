// *******************************************************************
// * 文件名称： 任务_开通队列.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 23:46:27
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 建筑队列
    /// </summary>
    public class 任务_开通队列 : WlyMainTask
    {
        #region Constructors

        public 任务_开通队列(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);

            var rect = new WxRect(99, 253, 134, 275);
            if (!FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftDown(dmGuid, new WxPoint(89, 225));

                return DMService.Instance.FindStr(dmGuid, rect, "开通", "ff8c00-000000")
                       || DMService.Instance.FindStr(dmGuid, rect, "空闲", "1fed4a-000000");
            }, TimeSpan.FromSeconds(10)))
            {
                throw new InvalidOperationException("无法开通");
            }

            if (DMService.Instance.FindStr(dmGuid, rect, "开通", "ff8c00-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(116, 263));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 355));
                DMService.Instance.LeftUp(dmGuid, new WxPoint(89, 225));
            }

            Thread.Sleep(2000);
            rect = new WxRect(99, 273, 134, 299);
            if (!FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftDown(dmGuid, new WxPoint(89, 225));

                return DMService.Instance.FindStr(dmGuid, rect, "开通", "ff8c00-000000")
                       || DMService.Instance.FindStr(dmGuid, rect, "空闲", "1fed4a-000000");
            }, TimeSpan.FromSeconds(10)))
            {
                throw new InvalidOperationException("无法开通");
            }

            if (DMService.Instance.FindStr(dmGuid, rect, "开通", "ff8c00-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(118, 285));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 355));
                DMService.Instance.LeftUp(dmGuid, new WxPoint(89, 225));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}