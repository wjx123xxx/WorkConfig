// *******************************************************************
// * 文件名称： 任务_游历.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 22:57:37
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
    /// 游历
    /// </summary>
    public class 任务_游历 : WlyDailyTask
    {
        #region Constructors

        public 任务_游历(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_游历);
            var fightView = WlyViewMgr.GetView(WlyViewType.场景_战斗);
            var oldCount = 0;
            var tryCount = 0;
            while (true)
            {
                if (DMService.Instance.FindStr(dmGuid, WlyUtilityBiz.GameWndRect, "购买", WlyColor.Normal, out var x, out var y))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 331));
                }

                var countStr = DMService.Instance.GetWords(dmGuid, new WxRect(543, 583, 586, 600), "00ff00-000000");
                if (!string.IsNullOrEmpty(countStr))
                {
                    var count = int.Parse(countStr.Split('/')[0]);
                    if (count == 0)
                    {
                        break;
                    }

                    if (oldCount == count)
                    {
                        tryCount++;
                        if (tryCount >= 10)
                        {
                            throw new InvalidOperationException("可能掉线了游历失败");
                        }
                    }
                    else
                    {
                        oldCount = count;
                        tryCount = 0;
                    }
                }

                if (fightView.IsCurrentView(dmGuid))
                {
                    fightView.Exit(dmGuid);
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(402, 321, 436, 346), "赌场", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(514, 437));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(613, 440));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(529, 339, 565, 369), "取消", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(547, 352));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(478, 337, 521, 372), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 353));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(471, 410, 515, 442), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(494, 428));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(657, 426, 704, 452), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(681, 438));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(453, 537, 490, 560), "移动", WlyColor.Normal, WlyColor.White))
                {
                    Thread.Sleep(1000);
                    if (DMService.Instance.FindStr(dmGuid, WlyUtilityBiz.GameWndRect, "购买", WlyColor.Normal, out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 331));
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(473, 520), TimeSpan.FromSeconds(2));
                }

                Thread.Sleep(1000);
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}