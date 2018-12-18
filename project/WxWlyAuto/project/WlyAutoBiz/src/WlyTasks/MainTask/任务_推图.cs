// *******************************************************************
// * 文件名称： 任务_推图.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 16:27:14
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 推图
    /// </summary>
    public class 任务_推图 : WlyMainTask
    {
        #region Fields

        private readonly int m_progress;

        #endregion

        #region Constructors

        public 任务_推图(string id, int progress, params string[] depends) : base(id, depends)
        {
            m_progress = progress;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"=> {m_progress}";

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            // 等待周一更新后再推图
            var dmGuid = entity.DMGuid;
            try
            {
                WlyEntityBiz.UpgradeAllEquipmenets(entity);
            }
            catch (Exception ex)
            {
                WxLog.Error($"任务_推图.InternalRun Error <{ex}>");
                throw new InvalidOperationException("内部异常");
            }

            // 勾上战斗结果
            if (entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level >= 20)
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(458, 166, 497, 195), "消费", WlyColor.Normal))
                    {
                        return true;
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(762, 14));
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                DMService.Instance.LeftDown(dmGuid, new WxPoint(646, 421));
                wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(506, 414, 540, 436), "20级", "ffffb0-000000"))
                    {
                        DMService.Instance.LeftUp(dmGuid, new WxPoint(646, 421));
                        return true;
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(376, 414, 409, 433), "20级", "ffffb0-000000"))
                    {
                        DMService.Instance.LeftUp(dmGuid, new WxPoint(646, 421));
                        return true;
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(376, 414, 409, 433), "20级", "f3f3da-000000", out var x, out var y))
                    {
                        DMService.Instance.LeftUp(dmGuid, new WxPoint(646, 421));
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x + 10, y + 5));
                        return false;
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(506, 414, 540, 436), "20级", "f3f3da-000000", out var xx, out var yy))
                    {
                        DMService.Instance.LeftUp(dmGuid, new WxPoint(646, 421));
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(xx + 10, yy + 5));
                        return false;
                    }

                    Thread.Sleep(500);
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    DMService.Instance.LeftUp(dmGuid, new WxPoint(646, 421));
                    throw new InvalidOperationException();
                }
            }

            var result = WlyProgressMgr.Attack(entity, m_progress);
            if (result && DMService.Instance.FindStr(dmGuid, new WxRect(530, 323, 565, 340), "取消", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 330));
            }

            if (!result)
            {
                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            entity.AccountInfo.Progress = m_progress;
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}