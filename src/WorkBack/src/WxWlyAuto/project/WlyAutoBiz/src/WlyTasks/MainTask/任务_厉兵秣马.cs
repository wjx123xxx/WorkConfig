// *******************************************************************
// * 文件名称： 任务_厉兵秣马.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-07-15 11:23:23
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 厉兵秣马
    /// </summary>
    public class 任务_厉兵秣马 : WlyMainTask
    {
        #region Constructors

        public 任务_厉兵秣马(string id, params string[] depends) : base(id, depends)
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
            var endTime = new DateTime(2018, 7, 16, 4, 0, 0);
            if (DateTime.Now > endTime)
            {
                return new WlyTaskInfo(ID, true);
            }

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_厉兵秣马);

            // 领取两个大包厢
            DMService.Instance.LeftClick(dmGuid, new WxPoint(228, 227));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(227, 328));

            // 循环领取七天的奖励
            var start = new WxPoint(375, 177);
            for (var i = 0; i < 7; i++)
            {
                DMService.Instance.LeftClick(dmGuid, start.Shift(70 * i, 0), TimeSpan.FromSeconds(1));
                FlowLogicHelper.RepeatRun(() =>
                {
                    var rect = new WxRect(733, 290, 790, 312);
                    var point = new WxPoint(761, 299);

                    Thread.Sleep(1000);
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(357, 300, 436, 320), "打猎", "fffdd4-000000"))
                    {
                        rect = new WxRect(732, 378, 792, 400);
                        point = new WxPoint(759, 388);
                    }

                    if (DMService.Instance.FindStr(dmGuid, rect, "领取奖励", WlyColor.Normal, WlyColor.White))
                    {
                        DMService.Instance.LeftClick(dmGuid, point);
                        return false;
                    }

                    if (DMService.Instance.FindStr(dmGuid, rect, "前往", WlyColor.Normal))
                    {
                        return true;
                    }

                    if (DMService.Instance.FindStr(dmGuid, new WxRect(485, 317, 518, 342), "确定", WlyColor.Normal))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(495, 330));
                    }

                    return false;
                }, TimeSpan.FromSeconds(10));
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}