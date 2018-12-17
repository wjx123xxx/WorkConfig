// *******************************************************************
// * 文件名称： 任务_武将下野.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-17 13:41:43
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 任务_武将下野
    /// </summary>
    public class 任务_武将下野 : WlyMainTask
    {
        #region Fields

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将下野(string id, WlyStaffType staff, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
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

            // 武将除名
            entity.AccountInfo.RemoveStaffInfo(m_staff);

            // 首先卸载武将全部的装备
            WlyEquipMgr.UnequipStaff(dmGuid, m_staff);

            // 武将下野
            if (WlyStaffMgr.ThrowStaff(dmGuid, m_staff))
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
        }

        #endregion
    }
}