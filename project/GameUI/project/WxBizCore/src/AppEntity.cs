// *******************************************************************
// * 文件名称： AppEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-08 21:51:28
// *******************************************************************

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using dm;

using Wx.Utility.WxFramework.Common;

namespace Wx.App.BizCore
{
    /// <summary>
    /// 独立操作实体，代表一个账号
    /// </summary>
    public class AppEntity
    {
        #region Fields

        /// <summary>
        /// 账号名
        /// </summary>
        private readonly string m_account;

        /// <summary>
        /// 密码
        /// </summary>
        private readonly string m_password;

        /// <summary>
        /// 游戏进程
        /// </summary>
        private Process m_process;

        private string m_programPath;

        private dmsoft m_dmsoft;

        #endregion

        #region Constructors

        public AppEntity(string account, string password)
        {
            m_account = account;
            m_password = password;
        }

        public AppEntity(AppEntity old)
        {
            m_account = old.m_account;
            m_password = old.m_password;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 窗口运行开始时间
        /// </summary>
        public DateTime StartTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// 运行状态
        /// </summary>
        public RunningState State { get; set; } = RunningState.Wait;

        #endregion

        #region Public Methods

        /// <summary>
        /// 开始运行
        /// </summary>
        /// <param name="programPath">程序路径</param>
        public void Start(string programPath)
        {
            Task.Run(() => WLYBackupThread(programPath));
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        public void Stop()
        {
            State = RunningState.Stopped;
            m_process.Kill();
            m_process.Close();
            m_process.Dispose();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <returns></returns>
        private bool Login()
        {
            var dm = DMFactory.Instance.CreateDMSoft();

            // 获取窗口句柄
            var hwnd = 0;
            var retry = 0;
            while (hwnd == 0)
            {
                Thread.Sleep(1000);
                hwnd = dm.FindWindowByProcessId(m_process.Id, "", "");
                retry++;
                WxLog.Debug($"AppEntity.Login Get Process Hwnd Retry <{retry}> ProcessID <{m_process.Id}>");
                if (retry > 5)
                {
                    WxLog.Debug($"AppEntity.Login Fail To Get Process Hwnd Retry <{retry}>");
                    return false;
                }
            }

            WxLog.Debug($"AppEntity.Login Hwnd <{hwnd}>");

            // 获取登录窗的句柄
            var hwndStr = string.Empty;
            retry = 0;
            while (string.IsNullOrEmpty(hwndStr))
            {
                Thread.Sleep(1000);
                hwndStr = dm.EnumWindowByProcessId(m_process.Id, "", "Internet Explorer_Server", 2);
                WxLog.Debug($"AppEntity.Login Get Login Hwnd Retry <{retry}> hwndstr <{hwndStr}>");
                retry++;
                if (retry > 5)
                {
                    WxLog.Debug($"AppEntity.Login Fail To Get Login Hwnd Retry <{retry}>");
                    return false;
                }
            }

            WxLog.Debug($"AppEntity.Login Result <{hwndStr}>");
            dm.BindWindow(int.Parse(hwndStr), "gdi", "windows", "windows", 101);
            var resource = ResourceFactory.GetResource(ResourceEnum.EnterGame);

            // 确认窗口打开
            var findResult = -1;
            retry = 0;
            while (findResult == -1)
            {
                Thread.Sleep(500);
                findResult = dm.FindPicMem(415, 257, 535, 365, resource.Info, "101010", 0.95, 3, out var x, out var y);
                WxLog.Debug($"AppEntity.Login Ensure Open X <{x}> Y <{y}> result<{findResult}> retry<{retry}>");
                retry++;
                if (retry > 5)
                {
                    WxLog.Debug($"AppEntity.Login Fail To Ensure Open Retry <{retry}>");
                    return false;
                }
            }

            // 输入账号
            dm.MoveTo(324, 258);
            dm.LeftClick();
            Thread.Sleep(100);
            dm.SendString(int.Parse(hwndStr), m_account);
            dm.MoveTo(234, 291);
            dm.LeftClick();
            Thread.Sleep(100);
            dm.SendString(int.Parse(hwndStr), m_password);

            // 点击登录
            Thread.Sleep(100);
            dm.MoveTo(482, 322);
            dm.LeftClick();

            // 等待登录成功
            findResult = -1;
            resource = ResourceFactory.GetResource(ResourceEnum.LoginSuccess);
            retry = 0;
            while (findResult == -1)
            {
                Thread.Sleep(500);
                findResult = dm.FindPicMem(0, 0, 535, 365, resource.Info, "101010", 0.95, 3, out var x, out var y);
                WxLog.Debug($"AppEntity.Login Success X <{x}> Y <{y}> result<{findResult}> retry<{retry}>");
                retry++;
                if (retry > 5)
                {
                    WxLog.Debug($"AppEntity.Login Fail To Wait Success Retry <{retry}>");
                    return false;
                }
            }

            // 切换到双线区
            dm.MoveTo(137, 123);
            dm.LeftClick();
            Thread.Sleep(100);
            dm.LeftClick();
            Thread.Sleep(100);
            dm.LeftClick();
            Thread.Sleep(100);
            dm.LeftClick();
            Thread.Sleep(100);

            // 开始寻找指定区服 523,226
            findResult = -1;
            resource = ResourceFactory.GetResource(ResourceEnum.TargetServer);
            retry = 0;
            while (findResult == -1)
            {
                findResult = dm.FindPicMem(0, 0, 535, 365, resource.Info, "101010", 0.95, 3, out var x, out var y);
                if (findResult == -1)
                {
                    dm.MoveTo(523, 226);
                    dm.WheelDown();
                    WxLog.Debug($"AppEntity.Login Find Target Server <{findResult}> retry<{retry}>");
                    Thread.Sleep(200);
                }
                else
                {
                    dm.MoveTo(int.Parse(x.ToString()), int.Parse(y.ToString()));
                    dm.LeftClick();
                    WxLog.Debug($"AppEntity.Login Find TargetServer Success X <{x}> Y <{y}> result<{findResult}>");
                }

                retry++;
                if (retry > 50)
                {
                    WxLog.Debug($"AppEntity.Login Fail To Find TargetServer Retry <{retry}>");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 开始运行脚本
        /// </summary>
        private void StartGame()
        {
            if (m_process != null)
            {
                m_process.Close();
                m_process.Kill();
                m_process = null;
            }

            m_process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = m_programPath
            });

            var result = Login();
            if (!result)
            {
                // 启动失败，等待重启
                State = RunningState.Stopped;
                return;
            }

            // 设置开始时间
            StartTime = DateTime.Now;

            // 获取新窗口句柄
            m_dmsoft = DMFactory.Instance.CreateDMSoft();
            while (true)
            {
                var handle = m_dmsoft.EnumWindowByProcessId(m_process.Id, "", "MacromediaFlashPlayerActiveX", 2);
                WxLog.Debug($"AppEntity.StartGame Handl <{handle}>");
                var hs = handle.Split(',');
                foreach (var h in hs)
                {
                    try
                    {
                        var it = int.Parse(h);
                        m_dmsoft.BindWindow(it, "gdi", "windows", "windows", 101);
                        var title = m_dmsoft.GetWindowClass(it);
                        WxLog.Debug($"AppEntity.StartGame {title}");
                    }
                    catch
                    {
                    }
                }

                Thread.Sleep(10000);
            }

        }

        private void WLYBackupThread(string programPath)
        {
            m_programPath = programPath;
            State = RunningState.Running;

            // 进入运行循环
            StartGame();
        }

        #endregion
    }
}