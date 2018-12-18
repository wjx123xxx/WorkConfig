// *******************************************************************
// * 文件名称： 略_点击继续.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-18 21:34:35
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 点击任意按钮继续界面
    /// </summary>
    [WlyView(WlyViewType.略_点击继续)]
    public class 略_点击继续 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(596, 322));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(541, 312, 589, 342), "任意", "ffcc00-000000");
        }

        #endregion
    }
}