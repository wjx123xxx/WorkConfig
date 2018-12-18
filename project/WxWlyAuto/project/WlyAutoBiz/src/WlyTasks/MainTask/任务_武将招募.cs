// *******************************************************************
// * 文件名称： 任务_武将招募.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-17 13:43:49
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 任务_武将招募
    /// </summary>
    public class 任务_武将招募 : WlyMainTask
    {
        #region Fields

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将招募(string id, WlyStaffType staff, params string[] depends) : base(id, depends)
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
            var result = WlyStaffMgr.GetStaff(dmGuid, m_staff);
            if (!result)
            {
                throw new InvalidOperationException();
            }

            entity.AccountInfo.AddStaffInfo(m_staff);

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}