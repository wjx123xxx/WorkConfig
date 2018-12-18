// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： CoordinateRegion.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:06:12
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Wx.Common.BaiduMap.Base.Region;

    /// <summary>
    /// 經緯度坐標點围成的区域实体
    /// </summary>
    public class CoordinateRegion
    {
        #region Constructors

        /// <summary>
        /// 以一個區域字符串初始化一個區域
        /// </summary>
        /// <param name="regionData"></param>
        public CoordinateRegion(string regionData)
        {
            Points = new List<Coordinate>();
            var points = regionData.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in points)
            {
                var location = p.Split(',');
                var point = new Coordinate
                {
                    Longitude = Convert.ToDouble(location[0]),
                    Latitude = Convert.ToDouble(location[1])
                };
                Points.Add(point);
            }

            var first = Points.First();
            var last = Points.Last();
            if ((first.Longitude != last.Longitude) || (first.Latitude != last.Latitude))
            {
                Points.Add(new Coordinate
                {
                    Longitude = first.Longitude,
                    Latitude = first.Latitude
                });
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 围成一个区域的经纬度坐标
        /// </summary>
        public List<Coordinate> Points { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 獲取指定層級的墨卡托坐標點區域
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public MercatorRegion GetMercatorRegion(int zoom)
        {
            return new MercatorRegion(Points.Select(o => o.GetMercatorPoint(zoom)));
        }

        /// <summary>
        /// 獲取指定層級的瓦片點區域
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public TileRegion GetTileRegion(int zoom)
        {
            return GetMercatorRegion(zoom).GetTileRegion(zoom);
        }

        #endregion
    }
}