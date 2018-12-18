// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WxDatabase.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-04 22:26:03
// * 文件版本： 1.0.0.0
      
// *******************************************************************

namespace Wx.Common.Database
{
    /// <summary>
    /// 私有数据库服务类
    /// </summary>
    public class WxDatabase
    {
        #region Fields

        /// <summary>
        /// 单例创建锁
        /// </summary>
        private static readonly object InstanceLocker = new object();

        /// <summary>
        /// 单例对象
        /// </summary>
        private static WxDatabase instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="WxDatabase"/> class from being created.
        /// Constructor
        /// </summary>
        private WxDatabase()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of the <see cref="WxDatabase"/> class
        /// </summary>
        public static WxDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new WxDatabase();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion
    }
}