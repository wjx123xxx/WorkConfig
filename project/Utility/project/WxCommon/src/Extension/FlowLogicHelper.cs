// *******************************************************************
// * 文件名称： FlowLogicHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 11:14:23
// *******************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wx.Utility.WxCommon.Extension
{
    /// <summary>
    /// 流程逻辑辅助
    /// </summary>
    public static class FlowLogicHelper
    {
        #region Public Methods

        /// <summary>
        /// 反复执行，直到成功，或者超时
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool RepeatRun(Func<bool> handler, TimeSpan timeout)
        {
            var cancelToken = new CancellationTokenSource();
            var token = cancelToken.Token;
            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (handler())
                    {
                        return;
                    }
                }
            }, token);

            if (!task.Wait(timeout))
            {
                cancelToken.Cancel();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 反复执行指定的次数
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static void RepeatRun(Action handler, int count)
        {
            for (int i = 0; i < count; i++)
            {
                handler();
            }
        }

        /// <summary>
        /// 重复执行，直到当前值与目标值相等，或者超过重试次数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getCurrent"></param>
        /// <param name="target"></param>
        /// <param name="change"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static bool RunToTarget<T>(Func<T> getCurrent, Func<T, bool> target, Action change, int retryCount = 5)
        {
            var count = 0;
            var current = getCurrent();
            var old = current;
            while (!target(current))
            {
                change();
                current = getCurrent();
                if (Equals(old, current))
                {
                    count++;
                    if (count >= retryCount)
                    {
                        return false;
                    }
                }
                else
                {
                    count = 0;
                }

                old = current;
            }

            return true;
        }

        #endregion
    }
}