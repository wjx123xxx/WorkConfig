// *******************************************************************
// * 文件名称： WlyTaskSwitchWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 10:25:15
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Utility.UICommon;

namespace Wx.App.WlySubAutoPlayer.Common
{
    /// <summary>
    /// 任务开关包装器
    /// </summary>
    public class WlyTaskSwitchWrapper : WxUIEntity
    {
        #region Fields

        private readonly WlySwitchInfo m_switchInfo;

        #endregion

        #region Constructors

        public WlyTaskSwitchWrapper(WlySwitchInfo switchInfo)
        {
            m_switchInfo = switchInfo;
        }

        #endregion

        #region Events

        /// <summary>
        /// 开关变化
        /// </summary>
        public event EventHandler Changed;

        #endregion

        #region Public Properties

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable
        {
            get { return m_switchInfo.Enable; }
            set
            {
                if (m_switchInfo.Enable != value)
                {
                    m_switchInfo.Enable = value;
                    OnPropertyChanged(nameof(Enable));
                    OnChanged();
                }
            }
        }

        /// <summary>
        /// 开关类型
        /// </summary>
        public WlySwitchType Type
        {
            get { return m_switchInfo.Type; }
        }

        #endregion

        #region Private Methods

        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}