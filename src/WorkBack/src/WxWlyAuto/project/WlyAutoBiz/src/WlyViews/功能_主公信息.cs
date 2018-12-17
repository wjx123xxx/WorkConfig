// *******************************************************************
// * 文件名称： 功能_主公信息.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 11:01:41
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 主公信息
    /// </summary>
    [WlyView(WlyViewType.功能_主公信息)]
    public class 功能_主公信息 : WlyUIViewBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 功能_主公信息()
        {
            AddHandler(WlyViewType.功能_主公属性, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(501, 422)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(716, 169));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(475, 160, 529, 179), "主公信息", WlyColor.Normal);
        }

        #endregion
    }
}