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

using Wx.Common.BaiduMap.Base.Point;
using Wx.Common.BaiduMap.Base.Segment;
using Wx.Common.BaiduMap.Common;

namespace Wx.Common.BaiduMap.Base.Region
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

            for (int i = 0; i < m_points.Count - 1; i++)
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