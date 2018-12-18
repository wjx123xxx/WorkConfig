// *******************************************************************
// * 文件名称： AutoPlayerVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-27 22:59:50
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.UICommon;

namespace Wx.App.WlySubAutoPlayer.VM
{
    /// <summary>
    /// 自动小号界面VM
    /// </summary>
    public class AutoPlayerVM : WxUIEntity
    {
        #region Fields

        private static readonly object _instanceLocker = new object();

        private static AutoPlayerVM _instance;

        private readonly AutoResetEvent m_signal = new AutoResetEvent(false);

        /// <summary>
        /// 同时挂机的小号数量
        /// </summary>
        private int m_autoPlayCount;

        /// <summary>
        /// Comment
        /// </summary>
        private bool m_closeDWM = true;

        private int m_currentIndex;

        private IList<string> m_errorAccounts;

        /// <summary>
        /// 100
        /// </summary>
        private int m_level100;

        /// <summary>
        /// 100
        /// </summary>
        private int m_level100R;

        /// <summary>
        /// 110
        /// </summary>
        private int m_level110;

        /// <summary>
        /// 110R
        /// </summary>
        private int m_level110R;

        /// <summary>
        /// Comment
        /// </summary>
        private int m_level20;

        /// <summary>
        /// 20
        /// </summary>
        private int m_level20R;

        /// <summary>
        /// 40
        /// </summary>
        private int m_level40;

        /// <summary>
        /// 40
        /// </summary>
        private int m_level40R;

        /// <summary>
        /// 60
        /// </summary>
        private int m_level60;

        /// <summary>
        /// 60
        /// </summary>
        private int m_level60R;

        /// <summary>
        /// 80
        /// </summary>
        private int m_level80;

        /// <summary>
        /// 80
        /// </summary>
        private int m_level80R;

        private bool m_run;

        /// <summary>
        /// 选择的小号
        /// </summary>
        private SubEntityWrapper m_selectedWrapper;

        /// <summary>
        /// StartIndex
        /// </summary>
        private string m_startIndex;

        private IList<SubEntity> m_subList = new List<SubEntity>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private AutoPlayerVM()
        {
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "sub");
            if (Directory.Exists(dataDir))
            {
                var dataFiles = Directory.GetFiles(dataDir);
                foreach (var file in dataFiles)
                {
                    var sub = SubAccountInfo.LoadAccount(file);
                    var info = sub.GetTaskInfo("ebed05aaae1142adbfdc68a79026bc32");
                    if ((info.NextRunTime > DateTime.Now) && sub.Check)
                    {
                        File.Move(file, Path.Combine(dataDir, "Back", Path.GetFileName(file)));
                    }
                    else if ((sub.NextLoginTime > DateTime.Now) && !sub.Check)
                    {
                        File.Move(file, Path.Combine(dataDir, "Invalid", Path.GetFileName(file)));
                    }

                    if (!sub.GetTaskInfo("a989d3788be04141ae050185fb749d1b").IsComplete
                        && sub.GetTaskInfo("2738a73cc77a496983647a1717a9ca10").IsComplete)
                    {
                        sub.NextLoginTime = DateTime.MinValue;
                    }

                    var subEntity = new SubEntity(sub);
                    m_subList.Add(subEntity);
                    subEntity.Stopped += EntityOnStopped;
                }

                m_currentIndex = WlyUtilityBiz.SystemInfo.CurrentIndex;
            }

            m_subList = m_subList.OrderBy(o => o.Index).ToList();
            AutoPlayCount = 3;
            CalculateLevel();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static AutoPlayerVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new AutoPlayerVM();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 同时挂机的小号数量
        /// </summary>
        public int AutoPlayCount
        {
            get { return m_autoPlayCount; }
            set
            {
                m_autoPlayCount = value;
                OnPropertyChanged(nameof(AutoPlayCount));
            }
        }

        /// <summary>
        /// Comment
        /// </summary>
        public bool CloseDWM
        {
            get { return m_closeDWM; }
            set
            {
                if (m_closeDWM != value)
                {
                    m_closeDWM = value;
                    OnPropertyChanged(nameof(CloseDWM));
                }
            }
        }

        /// <summary>
        /// 100
        /// </summary>
        public int Level100
        {
            get { return m_level100; }
            set
            {
                if (m_level100 != value)
                {
                    m_level100 = value;
                    OnPropertyChanged(nameof(Level100));
                }
            }
        }

        /// <summary>
        /// 100
        /// </summary>
        public int Level100R
        {
            get { return m_level100R; }
            set
            {
                if (m_level100R != value)
                {
                    m_level100R = value;
                    OnPropertyChanged(nameof(Level100R));
                }
            }
        }

        /// <summary>
        /// 110
        /// </summary>
        public int Level110
        {
            get { return m_level110; }
            set
            {
                m_level110 = value;
                OnPropertyChanged(nameof(Level110));
            }
        }

        /// <summary>
        /// 110R
        /// </summary>
        public int Level110R
        {
            get { return m_level110R; }
            set
            {
                m_level110R = value;
                OnPropertyChanged(nameof(Level110R));
            }
        }

        /// <summary>
        /// Comment
        /// </summary>
        public int Level20
        {
            get { return m_level20; }
            set
            {
                if (m_level20 != value)
                {
                    m_level20 = value;
                    OnPropertyChanged(nameof(Level20));
                }
            }
        }

        /// <summary>
        /// 20
        /// </summary>
        public int Level20R
        {
            get { return m_level20R; }
            set
            {
                if (m_level20R != value)
                {
                    m_level20R = value;
                    OnPropertyChanged(nameof(Level20R));
                }
            }
        }

        /// <summary>
        /// 40
        /// </summary>
        public int Level40
        {
            get { return m_level40; }
            set
            {
                if (m_level40 != value)
                {
                    m_level40 = value;
                    OnPropertyChanged(nameof(Level40));
                }
            }
        }

        /// <summary>
        /// 40
        /// </summary>
        public int Level40R
        {
            get { return m_level40R; }
            set
            {
                if (m_level40R != value)
                {
                    m_level40R = value;
                    OnPropertyChanged(nameof(Level40R));
                }
            }
        }

        /// <summary>
        /// 60
        /// </summary>
        public int Level60
        {
            get { return m_level60; }
            set
            {
                if (m_level60 != value)
                {
                    m_level60 = value;
                    OnPropertyChanged(nameof(Level60));
                }
            }
        }

        /// <summary>
        /// 60
        /// </summary>
        public int Level60R
        {
            get { return m_level60R; }
            set
            {
                if (m_level60R != value)
                {
                    m_level60R = value;
                    OnPropertyChanged(nameof(Level60R));
                }
            }
        }

        /// <summary>
        /// 80
        /// </summary>
        public int Level80
        {
            get { return m_level80; }
            set
            {
                if (m_level80 != value)
                {
                    m_level80 = value;
                    OnPropertyChanged(nameof(Level80));
                }
            }
        }

        /// <summary>
        /// 80
        /// </summary>
        public int Level80R
        {
            get { return m_level80R; }
            set
            {
                if (m_level80R != value)
                {
                    m_level80R = value;
                    OnPropertyChanged(nameof(Level80R));
                }
            }
        }

        /// <summary>
        /// 选择的小号
        /// </summary>
        public SubEntityWrapper SelectedWrapper
        {
            get { return m_selectedWrapper; }
            set
            {
                if (m_selectedWrapper != value)
                {
                    m_selectedWrapper = value;
                    OnPropertyChanged(nameof(SelectedWrapper));
                }
            }
        }

        /// <summary>
        /// StartIndex
        /// </summary>
        public string StartIndex
        {
            get { return m_startIndex; }
            set
            {
                if (m_startIndex != value)
                {
                    m_startIndex = value;
                    OnPropertyChanged(nameof(StartIndex));
                }
            }
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        public WlySystemInfo SystemInfo => WlyUtilityBiz.SystemInfo;

        /// <summary>
        /// 正在挂机的小号
        /// </summary>
        public ObservableCollection<SubEntityWrapper> Wrappers { get; } = new ObservableCollection<SubEntityWrapper>();

        #endregion

        #region Public Methods

        public int CalculateLevel(int min, int max)
        {
            return m_subList.Count(o =>
            {
                var level = o.Info.GetBuildingInfo(WlyBuildingType.主城).Level;
                return (level >= min) && (level <= max);
            });
        }

        public int CalculateLevelR(int min, int max)
        {
            return m_subList.Count(o =>
            {
                var level = o.Info.GetBuildingInfo(WlyBuildingType.主城).Level;
                return (level >= min) && (level <= max) && (o.NextLoginTime < DateTime.Now);
            });
        }

        public string GetAvailableName()
        {
            var name = $"海潮{m_currentIndex:D4}";
            m_currentIndex++;

            while (m_subList.Any(o => o.Index == m_currentIndex))
            {
                m_currentIndex++;
            }

            WlyUtilityBiz.SystemInfo.CurrentIndex = m_currentIndex;
            WlyUtilityBiz.SystemInfo.Save();
            return name;
        }

        /// <summary>
        /// 重置小号列表的挂机时间
        /// </summary>
        public void ResetSubList()
        {
            foreach (var sub in m_subList)
            {
                sub.Reset();
            }
        }

        /// <summary>
        /// 开始小号挂机
        /// </summary>
        public void Start()
        {
            if (m_run)
            {
                return;
            }

            Task.Run(() => { BackupRun(); });
        }

        /// <summary>
        /// 停止挂机
        /// </summary>
        public void Stop()
        {
            m_run = false;
        }

        public void Test()
        {
            var entity = m_subList.FirstOrDefault(o => o.Index == 2);
            if (entity == null)
            {
                MessageBox.Show(Application.Current.MainWindow, $"未找到角色 海潮{m_startIndex:D4}");
                return;
            }

            entity.Start();

            //for (int i = 9; i < 400; i++)
            //{
            //    var entity = m_subList.FirstOrDefault(o => o.Index == i);
            //    if(entity == null)
            //        continue;

            //     entity.SpecialTask();
            //}
        }

        #endregion

        #region Private Methods

        private void BackupRun()
        {
            m_run = true;
            var startTime = DateTime.Now;
            while (m_run)
            {
                if (CloseDWM && (DateTime.Now - startTime > TimeSpan.FromHours(1)))
                {
                    var process = Process.GetProcessesByName("dwm");
                    if (process.Any())
                    {
                        process.First().Kill();
                    }

                    startTime = DateTime.Now;
                }

                foreach (var wrapper in Wrappers.ToList())
                {
                    if (!wrapper.Entity.Run)
                    {
                        if (Application.Current != null)
                        {
                            Application.Current.Dispatcher.Invoke(() => Wrappers.Remove(wrapper));
                        }
                        else
                        {
                            Wrappers.Remove(wrapper);
                        }
                    }
                }

                var now = DateTime.Now;
                var count = Wrappers.Count;
                while (count < AutoPlayCount)
                {
                    count++;

                    //var next = m_subList.FirstOrDefault(o => (o.DevelopTime < now) && (Wrappers.FirstOrDefault(w => Equals(o, w.Entity)) == null));
                    //if (next == null)
                    //{
                    var next = m_subList.FirstOrDefault(o => (o.NextLoginTime < now) && (Wrappers.FirstOrDefault(w => Equals(o, w.Entity)) == null));
                    //}

                    if (next == null)
                    {
                        break;
                    }

                    if (Application.Current != null)
                    {
                        Application.Current.Dispatcher.Invoke(() => Wrappers.Add(new SubEntityWrapper(next)));
                    }
                    else
                    {
                        Wrappers.Add(new SubEntityWrapper(next));
                    }

                    if (SelectedWrapper == null)
                    {
                        SelectedWrapper = Wrappers.FirstOrDefault();
                    }

                    next.Start();
                }

                //if (m_subList.Count < 1000)
                //{
                //    // 注册一个新账号
                //    var info = WlyUtilityBiz.Reg();
                //    var subInfo = new SubAccountInfo(MathHelper.GetNewGuid())
                //    {
                //        Account = info.Account,
                //        Password = info.Password
                //    };
                //    subInfo.Save();
                //    var entity = new SubEntity(subInfo);
                //    entity.Stopped += EntityOnStopped;
                //    m_subList.Add(entity);
                //}

                Thread.Sleep(TimeSpan.FromSeconds(10));
                m_signal.WaitOne(TimeSpan.FromSeconds(20));

                CalculateLevel();
                m_subList = m_subList.OrderBy(o => o.Index).ToList();
            }
        }

        private void CalculateLevel()
        {
            Level110 = CalculateLevel(101, 110);
            Level110R = CalculateLevelR(101, 110);

            Level100 = CalculateLevel(81, 100);
            Level100R = CalculateLevelR(81, 100);

            Level80 = CalculateLevel(61, 80);
            Level80R = CalculateLevelR(61, 80);

            Level60 = CalculateLevel(41, 60);
            Level60R = CalculateLevelR(41, 60);

            Level40 = CalculateLevel(21, 40);
            Level40R = CalculateLevelR(21, 40);

            Level20 = CalculateLevel(1, 20);
            Level20R = CalculateLevelR(1, 20);
        }

        #endregion

        #region Event Handlers

        private void EntityOnStopped(object sender, EventArgs eventArgs)
        {
            m_signal.Set();
        }

        #endregion

        #region Commands

        /// <summary>
        /// 增加挂机数量
        /// </summary>
        private ICommand m_addCmd;

        /// <summary>
        /// 处理错误账号
        /// </summary>
        private ICommand m_handleErrorCmd;

        /// <summary>
        /// 减少挂机数量
        /// </summary>
        private ICommand m_minusCmd;

        /// <summary>
        /// 启动
        /// </summary>
        private ICommand m_startIndexCmd;

        /// <summary>
        /// 增加挂机数量
        /// </summary>
        public ICommand AddCmd
        {
            get
            {
                if (m_addCmd == null)
                {
                    m_addCmd = WxCommandFactory.CreateCommand(AddCmdExecute);
                }

                return m_addCmd;
            }
        }

        /// <summary>
        /// 处理错误账号
        /// </summary>
        public ICommand HandleErrorCmd
        {
            get
            {
                if (m_handleErrorCmd == null)
                {
                    m_handleErrorCmd = WxCommandFactory.CreateCommand(HandleErrorCmdExecute, CanHandleErrorCmdExecute);
                }

                return m_handleErrorCmd;
            }
        }

        /// <summary>
        /// 减少挂机数量
        /// </summary>
        public ICommand MinusCmd
        {
            get
            {
                if (m_minusCmd == null)
                {
                    m_minusCmd = WxCommandFactory.CreateCommand(MinusCmdExecute);
                }

                return m_minusCmd;
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public ICommand StartIndexCmd
        {
            get
            {
                if (m_startIndexCmd == null)
                {
                    m_startIndexCmd = WxCommandFactory.CreateCommand(StartIndexCmdExecute);
                }

                return m_startIndexCmd;
            }
        }

        #endregion

        #region CommandExecutes

        private int m_previous = 0;
        private void StartIndexCmdExecute(object obj)
        {

            var entity = m_subList.FirstOrDefault(o => (o.Index.ToString() == m_startIndex) || (o.AccountInfo.UID == m_startIndex));
            if (entity == null)
            {
                MessageBox.Show(Application.Current.MainWindow, $"未找到角色 海潮{m_startIndex:D4}");
                var e = m_subList.FirstOrDefault(o => m_previous < o.Index && o.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level < 130);
                if (e != null)
                {
                    WlyUtilityBiz.Login(e);
                    m_previous = e.Index;
                    return;
                }

                return;
            }

            WlyUtilityBiz.Login(entity);
            m_previous = entity.Index;
        }

        private void HandleErrorCmdExecute(object obj)
        {
            if (m_errorAccounts == null)
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error", "errorAccount.data");
                m_errorAccounts = File.ReadAllLines(file).ToList();
            }

            var uid = m_errorAccounts.FirstOrDefault();
            if (uid != null)
            {
                var entity = m_subList.FirstOrDefault(o => o.AccountInfo.UID == uid);
                if (entity == null)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"未找到角色 海潮{m_startIndex:D4}");
                    return;
                }

                WlyUtilityBiz.Login(entity);
                m_errorAccounts.Remove(uid);
            }
            else
            {
                MessageBox.Show(Application.Current.MainWindow, $"错误处理完毕");
            }
        }

        private bool CanHandleErrorCmdExecute(object arg)
        {
            return true;
        }

        private void AddCmdExecute(object obj)
        {
            AutoPlayCount++;
        }

        private void MinusCmdExecute(object obj)
        {
            AutoPlayCount--;
        }

        #endregion
    }
}