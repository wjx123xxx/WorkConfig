// *******************************************************************
// * 文件名称： 功能_生产.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-28 23:27:14
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 生产
    /// </summary>
    [WlyView(WlyViewType.功能_生产)]
    public class 功能_生产 : 总成_主城
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(356, 149, 475, 171), "作坊", WlyColor.Normal);
        }

        #endregion
    }
}