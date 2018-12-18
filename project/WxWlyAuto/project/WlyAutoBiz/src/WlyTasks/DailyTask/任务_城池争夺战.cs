// *******************************************************************
// * 文件名称： 任务_城池争夺战.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:08:09
// *******************************************************************

using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 城池争夺战
    /// </summary>
    [WlyTask(WlyTaskType.城池争夺战)]
    public class 任务_城池争夺战 : WlyDailyTask
    {
        #region Constructors

        public 任务_城池争夺战(string id) : base(id)
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

            // 进入城池争夺战界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.日常_城池争夺战);

            // 等待所有UI加载完成
            Thread.Sleep(2000);

            if (!DMService.Instance.FindStr(dmGuid, new WxRect(625, 558, 690, 590), "一键挂机", "cccccc-000000"))
            {
                // 点击一键
                var oneClickPoint = new WxPoint(659, 574);
                DMService.Instance.LeftClick(dmGuid, oneClickPoint);

                // 点击确定
                var okPoint = new WxPoint(451, 330);
                DMService.Instance.LeftClick(dmGuid, okPoint);

                // 点击领取
                var getPoint = new WxPoint(544, 440);
                DMService.Instance.LeftClick(dmGuid, getPoint);

                // 点击退出
                var exitPoint = new WxPoint(949, 571);
                DMService.Instance.LeftClick(dmGuid, exitPoint);
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}