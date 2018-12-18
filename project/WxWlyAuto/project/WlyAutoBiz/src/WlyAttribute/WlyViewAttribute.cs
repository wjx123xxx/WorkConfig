// *******************************************************************
// * 文件名称： WlyViewAttribute.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 15:52:36
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyAttribute
{
    /// <summary>
    /// 卧龙吟界面属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WlyViewAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public WlyViewAttribute(WlyViewType type)
        {
            Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 界面类型
        /// </summary>
        public WlyViewType Type { get; }

        #endregion
    }
}