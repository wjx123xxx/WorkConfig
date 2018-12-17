// *******************************************************************
// * 文件名称： 任务_海鲜副本.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:44:03
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask
{
    /// <summary>
    /// 场景_海鲜副本
    /// </summary>
    [WlyTask(WlyTaskType.海鲜副本)]
    public class 任务_海鲜副本 : WlyTimeTask
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public 任务_海鲜副本(string id, TimeSpan startTime, TimeSpan endTime) : base(id, startTime, endTime)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 过滤检测
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool InternalFilter(WlyEntity entity)
        {
            return entity.AccountInfo.GetSwitchInfo(WlySwitchType.海鲜副本).Enable;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            if (!entity.AccountInfo.GetSwitchInfo(WlySwitchType.海鲜副本).Enable)
            {
                return new WlyTaskInfo(ID, true);
            }

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_海鲜副本);
            var view = WlyViewMgr.GetView(WlyViewType.导航_日常按钮栏);
            if (view.IsCurrentView(dmGuid))
            {
                view.Exit(dmGuid);
            }

            // 循环执行两分钟
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TimeSpan.FromMinutes(2))
            {
                Attack(dmGuid, new WxPoint(803, 441));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 攻击指定位置的NPC
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="target"></param>
        private void Attack(string dmGuid, WxPoint target)
        {
            DMService.Instance.LeftClick(dmGuid, target, TimeSpan.FromMilliseconds(200));

            if (DMService.Instance.FindStr(dmGuid, new WxRect(479, 316, 517, 346), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 331), TimeSpan.FromMilliseconds(200));
            }

            if (DMService.Instance.FindStr(dmGuid, new WxRect(565, 328, 601, 351), "强攻", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(640, 183), TimeSpan.FromMilliseconds(200));
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(616, 304), TimeSpan.FromMilliseconds(200));
        }

        #endregion
    }
}