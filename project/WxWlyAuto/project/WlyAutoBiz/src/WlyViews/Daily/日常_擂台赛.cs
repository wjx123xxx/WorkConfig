// *******************************************************************
// * 文件名称： 日常_擂台赛.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 22:59:48
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Daily
{
    /// <summary>
    /// 擂台赛界面
    /// </summary>
    [WlyView(WlyViewType.日常_擂台赛)]
    public class 日常_擂台赛 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(911, 546));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(96, 352, 160, 377), "单挑武将", WlyColor.Normal);
        }

        #endregion
    }
}