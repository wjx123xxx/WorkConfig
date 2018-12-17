// *******************************************************************
// * 文件名称： DMService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-08 21:51:28
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

using dm;

using Wx.Utility.WxCommon.Extension;

namespace Wx.Utility.WxForDM.Service
{
    /// <summary>
    /// 大漠实体工厂
    /// </summary>
    public partial class DMService
    {
        #region Fields

        /// <summary>
        /// 单例创建锁
        /// </summary>
        private static readonly object _instanceLocker = new object();

        /// <summary>
        /// 单例对象
        /// </summary>
        private static DMService _instance;

        private readonly string m_code = "wjx123xxx754310cc7463bc984fcae484d9a28e71";

        /// <summary>
        /// 大漠对象集合
        /// </summary>
        private readonly ConcurrentDictionary<string, dmsoft> m_dmsofts = new ConcurrentDictionary<string, dmsoft>();

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DMService"/> class from being created.
        /// Constructor
        /// </summary>
        private DMService()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of the <see cref="DMService"/> class
        /// </summary>
        public static DMService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new DMService();
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
        /// <param name="dmGuid">大漠对象唯一标识</param>
        /// <param name="hwnd"></param>
        public void BindWindow(string dmGuid, int hwnd)
        {
            var dm = GetDM(dmGuid);
            dm.BindWindow(hwnd, "gdi", "windows3", "windows", 0);
        }

        /// <summary>
        /// 专为账号输入提供的绑定
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="hwnd"></param>
        public void BindWindowIME(string dmGuid, int hwnd)
        {
            var dm = GetDM(dmGuid);
            dm.BindWindowEx(hwnd, "normal", "windows3", "windows", "dx.public.input.ime", 0);
        }

        /// <summary>
        /// 构建大漠插件实体
        /// </summary>
        /// <returns>大漠对象GUID</returns>
        public string CreateDMSoft(bool shareDict = true)
        {
            var dm = new dmsoft();
            var errorCode = 0;

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                var result = dm.RegEx(m_code, "", "221.229.162.75|58.218.204.170|221.229.162.171|221.229.162.40");
                if (result == 1)
                {
                    dm.SetShowErrorMsg(0);
                    dm.SetAero(0);
                    return true;
                }

                errorCode = result;
                Thread.Sleep(500);
                return false;
            }, TimeSpan.FromSeconds(10));
            if (wait)
            {
                var guid = MathHelper.GetNewGuid();
                m_dmsofts.TryAdd(guid, dm);
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resource", "WLYWord.txt");
                dm.SetDict(0, path);
                //if (shareDict)
                //{
                //    dm.EnableShareDict(1);
                //}

                return guid;
            }

            throw new InvalidOperationException($"无法创建大漠实体, 错误码: {errorCode}");
        }

        /// <summary>
        /// 释放大漠对象
        /// </summary>
        /// <param name="dmGuid"></param>
        public void ReleaseDMSoft(string dmGuid)
        {
            var result = m_dmsofts.TryRemove(dmGuid, out var dm);
            if (result)
            {
                dm.UnBindWindow();
            }
        }

        public void SetUAC(string dmGuid, int flag)
        {
            var dm = GetDM(dmGuid);
            dm.SetUAC(flag);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 根据GUID获取对应的大漠对象
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        private dmsoft GetDM(string dmGuid)
        {
            if (m_dmsofts.ContainsKey(dmGuid))
            {
                return m_dmsofts[dmGuid];
            }

            throw new InvalidOperationException($"无法找到对应的大漠对象 {dmGuid}");
        }

        #endregion
    }
}