// *******************************************************************
// * 文件名称： 功能_古代战场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 22:10:44
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 古代战场
    /// </summary>
    [WlyView(WlyViewType.功能_古代战场)]
    public class 功能_古代战场 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(816, 115));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            var result = DMService.Instance.FindStr(dmGuid, new WxRect(449, 77, 504, 94), "剩余时间", "00fc39-000000");
            return DMService.Instance.FindStr(dmGuid, new WxRect(465, 437, 519, 455), "开始匹配", WlyColor.Normal) || result;
        }

        #endregion
    }
}