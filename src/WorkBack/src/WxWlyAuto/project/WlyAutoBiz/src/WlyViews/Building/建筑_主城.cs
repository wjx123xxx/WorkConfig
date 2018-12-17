// *******************************************************************
// * 文件名称： 建筑_主城.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 23:10:32
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 主城建筑界面
    /// </summary>
    [WlyView(WlyViewType.主城建筑)]
    [WlyBuilding(WlyBuildingType.主城)]
    public class 建筑_主城 : WlyBuildingViewBase
    {
        #region Constructors

        public 建筑_主城()
        {
            AddHandler(WlyViewType.功能_征收, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 405)));
            AddHandler(WlyViewType.功能_官职, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 408)));
            AddHandler(WlyViewType.功能_属臣, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(555, 405)));
        }

        #endregion
    }
}