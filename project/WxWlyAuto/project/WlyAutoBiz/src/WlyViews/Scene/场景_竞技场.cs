﻿// *******************************************************************
// * 文件名称： 场景_竞技场.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-27 23:17:24
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 场景_竞技场
    /// </summary>
    [WlyView(WlyViewType.场景_竞技场)]
    public class 场景_竞技场 : WlyUIViewBase
    {
        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            if (DMService.Instance.FindStr(dmGuid, new WxRect(435, 321, 473, 343), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 332));
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(938, 561));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(468, 377, 530, 398), "更换对手", WlyColor.Normal);
        }

        #endregion
    }
}