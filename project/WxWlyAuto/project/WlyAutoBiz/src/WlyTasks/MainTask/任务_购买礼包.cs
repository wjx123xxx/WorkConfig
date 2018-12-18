// *******************************************************************
// * 文件名称： 任务_购买礼包.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 23:52:17
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 购买礼包
    /// </summary>
    public class 任务_购买礼包 : WlyMainTask
    {
        #region Constructors

        public 任务_购买礼包(string id, params string[] depends) : base(id, depends)
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_主公信息);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(355, 214), TimeSpan.FromSeconds(2));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(506, 157), TimeSpan.FromSeconds(2));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(793, 391), TimeSpan.FromSeconds(2));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(457, 353), TimeSpan.FromSeconds(2));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(863, 118), TimeSpan.FromSeconds(2));

            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_仓库);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(220, 230));
            if (DMService.Instance.FindStr(dmGuid, new WxRect(475, 403, 529, 433), "使用", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 417));
                return new WlyTaskInfo(ID, true);
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(278, 231));
            if (DMService.Instance.FindStr(dmGuid, new WxRect(475, 403, 529, 433), "使用", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 417));
                return new WlyTaskInfo(ID, true);
            }

            WlyEntityBiz.ClearEquipments(entity);
            if (entity.AccountInfo.GetStaffInfo(WlyStaffType.文凤卿).GetEquipInfo(WlyEquipType.物理防御).Quality >= WlyQualityType.Purple)
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID);
        }

        #endregion
    }
}