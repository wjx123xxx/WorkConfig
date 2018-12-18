// *******************************************************************
// * 文件名称： 任务_田矿占领.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:32:16
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
    /// 田矿占领
    /// </summary>
    [WlyTask(WlyTaskType.田矿占领)]
    public class 任务_田矿占领 : WlyDailyTask
    {
        #region Constructors

        public 任务_田矿占领(string id) : base(id)
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

            // 首先跳转到主界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_资源区);

            // 占领指定资源，返回下次检测时间
            DateTime OccupyPos(int x, int y)
            {
                var res = FlowLogicHelper.RepeatRun(() =>
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    return DMService.Instance.FindStr(dmGuid, new WxRect(365, 212, 426, 238), "产量", "ccf0c1-000000");
                }, TimeSpan.FromSeconds(5));

                if (!res)
                {
                    throw new InvalidOperationException($"无法打开资源点");
                }

                // 确认是否为自己拥有
                var owner = FlowLogicHelper.RepeatRun(
                    () => DMService.Instance.FindStr(dmGuid, new WxRect(426, 283, 554, 306), "海潮", WlyColor.Normal),
                    TimeSpan.FromSeconds(3));

                // 确认是否空闲
                if (!owner)
                {
                    var local = FlowLogicHelper.RepeatRun(
                        () => DMService.Instance.FindStr(dmGuid, new WxRect(426, 283, 554, 306), "地方势力", WlyColor.Normal),
                        TimeSpan.FromSeconds(3));

                    if (local)
                    {
                        Occupy(x, y);
                        return DateTime.Now.Add(TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(-20)));
                    }
                }

                // 检测剩余时间
                var lastTime = WlyUtilityBiz.GetTime(dmGuid, new WxRect(427, 237, 519, 256), "33ffff-000000");
                if (owner)
                {
                    if (lastTime < TimeSpan.FromMinutes(20))
                    {
                        // 点击放弃
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(605, 255));

                        // 点击确定
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 330));
                        return OccupyPos(x, y);
                    }

                    // 点击关闭
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(640, 186));
                    return DateTime.Now.Add(lastTime).AddMinutes(-19);
                }

                // 点击关闭
                DMService.Instance.LeftClick(dmGuid, new WxPoint(640, 186));

                if (lastTime > TimeSpan.FromMinutes(20))
                {
                    return DateTime.Now.Add(lastTime).AddMinutes(-20);
                }

                if (lastTime > TimeSpan.FromMinutes(10))
                {
                    return DateTime.Now.Add(lastTime).AddMinutes(-10);
                }

                // 等待别人的时间到
                return DateTime.Now.Add(lastTime).AddMinutes(1);
            }

            // 占领打开的窗口
            void Occupy(int x, int y)
            {
                // 点击占领
                DMService.Instance.LeftClick(dmGuid, new WxPoint(602, 227), TimeSpan.FromSeconds(2));

                // 开启状态
                DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));

                // 确认出现天道酬勤
                if (!FlowLogicHelper.RepeatRun(() =>
                {
                    Thread.Sleep(500);
                    return DMService.Instance.FindStr(dmGuid, new WxRect(569, 310, 642, 330), "天道酬勤", WlyColor.Normal);
                }, TimeSpan.FromSeconds(3)))
                {
                    throw new InvalidOperationException("占领失败");
                }

                // 点击天道酬勤
                DMService.Instance.LeftClick(dmGuid, new WxPoint(604, 320));

                // 点击确定
                DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 332));

                // 点击加强巡逻
                DMService.Instance.LeftClick(dmGuid, new WxPoint(606, 348));

                // 点击关闭
                DMService.Instance.LeftClick(dmGuid, new WxPoint(640, 186));
            }

            // 占领田1
            var nextTime = OccupyPos(600, 300);

            // 占领田2
            var tempTime = OccupyPos(690, 340);
            if (nextTime > tempTime)
            {
                nextTime = tempTime;
            }

            // 占领矿1
            tempTime = OccupyPos(566, 161);
            if (nextTime > tempTime)
            {
                nextTime = tempTime;
            }

            // 占领矿2
            tempTime = OccupyPos(724, 208);
            if (nextTime > tempTime)
            {
                nextTime = tempTime;
            }

            // 加入下次检测任务
            return new WlyTaskInfo(ID)
            {
                NextRunTime = nextTime
            };
        }

        #endregion
    }
}