// *******************************************************************
// * 文件名称： WlyAutoVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-15 22:52:35
// *******************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Wx.App.WlyAutoUI.Biz;
using Wx.App.WlyAutoUI.Common;
using Wx.App.WlySubAutoPlayer.VM;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.UICommon;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlyAutoUI.VM
{
    /// <summary>
    /// 界面VM
    /// </summary>
    public class WlyAutoVM : WxUIEntity
    {
        #region Fields

        /// <summary>
        /// 主账号文件
        /// </summary>
        private readonly string m_mainFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "main", "mainInfo.xml");

        private WlyAccountInfo m_accountInfo;

        private CancellationTokenSource m_cancellationTokenSource;

        /// <summary>
        /// 运行描述
        /// </summary>
        private string m_description;

        /// <summary>
        /// 主账号实体
        /// </summary>
        private MainEntity m_mainEntity;

        /// <summary>
        /// 下一次重置时间
        /// </summary>
        private DateTime m_resetTime;

        #endregion

        #region Public Properties

        /// <summary>
        /// 运行描述
        /// </summary>
        public string Description
        {
            get { return m_description; }
            set
            {
                if (m_description != value)
                {
                    m_description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        /// <summary>
        /// 古城探险可选列表
        /// </summary>
        public IEnumerable<WlyExploreType> ExploreTypes => WxHelper.GetEnumValues<WlyExploreType>();

        /// <summary>
        /// 可选军团副本类型
        /// </summary>
        public IEnumerable<WlyGroupType> GroupTypes => WxHelper.GetEnumValues<WlyGroupType>();

        /// <summary>
        /// 主账号实体
        /// </summary>
        public MainEntity MainEntity
        {
            get { return m_mainEntity; }
            set
            {
                m_mainEntity = value;
                OnPropertyChanged(nameof(MainEntity));
            }
        }

        /// <summary>
        /// 军令保留数量
        /// </summary>
        public int PointReserved
        {
            get { return m_mainEntity.AccountInfo.PointReserved; }
            set
            {
                m_mainEntity.AccountInfo.PointReserved = value;
                m_mainEntity.AccountInfo.Save();
                OnPropertyChanged(nameof(PointReserved));
            }
        }

        /// <summary>
        /// 下一次重置时间
        /// </summary>
        public DateTime ResetTime
        {
            get { return m_resetTime; }
            set
            {
                m_resetTime = value;
                OnPropertyChanged(nameof(ResetTime));
            }
        }

        /// <summary>
        /// 古城探险选择
        /// </summary>
        public WlyExploreType SelectedExploreType
        {
            get { return m_mainEntity.AccountInfo.SelectedExploreType; }
            set
            {
                m_mainEntity.AccountInfo.SelectedExploreType = value;
                m_mainEntity.AccountInfo.Save();
                OnPropertyChanged(nameof(SelectedExploreType));
            }
        }

        /// <summary>
        /// 军团副本选择
        /// </summary>
        public WlyGroupType SelectedGroupType
        {
            get { return m_mainEntity.AccountInfo.SelectedGroupType; }
            set
            {
                m_mainEntity.AccountInfo.SelectedGroupType = value;
                m_mainEntity.AccountInfo.Save();
                OnPropertyChanged(nameof(SelectedGroupType));
            }
        }

        /// <summary>
        /// 小号管理VM
        /// </summary>
        public AutoPlayerVM SubPlayerVM
        {
            get { return AutoPlayerVM.Instance; }
        }

        /// <summary>
        /// 任务开关列表
        /// </summary>
        public IList<WlyTaskSwitchWrapper> Switches { get; } = new List<WlyTaskSwitchWrapper>();

        /// <summary>
        /// 系统信息
        /// </summary>
        public WlySystemInfo SystemInfo => WlyUtilityBiz.SystemInfo;

        #endregion

        #region Public Methods

        public void Start()
        {
            m_accountInfo = MainAccountInfo.LoadAccount(m_mainFile);
            if (!File.Exists(m_mainFile))
            {
                m_accountInfo.Save();
            }

            m_mainEntity = new MainEntity(m_accountInfo);
            MainTaskMgr.Instance.InitEntityTasks(m_mainEntity);
            m_mainEntity.DescriptionChanged += MainEntityOnDescriptionChanged;
            m_mainEntity.Start();

            foreach (var t in WxHelper.GetEnumValues<WlySwitchType>())
            {
                var info = m_accountInfo.GetSwitchInfo(t);
                var wrapper = new WlyTaskSwitchWrapper(info);
                wrapper.Changed += WrapperOnChanged;
                Switches.Add(wrapper);
            }

            // 主账号监测
            m_cancellationTokenSource = new CancellationTokenSource();
            var token = m_cancellationTokenSource.Token;
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(1));
                        if (!m_mainEntity.Run)
                        {
                            WxLog.Debug($"WlyAutoVM.Start 对主账号进行重置 <{DateTime.Now}>");
                            m_mainEntity.Stop();
                            m_mainEntity.DescriptionChanged -= MainEntityOnDescriptionChanged;
                            MainEntity = new MainEntity(m_accountInfo);
                            MainTaskMgr.Instance.InitEntityTasks(m_mainEntity);
                            m_mainEntity.DescriptionChanged += MainEntityOnDescriptionChanged;
                            m_mainEntity.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        WxLog.Error($"WlyAutoVM.Start Error <{ex}>");
                    }
                }

                WxLog.Debug($"WlyAutoVM.Start Stop On <{ResetTime}>");
            }, token);

            //StartSubCmdExecute(this);
        }

        public void Stop()
        {
            WxLog.Debug($"WlyAutoVM.Stop  <{DateTime.Now}>");
            m_cancellationTokenSource.Cancel(false);
        }

        #endregion

        #region Event Handlers

        private void MainEntityOnDescriptionChanged(object sender, string s)
        {
            Description = s;
        }

        private void WrapperOnChanged(object sender, EventArgs eventArgs)
        {
            m_accountInfo.Save();
        }

        #endregion

        #region Commands

        /// <summary>
        /// 截图
        /// </summary>
        private ICommand m_captureCmd;

        /// <summary>
        /// 仓库清理任务追加
        /// </summary>
        private ICommand m_clearWareHouseCmd;

        /// <summary>
        /// 古城探险
        /// </summary>
        private ICommand m_exploreCmd;

        /// <summary>
        /// 暂停命令
        /// </summary>
        private ICommand m_pauseCmd;

        /// <summary>
        /// 装备重铸
        /// </summary>
        private ICommand m_rebuildCmd;

        /// <summary>
        /// 刷新任务
        /// </summary>
        private ICommand m_refreshCmd;

        /// <summary>
        /// ResetSub
        /// </summary>
        private ICommand m_resetSubCmd;

        /// <summary>
        /// 小号挂机
        /// </summary>
        private ICommand m_startSubCmd;

        /// <summary>
        /// 停止小号挂机
        /// </summary>
        private ICommand m_stopSubCmd;

        /// <summary>
        /// 截图
        /// </summary>
        public ICommand CaptureCmd
        {
            get
            {
                if (m_captureCmd == null)
                {
                    m_captureCmd = WxCommandFactory.CreateCommand(CaptureCmdExecute);
                }

                return m_captureCmd;
            }
        }

        /// <summary>
        /// 仓库清理任务追加
        /// </summary>
        public ICommand ClearWareHouseCmd
        {
            get
            {
                if (m_clearWareHouseCmd == null)
                {
                    m_clearWareHouseCmd = WxCommandFactory.CreateCommand(ClearWareHouseCmdExecute, CanClearWareHouseCmdExecute);
                }

                return m_clearWareHouseCmd;
            }
        }

        /// <summary>
        /// 古城探险
        /// </summary>
        public ICommand ExploreCmd
        {
            get
            {
                if (m_exploreCmd == null)
                {
                    m_exploreCmd = WxCommandFactory.CreateCommand(ExploreCmdExecute);
                }

                return m_exploreCmd;
            }
        }

        /// <summary>
        /// 暂停命令
        /// </summary>
        public ICommand PauseCmd
        {
            get
            {
                if (m_pauseCmd == null)
                {
                    m_pauseCmd = WxCommandFactory.CreateCommand(PauseCmdExecute);
                }

                return m_pauseCmd;
            }
        }

        /// <summary>
        /// 装备重铸
        /// </summary>
        public ICommand RebuildCmd
        {
            get
            {
                if (m_rebuildCmd == null)
                {
                    m_rebuildCmd = WxCommandFactory.CreateCommand(RebuildCmdExecute, CanRebuildCmdExecute);
                }

                return m_rebuildCmd;
            }
        }

        /// <summary>
        /// 刷新任务
        /// </summary>
        public ICommand RefreshCmd
        {
            get
            {
                if (m_refreshCmd == null)
                {
                    m_refreshCmd = WxCommandFactory.CreateCommand(RefreshCmdExecute);
                }

                return m_refreshCmd;
            }
        }

        /// <summary>
        /// ResetSub
        /// </summary>
        public ICommand ResetSubCmd
        {
            get
            {
                if (m_resetSubCmd == null)
                {
                    m_resetSubCmd = WxCommandFactory.CreateCommand(ResetSubCmdExecute);
                }

                return m_resetSubCmd;
            }
        }

        /// <summary>
        /// 小号挂机
        /// </summary>
        public ICommand StartSubCmd
        {
            get
            {
                if (m_startSubCmd == null)
                {
                    m_startSubCmd = WxCommandFactory.CreateCommand(StartSubCmdExecute);
                }

                return m_startSubCmd;
            }
        }

        /// <summary>
        /// 停止小号挂机
        /// </summary>
        public ICommand StopSubCmd
        {
            get
            {
                if (m_stopSubCmd == null)
                {
                    m_stopSubCmd = WxCommandFactory.CreateCommand(StopSubCmdExecute);
                }

                return m_stopSubCmd;
            }
        }

        #endregion

        #region CommandExecutes

        private void ResetSubCmdExecute(object obj)
        {
            AutoPlayerVM.Instance.ResetSubList();
        }

        private void StopSubCmdExecute(object obj)
        {
            AutoPlayerVM.Instance.Stop();
        }

        private void StartSubCmdExecute(object obj)
        {
            AutoPlayerVM.Instance.Start();
        }

        private void RefreshCmdExecute(object obj)
        {
            MainTaskMgr.Instance.InitEntityTasks(m_mainEntity);
        }

        private bool CanClearWareHouseCmdExecute(object arg)
        {
            return true;
        }

        private void ClearWareHouseCmdExecute(object obj)
        {
            m_mainEntity.AddTask(MainTaskMgr.Instance.GetTask(WlyTaskType.清理仓库), DateTime.Now);
        }

        private void CaptureCmdExecute(object obj)
        {
            m_mainEntity.Capture();
        }

        private void ExploreCmdExecute(object obj)
        {
            m_mainEntity.AddTask(MainTaskMgr.Instance.GetTask(WlyTaskType.古城探险), DateTime.Now);
        }

        private void RebuildCmdExecute(object obj)
        {
            m_mainEntity.AddTask(MainTaskMgr.Instance.GetTask(WlyTaskType.装备重铸), DateTime.Now);
        }

        private bool CanRebuildCmdExecute(object arg)
        {
            return true;
        }

        /// <summary>
        /// 小号重置
        /// </summary>
        private ICommand m_resetCmd = null;

        /// <summary>
        /// 小号重置
        /// </summary>
        public ICommand ResetCmd
        {
            get
            {
                if (m_resetCmd == null)
                {
                    m_resetCmd = WxCommandFactory.CreateCommand(ResetCmdExecute, CanResetCmdExecute);
                }

                return m_resetCmd;
            }
        }

        private void ResetCmdExecute(object obj)
        {
            SubPlayerVM.ResetSubList();
        }

        private bool CanResetCmdExecute(object arg)
        {
            return true;
        }

        private void PauseCmdExecute(object obj)
        {
        }

        #endregion
    }
}