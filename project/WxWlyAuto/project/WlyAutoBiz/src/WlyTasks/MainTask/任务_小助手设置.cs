// *******************************************************************
// * 文件名称： 任务_小助手设置.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-08 10:48:16
// *******************************************************************

using System;
using System.Threading;

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
    /// 助手
    /// </summary>
    public class 任务_小助手设置 : WlyMainTask
    {
        #region Constructors

        public 任务_小助手设置(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_世界地图);
            FlowLogicHelper.RepeatRun(() =>
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);
                if (DMService.Instance.FindColor("f7654f-000000|e6b3d2-000000", WlyUtilityBiz.GameWndRect, dmGuid, out var x, out var y))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    return true;
                }

                Thread.Sleep(500);
                return false;
            }, TimeSpan.FromSeconds(60));

            var leftRect = new WxRect(436, 138, 563, 496);

            Thread.Sleep(2000);
            if (DMService.Instance.FindStr(dmGuid, new WxRect(467, 94, 537, 125), "辅政大臣", WlyColor.Normal))
            {
                for (var i = 0; i < 4; i++)
                {
                    int x, y;
                    while (DMService.Instance.FindStr(dmGuid, new WxRect(585, 137, 677, 498), "开启", WlyColor.Normal, out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    if (DMService.Instance.FindStr(dmGuid, leftRect, "征兵", "f3f3da-000000", out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(582, 139, 634, 497), "暴击率", "f3f3da-000000", out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    if (DMService.Instance.FindStr(dmGuid, leftRect, "俸禄", "f3f3da-000000", out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    if (DMService.Instance.FindStr(dmGuid, leftRect, "自动免费", "f3f3da-000000", out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    if (DMService.Instance.FindStr(dmGuid, leftRect, "自动免费", "f3f3da-000000", out x, out y))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                    }

                    // 设置出航贸易
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(406, 146, 486, 498), "出航贸易", "ffffcc-000000", out x, out y))
                    {
                        var start = new WxPoint(x, y);
                        DMService.Instance.LeftClick(dmGuid, start.Shift(170, 28));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(149, 207));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(170, 53));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(148, 231));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(170, 75));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(148, 60));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(170, 101));
                        DMService.Instance.LeftClick(dmGuid, start.Shift(148, 85));
                    }

                    FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(806, 481), TimeSpan.FromMilliseconds(100)), 18);
                }

                // 保存
                DMService.Instance.LeftClick(dmGuid, new WxPoint(297, 468));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(482, 312, 523, 346), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(503, 333));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 123));
                    // 保存成功
                    return new WlyTaskInfo(ID, true);
                }
            }

            // 重启测试
            throw new InvalidOperationException();
        }

        #endregion
    }
}