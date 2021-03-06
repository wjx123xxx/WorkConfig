﻿// *******************************************************************
// * 文件名称： 功能_武将.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 21:31:04
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 武将
    /// </summary>
    [WlyView(WlyViewType.功能_武将)]
    public class 功能_武将 : 总成_武将
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(205, 151, 237, 171), "武将", WlyColor.Normal);
        }

        #endregion
    }
}