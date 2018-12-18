// *******************************************************************
// * 文件名称： 场景_活动副本.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 14:29:34
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 活动副本
    /// </summary>
    [WlyView(WlyViewType.场景_活动副本)]
    public class 场景_活动副本 : 场景_主界面
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindPic(dmGuid, WlyPicType.活动副本, new WxRect(709, 275, 835, 375));
        }

        #endregion
    }
}