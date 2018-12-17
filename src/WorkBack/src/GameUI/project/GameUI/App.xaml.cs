using System.Windows;

using Wx.App.GameUI.Main;
using Wx.Utility.WxFramework;

namespace Wx.App.GameUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Event Handlers

        private void OnStartup(object sender, StartupEventArgs e)
        {
            WxFrameworkService.Instance.Init();
            var wnd = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
            wnd.ShowDialog();
        }

        #endregion
    }
}