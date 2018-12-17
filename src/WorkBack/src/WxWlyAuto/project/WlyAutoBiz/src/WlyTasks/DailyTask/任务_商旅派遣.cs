// *******************************************************************
// * 文件名称： 任务_商旅派遣.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 23:56:25
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 商旅派遣
    /// </summary>
    public class 任务_商旅派遣 : WlyDailyTask
    {
        #region Constructors

        public 任务_商旅派遣(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_商旅);
            DMService.Instance.RepeatLeftClick(dmGuid, new WxPoint(604, 278), 12);

            if (DMService.Instance.FindStr(dmGuid, new WxRect(529, 346, 563, 363), "取消", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(547, 355));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}