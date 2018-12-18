// *******************************************************************
// * 文件名称： 任务_武将转职.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 21:29:48
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 武将转职
    /// </summary>
    public class 任务_武将转职 : WlyMainTask
    {
        #region Fields

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将转职(string id, WlyStaffType staff, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_staff.ToString()} 转职";

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
            WlyStaffMgr.Evolve(m_staff, dmGuid);
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}