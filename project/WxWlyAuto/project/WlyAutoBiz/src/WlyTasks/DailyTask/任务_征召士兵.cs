// *******************************************************************
// * 文件名称： 任务_征召士兵.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 21:50:52
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 征召士兵
    /// </summary>
    public class 任务_征召士兵 : WlyDailyTask
    {
        #region Constructors

        public 任务_征召士兵(string id, params string[] depends) : base(id, depends)
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
            var dmGuid = entity.DMGuid;
            var result = WlyUtilityBiz.GetPercent(dmGuid, new WxRect(137, 71, 236, 90), "e9e7cf-000000", out var percent);
            if (result && (percent < 1))
            {
                WlyUtilityBiz.GetSoldier(entity.DMGuid);
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}