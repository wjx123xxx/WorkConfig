// *******************************************************************
// * 文件名称： 任务_金币研发.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 01:36:40
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 金币研发
    /// </summary>
    public class 任务_金币研发 : WlyMainTask
    {
        #region Constructors

        public 任务_金币研发(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团科技);

            DMService.Instance.LeftClick(dmGuid, new WxPoint(423, 474));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(589, 285));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(436, 364));

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}