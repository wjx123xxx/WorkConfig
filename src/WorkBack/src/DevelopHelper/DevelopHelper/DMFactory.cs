// *******************************************************************
// * 文件名称： DMFactory.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-08 21:51:28
// *******************************************************************

using System.Threading;

using dm;

using Wx.Utility.WxFramework.Common;
using Wx.Utility.WxFramework.Common.Log;

namespace DevelopHelper
{
    /// <summary>
    /// 大漠实体工厂
    /// </summary>
    public class DMFactory
    {
        #region Fields

        /// <summary>
        /// 单例创建锁
        /// </summary>
        private static readonly object _instanceLocker = new object();

        /// <summary>
        /// 单例对象
        /// </summary>
        private static DMFactory _instance;

        private string m_code = "wjx123xxx754310cc7463bc984fcae484d9a28e71";

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DMFactory"/> class from being created.
        /// Constructor
        /// </summary>
        private DMFactory()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of the <see cref="DMFactory"/> class
        /// </summary>
        public static DMFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new DMFactory();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 通用窗口绑定方法
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="hwnd"></param>
        public void BindWindow(dmsoft dm, int hwnd)
        {
            dm.BindWindow(hwnd, "gdi", "windows", "windows", 101);
        }

        /// <summary>
        /// 构建大漠插件实体
        /// </summary>
        /// <returns></returns>
        public dmsoft CreateDMSoft()
        {
            while (true)
            {
                var dm = new dmsoft();
                var result = dm.Reg(m_code, "");
                if (result == 1)
                {
                    return dm;
                }

                WxLog.Debug($"DMFactory.CreateDMSoft Result <{result}> ReCreate");
                Thread.Sleep(1000);
            }
        }

        #endregion

        /// <summary>
        /// 寻找图片
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="resource"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        //public bool FindPic(dmsoft dm, BizResouce resource, out int x, out int y)
        //{
        //    var result = dm.FindPicMem(0, 0, 600, 600, resource.Info, "101010", 0.95, 0, out var xx, out var yy);
        //    if (result > -1)
        //    {
        //        x = (int)xx;
        //        y = (int)yy;
        //        return true;
        //    }

        //    x = 0;
        //    y = 0;
        //    return false;
        //}

        /// <summary>
        /// 寻找图片
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="res"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        //public bool FindPic(dmsoft dm, ResourceEnum res, out int x, out int y)
        //{
        //    var resource = ResourceFactory.GetResource(res);
        //    return FindPic(dm, resource, out x, out y);
        //}
    }
}