// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： ApplicationContext.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-02 10:20:19
// *******************************************************************

namespace Wx.Common.BaiduMap.Biz
{
    /// <summary>
    /// 應用程序上下文
    /// </summary>
    public class ApplicationContext
    {
        #region Fields

        /// <summary>
        /// 单例创建锁
        /// </summary>
        private static readonly object s_instanceLocker = new object();

        /// <summary>
        /// 单例对象
        /// </summary>
        private static ApplicationContext s_instance;

        public DataPreserveService DataPreserveService { get; } = new DataPreserveService();

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationContext"/> class from being created.
        /// Constructor
        /// </summary>
        private ApplicationContext()
        {
            DataPreserveService.Load();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of the <see cref="ApplicationContext"/> class
        /// </summary>
        public static ApplicationContext Instance
        {
            get
            {
                if (s_instance == null)
                {
                    lock (s_instanceLocker)
                    {
                        if (s_instance == null)
                        {
                            s_instance = new ApplicationContext();
                        }
                    }
                }

                return s_instance;
            }
        }

        #endregion
    }
}