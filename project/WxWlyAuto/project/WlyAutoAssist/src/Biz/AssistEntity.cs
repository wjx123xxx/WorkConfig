// *******************************************************************
// * 文件名称： AssistEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-12 22:45:32
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyInfo;

namespace Wx.App.WlyAutoAssist.Biz
{
    /// <summary>
    /// 辅助号实体
    /// </summary>
    public class AssistEntity : WlyEntity
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssistEntity(WlyAccountInfo accountInfo) : base(accountInfo)
        {
        }

        #endregion

        #region Protected Methods

        protected override void InternalBackupWorkThread()
        {
            AssistTaskMgr.Instance.InitEntityTasks(this);
        }

        /// <summary>
        /// 要求计算任务列表
        /// </summary>
        protected override void InternalInitTasks()
        {
            AssistTaskMgr.Instance.InitEntityTasks(this);
        }

        #endregion
    }
}