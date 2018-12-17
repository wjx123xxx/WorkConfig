// *******************************************************************
// * 文件名称： 建筑_工坊.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 23:23:25
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 工坊
    /// </summary>
    [WlyBuilding(WlyBuildingType.工坊)]
    [WlyView(WlyViewType.建筑_工坊)]
    public class 建筑_工坊 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_工坊()
        {
            AddHandler(WlyViewType.功能_生产, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(355, 406)));
        }

        #endregion
    }
}