// *******************************************************************
// * 文件名称： WlyTaskMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-11 17:50:47
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class WlyTaskMgr
    {
        #region Fields

        private readonly ConcurrentDictionary<string, WlyTaskBase> m_taskDict = new ConcurrentDictionary<string, WlyTaskBase>();

        private readonly ConcurrentDictionary<WlyTaskType, WlyTaskBase> m_taskTypeDict = new ConcurrentDictionary<WlyTaskType, WlyTaskBase>();

        #endregion

        #region Public Properties

        /// <summary>
        /// 任务列表
        /// </summary>
        public IEnumerable<WlyTaskBase> TaskList
        {
            get { return m_taskDict.Values; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WlyTaskBase GetTask(WlyTaskType type)
        {
            if (m_taskTypeDict.ContainsKey(type))
            {
                return m_taskTypeDict[type];
            }

            return null;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="entity"></param>
        public void InitEntityTasks(WlyEntity entity)
        {
            var timebase = DateTime.Today > DateTime.Now.AddHours(-4) ? DateTime.Today.AddDays(-1) : DateTime.Today;
            foreach (var task in m_taskDict.Values)
            {
                if (task is WlyTimeTask tt)
                {
                    var time = timebase.Add(tt.StartTime);
                    entity.AddTask(task, time);
                }
                else if (task is WlyDailyTask)
                {
                    entity.AddTask(task, timebase.AddHours(4));
                }
                else
                {
                    entity.AddTask(task, DateTime.MinValue);
                }
            }

            var tasklist = m_taskDict.Values.Select(o => o.ID);
            entity.ClearTasks(tasklist);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="task"></param>
        protected WlyTaskBase AddTask(WlyTaskBase task)
        {
            if (m_taskDict.ContainsKey(task.ID))
            {
                throw new InvalidOperationException($"重复的任务ID{task.ID}");
            }

            m_taskDict.TryAdd(task.ID, task);

            var attribute = task.GetType().GetCustomAttribute<WlyTaskAttribute>();
            if (attribute != null)
            {
                m_taskTypeDict.TryAdd(attribute.Type, task);
            }

            return task;
        }

        #endregion
    }
}