// *******************************************************************
// * 文件名称： 任务_设置兵种.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 21:48:33
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 设置兵种
    /// </summary>
    public class 任务_设置兵种 : WlyMainTask
    {
        #region Fields

        private readonly int m_index;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_设置兵种(string id, WlyStaffType staff, int index, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
            m_index = index;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"{m_staff.ToString()} 设置兵种 {m_index}";

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
            WlyStaffMgr.SetType(m_staff, m_index, dmGuid);
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}