// *******************************************************************
// * 文件名称： 功能_军团科技.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 00:29:30
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 军团科技
    /// </summary>
    [WlyView(WlyViewType.功能_军团科技)]
    public class 功能_军团科技 : 总成_军团
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(347, 149, 404, 173), "军团科技", WlyColor.Normal);
        }

        #endregion
    }
}