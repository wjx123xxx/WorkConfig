using System;
using System.Diagnostics;
using System.Threading;

using Wx.App.WlyAutoUI.VM;
using Wx.App.WlySubAutoPlayer.VM;
using Wx.Utility.WxFramework;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlyAutoCTL
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            WxFrameworkService.Instance.Init();

            var processes = Process.GetProcessesByName("uqee");
            foreach (var p in processes)
            {
                p.Kill();
                p.Close();
                p.Dispose();
            }

            var auto = new WlyAutoVM();
            auto.Start();
            AutoPlayerVM.Instance.Start();

            while (true)
            {
                Thread.Sleep(60000);
            }
        }

        #endregion

        #region Event Handlers

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e}>");
            WxLog.Fatal($"App.CurrentDomain_UnhandledException  <{e.ExceptionObject}>");
        }

        #endregion
    }
}