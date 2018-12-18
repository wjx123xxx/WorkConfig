// *******************************************************************
// * 文件名称： TaskTemplateSelector.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 17:52:24
// *******************************************************************

using System.Windows;
using System.Windows.Controls;

using Wx.App.WlyTaskViewer.VM;

namespace Wx.App.WlyTaskViewer.StyleSelector
{
    /// <summary>
    /// 模板选择器
    /// </summary>
    public class TaskTemplateSelector : DataTemplateSelector
    {
        #region Public Properties

        public DataTemplate LineTemplate { get; set; }

        public DataTemplate TaskTemplate { get; set; }

        #endregion

        #region Public Methods

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TaskWrapper)
            {
                return TaskTemplate;
            }

            if (item is LineWrapper)
            {
                return LineTemplate;
            }

            return base.SelectTemplate(item, container);
        }

        #endregion
    }
}