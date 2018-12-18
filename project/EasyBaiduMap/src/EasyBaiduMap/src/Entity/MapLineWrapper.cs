// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapLineWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-04-02 00:37:54
// *******************************************************************

namespace Wx.App.EasyBaiduMap.Entity
{
    /// <summary>
    /// 線段包裝器
    /// </summary>
    public class MapLineWrapper
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public MapLineWrapper(MapPointWrapper p1, MapPointWrapper p2)
        {
            Point1 = p1;
            Point2 = p2;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 點1
        /// </summary>
        public MapPointWrapper Point1 { get; }

        /// <summary>
        /// 點2
        /// </summary>
        public MapPointWrapper Point2 { get; }

        #endregion
    }
}