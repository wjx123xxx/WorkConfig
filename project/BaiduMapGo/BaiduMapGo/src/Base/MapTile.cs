// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapTile.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-28 10:13:26
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.BaiduMapGo.Base
{
    /// <summary>
    /// 地图瓦片
    /// </summary>
    public class MapTile
    {
        #region Fields

        /// <summary>
        /// 下载地址格式化字符串
        /// </summary>
        private static readonly string UrlFormat = "http://online{0}.map.bdimg.com/onlinelabel/?qt=tile&x={1}&y={2}&z={3}&v=014&styles=pl";

        #endregion

        #region Public Properties

        public long X { get; internal set; }

        public long Y { get; internal set; }

        public int Zoom { get; internal set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取该瓦片下载地址
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetTileAddress(int num = -1)
        {
            long index = num;
            if ((num < 0) || (num > 9))
            {
                index = (X + Y) % 10;
            }

            return string.Format(UrlFormat, index, X, Y, Zoom);
        }

        #endregion
    }
}