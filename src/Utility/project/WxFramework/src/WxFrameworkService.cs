// *******************************************************************
// * 文件名称： WxFrameworkService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-12 17:07:04
// *******************************************************************

using Wx.Utility.WxFramework.Common;

namespace Wx.Utility.WxFramework
{
    /// <summary>
    /// 框架服务
    /// </summary>
    public class WxFrameworkService
    {
        #region Fields

        private static readonly object _instanceLocker = new object();

        private static WxFrameworkService _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private WxFrameworkService()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Singleton
        /// </summary>
        public static WxFrameworkService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new WxFrameworkService();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 框架服务初始化
        /// 启动日志服务
        /// </summary>
        public void Init()
        {
            WxLog.Debug($"WxFrameworkService.Init Start!");

            WxLog.Debug($"WxFrameworkService.Init Finish!");
        }

        #endregion
    }
}