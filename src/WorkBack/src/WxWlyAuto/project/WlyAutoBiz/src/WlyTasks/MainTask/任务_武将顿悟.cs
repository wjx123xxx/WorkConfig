// *******************************************************************
// * 文件名称： 任务_武将顿悟.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 10:53:47
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 武将顿悟
    /// </summary>
    public class 任务_武将顿悟 : WlyMainTask
    {
        #region Fields

        private readonly WlyQualityType m_quality;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将顿悟(string id, WlyStaffType staff, WlyQualityType quality, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
            m_quality = quality;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"顿悟 {m_staff.ToString()} => {m_quality.ToString()}";

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
            var staff = entity.AccountInfo.GetStaffInfo(m_staff);
            if (staff.Quality >= m_quality)
            {
                return new WlyTaskInfo(ID, true);
            }

            WlyStaffMgr.EnvolveStaff(dmGuid, m_staff, m_quality);
            staff.Quality = WlyStaffMgr.GetStaffQuality(dmGuid, m_staff);
            if (staff.Quality >= m_quality)
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
        }

        #endregion
    }
}