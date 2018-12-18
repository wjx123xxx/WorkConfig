// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： ObjectToBooleanConverter.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-02 14:40:34
// *******************************************************************

namespace Wx.App.EasyBaiduMap.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// 對象轉換Bool值轉換器，Null值轉換成False，非Null值轉換為True
    /// </summary>
    public class ObjectToBooleanConverter : IValueConverter
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
            return value != null;
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