// *******************************************************************
// * 版权所有： 深圳市震有科技软件有限公司
// * 文件名称： TimeToColorConverter.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 17:24:19
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Wx.App.WlyAutoUI.Common
{
    /// <summary>
    /// 时间转换器
    /// </summary>
    public class TimeToColorConverter : IMultiValueConverter
    {
        #region Public Methods

        /// <summary>将源值转换为绑定目标的值。 数据绑定引擎在将该值从源绑定传播到绑定目标时会调用此方法。</summary>
        /// <param name="values">
        /// <see cref="T:System.Windows.Data.MultiBinding" /> 中的源绑定生成的值的数组。 值 <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 指示源绑定没有可供转换的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 null，则会使用有效的 null 值。<see cref="T:System.Windows.DependencyProperty" />。<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> 的返回值指示转换器没有生成值，并且绑定将使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />（如果可用），或者将使用默认值。<see cref="T:System.Windows.Data.Binding" /><see cref="F:System.Windows.Data.Binding.DoNothing" /> 的返回值指示绑定不会传输该值，或不使用 <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> 或默认值。</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime current = (DateTime)values[0];
            DateTime target = (DateTime)values[1];

            if (target < current)
            {
                return Brushes.ForestGreen;
            }

            return Brushes.Black;
        }

        /// <summary>将绑定目标值转换为源绑定值。</summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetTypes">要转换为的类型数组。 数组长度指示为要返回的方法所建议的值的数量与类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>已从目标值转换回源值的值的数组。</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }

        #endregion
    }
}