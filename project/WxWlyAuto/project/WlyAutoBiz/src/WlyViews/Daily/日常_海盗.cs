// *******************************************************************
// * 文件名称： 日常_海盗.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 23:00:51
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Daily
{
    /// <summary>
    /// 海盗活动界面
    /// </summary>
    [WlyView(WlyViewType.日常_海盗)]
    public class 日常_海盗 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(505, 430));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(7, 223, 65, 244), "剩余时间", "ffff00-101010")
                   || DMService.Instance.FindStr(dmGuid, new WxRect(523, 280, 559, 299), "海战", "e9e7cf-000000");
        }

        #endregion
    }
}