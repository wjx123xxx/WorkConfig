// *******************************************************************
// * 文件名称： MainWindowViewModel.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-11 23:07:18
// *******************************************************************

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

using Wx.Utility.UICommon;

namespace DevelopHelper
{
    /// <summary>
    /// 主窗口vm
    /// </summary>
    public class MainWindowViewModel : WxUIEntity
    {
        #region Fields

        /// <summary>
        /// 截图
        /// </summary>
        private ICommand m_captureCmd;

        /// <summary>
        /// 窗口类名
        /// </summary>
        private string m_className;

        /// <summary>
        /// 进程ID
        /// </summary>
        private int m_processID;

        /// <summary>
        /// 选择的窗口句柄
        /// </summary>
        private HwndEntity m_selectedEntity;

        /// <summary>
        /// 开始分析
        /// </summary>
        private ICommand m_startCmd;

        #endregion

        #region Public Properties

        /// <summary>
        /// 截图
        /// </summary>
        public ICommand CaptureCmd
        {
            get
            {
                if (m_captureCmd == null)
                {
                    m_captureCmd = WxCommandFactory.CreateCommand(CaptureCmdExecute, CanCaptureCmdExecute);
                }

                return m_captureCmd;
            }
        }

        /// <summary>
        /// 窗口类名
        /// </summary>
        public string ClassName
        {
            get { return m_className; }
            set
            {
                m_className = value;
                OnPropertyChanged(nameof(ClassName));
            }
        }

        public ObservableCollection<HwndEntity> HwndList { get; set; } = new ObservableCollection<HwndEntity>();

        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessID
        {
            get { return m_processID; }
            set
            {
                m_processID = value;
                OnPropertyChanged(nameof(ProcessID));
            }
        }

        /// <summary>
        /// 选择的窗口句柄
        /// </summary>
        public HwndEntity SelectedEntity
        {
            get { return m_selectedEntity; }
            set
            {
                m_selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }

        /// <summary>
        /// 开始分析
        /// </summary>
        public ICommand StartCmd
        {
            get
            {
                if (m_startCmd == null)
                {
                    m_startCmd = WxCommandFactory.CreateCommand(StartCmdExecute, CanStartCmdExecute);
                }

                return m_startCmd;
            }
        }

        #endregion

        #region Private Methods

        private bool CanCaptureCmdExecute(object arg)
        {
            return true;
        }

        private bool CanStartCmdExecute(object arg)
        {
            return true;
        }

        private void CaptureCmdExecute(object obj)
        {
            var dm = DMFactory.Instance.CreateDMSoft();
            DMFactory.Instance.BindWindow(dm, SelectedEntity.Hwnd);
            var result = dm.Capture(0, 0, 2000, 2000, "capture.png");
            MessageBox.Show(Application.Current.MainWindow, $"截图 {result}");
        }

        private void StartCmdExecute(object obj)
        {
            var dm = DMFactory.Instance.CreateDMSoft();
            var result = dm.EnumWindowByProcessId(ProcessID, "", ClassName, 2);
            HwndList.Clear();
            foreach (var h in result.Split(','))
            {
                var hwnd = int.Parse(h);
                HwndList.Add(new HwndEntity
                {
                    Hwnd = hwnd,
                    ClassName = dm.GetWindowClass(hwnd)
                });
            }
        }

        #endregion
    }
}