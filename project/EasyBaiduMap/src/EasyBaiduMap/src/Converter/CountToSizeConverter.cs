// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： CountToSizeConverter.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-04 23:39:51
// *******************************************************************

namespace Wx.App.EasyBaiduMap.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// 數量轉換成容量大小字符串
    /// </summary>
    public class CountToSizeConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double totalSize = System.Convert.ToInt64(value) * System.Convert.ToInt64(parameter);
            var str = "B";
            if (totalSize > 1024)
            {
                str = "KB";
                totalSize = totalSize / 1024;
            }

            if (totalSize > 1024)
            {
                str = "MB";
                totalSize = totalSize / 1024;
            }

            if (totalSize > 1024)
            {
                totalSize = totalSize / 1024;
                str = "GB";
            }

            if (totalSize > 1024)
            {
                totalSize = totalSize / 1024;
                str = "TB";
            }

            return $"{totalSize:N2}{str}";
        }
        /// <summary>转换值。</summary>
        /// <returns>转换后的值。如果该方法返回 null，则使用有效的 null 值。</returns>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}