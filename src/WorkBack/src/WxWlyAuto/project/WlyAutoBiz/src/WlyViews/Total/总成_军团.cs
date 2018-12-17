// *******************************************************************
// * 文件名称： 总成_军团.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 00:25:00
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 军团总成
    /// </summary>
    public abstract class 总成_军团 : WlyUIViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 总成_军团()
        {
            AddHandler(WlyViewType.功能_军团, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(220, 160)));
            AddHandler(WlyViewType.功能_军团信息, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(298, 161)));
            AddHandler(WlyViewType.功能_军团科技, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(375, 160)));
            AddHandler(WlyViewType.功能_团战报名, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(528, 158)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(802, 120));
        }

        #endregion
    }
}