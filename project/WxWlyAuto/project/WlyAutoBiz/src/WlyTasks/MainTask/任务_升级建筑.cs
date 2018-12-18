// *******************************************************************
// * 文件名称： 任务_升级建筑.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 23:04:50
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 升级建筑的任务
    /// </summary>
    public class 任务_升级建筑 : WlyMainTask
    {
        #region Fields

        private readonly WlyBuildingType m_buildingType;

        private readonly int m_targetLevel;

        #endregion

        #region Constructors

        public 任务_升级建筑(string id, WlyBuildingType type, int targetLevel, params string[] depends) : base(id, depends)
        {
            m_targetLevel = targetLevel;
            m_buildingType = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_buildingType} => {m_targetLevel}";

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
            var cost = WlyBuildingMgr.Upgrade(m_buildingType, m_targetLevel, dmGuid);

            var buildingInfo = entity.AccountInfo.GetBuildingInfo(m_buildingType);
            buildingInfo.Level = WlyBuildingMgr.GetBuildingLevel(m_buildingType, dmGuid);

            if (buildingInfo.Level >= m_targetLevel)
            {
                return new WlyTaskInfo(ID, true);
            }

            if (cost == 0)
            {
                return new WlyTaskInfo(ID, DateTime.Now.AddHours(1));
            }
            return new WlyTaskInfo(ID, DateTime.Now.AddMinutes((cost + 1) * 10));
        }

        #endregion
    }
}