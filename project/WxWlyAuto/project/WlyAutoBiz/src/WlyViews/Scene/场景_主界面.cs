// *******************************************************************
// * 文件名称： 场景_主界面.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-10 17:29:19
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 场景_主界面
    /// </summary>
    [WlyView(WlyViewType.场景_主界面)]
    public class 场景_主界面 : WlyUIViewBase
    {
        #region Fields

        private readonly WxRect m_rect = new WxRect(198, 90, 325, 124);

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 场景_主界面()
        {
            AddHandler(WlyViewType.场景_资源区, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(982, 496)));
            AddHandler(WlyViewType.场景_主城, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(953, 555)));
            AddHandler(WlyViewType.导航_日常按钮栏, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(828, 65), TimeSpan.FromSeconds(1)));
            AddHandler(WlyViewType.功能_军团, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(668, 553)));
            AddHandler(WlyViewType.场景_副本, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(899, 549)));
            AddHandler(WlyViewType.功能_主公信息, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(56, 61)));
            AddHandler(WlyViewType.场景_世界地图, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(946, 493)));
            AddHandler(WlyViewType.功能_主线任务, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(854, 555)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            //throw new InvalidOperationException("不能从主界面退出");
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, m_rect, "战斗力", "ffff00-000000");
        }

        #endregion
    }
}