// ++++++++++++++++++++++++++++++++++++++++++++
// 项目名称: DatabasePlugin
// 文件名称: MySqlDatabase.cs 
// 创建时间: 2016-10-01 20:46
// 版权所有: 王璟星
// ++++++++++++++++++++++++++++++++++++++++++++

namespace Wx.Common.Database.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;

    using Wx.Common.Log;

    /// <summary>
    /// MySql数据库实体
    /// </summary>
    internal class MySqlDatabase : AbstractDatabase
    {
        #region Fields

        /// <summary>
        /// 执行锁
        /// </summary>
        private readonly object m_executeLocker = new object();

        /// <summary>
        /// 队列修改锁
        /// </summary>
        private readonly object m_queueLocker = new object();

        /// <summary>
        /// 排队信号量
        /// </summary>
        private readonly Semaphore m_queueSemaphore = new Semaphore(0, 20000);

        private MySqlConnection m_connection;

        private string m_connectionString;

        /// <summary>
        /// 命令队列
        /// </summary>
        private Queue<MySqlCommand> m_sqlCommands = new Queue<MySqlCommand>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 批量执行语句，不关心返回结果
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        public override void BatchExecute(string sqlString, IEnumerable<object> parameters = null)
        {
            MySqlCommand cmd = new MySqlCommand { CommandText = sqlString };
            if (parameters != null)
            {
                foreach (var value in parameters)
                {
                    cmd.Parameters.Add(new MySqlParameter { Value = value });
                }
            }

            lock (m_queueLocker)
            {
                m_sqlCommands.Enqueue(cmd);
                m_queueSemaphore.Release();
            }
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public override bool Connect(string username, string password, string ip, string dbname)
        {
            try
            {
                m_connectionString = $"server={ip};User Id={username};password={password};Database={dbname}";
                m_connection = new MySqlConnection(m_connectionString);
                m_connection.Open();
                WxLog.Debug($"MySqlDatabase.Connect \t<{m_connectionString}>");

                // 初始化命令队列线程
                var cmdQueueThread = new Thread(HandleCmdQueue);
                cmdQueueThread.Start();
                WxLog.Debug("MySqlDatabase.Connect CmdQueueThead Start!");
                return true;
            }
            catch (Exception ex)
            {
                WxLog.Debug($"MySqlDatabase.Connect \t<{ex}>");
                return false;
            }
        }

        /// <summary>
        /// 执行 sql 语句
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override bool Execute(string sqlString, IEnumerable<object> parameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand { CommandText = sqlString, Connection = m_connection };
                if (parameters != null)
                {
                    foreach (var value in parameters)
                    {
                        cmd.Parameters.Add(new MySqlParameter { Value = value });
                    }
                }

                lock (m_executeLocker)
                {
                    return cmd.ExecuteNonQuery() == 0;
                }
            }
            catch (Exception ex)
            {
                WxLog.Error($"MySqlDatabase.Execute \n\tsqlString<{sqlString}>\n\tex<{ex}>");
                return false;
            }
        }

        /// <summary>
        /// 使用 sql 语句与参数查询数据库
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override DataSet Query(string sqlString, IEnumerable<object> parameters)
        {
            try
            {
                // 构建命令
                MySqlCommand cmd = new MySqlCommand { CommandText = sqlString, Connection = m_connection };
                if (parameters != null)
                {
                    foreach (var value in parameters)
                    {
                        cmd.Parameters.Add(new MySqlParameter { Value = value });
                    }
                }

                // 获取数据
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                var dataSet = new DataSet();
                lock (m_executeLocker)
                {
                    adapter.Fill(dataSet);
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                WxLog.Debug($"MySqlDatabase.Query \n\tsql<{sqlString}>\n\tex<{ex}>");
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 命令队列处理函数
        /// </summary>
        private void HandleCmdQueue()
        {
            while (true)
            {
                m_queueSemaphore.WaitOne();
                IList<MySqlCommand> cmdList;
                lock (m_queueLocker)
                {
                    WxLog.Warn($"MySqlDatabase.HandleCmdQueue \t<{m_sqlCommands.Count}>");
                    cmdList = m_sqlCommands.ToList();
                    m_sqlCommands.Clear();
                }

                // 执行批量命令
                MySqlTransaction tx = m_connection.BeginTransaction();
                foreach (MySqlCommand mySqlCommand in cmdList)
                {
                    mySqlCommand.Connection = m_connection;
                    mySqlCommand.Transaction = tx;
                    mySqlCommand.ExecuteNonQuery();
                }

                lock (m_executeLocker)
                {
                    tx.Commit();
                }

                // 消灭多余的信号量
                for (var i = 0; i < (cmdList.Count - 1); i++)
                {
                    m_queueSemaphore.WaitOne();
                }

                Thread.Sleep(1000);
            }
        }

        #endregion
    }
}