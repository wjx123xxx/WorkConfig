// *******************************************************************
// * 文件名称： 功能_征收.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 23:16:03
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 征收界面
    /// </summary>
    [WlyView(WlyViewType.功能_征收)]
    public class 功能_征收 : 总成_主城
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(222, 180, 262, 203), "征收", WlyColor.Normal);
        }

        #endregion
    }
}