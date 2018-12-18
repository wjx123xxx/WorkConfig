// *******************************************************************
// * 文件名称： 建筑_商货码头.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 08:34:11
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 码头
    /// </summary>
    [WlyView(WlyViewType.建筑_商贸码头)]
    [WlyBuilding(WlyBuildingType.商贸码头)]
    public class 建筑_商货码头 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_商货码头()
        {
            AddHandler(WlyViewType.功能_出航贸易, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(355, 406)));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 建筑名称的颜色
        /// </summary>
        protected override string BuildingColor => "ffffcc-000000";

        /// <summary>
        /// 建筑名称
        /// </summary>
        protected override string BuildingName => "码头";

        #endregion
    }
}