// *******************************************************************
// * 文件名称： CustomAppender.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 17:03:40
// *******************************************************************

using System;

using log4net.Appender;
using log4net.Core;

namespace Wx.Utility.WxFramework.Common.Log.Appender
{
    /// <summary>
    /// User Define Log Appender
    /// </summary>
    internal class CustomAppender : AppenderSkeleton
    {
        #region Events

        /// <summary>
        /// New Log Come
        /// </summary>
        internal event Action<string> ReceiveLog;

        #endregion

        #region Protected Methods

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="loggingEvent">
        /// The logging event.
        /// </param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            OnReceiveLog(RenderLoggingEvent(loggingEvent));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Appender Receive a new Log
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        private void OnReceiveLog(string log)
        {
            ReceiveLog?.Invoke(log);
        }

        #endregion
    }
}