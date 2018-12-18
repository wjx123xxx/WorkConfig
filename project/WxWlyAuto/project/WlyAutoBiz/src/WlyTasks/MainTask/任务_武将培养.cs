// *******************************************************************
// * 文件名称： 任务_武将培养.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-10 22:58:00
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
    /// 武将培养
    /// </summary>
    public class 任务_武将培养 : WlyMainTask
    {
        #region Fields

        private readonly int m_level;

        private readonly WlyStaffType m_staff;

        #endregion

        #region Constructors

        public 任务_武将培养(string id, WlyStaffType staff, int level, params string[] depends) : base(id, depends)
        {
            m_staff = staff;
            m_level = level;
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
            var level = Math.Min(m_level, entity.AccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level);
            var currentLevel = WlyStaffMgr.GetStaffLevel(dmGuid, m_staff);
            var growLevel = WlyStaffMgr.GetStaffGrowLevel(dmGuid, m_staff);

            if (growLevel > level && currentLevel >= level)
            {
                return new WlyTaskInfo(ID, true);
            }

            // 先尝试一键突飞
            WlyStaffMgr.PractiseStaff(m_staff, dmGuid);
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_训练);
            WlyUtilityBiz.SelectStaffInList(dmGuid, m_staff);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(376, 465));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(418, 398));


            // 如果转生等级大于需求等级，则突飞到需求等级，然后完成培养
            if (growLevel > level)
            {
                //WlyStaffMgr.Upgrade(m_staff, level, dmGuid);
                //var l = entity.AccountInfo.GetStaffInfo(m_staff).Level = WlyStaffMgr.GetStaffLevel(dmGuid, m_staff);

                //if (l == level)
                //{
                //    return new WlyTaskInfo(ID, true);
                //}

                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            // 如果离转生等级较近则突飞到指定等级
            if ((currentLevel + 10 >= growLevel) && (currentLevel < growLevel))
            {
                WlyStaffMgr.Upgrade(m_staff, growLevel, dmGuid);
            }
            else
            {
                WlyStaffMgr.PractiseStaff(m_staff, dmGuid);
            }

            // 转生
            WlyStaffMgr.Grow(dmGuid, m_staff);
            entity.AccountInfo.GetStaffInfo(m_staff).Level = WlyStaffMgr.GetStaffLevel(dmGuid, m_staff);
            entity.AccountInfo.GetStaffInfo(m_staff).GrowLevel = WlyStaffMgr.GetStaffGrowLevel(dmGuid, m_staff);

            return new WlyTaskInfo(ID, DateTime.Now.Add(TimeSpan.FromHours(10)));
        }

        #endregion
    }
}