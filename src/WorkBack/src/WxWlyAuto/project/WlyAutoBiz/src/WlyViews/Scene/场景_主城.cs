// *******************************************************************
// * 文件名称： 场景_主城.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 10:47:36
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 场景_主城
    /// </summary>
    [WlyView(WlyViewType.场景_主城)]
    public class 场景_主城 : 场景_主界面
    {
        #region Fields

        private readonly WxRect m_mainCityRect = new WxRect(772, 278, 838, 320);

        #endregion

        #region Constructors

        public 场景_主城()
        {
            AddHandler(WlyViewType.民居1, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(307, 351)));
            AddHandler(WlyViewType.民居2, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(156, 308)));
            AddHandler(WlyViewType.民居3, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(306, 261)));
            AddHandler(WlyViewType.民居4, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(219, 327)));
            AddHandler(WlyViewType.功能_市场, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(565, 463)));
            AddHandler(WlyViewType.功能_打猎, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(280, 151)));
            AddHandler(WlyViewType.建筑_商铺, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(399, 299)));
            AddHandler(WlyViewType.主城建筑, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(850, 168)));
            AddHandler(WlyViewType.建筑_策略府, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(518, 162)));
            AddHandler(WlyViewType.建筑_校场, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(791, 302)));
            AddHandler(WlyViewType.场景_港口, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(178, 505)));
            AddHandler(WlyViewType.场景_府邸, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(577, 551)));
            AddHandler(WlyViewType.建筑_商社, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(678, 506)));
            AddHandler(WlyViewType.建筑_兵营, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(851, 345)));
            AddHandler(WlyViewType.建筑_工坊, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(766, 473)));
            AddHandler(WlyViewType.建筑_账房, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(591, 122)));
            AddHandler(WlyViewType.建筑_钱庄, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(643, 433)));
            AddHandler(WlyViewType.建筑_农田, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(209, 200)));
            AddHandler(WlyViewType.建筑_试炼塔, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(519, 324)));
            AddHandler(WlyViewType.场景_国政, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(625, 555)));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, m_mainCityRect, "级", "ffffff-000000");
        }

        #endregion
    }
}