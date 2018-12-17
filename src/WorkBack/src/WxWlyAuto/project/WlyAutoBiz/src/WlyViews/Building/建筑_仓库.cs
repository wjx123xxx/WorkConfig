// *******************************************************************
// * 文件名称： 建筑_仓库.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-08-17 00:45:28
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 仓库
    /// </summary>
    [WlyView(WlyViewType.建筑_仓库)]
    [WlyBuilding(WlyBuildingType.仓库)]
    public class 建筑_仓库 : WlyBuildingViewBase
    {
        #region Protected Properties

        /// <summary>
        /// 建筑名称的颜色
        /// </summary>
        protected override string BuildingColor => "ffffcc-000000";

        /// <summary>
        /// 建筑名称
        /// </summary>
        protected override string BuildingName => "仓库";

        #endregion
    }
}