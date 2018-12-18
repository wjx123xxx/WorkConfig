// *******************************************************************
// * 版权所有： 深圳市震有科技软件有限公司
// * 文件名称： WxDispatcher.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-12 17:25:59
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Utility.WxFramework.Extension
{
    /// <summary>
    /// 分发器，用于分发实体（消息实体，数据实体等）
    /// </summary>
    public class WxDispatcher<T>
    {
        #region Fields

        /// <summary>
        /// 处理器列表
        /// </summary>
        private readonly ConcurrentDictionary<Type, Delegate> m_msgHandlers = new ConcurrentDictionary<Type, Delegate>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 实体分发
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="async">异步执行</param>
        public void Dispatch(T entity, bool async = true)
        {
            var msgType = entity.GetType();
            if (!m_msgHandlers.ContainsKey(msgType))
            {
                return;
            }

            if (!async)
            {
                foreach (var handler in m_msgHandlers[msgType].GetInvocationList())
                {
                    try
                    {
                        handler.DynamicInvoke(entity);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return;
            }

            new Task(() =>
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Thread.CurrentThread.Name = $"{msgType.Name}_Handler({threadId})";
                foreach (var handler in m_msgHandlers[msgType].GetInvocationList())
                {
                    try
                    {
                        handler.DynamicInvoke(entity);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }).Start();
        }

        /// <summary>
        /// 注册处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TS">注册类型，必须是T的子类</typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool RegisterHandler<TS>(Action<T> handler) where TS : T
        {
            // 检验参数 handler
            if (handler == null)
            {
                return false;
            }

            var type = typeof(TS);

            if (!m_msgHandlers.ContainsKey(type))
            {
                m_msgHandlers[type] = handler;
            }
            else
            {
                m_msgHandlers[type] = Delegate.Remove(m_msgHandlers[type], handler);
                m_msgHandlers[type] = Delegate.Combine(m_msgHandlers[type], handler);
            }

            return true;
        }

        /// <summary>
        /// 通过指定的属性注册处理器（处理器需要为static）
        /// </summary>
        /// <param name="sourceAssembly">源程序集</param>
        /// <param name="attributeType">属性类型</param>
        public void RegisterHandlersByAttribute(Assembly sourceAssembly, Type attributeType)
        {
            // 初始化注册函数
            var register = GetType().GetMethod(nameof(RegisterHandler), BindingFlags.Public | BindingFlags.Instance);
            if (register == null)
            {
                throw new InvalidOperationException("Method RegisterHandler Not Found!");
            }

            // 反射加载所有的消息处理器
            var methods = sourceAssembly.GetTypes().SelectMany(o => o.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttribute(attributeType);
                    if (attribute == null)
                    {
                        continue;
                    }

                    var para = method.GetParameters().First().ParameterType;
                    var handler = typeof(Action<>).MakeGenericType(para);
                    register.MakeGenericMethod(para).Invoke(this, new object[] { method.CreateDelegate(handler) });
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WxDispatcher.RegisterHandlersByAttribute Error <{ex}>");
                }
            }
        }

        /// <summary>
        /// 通过指定的属性注册处理器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attributeType"></param>
        public void RegisterHandlersByAttribute(object obj, Type attributeType)
        {
            // 初始化注册函数
            var register = GetType().GetMethod(nameof(RegisterHandler), BindingFlags.Public | BindingFlags.Instance);
            if (register == null)
            {
                throw new InvalidOperationException("Method RegisterHandler Not Found!");
            }

            // 反射加载所有的消息处理器
            var methods = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttribute(attributeType);
                    if (attribute == null)
                    {
                        continue;
                    }

                    var para = method.GetParameters().First().ParameterType;
                    var handler = typeof(Action<>).MakeGenericType(para);
                    register.MakeGenericMethod(para).Invoke(this, new object[] { method.CreateDelegate(handler, obj) });
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WxDispatcher.RegisterHandlersByAttribute Error <{ex}>");
                }
            }
        }

        /// <summary>
        /// 反注册处理器
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool UnRegisterHandler<TIn>(Action<TIn> handler)
        {
            try
            {
                if (handler == null)
                {
                    return false;
                }

                var type = typeof(TIn);
                if (m_msgHandlers.ContainsKey(type))
                {
                    m_msgHandlers[type] = Delegate.Remove(m_msgHandlers[type], handler);
                }

                return true;
            }
            catch (Exception ex)
            {
                WxLog.Error($"WxDispatcher.UnRegisterHandler Error <{ex}>");
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// 分发器，用于分发实体（消息实体，数据实体等）
    /// </summary>
    public class WxDispatcher<T, TResult>
    {
        #region Fields

        /// <summary>
        /// 处理器列表
        /// </summary>
        private readonly ConcurrentDictionary<Type, Delegate> m_msgHandlers = new ConcurrentDictionary<Type, Delegate>();

        #endregion

        #region Public Methods

        /// <summary>
        /// 实体分发
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="callback">执行结果回调</param>
        /// <param name="async">异步执行</param>
        public void Dispatch(T entity, Action<TResult> callback, bool async = true)
        {
            var msgType = entity.GetType();

            if (!m_msgHandlers.ContainsKey(msgType))
            {
                return;
            }

            if (!async)
            {
                DispatchImpl(entity, callback, msgType);
                return;
            }

            new Task(() =>
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Thread.CurrentThread.Name = $"{msgType.Name}_Handler({threadId})";
                DispatchImpl(entity, callback, msgType);
            }).Start();
        }

        /// <summary>
        /// 注册处理器
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool RegisterHandler<TIn>(Func<TIn, TResult> handler)
        {
            // 检验参数 handler
            if (handler == null)
            {
                return false;
            }

            var type = typeof(TIn);
            try
            {
                if (!m_msgHandlers.ContainsKey(type))
                {
                    m_msgHandlers[type] = handler;
                }
                else
                {
                    m_msgHandlers[type] = Delegate.Remove(m_msgHandlers[type], handler);
                    m_msgHandlers[type] = Delegate.Combine(m_msgHandlers[type], handler);
                }

                return true;
            }
            catch (Exception ex)
            {
                WxLog.Error($"WxDispatcher.RegisterHandler Error <{ex}>");
                return false;
            }
        }

        /// <summary>
        /// 通过指定的属性注册处理器（处理器需要为static）
        /// </summary>
        /// <param name="sourceAssembly">源程序集</param>
        /// <param name="attributeType">属性类型</param>
        public void RegisterHandlersByAttribute(Assembly sourceAssembly, Type attributeType)
        {
            // 初始化注册函数
            var register = GetType().GetMethod(nameof(RegisterHandler), BindingFlags.Public | BindingFlags.Instance);

            // 反射加载所有的消息处理器
            var methods = sourceAssembly.GetTypes().SelectMany(o => o.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttribute(attributeType);
                    if (attribute == null)
                    {
                        continue;
                    }

                    var para = method.GetParameters().First().ParameterType;
                    var handler = typeof(Func<,>).MakeGenericType(para, typeof(TResult));
                    if (register != null)
                    {
                        register.MakeGenericMethod(para).Invoke(this, new object[] { method.CreateDelegate(handler) });
                    }
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WxDispatcher.RegisterHandlersByAttribute Error <{ex}>");
                }
            }
        }

        /// <summary>
        /// 通过指定的属性注册处理器
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attributeType"></param>
        public void RegisterHandlersByAttribute(object obj, Type attributeType)
        {
            // 初始化注册函数
            var register = GetType().GetMethod(nameof(RegisterHandler), BindingFlags.Public | BindingFlags.Instance);

            // 反射加载所有的消息处理器
            var methods = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                try
                {
                    var attribute = method.GetCustomAttribute(attributeType);
                    if (attribute == null)
                    {
                        continue;
                    }

                    var para = method.GetParameters().First().ParameterType;
                    var handler = typeof(Func<,>).MakeGenericType(para, typeof(TResult));
                    if (register != null)
                    {
                        register.MakeGenericMethod(para).Invoke(this, new object[] { method.CreateDelegate(handler, obj) });
                    }
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WxDispatcher.RegisterHandlersByAttribute Error <{ex}>");
                }
            }
        }

        /// <summary>
        /// 反注册处理器
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool UnRegisterHandler<TIn>(Func<TIn, TResult> handler)
        {
            try
            {
                if (handler == null)
                {
                    return false;
                }

                var type = typeof(TIn);
                if (m_msgHandlers.ContainsKey(type))
                {
                    m_msgHandlers[type] = Delegate.Remove(m_msgHandlers[type], handler);
                }

                return true;
            }
            catch (Exception ex)
            {
                WxLog.Error($"WxDispatcher.UnRegisterHandler Error <{ex}>");
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void DispatchImpl(T entity, Action<TResult> callback, Type msgType)
        {
            foreach (var handler in m_msgHandlers[msgType].GetInvocationList())
            {
                try
                {
                    if (handler.DynamicInvoke(entity) is TResult result)
                    {
                        callback?.Invoke(result);
                    }
                }
                catch (Exception ex)
                {
                    WxLog.Error($"WxDispatcher.DispatchImpl Error <{ex}>");
                }
            }
        }

        #endregion
    }
}