using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

using Wx.App.WlyAutoUI.View;
using Wx.App.WlyAutoUI.VM;
using Wx.Utility.WxFramework;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlyAutoUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex m_mutex;

        #region Event Handlers

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e}>");
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e.ExceptionObject}>");
        }

        [MTAThread]
        private void OnStartup(object sender, StartupEventArgs e)
        {
            m_mutex = new Mutex(true, "WlyAuto", out var created);
            if (!created)
            {
                Current.Shutdown();
                return;
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            WxFrameworkService.Instance.Init();

            var processes = Process.GetProcessesByName("uqee");
            foreach (var p in processes)
            {
                p.Kill();
                p.Close();
                p.Dispose();
            }

            var wnd = new MainEntityWnd();
            var vm = new WlyAutoVM();
            wnd.DataContext = vm;
            vm.Start();

            // 显示信息界面
            wnd.ShowDialog();
            vm.Stop();
        }

        #endregion
    }
}