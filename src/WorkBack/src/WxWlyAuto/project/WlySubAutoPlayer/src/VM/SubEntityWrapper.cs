// *******************************************************************
// * 文件名称： SubEntityWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 15:38:08
// *******************************************************************

using System.Windows.Input;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.UICommon;

namespace Wx.App.WlySubAutoPlayer.VM
{
    /// <summary>
    /// 小号实体包装类
    /// </summary>
    public class SubEntityWrapper : WxUIEntity
    {
        #region Fields

        private readonly SubEntity m_subEntity;

        /// <summary>
        /// 描述
        /// </summary>
        private string m_description;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubEntityWrapper(SubEntity entity)
        {
            m_subEntity = entity;
            m_subEntity.DescriptionChanged += SubEntityOnDescriptionChanged;
        }

        #endregion

        #region Public Properties

        public int CityLevel
        {
            get { return m_subEntity.Info.GetBuildingInfo(WlyBuildingType.主城).Level; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return m_description; }
            set
            {
                if (m_description != value)
                {
                    m_description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public SubEntity Entity
        {
            get { return m_subEntity; }
        }

        public int Level
        {
            get { return m_subEntity.Info.Level; }
        }

        public string Name
        {
            get { return m_subEntity.Info.Name; }
        }

        #endregion

        #region Event Handlers

        private void SubEntityOnDescriptionChanged(object sender, string s)
        {
            Description = s;
        }

        #endregion

        #region Commands

        /// <summary>
        /// 截图
        /// </summary>
        private ICommand m_captureCmd;

        /// <summary>
        /// 截图
        /// </summary>
        public ICommand CaptureCmd
        {
            get
            {
                if (m_captureCmd == null)
                {
                    m_captureCmd = WxCommandFactory.CreateCommand(CaptureCmdExecute);
                }

                return m_captureCmd;
            }
        }

        #endregion

        #region CommandExecutes

        private void CaptureCmdExecute(object obj)
        {
            m_subEntity.Capture();
        }

        #endregion
    }
}