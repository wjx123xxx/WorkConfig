// *******************************************************************
// * 文件名称： 总成_装备.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 12:03:45
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 总成_装备
    /// </summary>
    public abstract class 总成_装备 : WlyUIViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 总成_装备()
        {
            AddHandler(WlyViewType.功能_强化装备, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 160)));
            AddHandler(WlyViewType.功能_委派, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(531, 160)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 120));
        }

        #endregion
    }
}