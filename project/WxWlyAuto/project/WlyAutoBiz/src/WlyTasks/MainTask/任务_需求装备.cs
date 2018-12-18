// *******************************************************************
// * 文件名称： 任务_需求装备.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-20 23:32:47
// *******************************************************************

using System.Collections.Generic;

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 武将需求装备设置
    /// </summary>
    public class 任务_需求装备 : WlyMainTask
    {
        #region Fields

        private readonly IEnumerable<WlyEquipType> m_requestList;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_需求装备(string id, WlyStaffType staff, IEnumerable<WlyEquipType> requestList, params string[] depends) : base(id, depends)
        {
            m_requestList = requestList;
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
            var staffInfo = entity.AccountInfo.GetStaffInfo(m_staff);
            if (staffInfo == null)
            {
                // 武将已下野
                return new WlyTaskInfo(ID, true);
            }

            foreach (var request in m_requestList)
            {
                staffInfo.EquipmentRequestDict[request] = true;
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}