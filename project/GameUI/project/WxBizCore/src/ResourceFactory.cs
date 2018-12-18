// *******************************************************************
// * 文件名称： ResourceFactory.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-09 20:47:12
// *******************************************************************

using System.Collections.Concurrent;

using Wx.Utility.WxFramework.Common;

namespace Wx.App.BizCore
{
    /// <summary>
    /// 资源工厂
    /// </summary>
    public static class ResourceFactory
    {
        #region Fields

        private static ConcurrentDictionary<string, BizResouce> m_dict = new ConcurrentDictionary<string, BizResouce>();

        #endregion

        #region Constructors

        static ResourceFactory()
        {
            var assembly = typeof(ResourceFactory).Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                WxLog.Debug($"ResourceFactory.ResourceFactory Res <{res}>");
                var stream = assembly.GetManifestResourceStream(res);
                var resource = new BizResouce(res, stream);
                m_dict.TryAdd(resource.Name, resource);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取指定的资源类型
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static BizResouce GetResource(ResourceEnum resourceType)
        {
            var res = resourceType.ToString();
            if (m_dict.TryGetValue(res, out var resource))
            {
                return resource;
            }

            return null;
        }

        #endregion
    }
}