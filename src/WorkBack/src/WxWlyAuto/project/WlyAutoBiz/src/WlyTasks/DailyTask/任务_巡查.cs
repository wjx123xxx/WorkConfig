// *******************************************************************
// * 文件名称： 任务_巡查.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:38:04
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 巡查
    /// </summary>
    [WlyTask(WlyTaskType.巡查)]
    public class 任务_巡查 : WlyDailyTask
    {
        #region Constructors

        public 任务_巡查(string id, params string[] depends) : base(id, depends)
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

            // 获取剩余巡查次数
            var countRect = new WxRect(463, 447, 498, 464);
            var countStr = DMService.Instance.GetWords(dmGuid, countRect, "20ef4c-000000");
            var count = int.Parse(countStr.Split('/')[0]);
            if (count == 0)
            {
                return new WlyTaskInfo(ID, true);
            }

            // 如果已经正在巡查了，则直接返回即可
            var autoRect = new WxRect(636, 445, 708, 469);
            if (!DMService.Instance.FindStr(dmGuid, autoRect, "停止自动", WlyColor.Normal))
            {
                // 点击巡查
                DMService.Instance.LeftClick(dmGuid, new WxPoint(670, 456));

                // 判断是否目标为公主
                if (!DMService.Instance.FindStr(dmGuid, new WxRect(578, 268, 611, 290), "公主", "e9e7cf-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(637, 278));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(596, 330));
                }

                // 开始巡查
                DMService.Instance.LeftClick(dmGuid, new WxPoint(439, 364));
            }

            // 五分钟后再次执行
            return new WlyTaskInfo(ID)
            {
                NextRunTime = DateTime.Now.AddMinutes(5)
            };
        }

        #endregion
    }
}