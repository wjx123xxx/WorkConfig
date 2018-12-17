// *******************************************************************
// * 文件名称： WxLog.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 17:02:11
// *******************************************************************

using System;
using System.Linq;

using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

using Wx.Utility.WxFramework.Common.Log.Appender;

namespace Wx.Utility.WxFramework.Common.Log
{
    /// <summary>
    /// Log Library
    /// </summary>
    public class WxLog
    {
        #region Fields

        /// <summary>
        /// The instance locker.
        /// </summary>
        private static readonly object _instanceLocker = new object();

        /// <summary>
        /// The instance.
        /// </summary>
        private static WxLog _instance;

        /// <summary>
        /// The custom appender.
        /// </summary>
        private readonly CustomAppender m_customAppender;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog m_logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="WxLog"/> class from being created.
        /// </summary>
        private WxLog()
        {
            var type = GetType();
            var assembly = type.Assembly;
            var logName = type.Name;
            var repository = LogManager.CreateRepository(logName);

            // 配置
            var config = assembly.GetManifestResourceStream("Wx.Utility.WxFramework.resource.log.config");
            XmlConfigurator.Configure(repository, config);
            var root = ((Hierarchy)repository).Root;

            // 添加自定义Appender
            m_customAppender = root.Appenders.OfType<CustomAppender>().FirstOrDefault();
            m_logger = LogManager.GetLogger(logName, logName);
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets Instance of Log
        /// </summary>
        private static WxLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new WxLog();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public static void Debug(string log)
        {
            Instance.m_logger.Debug(log);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public static void Error(string log)
        {
            Instance.m_logger.Error(log);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public static void Fatal(string log)
        {
            Instance.m_logger.Fatal(log);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public static void Info(string log)
        {
            Instance.m_logger.Info(log);
        }

        /// <summary>
        /// when new log come, output will be called automatic
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        public static void RegisterOutput(Action<string> output)
        {
            if (Instance.m_customAppender != null)
            {
                Instance.m_customAppender.ReceiveLog += output;
            }
        }

        /// <summary>
        /// The un register output.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        public static void UnRegisterOutput(Action<string> output)
        {
            if (Instance.m_customAppender != null)
            {
                Instance.m_customAppender.ReceiveLog -= output;
            }
        }

        /// <summary>
        /// The warn.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public static void Warn(string log)
        {
            Instance.m_logger.Warn(log);
        }

        #endregion
    }
}