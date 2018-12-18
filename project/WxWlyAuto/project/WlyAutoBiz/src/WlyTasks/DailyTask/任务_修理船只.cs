// *******************************************************************
// * 文件名称： 任务_修理船只.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-13 23:09:26
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 修理船只
    /// </summary>
    [WlyTask(WlyTaskType.修理船只)]
    public class 任务_修理船只 : WlyDailyTask
    {
        #region Constructors

        public 任务_修理船只(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_船坞);

            for (int i = 0; i < 10; i++)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(651, 470));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(535, 203, 618, 456), "修理", WlyColor.Normal, out var x, out var y))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}