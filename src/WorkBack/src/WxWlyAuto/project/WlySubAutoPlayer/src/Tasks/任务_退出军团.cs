// *******************************************************************
// * 文件名称： 任务_退出军团.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-08-12 12:12:48
// *******************************************************************

using System;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// Comment
    /// </summary>
    public class 任务_退出军团 : WlyMainTask
    {
        #region Constructors

        public 任务_退出军团(string id, params string[] depends) : base(id, depends)
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
            var sub = entity as SubEntity;
            var index = sub.Index;

            if (((index > 100) && (index < 200)) || ((index > 300) && (index < 400)))
            {
                var dmGuid = entity.DMGuid;
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(770, 478), TimeSpan.FromSeconds(1));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 333), TimeSpan.FromSeconds(1));

                if (DMService.Instance.FindStr(dmGuid, new WxRect(481, 320, 516, 343), "确定", WlyColor.Normal))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 330));
                    WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主城);
                    return new WlyTaskInfo(ID, true);
                }
            }
            else
            {
                return new WlyTaskInfo(ID, true);
            }

            throw new InvalidOperationException();
        }

        #endregion
    }
}