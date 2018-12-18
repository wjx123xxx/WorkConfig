// *******************************************************************
// * 文件名称： 总成_任务.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 19:23:05
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 任务总成
    /// </summary>
    public abstract class 总成_任务 : WlyUIViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 总成_任务()
        {
            AddHandler(WlyViewType.功能_主线任务, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(223, 156)));
            AddHandler(WlyViewType.功能_每日任务, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(297, 160)));
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