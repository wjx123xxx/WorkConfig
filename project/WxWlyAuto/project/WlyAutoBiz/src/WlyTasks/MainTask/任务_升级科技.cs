// *******************************************************************
// * 文件名称： 任务_升级科技.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 14:00:49
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 升级科技
    /// </summary>
    public class 任务_升级科技 : WlyMainTask
    {
        #region Fields

        private readonly int m_level;

        #endregion

        #region Constructors

        public 任务_升级科技(string id, int level, params string[] depends) : base(id, depends)
        {
            m_level = level;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"=> {m_level}";

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
            if (entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level >= 100)
            {
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_升级科技);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(524, 473));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 333));
            }

            var result = WlyTechnologyMgr.UpgradeTechnology(m_level, dmGuid);
            if (result)
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID)
            {
                NextRunTime = DateTime.Now.AddMinutes(30)
            };
        }

        #endregion
    }
}