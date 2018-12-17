// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MercatorRegion.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 19:13:13
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System.Collections.Generic;
using System.Linq;

using Wx.App.BaiduMapGo.Common;

namespace Wx.App.BaiduMapGo.Struct
{
    /// <summary>
    /// 墨卡托多边形区域
    /// </summary>
    public class MercatorRegion
    {
        #region Fields

        private readonly List<MercatorPoint> m_points;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public MercatorRegion(IEnumerable<MercatorPoint> points)
        {
            m_points = points.ToList();
            var first = m_points.First();
            var last = m_points.Last();
            if ((first.X != last.X) || (first.Y != last.Y))
            {
                m_points.Add(
                    new MercatorPoint
                    {
                        X = first.X,
                        Y = first.Y
                    });
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取组成多边形的所有线段
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MercatorSegment> GetMercatorSegments()
        {
            if (m_points.Count == 1)
            {
                yield return new MercatorSegment(m_points[0], m_points[0]);
            }

            for (var i = 0; i < m_points.Count - 1; i++)
            {
                yield return new MercatorSegment(m_points[i], m_points[i + 1]);
            }
        }

        /// <summary>
        /// 获取指定级别的瓦片区域
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public TileRegion GetTileRegion(int zoom)
        {
            return new TileRegion(BaiduMapProvider.Instance.GetTilePointsFromMercatorPoints(m_points, zoom));
        }

        #endregion
    }
}