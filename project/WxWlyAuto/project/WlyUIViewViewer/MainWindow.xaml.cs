using System.Windows;

using Wx.App.WlyUIViewViewer.VM;

namespace Wx.App.WlyUIViewViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowVM();
        }

        #endregion
    }
}