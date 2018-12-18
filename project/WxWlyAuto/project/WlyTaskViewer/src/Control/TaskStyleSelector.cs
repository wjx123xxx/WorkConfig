// *******************************************************************
// * 文件名称： TaskStyleSelector.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-25 10:28:09
// *******************************************************************

using System.Windows;
using System.Windows.Controls;

using Wx.App.WlyTaskViewer.VM;

namespace Wx.App.WlyTaskViewer.Control
{
    /// <summary>
    /// Style选择器
    /// </summary>
    public class TaskStyleSelector : StyleSelector
    {
        #region Constructors

        #endregion

        #region Public Properties

        public Style LineItemContainerStyle { get; set; }

        public Style TaskItemContainerStyle { get; set; }

        #endregion

        #region Public Methods

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.Style" /> based on custom logic.</summary>
        /// <param name="item">The content.</param>
        /// <param name="container">The element to which the style will be applied.</param>
        /// <returns>Returns an application-specific style to apply; otherwise, null.</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is TaskWrapper)
            {
                return TaskItemContainerStyle;
            }

            if (item is LineWrapper)
            {
                return LineItemContainerStyle;
            }

            return base.SelectStyle(item, container);
        }

        #endregion
    }
}