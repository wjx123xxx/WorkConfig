// ++++++++++++++++++++++++++++++++++++++++++++
// 项目名称: PluginProtocol
// 文件名称: IWxDatabase.cs 
// 创建时间: 2016-10-01 20:42
// 版权所有: 王璟星
// ++++++++++++++++++++++++++++++++++++++++++++

namespace Wx.Common.Database.Interface
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IWxDatabase
    {
        #region Public Methods

        /// <summary>
        /// 批量执行语句，不关心返回结果
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        void BatchExecute(string sqlString, IEnumerable<object> parameters = null);

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        bool Connect(string username, string password, string ip, string dbname);

        /// <summary>
        /// 使用 sql 语句与参数执行
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool Execute(string sqlString, IEnumerable<object> parameters = null);

        /// <summary>
        /// 使用 sql 语句与参数查询数据库
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet Query(string sqlString, IEnumerable<object> parameters = null);

        #endregion
    }
}