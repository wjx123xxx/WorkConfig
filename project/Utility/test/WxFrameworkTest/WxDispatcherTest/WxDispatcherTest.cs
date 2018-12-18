// *******************************************************************
// * 文件名称： WxDispatcherTest.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 15:41:58
// *******************************************************************

using System.Threading;
using Wx.Test.WxFrameworkTest.TestHelper;
using Wx.Utility.WxFramework.Common;

using Xunit;
using Xunit.Abstractions;

namespace Wx.Test.WxFrameworkTest
{
    /// <summary>
    /// 测试分发器
    /// </summary>
    public class WxDispatcherTest:IClassFixture<BaseFixture>
    {
        private WxDispatcher<TestEntityBase> m_wxDispatcher;
        private BaseFixture m_fixture;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxDispatcherTest(ITestOutputHelper output, BaseFixture fixture)
        {
            m_wxDispatcher = new WxDispatcher<TestEntityBase>();
            m_fixture = fixture;
            fixture.Init(output);
        }

        [Fact]
        private void Dispatcher_Test_NoResult()
        {
            var business = new TestMethodBussiness();
            m_wxDispatcher.RegisterHandlersByAttribute(business, typeof(TestMethodAttribute));
            var a = new TestEntityA();
            m_wxDispatcher.Dispatch(a, false);
            Assert.Equal(a, business.ResultA);
            var b = new TestEntityB();
            m_wxDispatcher.Dispatch(b);
            Thread.Sleep(100);
            Assert.Equal(b, business.ResultB);
        }
    }
}