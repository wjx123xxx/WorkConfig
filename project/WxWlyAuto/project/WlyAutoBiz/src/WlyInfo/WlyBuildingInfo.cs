// *******************************************************************
// * 文件名称： WlyBuildingInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-18 17:40:03
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 建筑信息
    /// </summary>
    public class WlyBuildingInfo
    {
        #region Constructors

        public WlyBuildingInfo(WlyBuildingType type)
        {
            Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 建筑等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 建筑类型
        /// </summary>
        public WlyBuildingType Type { get; set; }

        #endregion
    }
}