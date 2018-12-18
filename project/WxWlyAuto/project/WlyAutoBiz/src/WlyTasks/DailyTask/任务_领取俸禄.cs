// *******************************************************************
// * 文件名称： 任务_领取俸禄.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-29 23:29:22
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 领取俸禄
    /// </summary>
    public class 任务_领取俸禄 : WlyDailyTask
    {
        #region Constructors

        public 任务_领取俸禄(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_官职);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(494, 470));


            do
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(494, 470));
            }
            while (DMService.Instance.FindColor(dmGuid, WlyColor.Normal, new WxRect(467, 463, 521, 483)));

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}