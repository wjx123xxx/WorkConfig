// *******************************************************************
// * 文件名称： 场景_战斗.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 16:48:34
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 战斗界面
    /// </summary>
    [WlyView(WlyViewType.场景_战斗)]
    public class 场景_战斗 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            if (DMService.Instance.FindStr(dmGuid, new WxRect(723, 560, 768, 591), "结果", "f3f3da-000000"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(751, 576));
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(537, 410));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(814, 563, 848, 589), "特效",
                $"{WlyColor.Normal}|f3f3da-000000");
        }

        #endregion
    }
}