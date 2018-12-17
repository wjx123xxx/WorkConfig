// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MercatorPoint.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 17:00:04
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;

namespace Wx.Common.BaiduMap.Base.Point
{
    /// <summary>
    /// 墨卡托坐标
    /// </summary>
    public class MercatorPoint : MapPoint
    {
        #region Public Methods

        /// <summary>
        /// 轉換到指定層級的墨卡托坐標
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public MercatorPoint GetMercatorPoint(int zoom)
        {
            var level = Math.Pow(2, zoom - Zoom);
            return new MercatorPoint
            {
                X = X * level,
                Y = Y * level,
                Zoom = zoom
            };
        }

        /// <summary>
        /// 獲取該坐標對應的墨卡托坐標
        /// </summary>
        /// <returns></returns>
        public TilePoint GetTilePoint()
        {
            return new TilePoint
            {
                X = (long)(X / 256),
                Y = (long)(Y / 256),
                Zoom = Zoom
            };
        }

        /// <summary>
        /// 獲取該坐標指定層級的墨卡托坐標
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public TilePoint GetTilePoint(int zoom)
        {
            return GetMercatorPoint(zoom).GetTilePoint();
        }

        #endregion
    }
}