// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapDownloader.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-28 16:49:58
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading;

using Wx.App.BaiduMapGo.Base;
using Wx.App.BaiduMapGo.Entity;

namespace Wx.App.BaiduMapGo.Common
{
    /// <summary>
    /// 地图下载器
    /// </summary>
    public class MapDownloader
    {
        #region Constants

        /// <summary>
        /// 经验值，平均每个瓦片大小为6KB
        /// </summary>
        private const long AverageTileSize = 6 * 1024;

        #endregion

        #region Fields

        /// <summary>
        /// 已完成数量
        /// </summary>
        private long m_completeCount = 0;

        /// <summary>
        /// 下载队列
        /// </summary>
        private ConcurrentQueue<MapLine> m_taskQueue;

        /// <summary>
        /// 下载总数量
        /// </summary>
        private long m_totalCount;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public MapDownloader(RegionTask task)
        {
            m_totalCount = task.TaskCount;
            var tasks = Enumerable.Empty<MapLine>();
            foreach (var t in task)
            {
                tasks = tasks.Concat(t);
            }

            m_taskQueue = new ConcurrentQueue<MapLine>(tasks);
            var test = m_taskQueue.Sum(o => o.TaskCount);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 下载完成标识
        /// </summary>
        public bool IsComplete => false;

        /// <summary>
        /// 预计下载大小
        /// </summary>
        public string TotalSize { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取下载状态描述
        /// </summary>
        public string GetDescription()
        {
            var size = ConvertSizeToDescription((m_totalCount - m_completeCount) * AverageTileSize);
            return $"{DateTime.Now} 剩余大小: {size} 网速:{1} 进度:{(m_completeCount * 1.0) / m_totalCount:F3}%";
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        public void Start()
        {
            ServicePointManager.DefaultConnectionLimit = 512;

            // 创建20条线程
            for (var index = 0; index < 20; index++)
            {
                var i = index;
                new Action(() => DownloadThread(i)).BeginInvoke(null, null);
            }
        }

        #endregion

        #region Private Methods

        private string ConvertSizeToDescription(double totalSize)
        {
            var size = "KB";
            totalSize = totalSize / 1024;
            if (totalSize > 1024)
            {
                totalSize = totalSize / 1024;
                size = "MB";
            }
            if (totalSize > 1024)
            {
                totalSize = totalSize / 1024;
                size = "GB";
            }
            return $"{totalSize:F2}{size}";
        }

        /// <summary>
        /// 线程序号
        /// </summary>
        /// <param name="index"></param>
        private void DownloadThread(int index)
        {
            while (m_taskQueue.Any())
            {
                if (m_taskQueue.TryDequeue(out var mapLine))
                {
                }
                else
                {
                    // 获取失败则重试
                    Thread.Sleep(1000);
                }
            }
        }

        #endregion
    }
}