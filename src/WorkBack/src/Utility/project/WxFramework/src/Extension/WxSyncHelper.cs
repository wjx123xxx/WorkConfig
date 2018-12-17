// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WxSyncHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-22 11:21:20
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Wx.Utility.WxFramework.Extension
{
    /// <summary>
    /// 消息同步器
    /// </summary>
    /// <remarks>用于发送消息与接收消息的同步等待</remarks>
    /// <typeparam name="T">消息类型基类</typeparam>
    public class WxSyncHelper<T> where T : class
    {
        #region Fields

        private readonly IDictionary<T, MsgSync<T>> m_ackSyncs = new Dictionary<T, MsgSync<T>>();

        private readonly object m_locker = new object();

        private readonly Func<T, T, bool> m_msgComparer;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msgComparer">消息比较器，将发送与接收消息进行适配判断</param>
        public WxSyncHelper(Func<T, T, bool> msgComparer)
        {
            m_msgComparer = msgComparer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 发送前的准备
        /// </summary>
        /// <param name="msg"></param>
        public void PrepareMsg(T msg)
        {
            MsgSync<T> sync = new MsgSync<T>
            {
                SendMsg = msg
            };

            lock (m_locker)
            {
                m_ackSyncs.Add(msg, sync);
            }
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReceiveMsg(T msg)
        {
            MsgSync<T> sync = null;
            lock (m_locker)
            {
                sync = m_ackSyncs.Values.FirstOrDefault(o => m_msgComparer(o.SendMsg, msg));
            }

            if (sync != null)
            {
                sync.ReceiveMsg = msg;
                sync.SyncLocker.Set();
            }
        }

        /// <summary>
        /// 等待消息回复
        /// </summary>
        /// <typeparam name="TResponse">回复的消息类型</typeparam>
        /// <param name="msg">发送的消息</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public TResponse WaitMsg<TResponse>(T msg, int timeout = 5000) where TResponse : class, T
        {
            MsgSync<T> sync = null;
            lock (m_locker)
            {
                if (m_ackSyncs.ContainsKey(msg))
                {
                    sync = m_ackSyncs[msg];
                }
            }

            if (sync == null)
            {
                return null;
            }

            sync.SyncLocker.WaitOne(timeout);
            return sync.ReceiveMsg as TResponse;
        }

        #endregion
    }

    /// <summary>
    /// 消息同步
    /// </summary>
    internal class MsgSync<T>
    {
        #region Public Properties

        /// <summary>
        /// 接收到的消息
        /// </summary>
        public T ReceiveMsg { get; set; }

        /// <summary>
        /// 发送的消息
        /// </summary>
        public T SendMsg { get; set; }

        /// <summary>
        /// 消息同步锁
        /// </summary>
        public AutoResetEvent SyncLocker { get; } = new AutoResetEvent(false);

        #endregion
    }
}