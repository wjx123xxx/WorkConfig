// *******************************************************************
// * 文件名称： 功能_招募.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-23 22:17:23
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 招募
    /// </summary>
    [WlyView(WlyViewType.功能_招募)]
    public class 功能_招募 : 总成_武将
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(513, 150, 547, 171), "招募", WlyColor.Normal);
        }

        #endregion
    }
}