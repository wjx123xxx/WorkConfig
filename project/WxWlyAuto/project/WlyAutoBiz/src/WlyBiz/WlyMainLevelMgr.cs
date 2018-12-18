// *******************************************************************
// * 文件名称： WlyMainLevelMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 10:59:52
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 主公等级管理器
    /// </summary>
    public static class WlyMainLevelMgr
    {
        #region Public Methods

        /// <summary>
        /// 升级主公属性
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static bool Upgrade(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_主公属性);

            // 升级进攻
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(220, 157));
                return DMService.Instance.FindStr(dmGuid, new WxRect(485, 188, 518, 206), "进攻", "ffcc00-000000");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法打开主公属性进攻");
            }

            while (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(506, 210, 536, 228), "66ff00-000000", out var _))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(750, 265));
            }

            // 升级防御
            wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(296, 158));
                return DMService.Instance.FindStr(dmGuid, new WxRect(485, 188, 518, 206), "防御", "ffcc00-000000");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法打开主公属性防御");
            }

            while (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(506, 210, 536, 228), "66ff00-000000", out var _))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(751, 420));
            }

            // 升级经济
            wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(375, 160));
                return DMService.Instance.FindStr(dmGuid, new WxRect(485, 188, 518, 206), "经济", "ffcc00-000000");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法打开主公属性经济");
            }

            while (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(506, 210, 536, 228), "66ff00-000000", out var _))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(745, 264));
            }

            // 升级通用
            wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 160));
                return DMService.Instance.FindStr(dmGuid, new WxRect(485, 188, 518, 206), "通用", "ffcc00-000000");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法打开主公属性通用");
            }

            while (WlyUtilityBiz.GetAmount(dmGuid, new WxRect(506, 210, 536, 228), "66ff00-000000", out var _))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(750, 420));
            }

            return true;
        }

        #endregion
    }
}