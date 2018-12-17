// *******************************************************************
// * 文件名称： PicResource.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-09 20:48:40
// *******************************************************************

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Wx.Utility.WxForDM.Common
{
    /// <summary>
    /// 图片资源
    /// </summary>
    internal class PicResource
    {
        #region Fields

        private readonly int m_length;

        private readonly IntPtr m_ptr;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream">资源数据流</param>
        public PicResource(Stream stream)
        {
            m_length = (int)stream.Length;
            var buffer = new byte[m_length];
            stream.Read(buffer, 0, m_length);
            m_ptr = Marshal.AllocHGlobal(m_length);
            Marshal.Copy(buffer, 0, m_ptr, m_length);
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

        #endregion

        #region Public Methods

        /// <summary>
        /// 连接多个图片
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string Add(PicResource res)
        {
            return $"{Info}|{res.Info}";
        }

        #endregion
    }
}