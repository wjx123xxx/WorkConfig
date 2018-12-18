// *******************************************************************
// * 文件名称： WlyColor.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-17 14:04:34
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;

namespace Wx.Lib.WlyAutoBiz.WlyDefinition
{
    /// <summary>
    /// 游戏中使用的颜色
    /// </summary>
    public static class WlyColor
    {
        #region Fields

        /// <summary>
        /// 蓝色
        /// </summary>
        [WlyQuality(WlyQualityType.Blue)]
        public static readonly string Blue = "33ccff-000000";

        /// <summary>
        /// 绿色
        /// </summary>
        [WlyQuality(WlyQualityType.Green)]
        public static readonly string Green = "66ff33-000000";

        /// <summary>
        /// 紫色
        /// </summary>
        [WlyQuality(WlyQualityType.Purple)]
        public static readonly string Purple = "9900ff-000000";

        /// <summary>
        /// 红色
        /// </summary>
        [WlyQuality(WlyQualityType.Red)]
        public static readonly string Red = "ff3333-000000";

        /// <summary>
        /// 白色
        /// </summary>
        [WlyQuality(WlyQualityType.White)]
        public static readonly string White = "ffffff-000000";

        /// <summary>
        /// 黄色
        /// </summary>
        [WlyQuality(WlyQualityType.Yellow)]
        public static readonly string Yellow = "ffff00-000000";

        /// <summary>
        /// 装备的颜色
        /// </summary>
        public static readonly string EquipmentColor = $"{White}|{Blue}|{Green}|{Yellow}|{Red}|{Purple}";

        /// <summary>
        /// 普通常用的颜色
        /// </summary>
        public static readonly string Normal = "ffffb0-000000";

        /// <summary>
        /// 橙色
        /// </summary>
        [WlyQuality(WlyQualityType.Orange)]
        public static readonly string Orange = "ffcc00-000000";

        /// <summary>
        /// 被选中时的颜色
        /// </summary>
        public static readonly string Selected = "ffff00-000000";

        /// <summary>
        /// 武将的颜色
        /// </summary>
        public static readonly string StaffColor = $"{White}|{Blue}|{Green}|{Red}|{Selected}";

        /// <summary>
        /// 混沌
        /// </summary>
        [WlyQuality(WlyQualityType.混沌)]
        public static readonly string 混沌 = "cc9933-0000000";

        /// <summary>
        /// 灭世
        /// </summary>
        [WlyQuality(WlyQualityType.灭世)]
        public static readonly string 灭世 = "ff6600-000000";

        /// <summary>
        /// 盘古
        /// </summary>
        [WlyQuality(WlyQualityType.盘古)]
        public static readonly string 盘古 = "ff6666-000000";

        /// <summary>
        /// 神皇
        /// </summary>
        [WlyQuality(WlyQualityType.神皇)]
        public static readonly string 神皇 = "ff9933-000000";

        #endregion
    }
}