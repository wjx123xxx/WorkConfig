// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： CommandWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-24 15:34:54
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.App.EasyBaiduMap.Common
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// 命令包裝器
    /// </summary>
    internal class CommandWrapper : ICommand
    {
        #region Fields

        private Func<object, bool> m_canExecute;

        private string m_displayDetail;

        private Action<object> m_execute;

        private bool m_isAsync;

        #endregion

        #region Constructors

        public CommandWrapper(Action<object> execute, Func<object, bool> canExecute = null, bool isAsync = false)
        {
            m_canExecute = canExecute;
            m_execute = execute;
            m_isAsync = isAsync;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion

        #region Public Methods

        public bool CanExecute(object parameter)
        {
            if (null != m_canExecute)
            {
                return m_canExecute(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (null != m_execute)
            {
                if (CanExecute(parameter))
                {
                    if (m_isAsync)
                    {
                        //打印异步执行的异常
                        new Action(() =>
                        {
                            try
                            {
                                m_execute(parameter);
                            }
                            catch (Exception ex)
                            {
                            }
                        }).BeginInvoke(null, null);
                    }
                    else
                    {
                        m_execute(parameter);
                    }
                }
            }
        }

        #endregion
    }
}