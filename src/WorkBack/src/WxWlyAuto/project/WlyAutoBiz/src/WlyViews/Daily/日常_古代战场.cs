// *******************************************************************
// * 文件名称： 日常_古代战场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-26 22:36:19
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Daily
{
    /// <summary>
    /// 古代战场界面
    /// </summary>
    [WlyView(WlyViewType.日常_古代战场)]
    public class 日常_古代战场 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(814, 114));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(627, 132, 669, 159), "开启", "ffffff-000000");
        }

        #endregion
    }
}