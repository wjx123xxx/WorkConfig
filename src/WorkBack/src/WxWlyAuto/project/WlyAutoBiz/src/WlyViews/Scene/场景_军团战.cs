// *******************************************************************
// * 文件名称： 场景_军团战.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 16:47:10
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 军团战界面
    /// </summary>
    [WlyView(WlyViewType.场景_军团战)]
    public class 场景_军团战 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            if (DMService.Instance.FindStr(dmGuid, new WxRect(729, 563, 765, 592), "结果", "f3f3da-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(747, 578));
            }

            if (DMService.Instance.FindStr(dmGuid, new WxRect(942, 566, 980, 590), "退出", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(960, 578));
            }
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(554, 11, 728, 41), "军团", WlyColor.Normal);
        }

        #endregion
    }
}