// *******************************************************************
// * 文件名称： 日常_古城探秘.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 23:02:26
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Daily
{
    /// <summary>
    /// 古城探险界面
    /// </summary>
    [WlyView(WlyViewType.日常_古城探秘)]
    public class 日常_古城探秘 : WlyUIViewBase
    {
        #region Fields

        private readonly WxRect m_rect = new WxRect(467, 64, 532, 92);

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(913, 84));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, m_rect, "古城探秘", WlyColor.Normal);
        }

        #endregion
    }
}