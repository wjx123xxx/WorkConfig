// *******************************************************************
// * 文件名称： WlyTaskAttribute.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-23 17:43:04
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyAttribute
{
    /// <summary>
    /// 通用任务属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WlyTaskAttribute : Attribute
    {
        #region Constructors

        public WlyTaskAttribute(WlyTaskType type)
        {
            Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 任务类型
        /// </summary>
        public WlyTaskType Type { get; }

        #endregion
    }
}