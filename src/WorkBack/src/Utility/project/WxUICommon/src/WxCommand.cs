// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WxCommand.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-24 15:34:54
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Utility.UICommon
{
    /// <summary>
    /// 命令基类
    /// </summary>
    public class WxCommand : ICommand
    {
        #region Fields

        private readonly Func<object, bool> m_canExecute;

        private readonly Action<object> m_execute;

        private readonly bool m_isAsync;

        #endregion

        #region Constructors

        public WxCommand(Action<object> execute, Func<object, bool> canExecute = null, bool isAsync = false)
        {
            m_canExecute = canExecute;
            m_execute = execute;
            m_isAsync = isAsync;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

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
            if (null == m_execute)
            {
                return;
            }

            if (!CanExecute(parameter))
            {
                return;
            }

            if (!m_isAsync)
            {
                m_execute(parameter);
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    m_execute(parameter);
                }
                catch (Exception ex)
                {
                    WxLog.Debug($"WxCommand.Execute {ex}");
                }
            });
        }

        #endregion

        #region Protected Methods

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}