// *******************************************************************
// * 文件名称： 功能_军团.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-28 22:46:03
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 军团界面
    /// </summary>
    [WlyView(WlyViewType.功能_军团)]
    public class 功能_军团 : 总成_军团
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(193, 149, 254, 171), "世界军团", WlyColor.Normal);
        }

        #endregion
    }
}