// *******************************************************************
// * 文件名称： WxHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 16:54:36
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wx.Utility.WxCommon.Extension
{
    /// <summary>
    /// 个人逻辑封装
    /// </summary>
    public static class WxHelper
    {
        #region Public Methods

        /// <summary>
        /// 获取指定枚举类型的所有值列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        #endregion
    }
}