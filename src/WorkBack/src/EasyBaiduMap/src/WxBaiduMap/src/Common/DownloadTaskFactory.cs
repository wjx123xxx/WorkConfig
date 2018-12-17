// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： DownloadTaskFactory.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-27 17:41:53
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using Wx.Common.BaiduMap.Base;
using Wx.Common.BaiduMap.Base.Point;

namespace Wx.Common.BaiduMap.Common
{
    using System.Collections.Generic;

    using Wx.Common.BaiduMap.Entity;
    using Wx.Common.BaiduMap.Interface;

    /// <summary>
    /// 下载任务工厂
    /// </summary>
    public class DownloadTaskFactory
    {
        #region Public Methods

        /// <summary>
        /// 创建一个区域下载任务
        /// </summary>
        /// <param name="points"></param>
        /// <param name="minZoom"></param>
        /// <param name="maxZoom"></param>
        /// <returns></returns>
        public IMapTask CreateReginoTask(IEnumerable<MercatorPoint> points, int minZoom, int maxZoom)
        {
            return new RegionTask(points, minZoom, maxZoom);
        }

        #endregion
    }
}