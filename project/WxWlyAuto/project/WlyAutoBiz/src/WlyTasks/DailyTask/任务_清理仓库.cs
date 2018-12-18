// *******************************************************************
// * 文件名称： 任务_清理仓库.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:09:51
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
    /// 清理仓库
    /// </summary>
    [WlyTask(WlyTaskType.清理仓库)]
    public class 任务_清理仓库 : WlyDailyTask
    {
        #region Constructors

        public 任务_清理仓库(string id) : base(id)
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

            var startX = 215;
            var startY = 233;
            var column = 8;
            var row = 3;
            var width = 56;
            var height = 56;

            var key = true;
            while (key)
            {
                for (var r = 0; r < row; r++)
                {
                    for (var c = 0; c < column; c++)
                    {
                        var x = startX + (c * width);
                        var y = startY + (r * height);

                        if (DMService.Instance.FindColor(dmGuid, "17201d-000000", new WxRect(new WxPoint(x, y), 10, 10)))
                        {
                            continue;
                        }

                        DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                        if (DMService.Instance.FindStr(dmGuid, new WxRect(438, 259, 468, 277), "仓库", "e9e7cf-000000"))
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(547, 356));
                            key = false;
                            r = row;
                            break;
                        }

                        while (WlyEquipMgr.IsEquipment(dmGuid, new WxRect(712, 339, 808, 358)))
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(412, 419));
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 328));
                        }
                    }
                }

                if (key)
                {
                    for (var count = 0; count < 10; count++)
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(653, 378), TimeSpan.FromMilliseconds(100));
                    }
                }
            }

            // 退出，重新进入，进行一键领取
            WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(5));
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_仓库);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(startX, startY));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 452));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 452));

            // 将器合成
            DMService.Instance.LeftClick(dmGuid, new WxPoint(609, 159));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(365, 467));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(451, 330));

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}