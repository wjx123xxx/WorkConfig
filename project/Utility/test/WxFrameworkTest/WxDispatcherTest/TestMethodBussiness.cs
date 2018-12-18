// *******************************************************************
// * 版权所有： 深圳市震有科技软件有限公司
// * 文件名称： TestMethodBussiness.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 16:02:51
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using Wx.Utility.WxFramework.Common;

namespace Wx.Test.WxFrameworkTest
{
    /// <summary>
    /// 测试函数业务类
    /// </summary>
    public class TestMethodBussiness
    {
        #region Public Properties

        public TestEntityA ResultA { get; set; }

        public TestEntityB ResultB { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        public void TestMethodA(TestEntityA A)
        {
            ResultA = A;
            WxLog.Debug($"TestMethodBussiness.TestMethodA{A.GetType().FullName}");
        }

        #endregion

        #region Private Methods

        [TestMethod]
        private void TestMethod(TestEntityB B)
        {
            ResultB = B;
            WxLog.Debug($"TestMethodBussiness.TestMethod {B.GetType().FullName}");
        }

        #endregion
    }
}