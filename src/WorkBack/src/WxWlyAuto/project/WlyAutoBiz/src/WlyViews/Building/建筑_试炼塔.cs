// *******************************************************************
// * 文件名称： 建筑_试炼塔.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-20 22:38:22
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 试练塔
    /// </summary>
    [WlyView(WlyViewType.建筑_试炼塔)]
    [WlyBuilding(WlyBuildingType.试炼塔)]
    public class 建筑_试炼塔 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_试炼塔()
        {
            AddHandler(WlyViewType.功能_试炼塔, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 412)));
        }

        #endregion
    }
}