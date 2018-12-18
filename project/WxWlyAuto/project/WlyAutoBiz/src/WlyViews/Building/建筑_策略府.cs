// *******************************************************************
// * 文件名称： 建筑_策略府.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-01 23:00:17
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 策略府界面
    /// </summary>
    [WlyView(WlyViewType.建筑_策略府)]
    [WlyBuilding(WlyBuildingType.策略府)]
    public class 建筑_策略府 : WlyBuildingViewBase
    {
        #region Constructors

        public 建筑_策略府()
        {
            AddHandler(WlyViewType.功能_升级科技, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(354, 405)));
        }

        #endregion
    }
}