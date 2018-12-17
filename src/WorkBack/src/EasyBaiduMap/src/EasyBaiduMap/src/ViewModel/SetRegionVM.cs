// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： SetRegionVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-31 23:42:09
// *******************************************************************

namespace Wx.App.EasyBaiduMap.ViewModel
{
    using Wx.App.EasyBaiduMap.Common;

    /// <summary>
    /// 設置行政區域VM
    /// </summary>
    public class SetRegionVM : NotifyEntity
    {
        #region Fields

        private string m_regionData;

        private string m_regionName;

        #endregion

        #region Public Properties

        /// <summary>
        /// 行政區數據
        /// </summary>
        public string RegionData
        {
            get
            {
                return m_regionData;
            }
            set
            {
                m_regionData = value;
                OnPropertyChanged(nameof(RegionData));
            }
        }

        /// <summary>
        /// 行政區名稱
        /// </summary>
        public string RegionName
        {
            get
            {
                return m_regionName;
            }
            set
            {
                m_regionName = value;
                OnPropertyChanged(nameof(RegionName));
            }
        }

        #endregion
    }
}