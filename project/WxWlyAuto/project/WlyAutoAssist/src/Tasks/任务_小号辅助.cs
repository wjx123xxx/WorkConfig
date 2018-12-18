// *******************************************************************
// * 文件名称： 任务_小号辅助.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-13 00:17:31
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlyAutoAssist.Tasks
{
    /// <summary>
    /// 小号辅助
    /// </summary>
    public class 任务_小号辅助 : WlyMainTask
    {
        #region Constructors

        public 任务_小号辅助(string id, params string[] depends) : base(id, depends)
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
            WlyProgressMgr.GoTo(dmGuid, 4);

            // 点开军团
            DMService.Instance.LeftClick(dmGuid, new WxPoint(831, 188), TimeSpan.FromSeconds(1));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(581, 303), TimeSpan.FromSeconds(1));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(724, 475), TimeSpan.FromSeconds(1));

            // 一直死循环点击就行了
            while (true)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(321, 476), TimeSpan.FromMilliseconds(5200));

                var view = WlyViewMgr.GetView(WlyViewType.略_防沉迷验证);
                if (view.IsCurrentView(dmGuid))
                {
                    view.Exit(dmGuid);
                }
            }
        }

        #endregion
    }
}