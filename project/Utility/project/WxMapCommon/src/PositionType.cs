// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： PositionType.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-19 10:23:26
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Utility.MapCommon
{
    /// <summary>
    /// 坐标类型
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// GPS设备获取的角度坐标，wgs84坐标
        /// </summary>
        WGS84LL = 1,

        /// <summary>
        /// GPS获取的米制坐标、sogou地图所用坐标
        /// </summary>
        Sogou = 2,

        /// <summary>
        /// google地图、soso地图、aliyun地图、mapabc地图和amap地图所用坐标，国测局坐标
        /// </summary>
        GCJ02LL = 3,

        /// <summary>
        /// 国测局坐标中列表地图坐标对应的米制坐标
        /// </summary>
        GCJ_M = 4,

        /// <summary>
        /// 百度地图采用的经纬度坐标
        /// </summary>
        BD09LL = 5,

        /// <summary>
        /// 百度地图采用的米制坐标
        /// </summary>
        BaiduM = 6,

        /// <summary>
        /// mapbar地图坐标
        /// </summary>
        MapBar = 7,

        /// <summary>
        /// 51地图坐标
        /// </summary>
        Map51 = 8
    }
}