// *******************************************************************
// * 文件名称： 场景_日常.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-18 22:26:21
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 日常活动界面
    /// </summary>
    [WlyView(WlyViewType.场景_日常)]
    public class 场景_日常 : WlyUIViewBase
    {
        #region Fields

        private readonly WxRect m_rect = new WxRect(447, 12, 536, 69);

        #endregion

        #region Constructors

        public 场景_日常()
        {
            AddHandler(WlyViewType.日常_城池争夺战, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(511, 231)));
            AddHandler(WlyViewType.日常_擂台赛, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(266, 233)));
            AddHandler(WlyViewType.日常_古城探秘, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(509, 402)));
            AddHandler(WlyViewType.日常_群雄逐鹿, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(746, 233)));
            AddHandler(WlyViewType.场景_虎牢关战役, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(266, 399)));
            AddHandler(WlyViewType.功能_古代战场, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(748, 394)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(940, 574));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindPic(dmGuid, WlyPicType.日常, m_rect);
        }

        #endregion
    }
}