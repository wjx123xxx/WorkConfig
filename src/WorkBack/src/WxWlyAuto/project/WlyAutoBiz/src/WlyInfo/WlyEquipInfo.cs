// *******************************************************************
// * 文件名称： WlyEquipInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-26 17:04:03
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 装备信息
    /// </summary>
    public class WlyEquipInfo
    {
        #region Public Properties

        /// <summary>
        /// 使用等级
        /// </summary>
        public int EquipLevel { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 装备品质
        /// </summary>
        public WlyQualityType Quality { get; set; }

        /// <summary>
        /// 装备类型
        /// </summary>
        public WlyEquipType Type { get; set; }

        #endregion

        #region Public Methods

        public static bool operator ==(WlyEquipInfo info1, WlyEquipInfo info2)
        {
            return (info2 != null) && (info1 != null) && (info1.Quality == info2.Quality) && (info1.EquipLevel == info2.EquipLevel);
        }

        public static bool operator >(WlyEquipInfo info1, WlyEquipInfo info2)
        {
            if (info1.Quality > info2.Quality)
            {
                return true;
            }

            if (info1.Quality < info2.Quality)
            {
                return false;
            }

            return info1.EquipLevel > info2.EquipLevel;
        }

        public static bool operator !=(WlyEquipInfo info1, WlyEquipInfo info2)
        {
            return !(info1 == info2);
        }

        public static bool operator <(WlyEquipInfo info1, WlyEquipInfo info2)
        {
            if (info1.Quality > info2.Quality)
            {
                return false;
            }

            if (info1.Quality < info2.Quality)
            {
                return true;
            }

            return info1.EquipLevel < info2.EquipLevel;
        }

        #endregion
    }
}