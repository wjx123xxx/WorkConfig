// *******************************************************************
// * 文件名称： 建筑_校场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-01 23:07:35
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 校场界面
    /// </summary>
    [WlyView(WlyViewType.建筑_校场)]
    [WlyBuilding(WlyBuildingType.校场)]
    public class 建筑_校场 : WlyBuildingViewBase
    {
        #region Constructors

        public 建筑_校场()
        {
            AddHandler(WlyViewType.功能_武将, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(353, 406)));
        }

        #endregion
    }
}