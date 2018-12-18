// *******************************************************************
// * 文件名称： 功能_寻访.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 22:00:46
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 寻访界面
    /// </summary>
    [WlyView(WlyViewType.功能_寻访)]
    public class 功能_寻访 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(904, 535));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(638, 462, 702, 489), "高级", WlyColor.Normal);
        }

        #endregion
    }
}