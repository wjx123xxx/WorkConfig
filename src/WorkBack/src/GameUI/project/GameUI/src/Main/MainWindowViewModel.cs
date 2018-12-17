// *******************************************************************
// * 版权所有： 深圳市震有科技软件有限公司
// * 文件名称： MainWindowViewModel.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-07 10:35:05
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Input;

using Microsoft.Win32;

using Wx.App.BizCore;
using Wx.App.GameUI.Biz;
using Wx.Utility.UICommon;
using Wx.Utility.WxFramework.Common;

namespace Wx.App.GameUI.Main
{
    /// <summary>
    /// 主窗口基类
    /// </summary>
    public class MainWindowViewModel : WxUIEntity
    {
        #region Fields

        private readonly IList<AppEntity> entityList = new List<AppEntity>();

        /// <summary>
        /// 配置实体
        /// </summary>
        private readonly AppConfig m_config;

        private readonly IList<RegEntity> regList = new List<RegEntity>();

        /// <summary>
        /// 账号
        /// </summary>
        private string m_account;

        /// <summary>
        /// 开始大号挂机
        /// </summary>
        private ICommand m_beginAutoHookCmd;

        /// <summary>
        /// 配置文件名称
        /// </summary>
        private string m_configFile = "app.config";

        private MainEntity m_mainEntity;

        /// <summary>
        /// 密码
        /// </summary>
        private string m_password;

        /// <summary>
        /// 设置程序路径
        /// </summary>
        private ICommand m_setProgramCmd;

        private ICommand m_startCmd;

        private Timer m_timer;

        /// <summary>
        /// comment
        /// </summary>
        private string m_wndHandle;

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_configFile);
            m_config = AppConfig.Load(configFile);

            // 计时器
            m_timer = new Timer(10000);
            m_timer.Elapsed += KeepAliveElapsed;
            m_timer.Start();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return m_config.Account; }
            set
            {
                m_config.Account = value;
                OnPropertyChanged(nameof(Account));
                m_config.Save();
            }
        }

        /// <summary>
        /// 开始大号挂机
        /// </summary>
        public ICommand BeginAutoHookCmd
        {
            get
            {
                if (m_beginAutoHookCmd == null)
                {
                    m_beginAutoHookCmd = WxCommandFactory.CreateCommand(BeginAutoHookCmdExecute, CanBeginAutoHookCmdExecute);
                }

                return m_beginAutoHookCmd;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return m_config.Password; }
            set
            {
                m_config.Password = value;
                OnPropertyChanged(nameof(Password));
                m_config.Save();
            }
        }

        /// <summary>
        /// 程序路径
        /// </summary>
        public string ProgramPath
        {
            get { return m_config.ProgramPath; }
            set
            {
                if (m_config.ProgramPath != value)
                {
                    m_config.ProgramPath = value;
                    OnPropertyChanged(nameof(ProgramPath));
                    m_config.Save();
                }
            }
        }

        /// <summary>
        /// 设置程序路径
        /// </summary>
        public ICommand SetProgramCmd
        {
            get
            {
                if (m_setProgramCmd == null)
                {
                    m_setProgramCmd = WxCommandFactory.CreateCommand(SetProgramCmdExecute, CanSetProgramCmdExecute);
                }

                return m_setProgramCmd;
            }
        }

        public ICommand StartCmd
        {
            get
            {
                if (m_startCmd == null)
                {
                    m_startCmd = WxCommandFactory.CreateCommand(StartCmdExecute);
                }

                return m_startCmd;
            }
        }

        /// <summary>
        /// comment
        /// </summary>
        public string WndHandle
        {
            get { return m_wndHandle; }
            set
            {
                m_wndHandle = value;
                OnPropertyChanged(nameof(WndHandle));
            }
        }

        #endregion

        #region Private Methods

        private void BeginAutoHookCmdExecute(object obj)
        {
            if (m_mainEntity != null)
            {
                return;
            }

            m_mainEntity = new MainEntity(Account, Password);
            m_mainEntity.Start(m_config.ProgramPath);
        }

        private bool CanBeginAutoHookCmdExecute(object arg)
        {
            return true;
        }

        private bool CanSetProgramCmdExecute(object arg)
        {
            return true;
        }

        private void SetProgramCmdExecute(object obj)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ProgramPath = ofd.FileName;
            }
        }

        private void StartCmdExecute(object obj)
        {
            var wlyEntity = new AppEntity("15820299689", "game1314");
            wlyEntity.Start(ProgramPath);
            entityList.Add(wlyEntity);
        }

        #endregion

        #region Event Handlers

        private void KeepAliveElapsed(object sender, ElapsedEventArgs e)
        {
            m_timer.Stop();
            var now = DateTime.Now;
            try
            {
                foreach (var entity in entityList.ToList())
                {
                    if ((entity.State == RunningState.Stopped) || ((now - entity.StartTime).TotalMinutes > 10))
                    {
                        entity.Stop();
                        entityList.Remove(entity);

                        var newEntity = new AppEntity(entity);
                        entityList.Add(newEntity);
                        newEntity.Start(m_config.ProgramPath);
                    }
                }

                foreach (var reg in regList)
                {
                    if (!reg.Run)
                    {
                        reg.Start(m_config.ProgramPath, reg.Index);
                    }
                }
            }
            catch (Exception ex)
            {
                WxLog.Error($"MainWindowViewModel.KeepAliveElapsed Ex <{ex}>");
            }
            finally
            {
                m_timer.Start();
            }
        }

        #endregion
    }
}