// *******************************************************************
// * 文件名称： 功能_市场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 17:40:46
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 市场界面
    /// </summary>
    [WlyView(WlyViewType.功能_市场)]
    [WlyBuilding(WlyBuildingType.市场)]
    public class 功能_市场 : WlyBuildingViewBase
    {
        #region Constructors

        public 功能_市场()
        {
            AddHandler(WlyViewType.功能_卖粮, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 409)));
        }

        #endregion
    }
}