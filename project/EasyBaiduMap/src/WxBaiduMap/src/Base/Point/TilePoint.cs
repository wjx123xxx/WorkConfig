// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： TilePoint.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 17:01:03
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Base.Point
{
    /// <summary>
    /// 瓦片坐标
    /// </summary>
    public class TilePoint : MapPoint
    {
        #region Fields

        /// <summary>
        /// 下载地址格式化字符串
        /// </summary>
        private static readonly string UrlFormat = "http://online{0}.map.bdimg.com/onlinelabel/?qt=tile&x={1}&y={2}&z={3}&v=014&styles=pl";

        #endregion

        #region Public Properties

        /// <summary>
        /// 瓦片地址
        /// </summary>
        public string Address => string.Format(UrlFormat, (int)(X + Y) % 10, (int)X, (int)Y, Zoom);

        /// <summary>
        /// 該瓦片下面的瓦片
        /// </summary>
        public TilePoint Bottom => new TilePoint
        {
            X = X,
            Y = Y - 1,
            Zoom = Zoom
        };

        /// <summary>
        /// 該瓦片左邊的瓦片
        /// </summary>
        public TilePoint Left => new TilePoint
        {
            X = X - 1,
            Y = Y,
            Zoom = Zoom
        };

        /// <summary>
        /// 該瓦片右邊的瓦片
        /// </summary>
        public TilePoint Right => new TilePoint
        {
            X = X + 1,
            Y = Y,
            Zoom = Zoom
        };

        /// <summary>
        /// 該瓦片上邊的瓦片
        /// </summary>
        public TilePoint Top => new TilePoint
        {
            X = X,
            Y = Y + 1,
            Zoom = Zoom
        };

        #endregion
    }
}