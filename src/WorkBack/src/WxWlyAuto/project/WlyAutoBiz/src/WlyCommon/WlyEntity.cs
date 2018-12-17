// *******************************************************************
// * 文件名称： WlyEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-22 21:19:25
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

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.UICommon;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 挂机实体基类
    /// </summary>
    public abstract class WlyEntity : WxUIEntity
    {
        #region Fields

        /// <summary>
        /// 账号信息
        /// </summary>
        private readonly WlyAccountInfo m_accountInfo;

        private readonly TimeSpan m_restartInterval = TimeSpan.FromMinutes(10);

        /// <summary>
        /// 心跳时间
        /// </summary>
        private DateTime m_aliveTime;

        /// <summary>
        /// 日常任务列表
        /// </summary>
        private IList<WlyTaskRunner> m_dailyList = new List<WlyTaskRunner>();

        private bool m_final;

        /// <summary>
        /// 收尾任务列表
        /// </summary>
        private IList<WlyTaskRunner> m_finalList = new List<WlyTaskRunner>();

        private bool m_isDevelop;

        /// <summary>
        /// 主线任务列表
        /// </summary>
        private IList<WlyTaskRunner> m_mainList = new List<WlyTaskRunner>();

        /// <summary>
        /// 运行状态
        /// </summary>
        private bool m_run;

        /// <summary>
        /// 窗口句柄
        /// </summary>
        private int m_wndHwnd;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public WlyEntity(WlyAccountInfo accountInfo)
        {
            m_accountInfo = accountInfo;
        }

        #endregion

        #region Events

        /// <summary>
        /// 描述变更
        /// </summary>
        public event EventHandler<string> DescriptionChanged;

        /// <summary>
        /// 挂机停止
        /// </summary>
        public event EventHandler Stopped;

        #endregion

        #region Public Properties

        /// <summary>
        /// 账号
        /// </summary>
        public string Account => m_accountInfo.Account;

        /// <summary>
        /// 账号信息
        /// </summary>
        public WlyAccountInfo AccountInfo
        {
            get { return m_accountInfo; }
        }

        /// <summary>
        /// 当前正在进行的任务
        /// </summary>
        public WlyTaskRunner CurrentRunner { get; private set; }

        /// <summary>
        /// 大漠实体
        /// </summary>
        public string DMGuid { get; protected set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password => m_accountInfo.Password;

        /// <summary>
        /// 运行状态
        /// </summary>
        public bool Run
        {
            get { return m_run || (EntityProcess != null); }
        }

        /// <summary>
        /// 任务信息字典
        /// </summary>
        public IDictionary<string, WlyTaskInfo> TaskInfoDict { get; set; } = new Dictionary<string, WlyTaskInfo>();

        /// <summary>
        /// 任务列表，供界面显示
        /// </summary>
        public ObservableCollection<WlyTaskRunner> TaskList { get; } = new ObservableCollection<WlyTaskRunner>();

        /// <summary>
        /// 窗口句柄
        /// </summary>
        public int WndHwnd
        {
            get { return m_wndHwnd; }
            set
            {
                if (m_wndHwnd != value)
                {
                    m_wndHwnd = value;
                    OnPropertyChanged(nameof(WndHwnd));
                }
            }
        }

        #endregion

        #region Protected Internal Properties

        /// <summary>
        /// 实体使用的进程
        /// </summary>
        protected internal Process EntityProcess { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 增加一个任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="startTime"></param>
        public void AddTask(WlyTaskBase task, DateTime startTime)
        {
            if ((m_dailyList.FirstOrDefault(o => o.Task.ID == task.ID) != null) || (m_mainList.FirstOrDefault(o => o.Task.ID == task.ID) != null))
            {
                return;
            }

            if (!task.Filter(this))
            {
                return;
            }

            if (task is WlyTimeTask || task is WlyDailyTask)
            {
                var info = m_accountInfo.GetTaskInfo(task.ID);
                if (!info.IsComplete || (info.CompleteTime < startTime))
                {
                    if (task is WlyFinalTask)
                    {
                        if (m_finalList.FirstOrDefault(o => o.Task.ID == task.ID) == null)
                        {
                            m_finalList.Add(new WlyTaskRunner(task, startTime > info.NextRunTime ? startTime : info.NextRunTime));
                        }
                    }
                    else if (m_dailyList.FirstOrDefault(o => o.Task.ID == task.ID) == null)
                    {
                        m_dailyList.Add(new WlyTaskRunner(task, startTime > info.NextRunTime ? startTime : info.NextRunTime));
                        m_dailyList = m_dailyList.OrderBy(o => o.StartTime).ToList();
                    }
                }
            }
            else if (task is WlyMainTask)
            {
                var info = m_accountInfo.GetTaskInfo(task.ID);
                m_mainList.Add(new WlyTaskRunner(task, startTime > info.NextRunTime ? startTime : info.NextRunTime));
            }

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TaskList.Clear();
                    foreach (var t in m_dailyList.ToList())
                    {
                        TaskList.Add(t);
                    }

                    foreach (var t in m_mainList.ToList())
                    {
                        TaskList.Add(t);
                    }
                });
            }
        }

        /// <summary>
        /// 截图
        /// </summary>
        public void Capture(string description = "")
        {
            var fileName = $"wly{description}{DateTime.Now:yyyyMMddHHmmss}.bmp";
            if (!Directory.Exists("capture"))
            {
                Directory.CreateDirectory("capture");
            }

            if (string.IsNullOrEmpty(DMGuid))
            {
                return;
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "capture", fileName);
            DMService.Instance.Capture(DMGuid, WlyUtilityBiz.GameWndRect, filePath);
        }

        public void ClearTasks(IEnumerable<string> tasklist)
        {
            foreach (var info in m_accountInfo.TaskInfoDict.Values.Select(o => o.ID).ToList())
            {
                if (!tasklist.Contains(info))
                {
                    m_accountInfo.TaskInfoDict.Remove(info);
                }
            }

            Save();
        }

        /// <summary>
        /// 重启游戏
        /// </summary>
        public void Restart()
        {
            InternalRestart();
        }

        /// <summary>
        /// 挂机开始
        /// </summary>
        public void Start()
        {
            Task.Run(() => AutoPlayThread());
        }

        /// <summary>
        /// 结束挂机
        /// </summary>
        public void Stop()
        {
            m_run = false;
        }

        #endregion

        #region Protected Methods

        protected abstract void InternalBackupWorkThread();

        /// <summary>
        /// 要求计算任务列表
        /// </summary>
        protected abstract void InternalInitTasks();

        protected virtual void InternalAfterLogin()
        {
        }

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="errorCount"></param>
        protected virtual void InternalHandleError(int errorCount)
        {
        }

        protected virtual void InternalRestart()
        {
        }

        protected virtual void OnDescriptionChanged(string description)
        {
            DescriptionChanged?.Invoke(this, description);
        }

        /// <summary>
        /// 绑定游戏窗口
        /// </summary>
        protected void BindGameWnd()
        {
            DMGuid = DMService.Instance.CreateDMSoft();
            try
            {
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    var hwnd = DMService.Instance.FindWindowByProcessId(DMGuid, EntityProcess.Id, "ActiveX", "");
                    var state = DMService.Instance.GetWindowState(DMGuid, hwnd, 2);
                    if (state == 0)
                    {
                        Thread.Sleep(500);
                        return false;
                    }

                    DMService.Instance.BindWindow(DMGuid, hwnd);
                    WndHwnd = hwnd;
                    return true;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException("无法绑定游戏窗口");
                }
            }
            catch
            {
                DMService.Instance.ReleaseDMSoft(DMGuid);
                throw;
            }
        }

        /// <summary>
        /// 为当前错误截图
        /// </summary>
        /// <param name="ex"></param>
        protected void CaptureError(Exception ex)
        {
            CaptureError(ex.Message);
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        protected void CloseProcess()
        {
            try
            {
                if (EntityProcess != null)
                {
                    EntityProcess.Kill();
                    EntityProcess.Close();
                    EntityProcess.Dispose();
                    EntityProcess = null;
                }
            }
            catch (Exception ex)
            {
                WxLog.Error($"WlyEntity.CloseProcess Error <{ex}>");
            }

            if (!string.IsNullOrEmpty(DMGuid))
            {
                DMService.Instance.ReleaseDMSoft(DMGuid);
                DMGuid = null;
            }
        }

        protected void DevelopCity()
        {
            var runner = m_mainList.FirstOrDefault(o => o.Task.ID == "be18cfefa68743c78d6992580360491e");
            if (runner != null)
            {
                var info = runner.Task.Run(this);
                SaveTaskInfo(info);
                m_isDevelop = true;
            }
        }

        protected void KeepAlive()
        {
            m_aliveTime = DateTime.Now;
        }

        /// <summary>
        /// 移除一个任务
        /// </summary>
        /// <param name="task"></param>
        protected void RemoveTaskRunner(WlyTaskRunner task)
        {
            if (m_mainList.Remove(task))
            {
                InternalInitTasks();
            }
            else
            {
                m_dailyList.Remove(task);
            }

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { TaskList.Remove(task); }));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 挂机线程
        /// </summary>
        private void AutoPlayThread()
        {
            m_run = true;
            m_final = true;
            m_isDevelop = false;
            var errorCount = 0;

            // 最外层死循环，进程守护
            while (m_run)
            {
                try
                {
                    // 登录
                    OnDescriptionChanged($"开始登录 {Account}");
                    DMGuid = string.Empty;
                    WlyUtilityBiz.Login(this);

                    // 绑定游戏窗口
                    BindGameWnd();
                    InternalAfterLogin();

                    // 登录成功，等待登录完成
                    OnDescriptionChanged("等待加载完成");
                    WlyViewMgr.GoTo(DMGuid, WlyViewType.场景_主界面, TimeSpan.FromSeconds(2));

                    // 开始挂机
                    m_aliveTime = DateTime.Now;
                    while (m_run)
                    {
                        if (DMService.Instance.GetWindowState(DMGuid, WndHwnd, 0) == 0)
                        {
                            WxLog.Error($"WlyEntity.AutoPlayThread  游戏窗口不存在");
                            break;
                        }

                        WlyTaskRunner runner = null;

                        // 检测是否有可以运行的日常任务
                        if (m_dailyList.Any() && (m_dailyList.First().StartTime < DateTime.Now))
                        {
                            runner = m_dailyList.First();
                            OnDescriptionChanged(runner.Task.MainTitle);
                            CurrentRunner = runner;
                            var info = runner.Task.Run(this);
                            WxLog.Debug($"WlyEntity.AutoPlayThread Name <{this}> Run <{runner.Task}> Result <{info}>");

                            // 无论结果，先进行保存
                            SaveTaskInfo(info);
                            RemoveTaskRunner(runner);
                            if (!info.IsComplete)
                            {
                                // 未完成的任务重新加入，等待下一次运行
                                AddTask(runner.Task, info.NextRunTime);
                            }

                            m_aliveTime = DateTime.Now;
                            continue;
                        }

                        // 检测是否有可以运行的主线任务
                        runner = m_mainList.FirstOrDefault(o => o.StartTime < DateTime.Now);
                        if (runner != null)
                        {
                            OnDescriptionChanged(runner.Task.MainTitle);
                            CurrentRunner = runner;
                            var info = runner.Task.Run(this);

                            // 无论结果，先进行保存
                            SaveTaskInfo(info);
                            RemoveTaskRunner(runner);
                            if (!info.IsComplete)
                            {
                                // 未完成的任务重新加入，等待下一次运行
                                AddTask(runner.Task, info.NextRunTime);
                            }

                            m_aliveTime = DateTime.Now;
                            continue;
                        }

                        CurrentRunner = null;

                        // 运行空闲任务
                        InternalBackupWorkThread();

                        // 超时重启
                        if (DateTime.Now - m_aliveTime > m_restartInterval)
                        {
                            break;
                        }
                    }

                    while (m_finalList.Any(o => o.StartTime < DateTime.Now))
                    {
                        var runner = m_finalList.FirstOrDefault(o => o.StartTime < DateTime.Now);
                        if (runner == null)
                        {
                            break;
                        }

                        OnDescriptionChanged(runner.Task.MainTitle);
                        CurrentRunner = runner;
                        var info = runner.Task.Run(this);
                        WxLog.Debug($"WlyEntity.AutoPlayThread Name <{this}> Run <{runner.Task}> Result <{info}>");

                        // 无论结果，先进行保存
                        SaveTaskInfo(info);
                        m_finalList.Remove(runner);
                        if (!info.IsComplete)
                        {
                            // 未完成的任务重新加入，等待下一次运行
                            AddTask(runner.Task, info.NextRunTime);
                        }

                        m_aliveTime = DateTime.Now;
                    }

                    m_final = false;
                }
                catch (InvalidOperationException ex)
                {
                    WxLog.Error($"WlyEntity.InternalBackupWorkThread Error {ex}");
                    errorCount++;
                    InternalHandleError(errorCount);

                    //CaptureError(ex);
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WlyEntity.AutoPlayThread Error <{ex}>");
                }
                finally
                {
                    CloseProcess();
                }
            }

            WxLog.Debug($"WlyEntity.AutoPlayThread Stopped, Hwnd <{WndHwnd}>, Run <{Run}>");
            OnStopped();
        }

        /// <summary>
        /// 为当前错误截图
        /// </summary>
        /// <param name="message"></param>
        private void CaptureError(string message)
        {
            if (string.IsNullOrEmpty(DMGuid))
            {
                return;
            }

            var fileName = $"Error{DateTime.Now:yyyyMMddHHmmss}_{message}.bmp";
            if (!Directory.Exists("error"))
            {
                Directory.CreateDirectory("error");
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error", fileName);
            try
            {
                DMService.Instance.Capture(DMGuid, WlyUtilityBiz.GameWndRect, filePath);
            }
            catch (Exception ex)
            {
                WxLog.Error($"WlyEntity.CaptureError <{ex}>");
            }
        }

        private void OnStopped()
        {
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 保存账号信息
        /// </summary>
        private void Save()
        {
            m_accountInfo.Save();
        }

        /// <summary>
        /// 保存任务信息
        /// </summary>
        /// <param name="info"></param>
        private void SaveTaskInfo(WlyTaskInfo info)
        {
            if (!m_accountInfo.TaskInfoDict.ContainsKey(info.ID))
            {
                m_accountInfo.TaskInfoDict.Add(info.ID, info);
            }
            else
            {
                m_accountInfo.TaskInfoDict[info.ID] = info;
            }

            Save();
        }

        #endregion
    }
}