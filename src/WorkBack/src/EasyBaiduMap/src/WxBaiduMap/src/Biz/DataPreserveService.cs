// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： DataPreserveService.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-02 10:22:06
// *******************************************************************

namespace Wx.Common.BaiduMap.Biz
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;

    using Wx.Common.BaiduMap.Entity;

    /// <summary>
    /// 數據持久化服務
    /// </summary>
    public class DataPreserveService
    {
        #region Constants

        /// <summary>
        /// 元數據文件名稱
        /// </summary>
        private const string OriginDataFile = "origin.wx";

        #endregion

        #region Fields

        /// <summary>
        /// 數據緩存目錄
        /// </summary>
        private string m_dir;

        #endregion

        #region Public Properties

        /// <summary>
        /// 行政區名稱集合
        /// </summary>
        public ObservableCollection<string> DistrictNames { get; } = new ObservableCollection<string>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 緩存指定的行政區信息
        /// </summary>
        /// <param name="district"></param>
        public void CacheDistrict(District district)
        {
            // 創建文件夾
            var districtDir = Path.Combine(m_dir, district.RegionName);
            if (!Directory.Exists(districtDir))
            {
                Directory.CreateDirectory(districtDir);
            }

            // 保存原始區域數據
            var originFile = Path.Combine(districtDir, OriginDataFile);
            File.WriteAllText(originFile, district.RegionData);

            // 加入新數據到列表
            DistrictNames.Add(district.RegionName);
        }

        /// <summary>
        /// 是否存在指定名稱的行政區
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExistDistrict(string name)
        {
            return DistrictNames.Contains(name);
        }

        /// <summary>
        /// 根據名稱獲取對應的行政區
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public District GetDistrict(string name)
        {
            if (!DistrictNames.Contains(name))
            {
                throw new InvalidOperationException();
            }

            var districtDir = Path.Combine(m_dir, name);
            if (!Directory.Exists(districtDir))
            {
                throw new FileNotFoundException("指定行政區數據已被刪除，請重啟應用程序");
            }

            // 讀取相關數據
            var data = File.ReadAllText(Path.Combine(districtDir, OriginDataFile));
            var district = new District(name, data);
            return district;
        }

        /// <summary>
        /// 加載數據
        /// </summary>
        public void Load()
        {
            m_dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            var dirs = Directory.GetDirectories(m_dir);
            foreach (var dir in dirs)
            {
                var di = new DirectoryInfo(dir);
                DistrictNames.Add(di.Name);
            }
        }

        #endregion
    }
}