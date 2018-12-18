// *******************************************************************
// * 文件名称： 功能_强化装备.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 14:18:19
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 强化装备
    /// </summary>
    [WlyView(WlyViewType.功能_强化装备)]
    public class 功能_强化装备 : 总成_装备
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(435, 151, 473, 172), "强", WlyColor.Normal);
        }

        #endregion
    }
}