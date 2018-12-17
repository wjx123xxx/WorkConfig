// *******************************************************************
// * 文件名称： 任务_强征.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:22:39
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 强征
    /// </summary>
    [WlyTask(WlyTaskType.强征)]
    public class 任务_强征 : WlyDailyTask
    {
        #region Constructors

        public 任务_强征(string id, params string[] depends) : base(id, depends)
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
            //var dmGuid = entity.DMGuid;
            //WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_征收);

            //while (true)
            //{
            //    var wait = FlowLogicHelper.RepeatRun(() => DMService.Instance.FindStr(dmGuid, new WxRect(447, 246, 503, 270), "金币", "ffff00-000000"),
            //        TimeSpan.FromSeconds(20));
            //    if (!wait)
            //    {
            //        throw new InvalidOperationException("无法识别强征花费");
            //    }

            //    if (DMService.Instance.FindStr(dmGuid, new WxRect(447, 246, 503, 270), "2金币", "ffff00-000000"))
            //    {
            //        DMService.Instance.LeftClick(dmGuid, new WxPoint(685, 231), TimeSpan.FromSeconds(1));
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}