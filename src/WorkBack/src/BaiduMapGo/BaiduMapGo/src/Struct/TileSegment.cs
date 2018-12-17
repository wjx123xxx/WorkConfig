// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： TileSegment.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-22 09:51:15
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.BaiduMapGo.Struct
{
    using System;

    /// <summary>
    /// 瓦片点组成的线段
    /// </summary>
    public class TileSegment
    {
        #region Fields

        private readonly TilePoint m_point1;

        private readonly TilePoint m_point2;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数，以两点初始化一个线段
        /// </summary>
        public TileSegment(TilePoint point1, TilePoint point2)
        {
            m_point1 = point1;
            m_point2 = point2;

            MaxY = Math.Max(m_point1.Y, m_point2.Y);
            MinY = Math.Min(m_point1.Y, m_point2.Y);
            MaxX = Math.Max(m_point1.X, m_point2.X);
            MinX = Math.Min(m_point1.X, m_point2.X);

            A = m_point1.Y - m_point2.Y;
            B = m_point2.X - m_point1.X;
            C = (m_point1.X * (m_point2.Y - m_point1.Y)) + (m_point1.Y * (m_point1.X - m_point2.X));
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

        public TilePoint Point1 => m_point1;

        public TilePoint Point2 => m_point2;

        #endregion

        #region Public Methods

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
                Y = (C + (A * x)) / -B
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
                X = (C + (B * y)) / -A,
                Y = y
            };
        }

        #endregion
    }
}