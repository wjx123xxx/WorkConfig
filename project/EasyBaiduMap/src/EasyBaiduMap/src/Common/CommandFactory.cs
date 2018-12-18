// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： CommandFactory.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-24 15:39:27
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.EasyBaiduMap.Common
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// 命令创建工厂
    /// </summary>
    public class CommandFactory
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
            return new CommandWrapper(execute, canExecute, isAsync);
        }

        #endregion
    }
}