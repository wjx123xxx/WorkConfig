// *******************************************************************
// * 文件名称： WlyTaskRunner.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-16 15:18:42
// *******************************************************************

using System;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 任务运行器
    /// </summary>
    public class WlyTaskRunner
    {
        #region Constructors

        public WlyTaskRunner(WlyTaskBase task, DateTime startTime)
        {
            Task = task;
            StartTime = startTime;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; }

        /// <summary>
        /// 执行的任务
        /// </summary>
        public WlyTaskBase Task { get; }

        #endregion
    }
}