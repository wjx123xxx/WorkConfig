// *******************************************************************
// * 文件名称： 任务_普通砸罐.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 11:42:21
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 普通砸罐
    /// </summary>
    public class 任务_普通砸罐 : WlyDailyTask
    {
        #region Constructors

        public 任务_普通砸罐(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_普通砸罐);
            Thread.Sleep(2000);
            var startPoint = new WxPoint(428, 261);
            while (!DMService.Instance.FindStr(dmGuid, new WxRect(801, 351, 842, 384), "需要", WlyColor.Selected))
            {
                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        var point = startPoint.Shift(i * 86, j * 88);
                        DMService.Instance.LeftClick(dmGuid, point, TimeSpan.FromMilliseconds(600));
                    }
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}