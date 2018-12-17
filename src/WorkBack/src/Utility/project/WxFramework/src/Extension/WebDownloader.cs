// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WebDownloader.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-23 16:38:04
// * 文件版本： 1.0.0.0
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Wx.Common.Net.Downloader
{
    /// <summary>
    /// 网页下载器
    /// </summary>
    public class WebDownloader
    {
        #region Constants

        public const string USER_AGENT = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private const int INIT_COUNT = 30;

        #endregion

        #region Fields

        private static readonly object _instanceLocker = new object();

        private static WebDownloader _instance;

        private readonly Semaphore m_poolSemaphore = new Semaphore(INIT_COUNT, 1000);

        private readonly object m_queueLocker = new object();

        private readonly Queue<WebClient> m_webClientQueue = new Queue<WebClient>(INIT_COUNT);

        /// <summary>
        /// 可用数量
        /// </summary>
        private int m_availableCount = INIT_COUNT;

        private int m_errorCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private WebDownloader()
        {
            ServicePointManager.DefaultConnectionLimit = 512;

            // 初始化一堆的资源
            for (var i = 0; i < m_availableCount; i++)
            {
                var client = new WebClient();
                client.Headers.Add("User-Agent", USER_AGENT);
                m_webClientQueue.Enqueue(client);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static WebDownloader Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebDownloader();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 指定url下载数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Download Fail.</exception>
        public byte[] Download(string url)
        {
            var wc = WaitOne();
            try
            {
                return wc.DownloadData(url);
            }
            catch (Exception ex)
            {
                m_errorCount++;
                throw new Exception(url, ex);
            }
            finally
            {
                // 最后将资源放回池中
                Release(wc);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 释放一个下载端资源
        /// </summary>
        /// <param name="wc"></param>
        private void Release(WebClient wc)
        {
            lock (m_queueLocker)
            {
                m_webClientQueue.Enqueue(wc);
            }
            m_poolSemaphore.Release();
        }

        /// <summary>
        /// 获取一个可用的下载端资源
        /// </summary>
        /// <returns></returns>
        private WebClient WaitOne()
        {
            m_poolSemaphore.WaitOne();
            WebClient wc;
            lock (m_queueLocker)
            {
                wc = m_webClientQueue.Dequeue();
            }
            return wc;
        }

        #endregion
    }
}