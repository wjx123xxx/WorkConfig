// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： TileSegment.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-22 09:51:15
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Base.Segment
{
    using System;
    using System.Collections.Generic;

    using Wx.Common.BaiduMap.Base.Point;

    /// <summary>
    /// 瓦片点组成的线段
    /// </summary>
    public class TileSegment
    {
        #region Constructors

        /// <summary>
        /// 构造函数，以两点初始化一个线段
        /// </summary>
        public TileSegment(TilePoint point1, TilePoint point2)
        {
            Point1 = point1;
            Point2 = point2;

            MaxY = Math.Max(Point1.Y, Point2.Y);
            MinY = Math.Min(Point1.Y, Point2.Y);
            MaxX = Math.Max(Point1.X, Point2.X);
            MinX = Math.Min(Point1.X, Point2.X);

            A = Point1.Y - Point2.Y;
            B = Point2.X - Point1.X;
            C = Point1.X * (Point2.Y - Point1.Y) + Point1.Y * (Point1.X - Point2.X);
        }

        #endregion

        #region Public Properties

        public double A { get; }

        public double B { get; }

        public double C { get; }

        public double MaxX { get; }

        public double MaxY { get; }

        public double MinX { get; }

        public double MinY { get; }

        public TilePoint Point1 { get; }

        public TilePoint Point2 { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 獲取所有與X軸相交的點
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TilePoint> GetTilePointIntersectX()
        {
            for (var index = Math.Ceiling(MinX); index <= MaxX; index++)
            {
                yield return IntersectX(index);
            }
        }

        /// <summary>
        /// 獲取所有與Y軸相交的點
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TilePoint> GetTilePointIntersectY()
        {
            for (var index = Math.Ceiling(MinY); index <= MaxY; index++)
            {
                yield return IntersectY(index);
            }
        }

        /// <summary>
        /// 与一条垂直线相交
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public TilePoint IntersectX(double x)
        {
            return new TilePoint
            {
                X = x,
                Y = (C + A * x) / -B,
                Zoom = Point1.Zoom
            };
        }

        /// <summary>
        /// 与一条水平线相交
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public TilePoint IntersectY(double y)
        {
            return new TilePoint
            {
                X = (C + B * y) / -A,
                Y = y,
                Zoom = Point1.Zoom
            };
        }

        #endregion
    }
}