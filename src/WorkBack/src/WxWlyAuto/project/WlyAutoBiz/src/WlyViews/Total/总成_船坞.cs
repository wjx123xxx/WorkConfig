// *******************************************************************
// * 文件名称： 总成_船坞.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 23:11:49
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 船坞
    /// </summary>
    public abstract class 总成_船坞 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(803, 119));
        }

        #endregion
    }
}