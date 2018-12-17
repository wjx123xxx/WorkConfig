// *******************************************************************
// * 文件名称： 场景_海鲜副本.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-22 00:18:37
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 海鲜活动界面
    /// </summary>
    [WlyView(WlyViewType.场景_海鲜副本)]
    public class 场景_海鲜副本 : WlyUIViewBase
    {
        #region Fields

        private readonly WxRect m_rect = new WxRect(899, 89, 977, 144);

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(953, 555));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindPic(dmGuid, WlyPicType.海鲜副本, m_rect);
        }

        #endregion
    }
}