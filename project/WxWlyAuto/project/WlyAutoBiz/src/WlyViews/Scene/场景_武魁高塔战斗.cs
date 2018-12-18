// *******************************************************************
// * 文件名称： 场景_武魁高塔战斗.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-16 23:23:39
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 武魁高塔内部界面
    /// </summary>
    [WlyView(WlyViewType.场景_武魁高塔战斗)]
    public class 场景_武魁高塔战斗 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(493, 426));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(477, 413, 509, 436), "确定", WlyColor.Normal);
        }

        #endregion
    }
}