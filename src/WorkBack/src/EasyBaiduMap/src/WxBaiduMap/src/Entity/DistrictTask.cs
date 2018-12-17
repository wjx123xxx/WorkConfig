// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： DistrictTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-02 14:56:21
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Wx.Common.BaiduMap.Base.Point;
    using Wx.Common.BaiduMap.Base.Segment;

    /// <summary>
    /// 行政區地圖下載任務
    /// </summary>
    public class DistrictTask
    {
        #region Fields

        private readonly IEnumerable<CoordinateRegion> m_regions;

        private readonly List<int> m_supportZooms;

        private readonly List<ZoomTask> m_zoomTasks;

        #endregion

        #region Constructors

        /// <summary>
        /// 使用行政區區域集合初始化一個下載任務
        /// </summary>
        /// <param name="districtName"></param>
        /// <param name="regions"></param>
        public DistrictTask(string districtName, IEnumerable<CoordinateRegion> regions)
        {
            DistrictName = districtName;
            m_regions = regions;
            m_supportZooms = new List<int>();
            m_zoomTasks = new List<ZoomTask>();

            for (var index = 1; index < 20; index++)
            {
                m_supportZooms.Add(index);
            }

            Calculate();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 行政区名称
        /// </summary>
        public string DistrictName { get; }

        /// <summary>
        /// 可支持的層級
        /// </summary>
        public IEnumerable<int> SupportZooms => m_supportZooms;

        #endregion

        #region Public Methods

        /// <summary>
        /// 獲取指定層級之間的下載瓦片數量
        /// </summary>
        /// <param name="minZoom"></param>
        /// <param name="maxZoom"></param>
        /// <returns></returns>
        public long GetTaskCount(int minZoom, int maxZoom)
        {
            return m_zoomTasks.Where(o => (o.Zoom >= minZoom) && (o.Zoom <= maxZoom)).Sum(task => task.TaskCount);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 计算下载任务列表
        /// </summary>
        private void Calculate()
        {
            var minZoom = m_supportZooms.Min();
            var maxZoom = m_supportZooms.Max();

            // 只计算最大层，其余层通过缩小计算得出
            var tileRegionList = m_regions.Select(o => o.GetTileRegion(maxZoom));
            var segments =
                tileRegionList.Aggregate(Enumerable.Empty<TileSegment>(), (current, region) => current.Concat(region.GetTileSegments())).ToList();

            // 每个Y值计算一次交点
            var zoomTask = new ZoomTask(maxZoom);
            var pointDict = new ConcurrentDictionary<double, ConcurrentBag<TilePoint>>();
            foreach (var segment in segments)
            {
                foreach (var tp in segment.GetTilePointIntersectY())
                {
                    if (!pointDict.ContainsKey(tp.Y))
                    {
                        pointDict.TryAdd(tp.Y, new ConcurrentBag<TilePoint>());
                    }
                    pointDict[tp.Y].Add(tp);
                }
            }

            foreach (var group in pointDict)
            {
                var points = group.Value.OrderBy(o => o.X).ToList();
                for (var index = 0; index < points.Count; index += 2)
                {
                    var x1 = (long)Math.Floor(points[index].X);
                    var x2 = (long)Math.Floor(points[index + 1].X);

                    var task = new MapLine
                    {
                        StartX = x1,
                        EndX = x2,
                        Y = (long)group.Key,
                        Zoom = maxZoom
                    };
                    zoomTask.AddTask(task);
                }
            }

            // 将线段两点加入任务组
            foreach (var s in segments)
            {
                zoomTask.AddTask(GetTaskFromTilePoint(s.Point1, maxZoom));
                zoomTask.AddTask(GetTaskFromTilePoint(s.Point2, maxZoom));
            }

            // 将线段所有x轴点加入任务组
            var minX = (long)Math.Floor(segments.Min(o => o.MinX));
            var maxX = (long)Math.Floor(segments.Max(o => o.MaxX));
            foreach (var segment in segments)
            {
                foreach (var tp in segment.GetTilePointIntersectX())
                {
                    zoomTask.AddTask(GetTaskFromTilePoint(tp, maxZoom));
                }
            }

            // 整合任务组
            m_zoomTasks.Add(zoomTask);
            var tg = zoomTask;
            for (var index = maxZoom - 1; index >= minZoom; index--)
            {
                tg = tg.ScaleTo(index);
                m_zoomTasks.Add(tg);
            }
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