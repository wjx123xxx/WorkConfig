// *******************************************************************
// * 文件名称： 场景_密室.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 21:36:02
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 密室
    /// </summary>
    [WlyView(WlyViewType.场景_密室)]
    public class 场景_密室 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 579));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(481, 563, 520, 595), "返回", "ffffcc-000000");
        }

        #endregion
    }
}