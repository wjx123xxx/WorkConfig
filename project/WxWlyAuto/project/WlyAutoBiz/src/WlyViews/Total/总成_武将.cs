// *******************************************************************
// * 文件名称： 总成_武将.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 14:21:16
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 武将底层界面
    /// </summary>
    public abstract class 总成_武将 : WlyUIViewBase
    {
        #region Fields

        /// <summary>
        /// 武将列表矩形框
        /// </summary>
        public static readonly WxRect 武将列表 = new WxRect(193, 205, 304, 462);

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 总成_武将()
        {
            AddHandler(WlyViewType.功能_装备, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(376, 158)));
            AddHandler(WlyViewType.功能_训练, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(298, 159)));
            AddHandler(WlyViewType.功能_阵型, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(451, 160)));
            AddHandler(WlyViewType.功能_武将, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(223, 157)));
            AddHandler(WlyViewType.功能_招募, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(531, 156)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(803, 119));
        }

        #endregion
    }
}