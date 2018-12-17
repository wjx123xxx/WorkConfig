// *******************************************************************
// * 文件名称： BizResouce.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-09 20:48:40
// *******************************************************************

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Wx.App.BizCore
{
    /// <summary>
    /// 图片资源
    /// </summary>
    public class BizResouce
    {
        #region Fields

        private readonly int m_length;

        private readonly IntPtr m_ptr;

        #endregion

        #region Constructors

        public BizResouce(string name, Stream stream)
        {
            m_length = (int)stream.Length;
            byte[] buffer = new byte[m_length];
            stream.Read(buffer, 0, m_length);
            m_ptr = Marshal.AllocHGlobal(m_length);
            Marshal.Copy(buffer, 0, m_ptr, m_length);

            var ss = "Wx.App.BizCore.resource.";
            Name = Path.GetFileNameWithoutExtension(name.Substring(ss.Length));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 图片信息
        /// </summary>
        public string Info
        {
            get { return $"{m_ptr.ToInt32()},{m_length}"; }
        }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 连接多个图片
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string Add(BizResouce res)
        {
            return $"{Info}|{res.Info}";
        }

        #endregion
    }
}