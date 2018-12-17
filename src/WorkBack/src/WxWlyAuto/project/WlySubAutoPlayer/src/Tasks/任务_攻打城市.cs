// *******************************************************************
// * 文件名称： 任务_攻打城市.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-08-12 10:59:29
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 任务_攻打城市
    /// </summary>
    public class 任务_攻打城市 : WlyDailyTask
    {
        #region Constructors

        public 任务_攻打城市(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_团战报名);

            var day = WlyUtilityBiz.SystemInfo.GameTime;
            if (day.Contains("夏") || day.Contains("秋"))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(693, 231), TimeSpan.FromSeconds(1));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(346, 479), TimeSpan.FromSeconds(1));

                if (DMService.Instance.FindStr(dmGuid, new WxRect(438, 268, 570, 369), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 331));
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(560, 225));
                if (DMService.Instance.FindColor(dmGuid, "ffffff-000000", new WxRect(503, 253, 536, 264)))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(519, 259));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(693, 231), TimeSpan.FromSeconds(1));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(346, 479), TimeSpan.FromSeconds(1));

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(438, 268, 570, 369), "确定", WlyColor.Normal))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 331));
                    }
                }
            }

            if (DMService.Instance.FindColor(dmGuid, "f3f3da-000000", new WxRect(347, 222, 400, 242)))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(372, 231));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(343, 478));

                if (DMService.Instance.FindStr(dmGuid, new WxRect(438, 268, 570, 369), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(499, 331));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}