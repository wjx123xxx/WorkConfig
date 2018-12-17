// *******************************************************************
// * 文件名称： BaseFixture.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 17:29:28
// *******************************************************************

using log4net.Repository.Hierarchy;

using Wx.Test.WxFrameworkTest.WxLogTest;
using Wx.Utility.WxFramework;
using Wx.Utility.WxFramework.Common;

using Xunit.Abstractions;

namespace Wx.Test.WxFrameworkTest.TestHelper
{
    /// <summary>
    /// 日志专用
    /// </summary>
    public class BaseFixture
    {
        #region Fields

        private readonly object m_locker = new object();

        private Logger m_attachable;

        private bool m_isInit;

        private TestOutputAppender m_testOutputAppender;

        #endregion

        #region Public Methods

        public virtual void Init(ITestOutputHelper output)
        {
            lock (m_locker)
            {
                if (m_isInit)
                {
                    return;
                }

                m_isInit = true;

                WxLog.RegisterOutput(log => { output.WriteLine(log); });
                WxFrameworkService.Instance.Init();
            }
        }

        #endregion
    }
}