// *******************************************************************
// * 文件名称： 场景_副本.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 18:37:07
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 副本界面
    /// </summary>
    [WlyView(WlyViewType.场景_副本)]
    public class 场景_副本 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            var rect = new WxRect(436, 563, 566, 594);
            var word = DMService.Instance.GetWords(dmGuid, rect, "ffffcc-000000");
            if (word.Contains("Wly副本"))
            {
                return true;
            }

            return DMService.Instance.FindStr(dmGuid, rect, "势力", "ffffcc-000000");
        }

        #endregion
    }
}