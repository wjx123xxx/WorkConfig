// *******************************************************************
// * 文件名称： 导航_日常按钮栏.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-22 00:05:58
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
    /// 日常工具栏界面
    /// </summary>
    [WlyView(WlyViewType.导航_日常按钮栏)]
    public class 导航_日常按钮栏 : WlyUIViewBase
    {
        #region Fields

        /// <summary>
        /// 界面上部分的菜单区
        /// </summary>
        private readonly WxRect m_topToolBar = new WxRect(350, 5, 815, 236);

        #endregion

        #region Constructors

        public 导航_日常按钮栏()
        {
            // 跳转到日常界面
            AddHandler(WlyViewType.场景_日常, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.日常按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.活动界面, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.活动一览按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.日常_海盗, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.海盗按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.场景_海鲜副本, dmGuid =>
            {
                var find = DMService.Instance.FindStr(dmGuid, m_topToolBar, "活动副本", WlyColor.Normal, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(20, -20));
                }
            });

            AddHandler(WlyViewType.场景_活动副本, dmGuid =>
            {
                var find = DMService.Instance.FindStr(dmGuid, m_topToolBar, "活动副本", WlyColor.Normal, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(20, -20));
                }
            });

            AddHandler(WlyViewType.功能_普通砸罐, dmGuid =>
            {
                var find = DMService.Instance.FindStr(dmGuid, m_topToolBar, "砸罐", WlyColor.Normal, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, -20));
                }
            });

            AddHandler(WlyViewType.功能_红包, dmGuid =>
            {
                var find = DMService.Instance.FindStr(dmGuid, m_topToolBar, "红包", WlyColor.Normal, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, -20));
                }
            });

            AddHandler(WlyViewType.场景_游历, dmGuid =>
            {
                var find = DMService.Instance.FindStr(dmGuid, m_topToolBar, "游历", WlyColor.Normal, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, -20));
                }
            });

            AddHandler(WlyViewType.功能_酒馆, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.酒馆按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.功能_游戏助手, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.游戏助手按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.功能_活跃, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.活跃按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.场景_竞技场, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.竞技场按钮, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.功能_王位争夺战, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.王位争夺战, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });

            AddHandler(WlyViewType.功能_厉兵秣马, dmGuid =>
            {
                var find = DMService.Instance.FindPic(dmGuid, WlyPicType.厉兵秣马, m_topToolBar, out var x, out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y).Shift(5, 5), TimeSpan.FromSeconds(1));
                }
            });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(828, 63), TimeSpan.FromMilliseconds(100));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(828, 80), TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindPic(dmGuid, WlyPicType.日常关闭, new WxRect(810, 30, 850, 140));
        }

        #endregion
    }
}