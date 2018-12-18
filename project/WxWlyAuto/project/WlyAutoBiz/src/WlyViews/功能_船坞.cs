// *******************************************************************
// * 文件名称： 功能_船坞.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 23:10:40
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 造船修船的地方
    /// </summary>
    [WlyView(WlyViewType.功能_船坞)]
    public class 功能_船坞 : 总成_船坞
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(408, 173, 442, 192), "船坞", WlyColor.Normal);
        }

        #endregion
    }
}