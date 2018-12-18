// *******************************************************************
// * 文件名称： 总成_主城.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 14:45:48
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 总成_主城
    /// </summary>
    public abstract class 总成_主城 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 构造函数
        /// </summary>
        public 总成_主城()
        {
            AddHandler(WlyViewType.功能_市场, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(530, 157)));
            AddHandler(WlyViewType.功能_生产, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(450, 158)));
            AddHandler(WlyViewType.功能_征收, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(222, 160)));
        }


        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 119));
        }

        #endregion
    }
}