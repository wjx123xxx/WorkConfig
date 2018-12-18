// *******************************************************************
// * 文件名称： 略_加入失败.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 15:54:48
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 加入军团战失败界面
    /// </summary>
    [WlyView(WlyViewType.略_加入失败)]
    public class 略_加入失败 : WlyUIViewBase
    {
        #region Fields

        private readonly WxPoint m_exitPoint = new WxPoint(500, 334);

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            var guid = dmGuid;
            DMService.Instance.LeftClick(guid, m_exitPoint, TimeSpan.FromMilliseconds(200));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            var guid = dmGuid;
            return DMService.Instance.FindStr(guid, new WxRect(408, 276, 468, 300), "加入失败", "e9e7cf-000000");
        }

        #endregion
    }
}