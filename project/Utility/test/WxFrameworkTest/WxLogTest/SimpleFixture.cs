// *******************************************************************
// * 版权所有： 深圳市震有科技软件有限公司
// * 文件名称： SimpleFixture.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-13 16:01:48
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using Wx.Test.WxFrameworkTest.TestHelper;

using Xunit.Abstractions;

namespace Wx.Test.WxFrameworkTest.WxLogTest
{
    /// <summary>
    /// Fixture
    /// </summary>
    public class SimpleFixture : BaseFixture
    {
        #region Fields

        private bool m_isInit;

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        public override void Init(ITestOutputHelper output)
        {
            base.Init(output);

            if (m_isInit)
            {
                return;
            }

            m_isInit = true;
        }

        #endregion
    }
}