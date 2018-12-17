// *******************************************************************
// * ��Ȩ���У� ���Z��
// * �ļ����ƣ� MercatorPoint.cs
// * �������ߣ� ���Z��
// * �������ڣ� 2017-03-21 17:00:04
// * �ļ��汾�� 1.0.0.0
// * �޸�ʱ�䣺             �޸��ˣ�                �޸����ݣ�
// *******************************************************************

using System;

namespace Wx.Common.BaiduMap.Base.Point
{
    /// <summary>
    /// ī��������
    /// </summary>
    public class MercatorPoint : MapPoint
    {
        #region Public Methods

        /// <summary>
        /// �D�Q��ָ���Ӽ���ī��������
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
        /// �@ȡԓ���ˌ�����ī��������
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
        /// �@ȡԓ����ָ���Ӽ���ī��������
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