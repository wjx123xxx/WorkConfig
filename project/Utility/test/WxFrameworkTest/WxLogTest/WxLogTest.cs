// *******************************************************************
// * 文件名称： WxLogTest.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 17:13:44
// *******************************************************************

using Wx.Utility.WxFramework.Common;

using Xunit;
using Xunit.Abstractions;

namespace Wx.Test.WxFrameworkTest.WxLogTest
{
    /// <summary>
    /// 日志类测试
    /// </summary>
    public class WxLogTest : IClassFixture<SimpleFixture>
    {
        #region Fields

        private SimpleFixture m_fixture;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxLogTest(ITestOutputHelper output, SimpleFixture fixture)
        {
            m_fixture = fixture;
            fixture.Init(output);
        }

        #endregion

        #region Private Methods

        [Fact]
        private void WxLog_Test_Debug()
        {
            WxLog.Debug("Test");
        }

        [Fact]
        private void WxLog_Test_Info()
        {
            WxLog.Info("Test");
        }

        [Fact]
        private void WxLog_Test_Warn()
        {
            WxLog.Warn("Test");
        }

        [Fact]
        private void WxLog_Test_Fatal()
        {
            WxLog.Fatal("Test");
        }
        #endregion
    }
}