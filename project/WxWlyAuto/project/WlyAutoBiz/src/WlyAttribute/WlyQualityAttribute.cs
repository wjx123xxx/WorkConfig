// *******************************************************************
// * 文件名称： WlyQualityAttribute.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-17 14:05:39
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyAttribute
{
    /// <summary>
    /// 品质属性
    /// </summary>
    public class WlyQualityAttribute : Attribute
    {
        #region Constructors

        public WlyQualityAttribute(WlyQualityType quality)
        {
            Quality = quality;
        }

        #endregion

        #region Public Properties

        public WlyQualityType Quality { get; }

        #endregion
    }
}