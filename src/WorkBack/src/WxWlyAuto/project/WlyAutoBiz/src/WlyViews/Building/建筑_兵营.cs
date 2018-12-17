// *******************************************************************
// * 文件名称： 建筑_兵营.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 21:14:09
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 兵营
    /// </summary>
    [WlyBuilding(WlyBuildingType.兵营)]
    [WlyView(WlyViewType.建筑_兵营)]
    public class 建筑_兵营 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_兵营()
        {
            AddHandler(WlyViewType.功能_征兵, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(356, 405)));
        }

        #endregion
    }
}