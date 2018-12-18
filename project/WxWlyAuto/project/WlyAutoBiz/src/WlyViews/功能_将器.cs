// *******************************************************************
// * 文件名称： 功能_将器.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 01:16:17
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 将器
    /// </summary>
    [WlyView(WlyViewType.功能_将器)]
    public class 功能_将器 : 总成_装备
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(287, 181, 307, 200), "将", WlyColor.Normal);
        }

        #endregion
    }
}