// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： PositionConverter.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-19 10:21:57
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wx.Utility.MapCommon
{
    /// <summary>
    /// 坐标类型转换器
    /// </summary>
    public class PositionConverter
    {
        #region Constructors

        static PositionConverter()
        {
            // 百度坐标转换成国测局坐标
            _transferManager.Register(PositionType.BD09LL, PositionType.GCJ02LL, src =>
            {
                double x = src.Longitude - 0.0065;
                double y = src.Latitude - 0.006;
                double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * X_PI);
                double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * X_PI);

                return new GPSCoordinate
                {
                    Longitude = z * Math.Cos(theta),
                    Latitude = z * Math.Sin(theta)
                };
            });

            // 国测局坐标转换成百度坐标
            _transferManager.Register(PositionType.GCJ02LL, PositionType.BD09LL, src =>
            {
                double lng = src.Longitude;
                double lat = src.Latitude;
                double z = Math.Sqrt(lng * lng + lat * lat) + 0.00002 * Math.Sin(lat * X_PI);
                double theta = Math.Atan2(lat, lng) + 0.000003 * Math.Cos(lng * X_PI);
                double bdLng = z * Math.Cos(theta) + 0.0065;
                double bdLat = z * Math.Sin(theta) + 0.006;
                return new GPSCoordinate
                {
                    Longitude = bdLng,
                    Latitude = bdLat
                };
            });

            // 国测局坐标转换成标准GPS坐标
            _transferManager.Register(PositionType.GCJ02LL, PositionType.WGS84LL, src =>
            {
                double lng = src.Longitude;
                double lat = src.Latitude;
                if (OutOfChina(lng, lat))
                {
                    return src;
                }
                double dlat = TransformLatitude(lng - 105.0, lat - 35.0);
                double dlng = TransformLongitude(lng - 105.0, lat - 35.0);
                double radlat = lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (A / sqrtmagic * Math.Cos(radlat) * PI);
                double mglat = lat + dlat;
                double mglng = lng + dlng;
                return new GPSCoordinate
                {
                    Longitude = mglng,
                    Latitude = mglat
                };
            });

            // GPS标准坐标转换成国测局坐标
            _transferManager.Register(PositionType.WGS84LL, PositionType.GCJ02LL, src =>
            {
                double lng = src.Longitude;
                double lat = src.Latitude;
                if (OutOfChina(lng, lat))
                {
                    return src;
                }
                double dlat = TransformLatitude(lng - 105.0, lat - 35.0);
                double dlng = TransformLongitude(lng - 105.0, lat - 35.0);
                double radlat = lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (A / sqrtmagic * Math.Cos(radlat) * PI);
                double mglat = lat + dlat;
                double mglng = lng + dlng;
                return new GPSCoordinate
                {
                    Longitude = mglng,
                    Latitude = mglat
                };
            });
        }

        #endregion

        #region Fields

        private static readonly TypeTransferManager _transferManager = new TypeTransferManager();

        #endregion

        #region Static Members

        #region Static Constant

        private const double A = 6378245.0;

        private const double EE = 0.00669342162296594323;

        private const double PI = 3.1415926535897932384626;

        private const double X_PI = 3.14159265358979324 * 3000.0 / 180.0;

        #endregion

        #region Static Method

        /// <summary>
        /// 坐标系转换
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        public static void Convert(PositionType from, PositionType to, ref double lng, ref double lat)
        {
            GPSCoordinate coordinate = new GPSCoordinate
            {
                Longitude = lng,
                Latitude = lat
            };

            coordinate = _transferManager.Transfer(from, to, coordinate);
            lng = coordinate.Longitude;
            lat = coordinate.Latitude;
        }

        private static bool OutOfChina(double lng, double lat)
        {
            return (lng < 72.004 || lng > 137.8347) || ((lat < 0.8293 || lat > 55.8271));
        }

        private static double TransformLatitude(double lng, double lat)
        {
            double ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * PI) + 40.0 * Math.Sin(lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * PI) + 320 * Math.Sin(lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double TransformLongitude(double lng, double lat)
        {
            double ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lng * PI) + 40.0 * Math.Sin(lng / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lng / 12.0 * PI) + 300.0 * Math.Sin(lng / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 坐标
    /// </summary>
    internal class GPSCoordinate
    {
        #region Public Properties

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        #endregion
    }

    /// <summary>
    /// 类型迁移管理器
    /// </summary>
    internal class TypeTransferManager
    {
        #region Fields

        private IList<TypeTransfer> m_transfers = new List<TypeTransfer>();

        #endregion

        #region Internal Methods

        /// <exception cref="ArgumentException">Dupliated Converter.</exception>
        internal void Register(PositionType from, PositionType to, Func<GPSCoordinate, GPSCoordinate> converter)
        {
            TypeTransfer transfer = new TypeTransfer(from, to);
            transfer.AddConverter(converter);
            if (m_transfers.Contains(transfer))
            {
                throw new ArgumentException(string.Format("From:{0} To:{1} Is Already Exists!", from, to));
            }
            m_transfers.Add(transfer);
        }

        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        internal GPSCoordinate Transfer(PositionType from, PositionType to, GPSCoordinate coordinate)
        {
            TypeTransfer transfer = SeekTransfer(from, to);
            return transfer.Convert(coordinate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 递归暴力搜寻算法
        /// </summary>
        /// <param name="transferStack"></param>
        /// <param name="current"></param>
        /// <param name="target"></param>
        private bool SeekTarget(Stack<TypeTransfer> transferStack, PositionType current, PositionType target)
        {
            TypeTransfer transfer = m_transfers.FirstOrDefault(o => o.From == current && o.To == target);
            if (transfer != null)
            {
                transferStack.Push(transfer);
                return true;
            }

            foreach (TypeTransfer t in m_transfers.Where(o => o.From == current))
            {
                // 防止产生算法回环
                if (transferStack.FirstOrDefault(o => o.From == t.To) != null)
                {
                    continue;
                }

                // 递归搜索
                transferStack.Push(t);
                if (SeekTarget(transferStack, t.To, target))
                {
                    return true;
                }
                transferStack.Pop();
            }
            return false;
        }

        private TypeTransfer SeekTransfer(PositionType from, PositionType to)
        {
            TypeTransfer transfer = m_transfers.FirstOrDefault(o => o.From == from && o.To == to);
            if (transfer != null)
            {
                return transfer;
            }

            transfer = new TypeTransfer(from, to);
            Stack<TypeTransfer> transferStack = new Stack<TypeTransfer>();
            if (SeekTarget(transferStack, from, to))
            {
                foreach (var t in transferStack)
                {
                    transfer.AddTransfer(t);
                }
            }
            m_transfers.Add(transfer);
            return transfer;
        }

        #endregion

        private class TypeTransfer
        {
            #region Constructors

            /// <summary>
            /// 构造函数
            /// </summary>
            public TypeTransfer(PositionType from, PositionType to)
            {
                From = from;
                To = to;
            }

            #endregion

            #region Fields

            private readonly IList<Func<GPSCoordinate, GPSCoordinate>> m_converters = new List<Func<GPSCoordinate, GPSCoordinate>>();

            #endregion

            #region Internal Properties

            internal PositionType From { get; private set; }

            internal PositionType To { get; private set; }

            #endregion

            #region Private Properties

            private IList<Func<GPSCoordinate, GPSCoordinate>> Converters
            {
                get { return m_converters; }
            }

            #endregion

            #region Internal Methods

            /// <exception cref="Exception">A delegate callback throws an exception.</exception>
            /// <exception cref="NotImplementedException">No Converter Implement.</exception>
            internal GPSCoordinate Convert(GPSCoordinate coordinate)
            {
                if (!m_converters.Any())
                {
                    throw new NotImplementedException(string.Format("No Converter from {0} to {1}", From, To));
                }
                return m_converters.Aggregate(coordinate, (current, converter) => converter(current));
            }

            #endregion

            #region Private Methods

            private bool Equals(TypeTransfer other)
            {
                return From == other.From && To == other.To;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
            /// </returns>
            /// <param name="obj">The object to compare with the current object. </param>
            public override bool Equals(object obj)
            {
                if (obj is TypeTransfer)
                {
                    return Equals((TypeTransfer)obj);
                }
                return ReferenceEquals(this, obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object"/>.
            /// </returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int)From * 397) ^ (int)To;
                }
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>
            /// A string that represents the current object.
            /// </returns>
            public override string ToString()
            {
                return string.Format("(From {0} To {1})", From, To);
            }

            public void AddConverter(Func<GPSCoordinate, GPSCoordinate> converter)
            {
                m_converters.Add(converter);
            }

            public void AddTransfer(TypeTransfer typeTransfer)
            {
                foreach (Func<GPSCoordinate, GPSCoordinate> c in typeTransfer.Converters)
                {
                    AddConverter(c);
                }
            }

            #endregion
        }
    }
}