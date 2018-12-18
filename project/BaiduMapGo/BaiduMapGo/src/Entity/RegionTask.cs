// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： RegionTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 19:06:27
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Wx.App.BaiduMapGo.Base;
using Wx.App.BaiduMapGo.Interface;
using Wx.App.BaiduMapGo.Struct;

namespace Wx.App.BaiduMapGo.Entity
{
    /// <summary>
    /// 多边形区域下载任务
    /// </summary>
    public class RegionTask : IMapTask
    {
        #region Fields

        /// <summary>
        /// 原始边界数据
        /// </summary>
        private readonly List<MercatorPoint> m_points;

        /// <summary>
        /// 任务组列表
        /// </summary>
        private readonly List<MapZoom> m_zoomTasks = new List<MapZoom>();

        private IList<MercatorRegion> m_mercatorRegions = new List<MercatorRegion>();

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        internal RegionTask(int minZoom, int maxZoom)
        {
            //m_points = points.ToList();
            MinZoom = minZoom;
            MaxZoom = maxZoom;
            //CalculateQuest();
            //TaskCount = m_zoomTasks.Sum(o => o.TaskCount);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 最大Zoom
        /// </summary>
        public int MaxZoom { get; }

        /// <summary>
        /// 最小Zoom
        /// </summary>
        public int MinZoom { get; }

        /// <summary>
        /// 原始边界数据
        /// </summary>
        public List<MercatorPoint> Points => m_points;

        /// <summary>
        /// 任务数量
        /// </summary>
        public long TaskCount { get; }

        #endregion

        #region Public Methods

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>用于循环访问集合的枚举数。</returns>
        public IEnumerator<MapZoom> GetEnumerator()
        {
            return m_zoomTasks.GetEnumerator();
        }

        public void AddRegion(IEnumerable<MercatorPoint> region)
        {
            m_mercatorRegions.Add(new MercatorRegion(region));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 计算下载任务列表
        /// </summary>
        public void CalculateQuest()
        {
            // 先按多边形分组，一组点可能组成多个多边形
            var points = m_points;
            //while (points.Any())
            //{
            //    var index = points.ToList().LastIndexOf(points.First());
            //    if (index == 0)
            //    {
            //        if (points.Any())
            //        {
            //            File.AppendAllLines("points.txt", points.Select(o=>$"{o.X},{o.Y}"));
            //        }
            //        break;
            //    }

            //    var region = new MercatorRegion(points.Take(index + 1));
            //    m_mercatorRegions.Add(region);
            //    points = points.Skip(index + 1).ToList();
            //}

            if (!m_mercatorRegions.Any())
            {
                throw new ArgumentException("无法构建成多边形，请检查点是否闭合");
            }

            // 只计算最大层，其余层通过缩小计算得出
            var tileRegionList = m_mercatorRegions.Select(o => o.GetTileRegion(MaxZoom));
            var segments = tileRegionList.Aggregate(Enumerable.Empty<TileSegment>(), (current, region) => current.Concat(region.GetTileSegments())).ToList();
            var minY = (long)Math.Floor(segments.Min(o => o.MinY));
            var maxY = (long)Math.Floor(segments.Max(o => o.MaxY));

            // 每个Y值计算一次交点
            var taskGroup = new MapZoom(MaxZoom);
            for (var i = minY; i <= maxY; i++)
            {
                var y = i;
                var matchSegments = segments.Where(o => (o.MaxY >= y) && (o.MinY <= y));
                var intersectList = new List<TilePoint>();
                foreach (var s in matchSegments)
                {
                    intersectList.Add(s.IntersectY(y));
                }

                // 分解任务
                intersectList = intersectList.OrderBy(o => o.X).ToList();
                for (var index = 0; index < intersectList.Count; index += 2)
                {
                    var x1 = (long)Math.Floor(intersectList[index].X);
                    var x2 = (long)Math.Floor(intersectList[index + 1].X);

                    var task = new MapLine
                    {
                        StartX = x1,
                        EndX = x2,
                        Y = y,
                        Zoom = MaxZoom
                    };
                    taskGroup.AddTask(task);
                }
            }

            // 将线段两点加入任务组
            foreach (var s in segments)
            {
                taskGroup.AddTask(GetTaskFromTilePoint(s.Point1, MaxZoom));
                taskGroup.AddTask(GetTaskFromTilePoint(s.Point2, MaxZoom));
            }

            // 将线段所有x轴点加入任务组
            var minX = (long)Math.Floor(segments.Min(o => o.MinX));
            var maxX = (long)Math.Floor(segments.Max(o => o.MaxX));
            for (var i = minX; i <= maxX; i++)
            {
                var x = i;
                var matchSegments = segments.Where(o => (o.MaxX >= x) && (o.MinX <= x));
                foreach (var s in matchSegments)
                {
                    taskGroup.AddTask(GetTaskFromTilePoint(s.IntersectX(x), MaxZoom));
                }
            }

            // 整合任务组
            m_zoomTasks.Add(taskGroup);
            var tg = taskGroup;
            for (var index = MaxZoom - 1; index >= MinZoom; index--)
            {
                tg = tg.ScaleTo(index);
                m_zoomTasks.Add(tg);
            }
        }

        /// <summary>返回循环访问集合的枚举数。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 生成一个点任务
        /// </summary>
        /// <param name="point"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        private MapLine GetTaskFromTilePoint(TilePoint point, int zoom)
        {
            var x = (long)Math.Floor(point.X);
            var y = (long)Math.Floor(point.Y);
            return new MapLine
            {
                StartX = x,
                EndX = x,
                Y = y,
                Zoom = zoom
            };
        }

        #endregion
    }
}