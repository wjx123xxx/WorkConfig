// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WxCommandFactory.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-24 15:39:27
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Windows.Input;

namespace Wx.Utility.UICommon
{
    /// <summary>
    /// 命令创建工厂
    /// </summary>
    public class WxCommandFactory
    {
        #region Public Methods

        /// <summary>
        /// 根据委托创建命令对象
        /// </summary>
        /// <param name="execute">命令执行委托</param>
        /// <param name="canExecute">命令可否执行委托</param>
        /// <param name="isAsync">是否异步执行命令</param>
        /// <returns></returns>
        public static ICommand CreateCommand(Action<object> execute, Func<object, bool> canExecute = null, bool isAsync = false)
        {
            return new WxCommand(execute, canExecute, isAsync);
        }

        #endregion
    }
}