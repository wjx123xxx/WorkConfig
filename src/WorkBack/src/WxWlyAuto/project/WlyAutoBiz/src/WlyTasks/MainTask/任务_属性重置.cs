// *******************************************************************
// * 文件名称： 任务_属性重置.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-30 11:03:34
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 重置经济属性，改成做饭
    /// </summary>
    public class 任务_属性重置 : WlyMainTask
    {
        #region Constructors

        public 任务_属性重置(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_主公属性);

            // 升级经济
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(375, 160));
                return DMService.Instance.FindStr(dmGuid, new WxRect(485, 188, 518, 206), "经济", "ffcc00-000000");
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法打开主公属性经济");
            }

            var key = false;
            var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(659, 304, 710, 329), WlyColor.White, out var amount);
            if (result && (amount > 0))
            {
                key = true;
            }

            //result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(663, 355, 705, 379), WlyColor.White, out amount);
            //if (result && (amount > 0))
            //{
            //    key = true;
            //}

            //result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(662, 409, 706, 432), WlyColor.White, out amount);
            //if (result && (amount > 0))
            //{
            //    key = true;
            //}

            if (key)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(507, 474));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 331));
                entity.AccountInfo.ResetMainLevel();
                entity.AccountInfo.Save();
                throw new InvalidOperationException();
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}