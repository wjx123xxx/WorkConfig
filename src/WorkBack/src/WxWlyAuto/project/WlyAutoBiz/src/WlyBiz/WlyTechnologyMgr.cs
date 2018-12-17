// *******************************************************************
// * 文件名称： WlyTechnologyMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-09 23:23:42
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 科技管理器
    /// </summary>
    public static class WlyTechnologyMgr
    {
        #region Public Methods

        /// <summary>
        /// 升级科技到指定的策略府等级
        /// </summary>
        /// <param name="level"></param>
        /// <param name="dmGuid"></param>
        public static bool UpgradeTechnology(int level, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_升级科技);

            var startPoint = new WxPoint(239, 249);
            for (int column = 0; column < 6; column++)
            {
                for (int row = 0; row < 2; row++)
                {
                    DMService.Instance.LeftClick(dmGuid, startPoint.Shift(column * 72, row * 72));
                    if (!FlowLogicHelper.RunToTarget(() =>
                    {
                        var str = DMService.Instance.GetWords(dmGuid, new WxRect(719, 358, 800, 382), "33ffff-000000");
                        return int.Parse(str.Substring(3, str.Length - 4));
                    }, current => current > level, () => DMService.Instance.LeftClick(dmGuid, new WxPoint(613, 472))))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}