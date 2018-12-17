// *******************************************************************
// * 文件名称： UIViewStyleSelector.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 17:52:32
// *******************************************************************

using System.Windows;

using Wx.App.WlyUIViewViewer.VM;

namespace Wx.App.WlyUIViewViewer.StyleSelector
{
    /// <summary>
    /// 视图样式选择器
    /// </summary>
    public class UIViewStyleSelector : System.Windows.Controls.StyleSelector
    {
        #region Public Properties

        public Style LineStyle { get; set; }

        public Style UIViewStyle { get; set; }

        #endregion

        #region Public Methods

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.Style" /> based on custom logic.</summary>
        /// <param name="item">The content.</param>
        /// <param name="container">The element to which the style will be applied.</param>
        /// <returns>Returns an application-specific style to apply; otherwise, null.</returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is WlyUIViewWrapper)
            {
                return UIViewStyle;
            }

            return LineStyle;
        }

        #endregion
    }
}