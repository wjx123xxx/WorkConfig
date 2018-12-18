// *******************************************************************
// * 文件名称： 功能_团战报名.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-08-12 11:07:01
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 功能_团战报名
    /// </summary>
    [WlyView(WlyViewType.功能_团战报名)]
    public class 功能_团战报名 : 总成_军团
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(501, 149, 559, 172), "团战", WlyColor.Normal);
        }

        #endregion
    }
}