// *******************************************************************
// * 文件名称： 任务_武将突飞.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 14:24:10
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 武将突飞
    /// </summary>
    public class 任务_武将突飞 : WlyMainTask
    {
        #region Fields

        private int m_level;

        private WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将突飞(string id, WlyStaffType staff, int level, params string[] depends) : base(id, depends)
        {
            m_level = level;
            m_staff = staff;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_staff.ToString()} => {m_level}";

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

            staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, m_staff);
            if (staff.Level < m_level)
            {
                WlyStaffMgr.Upgrade(m_staff, m_level, dmGuid);
                staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, m_staff);
            }

            if (staff.Level < m_level)
            {
                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}