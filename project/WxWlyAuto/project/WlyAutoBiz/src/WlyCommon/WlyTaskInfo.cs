// *******************************************************************
// * 文件名称： WlyTaskInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 23:07:48
// *******************************************************************

using System;

using Newtonsoft.Json;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 任务执行结果
    /// </summary>
    public class WlyTaskInfo
    {
        #region Constructors

        [JsonConstructor]
        public WlyTaskInfo(string id)
        {
            ID = id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isComplete"></param>
        public WlyTaskInfo(string id, bool isComplete)
        {
            ID = id;
            IsComplete = isComplete;
            if (isComplete)
            {
                CompleteTime = DateTime.Now;
            }
        }

        public WlyTaskInfo(string id, DateTime nextRunTime)
        {
            ID = id;
            IsComplete = false;
            NextRunTime = nextRunTime;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }

        /// <summary>
        /// 对应的任务ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 任务是否完成
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// 未能完成的任务下一次执行的时间
        /// </summary>
        public DateTime NextRunTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return $"IsComplete:{IsComplete} NextRunTime:{NextRunTime:yyyy-MM-dd HH:mm:ss}";
        }

        #endregion
    }
}