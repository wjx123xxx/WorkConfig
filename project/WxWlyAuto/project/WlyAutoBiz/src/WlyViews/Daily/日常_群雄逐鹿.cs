// *******************************************************************
// * 文件名称： 日常_群雄逐鹿.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 11:10:26
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Daily
{
    /// <summary>
    /// 群雄逐鹿活动
    /// </summary>
    [WlyView(WlyViewType.日常_群雄逐鹿)]
    public class 日常_群雄逐鹿 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(931, 561));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(40, 84, 95, 111), "排行榜", WlyColor.Normal);
        }

        #endregion
    }
}