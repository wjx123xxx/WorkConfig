// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapPoint.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:56:00
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using Wx.Common.BaiduMap.Interface;

namespace Wx.Common.BaiduMap.Base.Point
{
    /// <summary>
    /// 点
    /// </summary>
    public abstract class MapPoint : IMapPoint
    {
        #region Public Properties

        public double X { get; set; }

        public double Y { get; set; }

        public int Zoom { get; set; }

        #endregion
    }
}