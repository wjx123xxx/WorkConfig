// *******************************************************************
// * 文件名称： 建筑_民居.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 16:34:38
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Building
{
    /// <summary>
    /// 民居基类
    /// </summary>
    [WlyView(WlyViewType.民居)]
    public class 建筑_民居 : WlyBuildingViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 建筑_民居()
        {
            AddHandler(WlyViewType.功能_巡查, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(353, 407)));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 建筑名称
        /// </summary>
        protected override string BuildingName => "民居";

        #endregion
    }
}