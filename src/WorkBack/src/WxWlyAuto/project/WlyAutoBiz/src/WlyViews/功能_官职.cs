// *******************************************************************
// * 文件名称： 功能_官职.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-29 23:26:24
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 功能_官职
    /// </summary>
    [WlyView(WlyViewType.功能_官职)]
    public class 功能_官职 : 总成_主城
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(285, 152, 313, 171), "官职", WlyColor.Normal);
        }

        #endregion
    }
}