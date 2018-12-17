// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： TileRegion.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 19:12:11
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.BaiduMapGo.Struct
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 瓦片多边形区域
    /// </summary>
    public class TileRegion
    {
        #region Fields

        private readonly List<TilePoint> m_points;

        #endregion

        #region Constructors

        public TileRegion(IEnumerable<TilePoint> points)
        {
            m_points = points.ToList();
        }

        #endregion

        #region Public Properties

        public List<TilePoint> Points => m_points;

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取瓦片线段
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TileSegment> GetTileSegments()
        {
            if (m_points.Count == 1)
            {
                yield return new TileSegment(m_points[0], m_points[0]);
            }

            for (var i = 0; i < m_points.Count - 1; i++)
            {
                yield return new TileSegment(m_points[i], m_points[i + 1]);
            }
        }

        #endregion
    }
}