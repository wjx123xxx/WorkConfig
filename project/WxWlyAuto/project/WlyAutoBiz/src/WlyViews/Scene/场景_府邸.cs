// *******************************************************************
// * 文件名称： 场景_府邸.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 21:57:03
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 府邸界面
    /// </summary>
    [WlyView(WlyViewType.场景_府邸)]
    public class 场景_府邸 : WlyUIViewBase
    {
        #region Constructors

        public 场景_府邸()
        {
            AddHandler(WlyViewType.功能_寻访, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(638, 170)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(942, 576));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(818, 562, 861, 586), "说明", "f2f2a7-101010");
        }

        #endregion
    }
}