// *******************************************************************
// * 文件名称： 招募_裴元绍.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 12:06:02
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
    /// 招募
    /// </summary>
    [WlyView(WlyViewType.招募_裴元绍)]
    public class 招募_裴元绍 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 554), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(530, 157), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(382, 346), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(614, 467), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 160), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(249, 246), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(249, 246), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(272, 477), TimeSpan.FromMilliseconds(2000));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(510, 355), TimeSpan.FromMilliseconds(2000));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(532, 250, 578, 269), "裴元绍", "00ff00-000000");
        }

        #endregion
    }
}