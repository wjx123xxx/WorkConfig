// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MainWndVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-29 00:21:10
// *******************************************************************

namespace Wx.App.EasyBaiduMap.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Wx.App.EasyBaiduMap.Common;
    using Wx.App.EasyBaiduMap.Wnd;
    using Wx.Common.BaiduMap.Biz;
    using Wx.Common.BaiduMap.Entity;

    /// <summary>
    /// 主窗口VM
    /// </summary>
    public class MainWndVM : NotifyEntity
    {
        #region Fields

        /// <summary>
        /// 下載任務實體
        /// </summary>
        private DistrictTask m_districtTask;

        /// <summary>
        /// 是否在下載中
        /// </summary>
        private bool m_isDownloading;

        /// <summary>
        /// 最大層級
        /// </summary>
        private int m_maxZoom;

        /// <summary>
        /// 最小層級
        /// </summary>
        private int m_minZoom;

        /// <summary>
        /// 當前選擇的行政區
        /// </summary>
        private string m_selectedDistrict;

        /// <summary>
        /// 下載任務數量
        /// </summary>
        private long m_taskCount;

        #endregion

        #region Constructors

        public MainWndVM()
        {
            AddDistrictCommand = CommandFactory.CreateCommand(AddDistrictExecute);
            BeginDownloadCommand = CommandFactory.CreateCommand(BeginDownloadExecute);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 設置行政區命令
        /// </summary>
        public ICommand AddDistrictCommand { get; private set; }

        /// <summary>
        /// 開始下載命令
        /// </summary>
        public ICommand BeginDownloadCommand { get; private set; }

        /// <summary>
        /// 行政區控件VM
        /// </summary>
        public MapDistrictVM District { get; set; } = new MapDistrictVM();

        /// <summary>
        /// 已緩存的行政區列表
        /// </summary>
        public ObservableCollection<string> DistrictNames => ApplicationContext.Instance.DataPreserveService.DistrictNames;

        /// <summary>
        /// 下載任務實體
        /// </summary>
        public DistrictTask DistrictTask
        {
            get
            {
                return m_districtTask;
            }
            set
            {
                m_districtTask = value;
                OnPropertyChanged(nameof(DistrictTask));
            }
        }

        /// <summary>
        /// 是否在下載中
        /// </summary>
        public bool IsDownloading
        {
            get
            {
                return m_isDownloading;
            }
            set
            {
                m_isDownloading = value;
                OnPropertyChanged(nameof(IsDownloading));
            }
        }

        /// <summary>
        /// 最大層級
        /// </summary>
        public int MaxZoom
        {
            get
            {
                return m_maxZoom;
            }
            set
            {
                m_maxZoom = value;
                OnPropertyChanged(nameof(MaxZoom));

                TaskCount = DistrictTask.GetTaskCount(MinZoom, MaxZoom);
            }
        }

        /// <summary>
        /// 最小層級
        /// </summary>
        public int MinZoom
        {
            get
            {
                return m_minZoom;
            }
            set
            {
                m_minZoom = value;
                OnPropertyChanged(nameof(MinZoom));

                TaskCount = DistrictTask.GetTaskCount(MinZoom, MaxZoom);
            }
        }

        /// <summary>
        /// 當前選擇的行政區
        /// </summary>
        public string SelectedDistrict
        {
            get
            {
                return m_selectedDistrict;
            }
            set
            {
                if (m_selectedDistrict == value)
                {
                    return;
                }

                m_selectedDistrict = value;
                OnPropertyChanged(nameof(SelectedDistrict));

                // 先将原先的任务置空
                DistrictTask = null;

                // 讀取數據并呈現
                var district = ApplicationContext.Instance.DataPreserveService.GetDistrict(value);
                District.SetRegionData(district);
                DistrictTask = district.GetTask();
                MinZoom = DistrictTask.SupportZooms.Min();
                MaxZoom = DistrictTask.SupportZooms.Max();
                TaskCount = DistrictTask.GetTaskCount(MinZoom, MaxZoom);
            }
        }

        /// <summary>
        /// 下載任務數量
        /// </summary>
        public long TaskCount
        {
            get
            {
                return m_taskCount;
            }
            set
            {
                m_taskCount = value;
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        /// <summary>
        /// 支持層級列表
        /// </summary>
        public IEnumerable<int> Zooms
        {
            get
            {
                var zooms = new List<int>();
                for (var index = 1; index < 20; index++)
                {
                    zooms.Add(index);
                }

                return zooms;
            }
        }

        #endregion

        #region Private Methods

        private void AddDistrictExecute(object obj)
        {
            var vm = new SetRegionVM();
            var wnd = new SetRegionWnd
            {
                DataContext = vm
            };
            if (wnd.ShowDialog() != true)
            {
                return;
            }

            // 保存行政區信息
            var district = new District(vm.RegionName, vm.RegionData);
            ApplicationContext.Instance.DataPreserveService.CacheDistrict(district);

            // 設置當前行政區為新增的行政區
            SelectedDistrict = vm.RegionName;
        }

        private void BeginDownloadExecute(object obj)
        {
            IsDownloading = true;
        }

        #endregion
    }
}