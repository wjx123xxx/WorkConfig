// *******************************************************************
// * 文件名称： 任务_开启格子.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:31:03
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
    /// 开启格子
    /// </summary>
    [WlyTask(WlyTaskType.开启格子)]
    public class 任务_开启格子 : WlyDailyTask
    {
        #region Constructors

        public 任务_开启格子(string id) : base(id)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_仓库);

            // 如果花费0金币则 开启
            var result = DMService.Instance.FindStr(dmGuid, new WxRect(390, 464, 434, 490), "0金币", "ffff00-000000");
            if (result)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(411, 456));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(451, 332));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}