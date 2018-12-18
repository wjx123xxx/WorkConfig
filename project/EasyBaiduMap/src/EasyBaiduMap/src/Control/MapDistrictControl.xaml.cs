using System.Windows;
using System.Windows.Controls;

using Wx.Common.BaiduMap.Entity;

namespace Wx.App.EasyBaiduMap.Control
{
    /// <summary>
    /// 行政区地图显示控件
    /// </summary>
    public partial class MapDistrictControl : UserControl
    {
        #region Fields

        /// <summary>
        /// 地图行政区属性
        /// </summary>
        public static readonly DependencyProperty DistrictProperty = DependencyProperty.Register(
            nameof(District), typeof(District), typeof(MapDistrictControl), new PropertyMetadata(HandleMapStrict));

        #endregion

        #region Constructors

        public MapDistrictControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 地图行政区
        /// </summary>
        public District District
        {
            get
            {
                return (District)GetValue(DistrictProperty);
            }
            set
            {
                SetValue(DistrictProperty, value);
            }
        }

        #endregion

        #region Private Methods

        private static void HandleMapStrict(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion
    }
}