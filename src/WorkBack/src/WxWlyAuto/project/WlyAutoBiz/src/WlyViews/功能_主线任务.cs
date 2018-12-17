// *******************************************************************
// * 文件名称： 功能_主线任务.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 19:22:11
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 主线任务
    /// </summary>
    [WlyView(WlyViewType.功能_主线任务)]
    public class 功能_主线任务 : 总成_任务
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(192, 146, 229, 174), "主线", WlyColor.Normal);
        }

        #endregion
    }
}