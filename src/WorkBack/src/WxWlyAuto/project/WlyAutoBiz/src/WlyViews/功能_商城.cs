// *******************************************************************
// * 文件名称： 功能_商城.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 11:58:46
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 商城界面
    /// </summary>
    [WlyView(WlyViewType.功能_商城)]
    public class 功能_商城 : 总成_装备
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(395, 176, 434, 201), "商品", WlyColor.Normal);
        }

        #endregion
    }
}