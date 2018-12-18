// *******************************************************************
// * 文件名称： MathHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-23 17:21:08
// *******************************************************************

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wx.Utility.WxCommon.Extension
{
    /// <summary>
    /// 一些算法封装
    /// </summary>
    public static class MathHelper
    {
        #region Public Methods

        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            var ms = new MemoryStream();
            using (var compressStream = new GZipStream(ms, CompressionMode.Compress, true))
            {
                compressStream.Write(data, 0, data.Length);
            }
            return ms.ToArray();
        }

        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符
        /// </summary>
        /// <param name="rawString">需要压缩的字符串</param>
        /// <returns>压缩后的Base64编码的字符串</returns>
        public static string CompressGZipString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || (rawString.Length == 0))
            {
                return "";
            }

            var rawData = Encoding.UTF8.GetBytes(rawString);
            var zippedData = Compress(rawData);
            return Convert.ToBase64String(zippedData);
        }

        /// <summary>
        /// GZip解压
        /// </summary>
        /// <param name="data">压缩的源数据</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            using (var decompressStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
            {
                var outBuffer = new MemoryStream();
                var buffer = new byte[2048];
                while (true)
                {
                    var bytesRead = decompressStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead <= 0)
                    {
                        break;
                    }

                    outBuffer.Write(buffer, 0, bytesRead);
                }

                return outBuffer.ToArray();
            }
        }

        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩
        /// </summary>
        /// <param name="zippedString">经GZip压缩后的二进制字符串</param>
        /// <returns>原始未压缩字符串</returns>
        public static string DecompressGZipString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || (zippedString.Length == 0))
            {
                return "";
            }

            var zippedData = Convert.FromBase64String(zippedString);
            var rawData = Decompress(zippedData);
            return Encoding.UTF8.GetString(rawData);
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileMD5(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return string.Empty;
            }

            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                return string.Join("", retVal.Select(o => o.ToString("x2")));
            }
        }

        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <returns></returns>
        public static string GetMD5(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(data);
            return string.Join("", retVal.Select(o => o.ToString("x2")));
        }

        /// <summary>
        /// 获取md5值
        /// </summary>
        /// <param name="data">目标字符串</param>
        /// <returns>UTF-8格式的MD5值</returns>
        public static string GetMD5(string data)
        {
            return GetMD5(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 统一获取guid的方式
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        #endregion
    }
}