// *******************************************************************
// * 文件名称： 功能_每日任务.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 19:24:50
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 每日任务
    /// </summary>
    [WlyView(WlyViewType.功能_每日任务)]
    public class 功能_每日任务 : 总成_任务
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(269, 147, 304, 174), "每日", WlyColor.Normal);
        }

        #endregion
    }
}