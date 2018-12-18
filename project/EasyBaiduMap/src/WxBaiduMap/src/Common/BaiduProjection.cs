// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： BaiduProjection.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:27:32
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Common
{
    using System;

    using Wx.Common.BaiduMap.Base.Point;
    using Wx.Common.BaiduMap.Entity;

    /// <summary>
    /// 百度坐标转换
    /// </summary>
    public class BaiduProjection
    {
        #region Fields

        private static readonly double[][] _ll2Mc =
        {
            new[]
            {
                -0.0015702102444, 111320.7020616939, 1704480524535203, -10338987376042340, 26112667856603880, -35149669176653700, 26595700718403920,
                -10725012454188240, 1800819912950474, 82.5
            },
            new[]
            {
                0.0008277824516172526, 111320.7020463578, 647795574.6671607, -4082003173.641316, 10774905663.51142, -15171875531.51559,
                12053065338.62167, -5124939663.577472, 913311935.9512032, 67.5
            },
            new[]
            {
                0.00337398766765, 111320.7020202162, 4481351.045890365, -23393751.19931662, 79682215.47186455, -115964993.2797253, 97236711.15602145,
                -43661946.33752821, 8477230.501135234, 52.5
            },
            new[]
            {
                0.00220636496208, 111320.7020209128, 51751.86112841131, 3796837.749470245, 992013.7397791013, -1221952.21711287, 1340652.697009075,
                -620943.6990984312, 144416.9293806241, 37.5
            },
            new[]
            {
                -0.0003441963504368392, 111320.7020576856, 278.2353980772752, 2485758.690035394, 6070.750963243378, 54821.18345352118,
                9540.606633304236, -2710.55326746645, 1405.483844121726, 22.5
            },
            new[]
            {
                -0.0003218135878613132, 111320.7020701615, 0.00369383431289, 823725.6402795718, 0.46104986909093, 2351.343141331292,
                1.58060784298199, 8.77738589078284, 0.37238884252424, 7.45
            }
        };

        private static readonly int[] _llband = { 75, 60, 45, 30, 15, 0 };

        private static readonly double[][] _mc2Ll =
        {
            new[]
            {
                1.410526172116255e-8, 0.00000898305509648872, -1.9939833816331, 200.9824383106796, -187.2403703815547, 91.6087516669843,
                -23.38765649603339, 2.57121317296198, -0.03801003308653, 17337981.2
            },
            new[]
            {
                -7.435856389565537e-9, 0.000008983055097726239, -0.78625201886289, 96.32687599759846, -1.85204757529826, -59.36935905485877,
                47.40033549296737, -16.50741931063887, 2.28786674699375, 10260144.86
            },
            new[]
            {
                -3.030883460898826e-8, 0.00000898305509983578, 0.30071316287616, 59.74293618442277, 7.357984074871, -25.38371002664745,
                13.45380521110908, -3.29883767235584, 0.32710905363475, 6856817.37
            },
            new[]
            {
                -1.981981304930552e-8, 0.000008983055099779535, 0.03278182852591, 40.31678527705744, 0.65659298677277, -4.44255534477492,
                0.85341911805263, 0.12923347998204, -0.04625736007561, 4482777.06
            },
            new[]
            {
                3.09191371068437e-9, 0.000008983055096812155, 0.00006995724062, 23.10934304144901, -0.00023663490511, -0.6321817810242,
                -0.00663494467273, 0.03430082397953, -0.00466043876332, 2555164.4
            },
            new[]
            {
                2.890871144776878e-9, 0.000008983055095805407, -3.068298e-8, 7.47137025468032, -0.00000353937994, -0.02145144861037,
                -0.00001234426596, 0.00010322952773, -0.00000323890364, 826088.5
            }
        };

        private static readonly double[] _mcband = { 12890594.86, 8362377.87, 5591021, 3481989.83, 1678043.12, 0 };

        // 百度经纬度 -> 百度墨卡托 -> 像素坐标
        public static readonly BaiduProjection Instance = new BaiduProjection();

        private static readonly double MaxLatitude = 74; // 最大纬度

        private static readonly double MaxLongitude = 180; // 最大经度

        private static readonly double MinLatitude = -74; // 最小纬度

        private static readonly double MinLongitude = -180; // 最小经度

        public static int TileSizeX = 256;

        public static int TileSizeY = 256;

        #endregion

        #region Public Properties

        /// <summary>
        /// 赤道半径墨卡托投影
        /// </summary>
        public double Axis => 6378137;

        /// <summary>
        /// 地球扁率
        /// </summary>
        public double Flattening => 1.0 / 298.257223563;

        #endregion

        #region Public Methods

        /// <summary>
        /// 经纬度转墨卡托坐标
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns>墨卡托平面坐标XY</returns>
        public MercatorPoint CoordinateToMercator(Coordinate coordinate)
        {
            double[] mc = null;

            var lat = Math.Min(Math.Max(coordinate.Latitude, MinLatitude), MaxLatitude);
            var lng = Math.Min(Math.Max(coordinate.Longitude, MinLongitude), MaxLongitude);

            for (var i = 0; i < _llband.Length; i++)
            {
                if (lat > _llband[i])
                {
                    mc = _ll2Mc[i];
                    break;
                }
            }

            if (mc == null)
            {
                for (var i = _llband.Length - 1; i >= 0; i--)
                {
                    if (lat <= -_llband[i])
                    {
                        mc = _ll2Mc[i];
                        break;
                    }
                }
            }

            var location = Convertor(lng, lat, mc);
            return new MercatorPoint
            {
                X = location[0],
                Y = location[1],
                Zoom = 18
            };
        }
        /// <summary>
        /// 墨卡托坐標轉經緯度坐標
        /// </summary>
        /// <param name="point"></param>
        /// <returns>经度, 纬度</returns>
        public Coordinate MercatorToCoordinate(MercatorPoint point)
        {
            double[] mc = null;
            var abs = Math.Abs(point.Y);
            for (var i = 0; i < _mcband.Length; i++)
            {
                if (abs >= _mcband[i])
                {
                    mc = _mc2Ll[i];
                    break;
                }
            }

            var location = Convertor(point.X, point.Y, mc);
            return new Coordinate
            {
                Longitude = location[0],
                Latitude = location[1]
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="x">墨卡托X 或者 经度</param>
        /// <param name="y">墨卡托Y 或者 纬度</param>
        /// <param name="mc">转换对照表</param>
        /// <returns>经纬度 或 墨卡托XY</returns>
        private double[] Convertor(double x, double y, double[] mc)
        {
            var nx = mc[0] + (mc[1] * Math.Abs(x));
            var c = Math.Abs(y) / mc[9];
            var ny = mc[2] + (mc[3] * c) + (mc[4] * c * c) + (mc[5] * c * c * c) + (mc[6] * c * c * c * c) + (mc[7] * c * c * c * c * c)
                     + (mc[8] * c * c * c * c * c * c);
            if (x < 0)
            {
                nx *= -1;
            }
            if (y < 0)
            {
                ny *= -1;
            }
            return new[] { nx, ny };
        }

        #endregion
    }
}