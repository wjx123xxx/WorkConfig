// *******************************************************************
// * 文件名称： 任务_征收.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 08:26:23
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 征收
    /// </summary>
    public class 任务_征收 : WlyDailyTask
    {
        #region Constructors

        public 任务_征收(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_征收);
            Thread.Sleep(1000);

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                var countStr = DMService.Instance.GetWords(dmGuid, new WxRect(455, 225, 501, 245), "e9e7cf-000000");
                if (!string.IsNullOrWhiteSpace(countStr))
                {
                    int.TryParse(countStr.Split('/')[0], out var count);
                    if (count == 0)
                    {
                        return true;
                    }

                    FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(587, 230), TimeSpan.FromMilliseconds(200)),
                        count);
                    Thread.Sleep(1000);
                }

                // 点击事件
                FlowLogicHelper.RepeatRun(() =>
                {
                    if (!DMService.Instance.FindColor(dmGuid, "e9e7cf-000000", new WxRect(600, 315, 797, 389)))
                    {
                        return true;
                    }

                    FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(498, 406), TimeSpan.FromMilliseconds(200)), 10);
                    Thread.Sleep(1000);
                    return false;
                }, TimeSpan.FromSeconds(10));

                return false;
            }, TimeSpan.FromSeconds(30));

            if (wait)
            {
                while (true)
                {
                    wait = FlowLogicHelper.RepeatRun(
                        () => DMService.Instance.FindStrFast(dmGuid, new WxRect(454, 247, 497, 268), "金币", "ffff00-000000", out _, out _),
                        TimeSpan.FromSeconds(20));
                    if (!wait)
                    {
                        throw new InvalidOperationException("无法识别强征花费");
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(447, 246, 503, 270), "2金币", "ffff00-000000"))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(685, 231), TimeSpan.FromSeconds(1));
                    }
                    //else if (DMService.Instance.FindStr(dmGuid, new WxRect(447, 246, 503, 270), "5金币", "ffff00-000000"))
                    //{
                    //    DMService.Instance.LeftClick(dmGuid, new WxPoint(685, 231), TimeSpan.FromSeconds(1));
                    //}
                    else
                    {
                        break;
                    }
                }

                // 点击事件
                FlowLogicHelper.RepeatRun(() =>
                {
                    if (!DMService.Instance.FindColor(dmGuid, "e9e7cf-000000", new WxRect(600, 315, 797, 389)))
                    {
                        return true;
                    }

                    FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(498, 406), TimeSpan.FromMilliseconds(200)), 10);
                    Thread.Sleep(1000);
                    return false;
                }, TimeSpan.FromSeconds(10));
            }

            if (wait)
            {
                return new WlyTaskInfo(ID, true);
            }

            // 重启
            throw new InvalidOperationException("征收失败");
        }

        #endregion
    }
}