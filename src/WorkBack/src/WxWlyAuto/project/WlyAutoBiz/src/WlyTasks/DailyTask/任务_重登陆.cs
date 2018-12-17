// *******************************************************************
// * 文件名称： 任务_重登陆.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 11:40:08
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyCommon;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 重新登录游戏
    /// </summary>
    public class 任务_重登陆 : WlyDailyTask
    {
        #region Constructors

        public 任务_重登陆(string id) : base(id)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override WlyTaskInfo InternalRun(WlyEntity entity)
        {
            entity.Restart();
            return new WlyTaskInfo(ID)
            {
                NextRunTime = DateTime.Today.AddDays(1).AddHours(4).AddMinutes(1)
            };
        }

        #endregion
    }
}