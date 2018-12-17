// *******************************************************************
// * 文件名称： 建筑_商铺.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 11:52:29
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 商铺界面
    /// </summary>
    [WlyView(WlyViewType.建筑_商铺)]
    [WlyBuilding(WlyBuildingType.商铺)]
    public class 建筑_商铺 : WlyBuildingViewBase
    {
        #region Constructors

        public 建筑_商铺()
        {
            AddHandler(WlyViewType.功能_商城, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 405)));
            AddHandler(WlyViewType.功能_仓库, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 407)));
            AddHandler(WlyViewType.功能_委派, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(655, 406)));
            AddHandler(WlyViewType.功能_强化装备, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(555, 405)));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 建筑名称
        /// </summary>
        protected override string BuildingName => "商铺";

        #endregion
    }
}