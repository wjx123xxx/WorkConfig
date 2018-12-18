// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： AbstractDatabase
// * 作　　者： 王璟星
// * 创建日期： 2017-03-04 22:34:55
// * 文件版本： 1.0.0.0
      
// *******************************************************************

namespace Wx.Common.Database.Entity
{
    using System.Collections.Generic;
    using System.Data;

    using Wx.Common.Database.Interface;

    /// <summary>
    /// 数据库基类
    /// </summary>
    public abstract class AbstractDatabase : IWxDatabase
    {
        #region Fields

        /// <summary>
        /// 构建锁
        /// </summary>
        private readonly object m_constructLocker = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// 批量执行语句，不关心返回结果
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        public abstract void BatchExecute(string sqlString, IEnumerable<object> parameters = null);

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public abstract bool Connect(string username, string password, string ip, string dbname);

        /// <summary>
        /// 执行 sql 语句
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract bool Execute(string sqlString, IEnumerable<object> parameters);

        /// <summary>
        /// 使用 sql 语句与参数查询数据库
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract DataSet Query(string sqlString, IEnumerable<object> parameters);

        #endregion
    }
}