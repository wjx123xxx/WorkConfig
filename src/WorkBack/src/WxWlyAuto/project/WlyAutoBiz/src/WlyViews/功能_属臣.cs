// *******************************************************************
// * 文件名称： 功能_属臣.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-26 23:12:51
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 属臣界面
    /// </summary>
    [WlyView(WlyViewType.功能_属臣)]
    public class 功能_属臣 : 总成_主城
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(590, 180, 623, 199), "信息", WlyColor.Normal);
        }

        #endregion
    }
}