namespace Wx.App.EasyBaiduMap
{
    using System.Windows;

    using Wx.App.EasyBaiduMap.ViewModel;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            var vm = new MainWndVM();
            DataContext = vm;

            InitializeComponent();
        }

        #endregion
    }
}