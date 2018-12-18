// *******************************************************************
// * 文件名称： 功能_活跃.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-20 23:06:20
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 活跃
    /// </summary>
    [WlyView(WlyViewType.功能_活跃)]
    public class 功能_活跃 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 119));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(740, 347, 777, 375), "签到", WlyColor.Normal, "cccccc-000000");
        }

        #endregion
    }
}