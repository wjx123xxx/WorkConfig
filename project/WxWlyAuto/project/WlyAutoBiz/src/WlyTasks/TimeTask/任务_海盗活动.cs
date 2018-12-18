// *******************************************************************
// * 文件名称： 任务_海盗活动.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:39:36
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

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 海盗活动
    /// </summary>
    [WlyTask(WlyTaskType.海盗活动)]
    public class 任务_海盗活动 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_海盗活动(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
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

            // 前往海盗活动界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.日常_海盗);

            if (DMService.Instance.FindStr(dmGuid, new WxRect(523, 280, 559, 299), "海战", "e9e7cf-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 331));
                return new WlyTaskInfo(ID, true);
            }

            // 等待活动开始
            var startRect = new WxRect(7, 223, 65, 244);
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                Thread.Sleep(400);
                return DMService.Instance.FindStr(dmGuid, startRect, "剩余时间", "ffff00-101010");
            }, TimeSpan.FromMinutes(1));
            if (!wait)
            {
                throw new InvalidOperationException("海盗活动未正常开始");
            }

            // 剩余时间
            var lastTimeRect = new WxRect(66, 226, 105, 245);

            // 冷却时间
            var coolTimeRect = new WxRect(65, 205, 105, 224);

            // 得到的分数
            var pointRect = new WxRect(972, 94, 994, 113);

            // 活动结束
            var finalRect = new WxRect(488, 416, 522, 438);

            // 攻击
            void Attack()
            {
                var fr = DMService.Instance.FindPic(dmGuid, WlyPicType.超强炮舰, WlyUtilityBiz.GameWndRect, out var xx, out var yy);
                if (fr)
                {
                    var p = new WxPoint(xx, yy);
                    DMService.Instance.LeftClick(dmGuid, p);
                }

                Thread.Sleep(100);

                // 如果需要金币进行冷却，则确定
                var r = new WxRect(436, 342, 472, 365);
                if (DMService.Instance.FindStr(dmGuid, r, "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 354));
                }
            }

            // 活动运行
            bool Run()
            {
                var lt = WlyUtilityBiz.GetTime(dmGuid, lastTimeRect, "ffffff-000000");
                var ct = WlyUtilityBiz.GetTime(dmGuid, coolTimeRect, "ffffff-000000");

                // 冷却时间到，开始攻击
                if (ct == TimeSpan.Zero)
                {
                    Attack();
                }
                else
                {
                    // 计算已经得到的分数，确定是否需要进行强攻
                    WlyUtilityBiz.GetAmount(dmGuid, pointRect, "ffffff-000000", out var point);
                    var times = point / 33;

                    // 需要在1分30秒之前完成10次攻打
                    if (lt.Add(TimeSpan.FromSeconds(30 * times)) < TimeSpan.FromMinutes(6).Add(TimeSpan.FromSeconds(30)))
                    {
                        Attack();
                    }
                    else
                    {
                        // 等待冷却完成
                        Thread.Sleep(ct.Add(TimeSpan.FromMilliseconds(100)));
                    }
                }

                // 检测活动是否结束
                if (DMService.Instance.FindStr(dmGuid, finalRect, "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(505, 430));
                    return true;
                }

                return false;
            }

            // 开始攻打海盗
            FlowLogicHelper.RepeatRun(Run, TimeSpan.FromMinutes(7));
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}