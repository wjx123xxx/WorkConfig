// *******************************************************************
// * 文件名称： WlyTimeTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-16 14:59:53
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 指定时间的日常任务
    /// </summary>
    public abstract class WlyTimeTask : WlyTaskBase
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public WlyTimeTask(string id, TimeSpan startTime, TimeSpan endTime) : base(id)
        {
            EndTime = endTime;
            StartTime = startTime;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => DateTime.Today.Add(StartTime).ToString("HH:mm:ss");

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan StartTime { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override WlyTaskInfo Run(WlyEntity entity)
        {
            if (DateTime.Now > DateTime.Today.Add(EndTime) || WlyUtilityBiz.GetRefreshTime() < DateTime.Today.Add(EndTime))
            {
                return new WlyTaskInfo(ID, true);
            }

            return base.Run(entity);
        }

        #endregion
    }
}