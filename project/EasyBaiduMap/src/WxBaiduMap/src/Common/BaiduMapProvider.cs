// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： BaiduMapProvider.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-20 15:18:41
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using Wx.Common.BaiduMap.Base;
using Wx.Common.BaiduMap.Base.Point;

namespace Wx.Common.BaiduMap.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 百度地图
    /// </summary>
    public class BaiduMapProvider
    {
        #region Fields

        private static readonly object s_instanceLocker = new object();

        private static BaiduMapProvider s_instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private BaiduMapProvider()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static BaiduMapProvider Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_instanceLocker)
                    {
                        if (s_instance == null)
                        {
                            s_instance = new BaiduMapProvider();
                        }
                    }
                }
                return s_instance;
            }
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// 将墨卡托坐标转换为瓦片坐标
        /// </summary>
        /// <param name="points"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public IEnumerable<TilePoint> GetTilePointsFromMercatorPoints(IEnumerable<MercatorPoint> points, int zoom)
        {
            var level = Math.Pow(2, zoom - 18);
            return points.Select(
                mercatorPoint => new TilePoint
                {
                    X = (mercatorPoint.X * level) / 256,
                    Y = (mercatorPoint.Y * level) / 256
                }).ToList();
        }

        #endregion
    }
}