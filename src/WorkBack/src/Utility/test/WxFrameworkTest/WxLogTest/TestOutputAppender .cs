// *******************************************************************
// * 版权所有： 深圳市震有科技有限公司
// * 文件名称： TestOutputAppender .cs
// * 作　　者： 王璟星
// * 创建日期： 2017-02-09 15:48:00
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using log4net.Appender;
using log4net.Core;

using Xunit.Abstractions;

namespace Wx.Test.WxFrameworkTest.WxLogTest
{
    /// <summary>
    /// 测试输出类
    /// </summary>
    public class TestOutputAppender : AppenderSkeleton
    {
        #region Fields

        private readonly ITestOutputHelper m_output;

        #endregion

        #region Constructors

        public TestOutputAppender(ITestOutputHelper output)
        {
            m_output = output;
            Name = "TestOutputAppender";
        }

        #endregion

        #region Protected Methods

        protected override void Append(LoggingEvent loggingEvent)
        {
            m_output.WriteLine(RenderLoggingEvent(loggingEvent));
        }

        #endregion
    }
}