// *******************************************************************
// * 文件名称： AbstractModel.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-09-17 00:59:58
// *******************************************************************

using System;

namespace Wx.Wly.Core.Base
{
    /// <summary>
    /// 数据模型基类
    /// </summary>
    public abstract class AbstractModel
    {
        #region Public Properties

        /// <summary>
        /// 数据描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 数据名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        #endregion
    }
}