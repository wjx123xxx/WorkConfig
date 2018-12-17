// *******************************************************************
// * 文件名称： WlyMapMgr
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 16:10:01
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
    /// 世界地图业务管理
    /// </summary>
    public static class WlyMapMgr
    {
        #region Public Methods

        /// <summary>
        /// 迁入指定的城池
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public static bool MoveTo(string dmGuid, WlyCityType city)
        {
            OpenCity(dmGuid, city);

            DMService.Instance.LeftClick(dmGuid, new WxPoint(664, 244));
            if (DMService.Instance.FindStr(dmGuid, new WxRect(483, 319, 533, 342), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 331));
            }

            return true;
        }

        /// <summary>
        /// 打开指定的城池
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="city"></param>
        public static void OpenCity(string dmGuid, WlyCityType city)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_世界地图);

            var find = DMService.Instance.FindStr(dmGuid, WlyUtilityBiz.GameWndRect, city.ToString(), "f3f3da-000000", out var x,
                out var y);
            if (!find)
            {
                throw new InvalidOperationException($"未找到城池{city.ToString()}");
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(x + 20, y - 20));
            var wait = FlowLogicHelper.RepeatRun(
                () => DMService.Instance.FindStr(dmGuid, new WxRect(485, 148, 518, 173), city.ToString(), WlyColor.Normal),
                TimeSpan.FromSeconds(5));
            if (!wait)
            {
                throw new InvalidOperationException($"无法打开城池{city.ToString()}");
            }
        }

        #endregion
    }
}