// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： District.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:05:49
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 地图行政区实体，一个行政区可以有多个多边形区域
    /// </summary>
    public class District
    {
        #region Fields

        /// <summary>
        /// 行政区内所有的多边形区域
        /// </summary>
        private List<CoordinateRegion> m_regions = new List<CoordinateRegion>();

        private string regionData1;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionName">行政區名稱</param>
        /// <param name="regionData">行政區邊界元數據</param>
        public District(string regionName, string regionData)
        {
            RegionData = regionData;
            RegionName = regionName;

            var regions = regionData.Split(new[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var region in regions)
            {
                m_regions.Add(new CoordinateRegion(region));
            }

            // 计算左上角与右下角坐标
            LeftTop.Longitude = m_regions.Min(o => o.Points.Min(p => p.Longitude));
            LeftTop.Latitude = m_regions.Max(o => o.Points.Max(p => p.Latitude));
            RightBottom.Longitude = m_regions.Max(o => o.Points.Max(p => p.Longitude));
            RightBottom.Latitude = m_regions.Min(o => o.Points.Min(p => p.Latitude));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 区域左上角坐标
        /// </summary>
        public Coordinate LeftTop { get; private set; } = new Coordinate();

        /// <summary>
        /// 原始的初始化数据
        /// </summary>
        public string RegionData { get; }

        /// <summary>
        /// 行政區名稱
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 行政区内所有的多边形区域
        /// </summary>
        public List<CoordinateRegion> Regions => m_regions.ToList();

        /// <summary>
        /// 区域右下角坐标
        /// </summary>
        public Coordinate RightBottom { get; private set; } = new Coordinate();

        #endregion

        #region Public Methods

        /// <summary>
        /// 根據行政區獲取對應的下載任務實體
        /// </summary>
        /// <returns></returns>
        public DistrictTask GetTask()
        {
            return new DistrictTask(RegionName, m_regions);
        }

        #endregion
    }
}