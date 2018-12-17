﻿// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： IMapTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-27 17:40:44
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.BaiduMapGo.Interface
{
    using System.Collections.Generic;

    using Wx.App.BaiduMapGo.Base;

    /// <summary>
    /// 地图下载任务接口
    /// </summary>
    public interface IMapTask : IEnumerable<MapZoom>
    {
    }
}