// *******************************************************************
// * 文件名称： 功能_军团信息.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 00:27:17
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 军团信息
    /// </summary>
    [WlyView(WlyViewType.功能_军团信息)]
    public class 功能_军团信息 : 总成_军团
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(269, 147, 327, 171), "军团信息", WlyColor.Normal);
        }

        #endregion
    }
}