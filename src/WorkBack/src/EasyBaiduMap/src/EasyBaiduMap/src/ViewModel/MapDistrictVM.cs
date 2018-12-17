// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapDistrictVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-31 23:20:27
// *******************************************************************

namespace Wx.App.EasyBaiduMap.ViewModel
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    using Wx.App.EasyBaiduMap.Common;
    using Wx.App.EasyBaiduMap.Entity;
    using Wx.Common.BaiduMap.Entity;

    /// <summary>
    /// 地圖行政區控件VM
    /// </summary>
    public class MapDistrictVM : NotifyEntity
    {
        #region Fields

        private District m_district;

        private ObservableCollection<MapLineWrapper> m_regionLines;

        #endregion

        #region Public Properties

        /// <summary>
        /// 邊界線集合
        /// </summary>
        public ObservableCollection<MapLineWrapper> RegionLines
        {
            get
            {
                return m_regionLines;
            }
            set
            {
                m_regionLines = value;
                OnPropertyChanged(nameof(RegionLines));
            }
        }

        /// <summary>
        /// 瓦片集合
        /// </summary>
        public ObservableCollection<BitmapImage> Tiles { get; set; } = new ObservableCollection<BitmapImage>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 設置行政區
        /// </summary>
        /// <param name="district"></param>
        public void SetRegionData(District district)
        {
            // 分割區域
            m_district = district;
            var lt = m_district.LeftTop.GetMercatorPoint();
            var rb = m_district.RightBottom.GetMercatorPoint();

            var zoom = lt.Zoom;
            while (((rb.GetTilePoint().X - lt.GetTilePoint().X) > 2) || ((lt.GetTilePoint().Y - rb.GetTilePoint().Y) > 2))
            {
                zoom--;
                lt = lt.GetMercatorPoint(zoom);
                rb = rb.GetMercatorPoint(zoom);
            }

            // 加载瓦片
            Tiles.Clear();
            var tilePoint = lt.GetTilePoint();
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Right.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Right.Right.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Right.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Right.Right.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Bottom.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Bottom.Right.Address)));
            Tiles.Add(new BitmapImage(new Uri(tilePoint.Bottom.Bottom.Right.Right.Address)));

            // 加载区域線段
            var wrappers = new ConcurrentBag<MapLineWrapper>();
            Parallel.ForEach(m_district.Regions,
                region =>
                {
                    var points = region.Points.Select(o => o.GetMercatorPoint(zoom)).ToList();
                    Parallel.For(0,
                        points.Count - 1,
                        index =>
                        {
                            var point1 = new MapPointWrapper
                            {
                                X = points[index].X - tilePoint.X * 256,
                                Y = tilePoint.Y * 256 - points[index].Y + 256
                            };

                            var point2 = new MapPointWrapper
                            {
                                X = points[index + 1].X - tilePoint.X * 256,
                                Y = tilePoint.Y * 256 - points[index + 1].Y + 256
                            };
                            wrappers.Add(new MapLineWrapper(point1, point2));
                        });
                });
            RegionLines = new ObservableCollection<MapLineWrapper>(wrappers);
        }

        #endregion
    }
}