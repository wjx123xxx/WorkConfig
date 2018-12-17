// ++++++++++++++++++++++++++++++++++++++++++++
// 项目名称: DatabasePlugin
// 文件名称: WxDatabasePlugin.cs 
// 创建时间: 2016-10-01 20:41
// 版权所有: 王璟星
// ++++++++++++++++++++++++++++++++++++++++++++

namespace Wx.Common.Database
{
    using Wx.Common.Database.Entity;
    using Wx.Common.Database.Interface;
    using Wx.Common.Log;

    /// <summary>
    /// 数据库插件
    /// </summary>
    public class WxDatabasePlugin
    {
        #region Fields

        private static WxDatabasePlugin _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private WxDatabasePlugin()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static WxDatabasePlugin Instance => _instance ?? (_instance = new WxDatabasePlugin());

        /// <summary>
        /// 默认数据库
        /// </summary>
        public IWxDatabase DefaultDatabase { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 根据数据库类型创建对应数据库实体
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <returns></returns>
        /// <exception cref="WxException">不支持的数据库类型.</exception>
        public IWxDatabase CreateDatabase(WxDatabaseType type)
        {
            WxLog.Debug($"WxDatabasePlugin.CreateDatabase \t<{type}>");
            switch (type)
            {
                case WxDatabaseType.MySql:
                    return new MySqlDatabase();
                default:
                    return null;
            }
        }

        #endregion
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum WxDatabaseType
    {
        /// <summary>
        /// MySql 5.6.17版本
        /// </summary>
        MySql
    }
}