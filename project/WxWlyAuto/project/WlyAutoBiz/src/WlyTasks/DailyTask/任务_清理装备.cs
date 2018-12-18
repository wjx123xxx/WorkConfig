// *******************************************************************
// * 文件名称： 任务_清理装备.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-29 17:53:56
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask
{
    /// <summary>
    /// 清理装备
    /// </summary>
    public class 任务_清理装备 : WlyDailyTask
    {
        #region Constructors

        public 任务_清理装备(string id, params string[] depends) : base(id, depends)
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
            var cityLevel = entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level;
            foreach (var staff in entity.AccountInfo.StaffInfoDict.Values)
            {
                if ((staff.Level == cityLevel) && (staff.GrowLevel > cityLevel))
                {
                    continue;
                }

                staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, staff.Name);
                if (cityLevel >= 100 && staff.Level < 80)
                {
                    WlyStaffMgr.Upgrade(staff.Name, 80, dmGuid);
                    staff.Level = WlyStaffMgr.GetStaffLevel(dmGuid, staff.Name);
                }
            }

            WlyEntityBiz.ClearEquipments(entity);
            //WlyEntityBiz.UpgradeAllEquipmenets(entity);
            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}