using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.App.WlySubAutoPlayer.View;
using Wx.App.WlySubAutoPlayer.VM;
using Wx.Utility.WxFramework;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlySubAutoPlayer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region Event Handlers

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e}>");
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e.ExceptionObject}>");
        }

        [MTAThread]
        private void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            WxFrameworkService.Instance.Init();

            //var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log", "errorAccount.txt");
            //var accounts = File.ReadAllLines(filePath);
            //foreach (var account in accounts)
            //{
            //    var ass = account.Split(' ');
            //    var info = new SubAccountInfo(ass[0]);
            //    info.Account = ass[1];
            //    info.Password = ass[2];
            //    info.Save();
            //}

            // 关闭残留的进程，重新开始
            var processes = Process.GetProcessesByName("uqee");
            foreach (var p in processes)
            {
                p.Kill();
                p.Close();
                p.Dispose();
            }

            var wnd = new AutoPlayerWnd
            {
                DataContext = AutoPlayerVM.Instance
            };

            // 开启小号挂机
            //AutoPlayerVM.Instance.Test();
            AutoPlayerVM.Instance.Start();

            // 显示信息界面
            wnd.ShowDialog();
            AutoPlayerVM.Instance.Stop();
        }

        #endregion
    }
}