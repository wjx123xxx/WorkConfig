// *******************************************************************
// * 文件名称： 任务_寻访.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-12 11:20:29
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
    /// 寻访
    /// </summary>
    [WlyTask(WlyTaskType.寻访)]
    public class 任务_寻访 : WlyDailyTask
    {
        #region Constructors

        public 任务_寻访(string id) : base(id)
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

            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_寻访);

            int GetLastTime()
            {
                var countStr = DMService.Instance.GetWords(dmGuid, new WxRect(462, 465, 501, 482), "20ef4c-000000");
                var result = int.TryParse(countStr.Split('/')[0], out var count);
                if (result)
                {
                    return count;
                }

                return 0;
            }

            while (GetLastTime() > 0)
            {
                var result = DMService.Instance.FindStr(dmGuid, new WxRect(401, 275, 903, 307), "商贾", WlyColor.Purple, out var x,
                    out var y);
                DMService.Instance.LeftClick(dmGuid, result ? new WxPoint(x, y) : new WxPoint(573, 474), TimeSpan.FromSeconds(1));

                if (DMService.Instance.FindStr(dmGuid, new WxRect(491, 266, 523, 283), "丝绸", WlyColor.Yellow))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(546, 355));
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(435, 333, 472, 367), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(458, 315));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 352));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}