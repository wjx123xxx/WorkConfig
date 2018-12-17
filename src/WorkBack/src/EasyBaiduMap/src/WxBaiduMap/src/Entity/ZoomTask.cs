// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： ZoomTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-23 10:57:39
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 一整個層級的下載任務
    /// </summary>
    public class ZoomTask : IEnumerable<MapLine>
    {
        #region Fields

        private readonly Dictionary<long, List<MapLine>> m_taskDict = new Dictionary<long, List<MapLine>>();

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public ZoomTask(int zoom)
        {
            Zoom = zoom;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 瓦片下載任務總數
        /// </summary>
        public long TaskCount => m_taskDict.Values.Sum(o => o.Sum(t => t.TaskCount));

        /// <summary>
        /// 缩放等级
        /// </summary>
        public int Zoom { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            // 测试代码
            var minx = m_taskDict.Values.Min(list => list.Min(t => t.StartX));
            var minY = m_taskDict.Keys.Min();
            var maxY = m_taskDict.Keys.Max();
            var result = new StringBuilder();
            for (var index = maxY; index >= minY; index--)
            {
                if (!m_taskDict.ContainsKey(index))
                {
                    result.AppendLine();
                    continue;
                }

                var group = m_taskDict[index];
                var sb = new StringBuilder();
                var count = minx;
                foreach (var downloadTask in group.OrderBy(o => o.StartX))
                {
                    while (count < downloadTask.StartX)
                    {
                        sb.Append(' ');
                        count++;
                    }
                    while (count <= downloadTask.EndX)
                    {
                        sb.Append('*');
                        count++;
                    }
                }

                result.AppendLine(sb.ToString());
            }

            return result.ToString();
        }

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>用于循环访问集合的枚举数。</returns>
        public IEnumerator<MapLine> GetEnumerator()
        {
            return m_taskDict.Values.Aggregate(Enumerable.Empty<MapLine>(), (current, list) => current.Concat(list)).GetEnumerator();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// 添加一个任务，会对任务进行连接优化
        /// </summary>
        /// <param name="task"></param>
        internal void AddTask(MapLine task)
        {
            List<MapLine> tasks;
            if (!m_taskDict.ContainsKey(task.Y))
            {
                tasks = new List<MapLine>();
                m_taskDict.Add(task.Y, tasks);
            }
            else
            {
                tasks = m_taskDict[task.Y];
            }

            if (!tasks.Any())
            {
                tasks.Add(task);
                return;
            }

            var tempTask = tasks.FirstOrDefault(o => (o.StartX <= (task.EndX + 1)) && ((o.EndX + 1) >= task.StartX));
            if (tempTask == null)
            {
                tasks.Add(task);
                return;
            }

            tempTask.StartX = Math.Min(task.StartX, tempTask.StartX);
            tempTask.EndX = Math.Max(task.EndX, tempTask.EndX);
            tasks.Remove(tempTask);
            AddTask(tempTask);
        }

        /// <summary>
        /// 缩放任务组到指定的等级
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        internal ZoomTask ScaleTo(int zoom)
        {
            // 计算缩放等级
            var level = Math.Pow(2, zoom - Zoom);
            var newGroup = new ZoomTask(zoom);
            foreach (var tasks in m_taskDict.Values)
            {
                foreach (var task in tasks)
                {
                    newGroup.AddTask(new MapLine
                    {
                        StartX = (long)Math.Floor(task.StartX * level),
                        EndX = (long)Math.Floor(task.EndX * level),
                        Y = (long)Math.Floor(task.Y * level),
                        Zoom = zoom
                    });
                }
            }

            return newGroup;
        }

        #endregion

        #region Private Methods

        /// <summary>返回循环访问集合的枚举数。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}