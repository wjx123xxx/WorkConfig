// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： IMapPoint.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-30 10:57:36
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Interface
{
    /// <summary>
    /// 点接口
    /// </summary>
    public interface IMapPoint
    {
        #region Public Properties

        double X { get; }

        double Y { get; }

        int Zoom { get; }

        #endregion
    }
}