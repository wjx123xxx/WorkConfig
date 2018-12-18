// *******************************************************************
// * 文件名称： PictureService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 14:45:58
// *******************************************************************

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Common;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Utility.WxForDM.Service
{
    /// <summary>
    /// 图色服务
    /// </summary>
    public partial class DMService
    {
        #region Fields

        /// <summary>
        /// 图片资源字典
        /// </summary>
        private readonly ConcurrentDictionary<object, PicResource> m_picResources = new ConcurrentDictionary<object, PicResource>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Capture(string dmGuid, WxRect rect, string filePath)
        {
            var dm = GetDM(dmGuid);
            var result = dm.Capture(rect.Left, rect.Top, rect.Right, rect.Bottom, filePath);
            return result == 1;
        }

        /// <summary>
        /// 找寻指定的颜色
        /// </summary>
        /// <param name="color"></param>
        /// <param name="rect"></param>
        /// <param name="dmGuid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool FindColor(string color, WxRect rect, string dmGuid, out int x, out int y)
        {
            var dm = GetDM(dmGuid);

            var result = dm.FindColor(rect.Left, rect.Top, rect.Right, rect.Bottom, color, 1, 1, out var xx, out var yy);
            if (result > 0)
            {
                x = (int)xx;
                y = (int)yy;
                return true;
            }

            x = -1;
            y = -1;
            return false;
        }

        /// <summary>
        /// 找寻指定的颜色
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="color"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool FindColor(string dmGuid, string color, WxRect rect)
        {
            return FindColor(color, rect, dmGuid, out var _, out var _);
        }

        /// <summary>
        /// 在指定范围内寻找图片
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="key">资源键值</param>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool FindPic(string dmGuid, object key, WxRect rect, out int x, out int y)
        {
            if (!m_picResources.ContainsKey(key))
            {
                x = -1;
                y = -1;
                WxLog.Debug($"DMService.FindPic No Pic Resource With Key <{key}>");
                return false;
            }

            var dm = GetDM(dmGuid);
            var resource = m_picResources[key];
            var result = dm.FindPicMem(rect.Left, rect.Top, rect.Right, rect.Bottom, resource.Info, "202020", 0.8, 0, out var xx, out var yy);
            if (result > -1)
            {
                x = (int)xx;
                y = (int)yy;
                return true;
            }

            x = -1;
            y = -1;
            return false;
        }

        /// <summary>
        /// 在指定范围内寻找图片
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="key"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool FindPic(string dmGuid, object key, WxRect rect)
        {
            return FindPic(dmGuid, key, rect, out var _, out var _);
        }

        /// <summary>
        /// 在指定范围内找寻所有符合条件的图片
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="key"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public IEnumerable<WxPoint> FindPics(string dmGuid, object key, WxRect rect)
        {
            var dm = GetDM(dmGuid);
            var result = dm.FindPicMemEx(rect.Left, rect.Top, rect.Right, rect.Bottom, m_picResources[key].Info, "202020", 0.8, 0);
            if (string.IsNullOrEmpty(result))
            {
                return Enumerable.Empty<WxPoint>();
            }

            IList<WxPoint> list = new List<WxPoint>();
            foreach (var p in result.Split('|'))
            {
                var ps = p.Split(',');
                list.Add(new WxPoint(int.Parse(ps[1]), int.Parse(ps[2])));
            }

            return list;
        }

        /// <summary>
        /// 注册图片资源到DM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="stream">资源流</param>
        public void RegisterPicResource<T>(T key, Stream stream)
        {
            if (!m_picResources.ContainsKey(key))
            {
                var resource = new PicResource(stream);
                m_picResources.TryAdd(key, resource);
            }
        }

        #endregion
    }
}