// *******************************************************************
// * 文件名称： WlyAIAttribute.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-28 22:24:11
// *******************************************************************

using System;

namespace Wx.Lib.WlyAutoBiz.WlyAttribute
{
    /// <summary>
    /// 卧龙吟自动挂机逻辑分级属性
    /// </summary>
    public class WlyAIAttribute : Attribute
    {
        #region Constructors

        public WlyAIAttribute(int level)
        {
            Level = level;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 逻辑适用等级
        /// </summary>
        public int Level { get; }

        #endregion
    }
}