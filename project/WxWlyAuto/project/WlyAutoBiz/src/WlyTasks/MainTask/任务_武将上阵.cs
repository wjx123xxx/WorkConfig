// *******************************************************************
// * 文件名称： 任务_武将上阵.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 13:33:08
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 武将上阵
    /// </summary>
    public class 任务_武将上阵 : WlyMainTask
    {
        #region Fields

        private readonly WlyFormationType m_formation;

        private readonly int m_index;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将上阵(string id, WlyStaffType staff, WlyFormationType formation, int index, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
            m_formation = formation;
            m_index = index;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_staff.ToString()} {m_formation.ToString()} {m_index}";

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
            WlyFormationMgr.SetStaff(m_staff, m_formation, m_index, dmGuid);
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}