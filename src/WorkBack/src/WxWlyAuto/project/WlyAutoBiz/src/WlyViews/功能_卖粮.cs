// *******************************************************************
// * 文件名称： 功能_卖粮.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 14:51:34
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 卖粮界面
    /// </summary>
    [WlyView(WlyViewType.功能_卖粮)]
    public class 功能_卖粮 : 总成_主城
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(472, 181, 507, 203), "市场", WlyColor.Normal);
        }

        #endregion
    }
}