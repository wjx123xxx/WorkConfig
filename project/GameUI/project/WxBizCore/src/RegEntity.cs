// *******************************************************************
// * 文件名称： RegEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-10 21:44:21
// *******************************************************************

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Wx.Utility.WxCommon;
using Wx.Utility.WxFramework.Common;
using Wx.Utility.WxFramework.Database;

namespace Wx.App.BizCore
{
    /// <summary>
    /// 注册实体，调用注册窗口
    /// </summary>
    public class RegEntity
    {
        #region Fields

        private int m_index;

        private MySqlDatabase m_mySqlDatabase;

        private Process m_process;

        #endregion

        #region Public Properties

        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        public bool Run { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 开始注册
        /// </summary>
        /// <param name="programPath"></param>
        /// <param name="index"></param>
        public void Start(string programPath, int index)
        {
            m_index = index;
            m_mySqlDatabase = new MySqlDatabase();
            m_mySqlDatabase.Connect("root", "game1314", "127.0.0.1", "wxbiz");

            // 打开窗口
            m_process = new Process();
            m_process.StartInfo.FileName = programPath;
            m_process.StartInfo.UseShellExecute = false;
            m_process.Start();

            Run = true;
            Task.Run(() => WorkThread(index));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建账号，写入数据库
        /// </summary>
        /// <param name="user"></param>
        /// <param name="psw"></param>
        private void CreateAccount(string user, string psw)
        {
            var sql = "insert into AccountInfo(UID, Account, Psw, LastLoginTime) values (?, ?, ?, ?);";
            var uid = MathHelper.GetNewGuid();
            var result = m_mySqlDatabase.Execute(sql, new object[] { uid, user, psw, DateTime.MinValue });
            WxLog.Debug($"RegEntity.CreateAccount <{user} | {psw}> Result<{result}>");
        }

        private string GetRandomStr(int len)
        {
            var guid = MathHelper.GetNewGuid().Replace("-", "");
            return guid.Substring(0, len);
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        /// <param name="index"></param>
        private void WorkThread(int index)
        {
            var dm = DMFactory.Instance.CreateDMSoft();

            // 获取登录窗的句柄
            var hwndStr = string.Empty;
            while (string.IsNullOrEmpty(hwndStr))
            {
                Thread.Sleep(1000);
                hwndStr = dm.EnumWindowByProcessId(m_process.Id, "", "Internet Explorer_Server", 2);
                WxLog.Debug($"AppEntity.Login Get Login Hwnd hwndstr <{hwndStr}>");
            }

            var hwnd = int.Parse(hwndStr);
            DMFactory.Instance.BindWindow(dm, hwnd);
            var rand = new Random();

            // 开始进入注册线程
            while (true)
            {
                var account = GetRandomStr(rand.Next(6, 9));
                var psw = GetRandomStr(rand.Next(6, 9));
                WxLog.Debug($"RegEntity.WorkThread Account <{account}> Password <{psw}>");

                // 点击新用户注册
                while (true)
                {
                    var result = DMFactory.Instance.FindPic(dm, ResourceEnum.RegUncheck, out var x, out var y);
                    if (result)
                    {
                        dm.MoveTo(x, y);
                        dm.LeftClick();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        break;
                    }
                }

                // 确认切换成功
                var retry = 0;
                while (true)
                {
                    var result = DMFactory.Instance.FindPic(dm, ResourceEnum.RegChecked, out var _, out var _);
                    if (result)
                    {
                        break;
                    }

                    retry++;
                    Thread.Sleep(100);
                    if (retry > 20)
                    {
                        break;
                    }
                }

                // 输入注册信息
                dm.MoveTo(323, 257);
                dm.LeftClick();
                dm.SendString(hwnd, account);
                Thread.Sleep(500);

                dm.MoveTo(300, 290);
                dm.LeftClick();
                dm.SendString(hwnd, psw);
                Thread.Sleep(500);

                dm.MoveTo(300, 330);
                dm.LeftClick();
                dm.SendString(hwnd, psw);
                Thread.Sleep(500);

                // 点击进入游戏
                dm.MoveTo(478, 323);
                dm.LeftClick();
                Thread.Sleep(2000);

                // 确认账号有效
                var r = DMFactory.Instance.FindPic(dm, ResourceEnum.ErrorInfo, out var _, out var _);
                if (r)
                {
                    WxLog.Debug($"RegEntity.WorkThread Error Account {account}");
                    break;
                }

                // 确认登录成功
                r = DMFactory.Instance.FindPic(dm, ResourceEnum.LoginSuccess, out var _, out var _);
                if (r)
                {
                    // 账号信息写入数据库
                    CreateAccount(account, psw);

                    // 注销
                    dm.MoveTo(377, 11);
                    dm.LeftClick();
                    Thread.Sleep(2000);
                }
                else
                {
                    break;
                }
            }

            m_process.Kill();
            m_process.Close();
            m_process.Dispose();
            Run = false;
        }

        #endregion
    }
}