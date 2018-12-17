// *******************************************************************
// * 文件名称： AppConfig.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-08 21:51:28
// *******************************************************************

using Wx.Utility.WxCommon;

namespace Wx.App.GameUI.Biz
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class AppConfig
    {
        #region Fields

        private string m_configFile;

        #endregion

        #region Public Properties

        /// <summary>
        /// 主号账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 主号密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 程序路径
        /// </summary>
        public string ProgramPath { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        public static AppConfig Load(string configFile)
        {
            var config = JsonHelper.LoadFromXmlFile<AppConfig>(configFile);
            config.m_configFile = configFile;
            return config;
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            JsonHelper.SaveToXmlFile(this, m_configFile);
        }

        #endregion
    }
}