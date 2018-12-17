// *******************************************************************
// * 文件名称： 场景_武魁高塔.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 16:58:44
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 武魁高塔界面
    /// </summary>
    [WlyView(WlyViewType.场景_武魁高塔)]
    public class 场景_武魁高塔 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            var guid = dmGuid;
            DMService.Instance.LeftClick(guid, new WxPoint(830, 512));
            DMService.Instance.LeftClick(guid, new WxPoint(493, 581));
            DMService.Instance.LeftClick(guid, new WxPoint(493, 581));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(470, 0, 530, 20), "武魁高塔", WlyColor.Normal);
        }

        #endregion
    }
}