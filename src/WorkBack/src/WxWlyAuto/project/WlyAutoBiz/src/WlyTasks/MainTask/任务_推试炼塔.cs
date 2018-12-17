// *******************************************************************
// * 文件名称： 任务_推试炼塔.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-02 21:51:29
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
    /// 试炼塔
    /// </summary>
    public class 任务_推试炼塔 : WlyMainTask
    {
        #region Fields

        private int m_floor;

        #endregion

        #region Constructors

        public 任务_推试炼塔(string id, int floor, params string[] depends) : base(id, depends)
        {
            m_floor = floor;
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
            WlyEntityBiz.UpgradeAllEquipmenets(entity);
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_试炼塔);
            Thread.Sleep(2000);

            // 检测当前的层数
            var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(237, 468, 256, 486), "ffffff-000000", out var level);
            if (!result)
            {
                throw new InvalidOperationException();
            }

            var current = 0;
            for (var i = (10 * (level - 1)) + 1; i <= level * 10; i++)
            {
                if (!DMService.Instance.FindStr(dmGuid, new WxRect(195, 201, 278, 455), $"第{i}层", "e9e7cf-000000", WlyColor.Normal))
                {
                    break;
                }

                current = i;
            }

            // 大于指定层时表示完成推塔
            if (current > m_floor)
            {
                return new WlyTaskInfo(ID, true);
            }

            int x = 0;
            int y = 0;
            while (current <= m_floor)
            {
                // 开始推塔
                FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(195, 201, 278, 455), $"第{current}层", WlyColor.Normal))
                    {
                        return true;
                    }

                    DMService.Instance.FindStr(dmGuid, new WxRect(195, 201, 278, 455), $"第{current}层", "e9e7cf-000000", out x, out y);
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x + 5, y + 5));
                    return true;
                }, TimeSpan.FromSeconds(10));

                DMService.Instance.LeftClick(dmGuid, new WxPoint(367, 470));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));

                // 等待进入塔中
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(480, 4, 519, 30), $"第{current}层", "ffff00-000000"))
                    {
                        return true;
                    }

                    Thread.Sleep(500);
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                if (!DMService.Instance.FindColor(dmGuid, WlyColor.Normal, new WxRect(636, 553, 688, 578)))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(650, 566));
                }

                if (DMService.Instance.FindColor(dmGuid, WlyColor.Normal, new WxRect(813, 550, 906, 580)))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(846, 568));
                }

                // 等待结果
                var key = false;
                wait = FlowLogicHelper.RepeatRun(() =>
                {
                    // 点击进攻
                    if (DMService.Instance.FindPic(dmGuid, WlyPicType.试炼塔目标, WlyUtilityBiz.GameWndRect, out var xx, out var yy))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(xx, yy));
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(523, 393, 558, 422), "退出", "e9e7cf-000000"))
                    {
                        key = true;
                        return true;
                    }

                    Thread.Sleep(2000);
                    return false;
                }, TimeSpan.FromSeconds(30));
                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(452, 390, 492, 422), "挑战", "e9e7cf-000000"))
                {
                    key = false;
                }

                if (key)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(540, 409));
                    WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_试炼塔);
                    if ((current % 10) == 0)
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(290, 475));
                    }

                    current++;
                }
                else
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(540, 409));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 331));
                    return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime().AddDays(3));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}