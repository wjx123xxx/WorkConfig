// *******************************************************************
// * 文件名称： 任务_免费军团.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-30 10:46:48
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 提供首攻给大号
    /// </summary>
    public class 任务_免费军团 : WlyFinalTask
    {
        #region Constructors

        public 任务_免费军团(string id, params string[] depends) : base(id, depends)
        {
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
            WlyProgressMgr.FreeAttack(entity);
            return new WlyTaskInfo(ID, DateTime.Now.AddMinutes(10));
        }

        #endregion
    }
}