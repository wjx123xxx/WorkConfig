// *******************************************************************
// * 文件名称： 功能_委派.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 08:36:52
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 委派
    /// </summary>
    [WlyView(WlyViewType.功能_委派)]
    public class 功能_委派 : 总成_装备
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(510, 147, 548, 169), "委派", WlyColor.Normal);
        }

        #endregion
    }
}