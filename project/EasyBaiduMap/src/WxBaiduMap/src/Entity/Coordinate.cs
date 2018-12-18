// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： Coordinate.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:53:19
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using Wx.Common.BaiduMap.Base.Point;
    using Wx.Common.BaiduMap.Common;

    /// <summary>
    /// 经纬度坐标
    /// </summary>
    public class Coordinate
    {
        #region Public Properties

        /// <summary>
        /// 緯度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 經度
        /// </summary>
        public double Longitude { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 獲取18層級的墨卡托點
        /// </summary>
        /// <returns></returns>
        public MercatorPoint GetMercatorPoint()
        {
            return BaiduProjection.Instance.CoordinateToMercator(this);
        }

        /// <summary>
        /// 获取指定層級的墨卡托點
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public MercatorPoint GetMercatorPoint(int zoom)
        {
            return GetMercatorPoint().GetMercatorPoint(zoom);
        }

        #endregion
    }
}