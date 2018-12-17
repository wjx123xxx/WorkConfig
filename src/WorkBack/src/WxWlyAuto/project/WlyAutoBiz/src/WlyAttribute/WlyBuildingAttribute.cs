// *******************************************************************
// * 文件名称： WlyBuildingAttribute.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 16:23:07
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyAttribute
{
    /// <summary>
    /// 建筑属性
    /// </summary>
    public class WlyBuildingAttribute : Attribute
    {
        #region Constructors

        public WlyBuildingAttribute(WlyBuildingType type)
        {
            Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 建筑类型
        /// </summary>
        public WlyBuildingType Type { get; }

        #endregion
    }
}