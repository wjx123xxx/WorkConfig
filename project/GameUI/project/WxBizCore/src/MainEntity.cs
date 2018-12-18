// *******************************************************************
// * 文件名称： MainEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-11 23:19:59
// *******************************************************************

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using dm;

using Wx.Utility.WxFramework.Common;

namespace Wx.App.BizCore
{
    /// <summary>
    /// 主号挂机实体
    /// </summary>
    public class MainEntity
    {
        #region Fields

        private string m_account;

        private dmsoft m_dm;

        private string m_password;

        private Process m_process;

        private string m_programPath;

        private bool m_run;

        private DateTime m_startTime;

        private int m_wndHandle;

        private int m_lastFight = DateTime.Now.Hour;

        #endregion

        #region Constructors

        public MainEntity(string account, string password)
        {
            m_password = password;
            m_account = account;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 挂机开始
        /// </summary>
        /// <param name="programPath"></param>
        public void Start(string programPath)
        {
            m_programPath = programPath;
            m_run = true;

            Task.Run(() => BackupWorkThread());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 后台运行线程
        /// </summary>
        private void BackupWorkThread()
        {
            // 最外层死循环，进程守护
            while (m_run)
            {
                // 开启进程
                OpenProcess();

                // 登录
                if (!Login())
                {
                    continue;
                }

                // 开始挂机
                BeginAutoPlay();
            }
        }

        /// <summary>
        /// 开启挂机
        /// </summary>
        private void BeginAutoPlay()
        {
            m_startTime = DateTime.Now;
            m_dm = DMFactory.Instance.CreateDMSoft();
            var dictPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resource", "WLYWord.txt");
            var setResult = m_dm.SetDict(0, dictPath);
            WxLog.Debug($"MainEntity.BeginAutoPlay SetDict {dictPath}  Result:<{setResult}>");

            var retry = 0;
            while (true)
            {
                m_wndHandle = m_dm.FindWindowByProcessId(m_process.Id, "ActiveX", "");
                var state = m_dm.GetWindowState(m_wndHandle, 2);
                if (state == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                DMFactory.Instance.BindWindow(m_dm, m_wndHandle);
                var result = DMFactory.Instance.FindPic(m_dm, ResourceEnum.InGameCheck, out var _, out var _);
                if (result)
                {
                    break;
                }

                // 超过30秒重新启动
                retry++;
                if (retry > 30)
                {
                    WxLog.Debug($"MainEntity.BeginAutoPlay Cannot Find InGameTag {retry}");
                    return;
                }

                Thread.Sleep(1000);
            }

            WxLog.Debug($"MainEntity.BeginAutoPlay Find InGameTag Success {m_wndHandle}");

            m_dm.MoveTo(815, 178);
            m_dm.LeftClick();
            Thread.Sleep(1000);
            m_dm.LeftClick();
            Thread.Sleep(1000);

            // 内层死循环，周期性重启
            while (m_run)
            {
                //回到主城界面
                GoBack();

                // 收起聊天框
                CloseChat();

                // 检测擂台站报名
                //SignUpFight();
                FindFirstAttack();

                //Thread.Sleep(30000);
                if (DateTime.Now - m_startTime > TimeSpan.FromMinutes(10) || (DateTime.Now.Minute == 0 && m_startTime.Minute != 0))
                {
                    // 10分钟启动一次
                    WxLog.Debug($"MainEntity.BeginAutoPlay Restart {DateTime.Now}");
                    return;
                }
            }
        }

        /// <summary>
        /// 关闭聊天窗
        /// </summary>
        private void CloseChat()
        {
            while (true)
            {
                var result = DMFactory.Instance.FindPic(m_dm, ResourceEnum.CloseChat, out var x, out var y);
                if (result)
                {
                    WxLog.Debug($"MainEntity.CloseChat Location:{x},{y}");
                    m_dm.MoveTo(x + 5, y + 5);
                    m_dm.LeftClick();
                    Thread.Sleep(100);
                }

                WxLog.Debug($"MainEntity.CloseChat Finish");
                break;
            }
        }

        /// <summary>
        /// 寻找首攻
        /// </summary>
        private void FindFirstAttack()
        {
            var startTime = DateTime.Now;
            while (true)
            {
                if (DateTime.Now - startTime > TimeSpan.FromMinutes(2))
                {
                    return;
                }

                var findResult = m_dm.FindStr(100, 470, 277, 550, "(首", "fff71c-000000|fcffa9-000000", 1, out var _, out var _);
                if (findResult == -1)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // 寻找到首攻，加入队伍
                WxLog.Debug($"MainEntity.FindFirstAttack Find First Attack {findResult}");
                findResult = m_dm.FindStr(100, 470, 277, 550, "加", "00ff00-000000", 1, out var x, out var y);
                if (findResult == -1)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // 点击加入队伍
                WxLog.Debug($"MainEntity.FindFirstAttack Click:{x},{y}");
                m_dm.MoveTo((int)x + 5, (int)y + 5);
                m_dm.LeftClick();

                findResult = -1;
                var retry = 0;
                while (findResult == -1)
                {
                    // 确认自己在队伍中
                    findResult = m_dm.FindStr(370, 198, 517, 219, "(首攻)", "ffffb0-000000", 1, out var _, out var _);
                    if (findResult == -1)
                    {
                        retry++;
                        if (retry > 30)
                        {
                            break;
                        }

                        Thread.Sleep(100);
                    }
                }

                findResult = m_dm.FindStr(632, 206, 807, 449, "海潮", "e9e7cf-000000", 1, out var _, out var _);
                if (findResult == -1)
                {
                    Thread.Sleep(100);
                    continue;
                }

                // 给首攻五秒钟时间
                Thread.Sleep(5000);
                return;
            }
        }

        /// <summary>
        /// 回到主城界面
        /// </summary>
        private void GoBack()
        {
        }

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
                    WxLog.Debug($"MainEntity.Login Find Target Server <{findResult}> retry<{retry}>");
                    Thread.Sleep(200);
                }
                else
                {
                    dm.MoveTo(int.Parse(x.ToString()), int.Parse(y.ToString()));
                    dm.LeftClick();
                    WxLog.Debug($"MainEntity.Login Find TargetServer Success X <{x}> Y <{y}> result<{findResult}>");
                }

                retry++;
                if (retry > 50)
                {
                    WxLog.Debug($"MainEntity.Login Fail To Find TargetServer Retry <{retry}>");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 开启进程
        /// </summary>
        private void OpenProcess()
        {
            if (m_process != null)
            {
                m_process.Kill();
                m_process.Close();
                m_process.Dispose();
                m_process = null;
            }

            m_process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = m_programPath
            });
        }

        /// <summary>
        /// 擂台报名
        /// </summary>
        private bool SignUpFight()
        {
            var time = DateTime.Now.Hour;
            if (m_lastFight == time)
            {
                return true;
            }

            if (time != 10 || time != 15 || time != 21)
            {
                return true;
            }


            // 点击日常
            var result = DMFactory.Instance.ClickPic(m_dm, ResourceEnum.Daily);
            if (!result)
            {
                return false;
            }

            // 确定进入了日常界面
            result = DMFactory.Instance.CheckGoTo(() => DMFactory.Instance.FindPic(m_dm, ResourceEnum.DailyUI, out var _, out var _),
                TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(5));
            if (!result)
            {
                return false;
            }

            // 点击擂台赛
            result = DMFactory.Instance.ClickPic(m_dm, ResourceEnum.Fight);
            if (!result)
            {
                return false;
            }

            // 确定进入了擂台赛界面
            result = DMFactory.Instance.CheckGoTo(() => DMFactory.Instance.FindPic(m_dm, ResourceEnum.FightUI, out var _, out var _),
                TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(5));
            if (!result)
            {
                return false;
            }

            // 点击报名
            m_dm.MoveTo(393, 551);
            m_dm.LeftClick();
            Thread.Sleep(1000);

            // 点击退出
            m_dm.MoveTo(912, 549);
            m_dm.LeftClick();
            Thread.Sleep(1000);
            m_lastFight = time;
            return true;
        }

        #endregion
    }
}