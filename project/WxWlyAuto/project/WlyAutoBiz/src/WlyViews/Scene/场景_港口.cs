// *******************************************************************
// * 文件名称： 场景_港口.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-06 23:56:37
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 场景_港口
    /// </summary>
    [WlyView(WlyViewType.场景_港口)]
    public class 场景_港口 : 场景_主界面
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 场景_港口()
        {
            AddHandler(WlyViewType.建筑_船坞, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(846, 433)));
            AddHandler(WlyViewType.建筑_商贸码头, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(545, 341)));
            AddHandler(WlyViewType.建筑_仓库, dmGuid => DMService.Instance.LeftClick(dmGuid, new WxPoint(872, 271)));
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
            return DMService.Instance.FindStr(dmGuid, new WxRect(354, 497, 407, 528), "海战", WlyColor.Normal);
        }

        #endregion
    }
}