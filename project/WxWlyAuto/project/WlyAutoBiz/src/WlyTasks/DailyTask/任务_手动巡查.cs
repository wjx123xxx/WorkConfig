// *******************************************************************
// * 文件名称： 任务_手动巡查.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-02 17:31:47
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 手动巡查
    /// </summary>
    public class 任务_手动巡查 : WlyDailyTask
    {
        #region Constructors

        public 任务_手动巡查(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_巡查);

            while (true)
            {
                // 获取剩余巡查次数
                var countRect = new WxRect(463, 447, 498, 464);
                var countStr = DMService.Instance.GetWords(dmGuid, countRect, "20ef4c-000000");
                var count = int.Parse(countStr.Split('/')[0]);
                if (count == 0)
                {
                    return new WlyTaskInfo(ID, true);
                }

                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(755, 459), TimeSpan.FromMilliseconds(200)), count);
                if (DMService.Instance.FindStr(dmGuid, new WxRect(479, 344, 519, 364), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(497, 355));
                }
            }
        }

        #endregion
    }
}