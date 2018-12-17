// *******************************************************************
// * 文件名称： 功能_仓库.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-29 22:20:16
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 仓库界面
    /// </summary>
    [WlyView(WlyViewType.功能_仓库)]
    public class 功能_仓库 : 总成_装备
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(200, 146, 247, 173), "仓库", WlyColor.Normal);
        }

        #endregion
    }
}