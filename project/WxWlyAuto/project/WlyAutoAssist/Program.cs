// *******************************************************************
// * 文件名称： Program.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-12 22:44:05
// *******************************************************************

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Wx.App.WlyAutoAssist.Biz;
using Wx.App.WlyAutoUI.Biz;
using Wx.Utility.WxFramework;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlyAutoAssist
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

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "account.xml");
            var account = MainAccountInfo.LoadAccount(file);
            var entity = new AssistEntity(account);
            AssistTaskMgr.Instance.InitEntityTasks(entity);
            entity.Start();

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