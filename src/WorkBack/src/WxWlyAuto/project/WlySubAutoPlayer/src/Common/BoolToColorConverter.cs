// *******************************************************************
// * 文件名称： BoolToColorConverter.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 16:01:47
// *******************************************************************

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Wx.App.WlySubAutoPlayer.Common
{
    /// <summary>
    /// 启动状态与颜色转换器
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        #region Constructors

        #endregion

        #region Public Methods

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, true))
            {
                return Brushes.ForestGreen;
            }

            return Brushes.MediumVioletRed;
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}