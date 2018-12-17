// *******************************************************************
// * 文件名称： 建筑_船坞.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 00:04:27
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 船坞
    /// </summary>
    [WlyView(WlyViewType.建筑_船坞)]
    [WlyBuilding(WlyBuildingType.船坞)]
    public class 建筑_船坞 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_船坞()
        {
            AddHandler(WlyViewType.功能_船坞, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 406)));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 建筑名称的颜色
        /// </summary>
        protected override string BuildingColor => "ffffcc-000000";

        #endregion
    }
}