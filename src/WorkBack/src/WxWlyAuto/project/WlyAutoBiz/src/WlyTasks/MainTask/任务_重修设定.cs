// *******************************************************************
// * 文件名称： 任务_重修设定.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-02 16:36:26
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 兵种设定
    /// </summary>
    public class 任务_重修设定 : WlyMainTask
    {
        #region Fields

        private readonly WlySoldierType m_soldierType;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_重修设定(string id, WlyStaffType staff, WlySoldierType soldierType, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
            m_soldierType = soldierType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_staff.ToString()} => {m_soldierType.ToString()}";

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            var info = entity.AccountInfo.GetStaffInfo(m_staff);
            info.TargetType = m_soldierType;
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}