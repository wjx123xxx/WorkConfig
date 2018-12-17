// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MercatorSegment.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-23 09:44:56
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;

using Wx.Common.BaiduMap.Base.Point;

namespace Wx.Common.BaiduMap.Base.Segment
{
    /// <summary>
    /// 墨卡托线段
    /// </summary>
    public class MercatorSegment
    {
        #region Fields

        private readonly MercatorPoint m_point1;

        private readonly MercatorPoint m_point2;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数，以两点初始化一个线段
        /// </summary>
        public MercatorSegment(MercatorPoint point1, MercatorPoint point2)
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

        #endregion

        #region Public Methods

        /// <summary>
        /// 与一条水平线相交
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public MercatorPoint IntersectY(double y)
        {
            if ((y > MaxY) || (y < MinY))
            {
                throw new InvalidOperationException();
            }

            var x = Math.Floor((C + (B * y)) / -A);
            return new MercatorPoint()
            {
                X = x,
                Y = y
            };
        }

        #endregion
    }
}