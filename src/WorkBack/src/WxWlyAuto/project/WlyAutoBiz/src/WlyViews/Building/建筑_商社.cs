// *******************************************************************
// * 文件名称： 建筑_商社.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 23:52:14
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 商社
    /// </summary>
    [WlyBuilding(WlyBuildingType.商社)]
    [WlyView(WlyViewType.建筑_商社)]
    public class 建筑_商社 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_商社()
        {
            AddHandler(WlyViewType.功能_商旅, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 407)));
        }

        #endregion
    }
}