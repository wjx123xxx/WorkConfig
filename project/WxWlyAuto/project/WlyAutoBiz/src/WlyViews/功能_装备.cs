// *******************************************************************
// * 文件名称： 功能_装备.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-09 23:49:25
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 装备更换界面
    /// </summary>
    [WlyView(WlyViewType.功能_装备)]
    public class 功能_装备 : 总成_武将
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(362, 152, 389, 169), "装备", WlyColor.Normal);
        }

        #endregion
    }
}