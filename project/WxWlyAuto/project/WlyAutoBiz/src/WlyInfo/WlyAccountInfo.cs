// *******************************************************************
// * 文件名称： WlyAccountInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-22 12:47:54
// *******************************************************************

using System.Collections.Generic;

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Extension;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class WlyAccountInfo
    {
        #region Fields

        /// <summary>
        /// 文件路径
        /// </summary>
        protected string m_filePath;

        #endregion

        #region Public Properties

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 建筑信息字典
        /// </summary>
        public IDictionary<WlyBuildingType, WlyBuildingInfo> BuildingInfoDict { get; set; } = new Dictionary<WlyBuildingType, WlyBuildingInfo>();

        /// <summary>
        /// 声望值
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 竞技场积分
        /// </summary>
        public int FightPoint { get; set; }

        /// <summary>
        /// 首攻进度
        /// </summary>
        public int FreeProgress { get; set; } = 4;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 军令数量
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 军令保留数量
        /// </summary>
        public int PointReserved { get; set; }

        /// <summary>
        /// 推图进度
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 攻击节点缓存
        /// </summary>
        public string ProgressNode { get; set; }

        /// <summary>
        /// 选择的古城探秘类型
        /// </summary>
        public WlyExploreType SelectedExploreType { get; set; }

        /// <summary>
        /// 选择刷军团的类型
        /// </summary>
        public WlyGroupType SelectedGroupType { get; set; }

        /// <summary>
        /// 武将信息字典
        /// </summary>
        public IDictionary<WlyStaffType, WlyStaffInfo> StaffInfoDict { get; set; } = new Dictionary<WlyStaffType, WlyStaffInfo>();

        /// <summary>
        /// 任务开关信息字典
        /// </summary>
        public IDictionary<WlySwitchType, WlySwitchInfo> SwitchInfoDict { get; set; } = new Dictionary<WlySwitchType, WlySwitchInfo>();

        /// <summary>
        /// 任务信息字典
        /// </summary>
        public IDictionary<string, WlyTaskInfo> TaskInfoDict { get; set; } = new Dictionary<string, WlyTaskInfo>();

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string UID { get; set; }

        #endregion

        #region Public Methods

        public virtual void ResetLoginTime()
        {
        }

        public virtual void ResetMainLevel()
        {
        }

        /// <summary>
        /// 新招募了武将
        /// </summary>
        /// <param name="staff"></param>
        public void AddStaffInfo(WlyStaffType staff)
        {
            if (StaffInfoDict.ContainsKey(staff))
            {
                return;
            }

            WlyStaffInfo info = new WlyStaffInfo
            {
                Name = staff
            };
            StaffInfoDict.Add(staff, info);
        }

        /// <summary>
        /// 获取建筑信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WlyBuildingInfo GetBuildingInfo(WlyBuildingType type)
        {
            if (BuildingInfoDict.ContainsKey(type))
            {
                return BuildingInfoDict[type];
            }

            WlyBuildingInfo info = new WlyBuildingInfo(type);
            BuildingInfoDict.Add(type, info);
            return info;
        }

        /// <summary>
        /// 获取武将信息
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public WlyStaffInfo GetStaffInfo(WlyStaffType staff)
        {
            if (StaffInfoDict.ContainsKey(staff))
            {
                return StaffInfoDict[staff];
            }

            return null;
        }

        /// <summary>
        /// 获取开关信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WlySwitchInfo GetSwitchInfo(WlySwitchType type)
        {
            if (SwitchInfoDict.ContainsKey(type))
            {
                return SwitchInfoDict[type];
            }

            WlySwitchInfo info = new WlySwitchInfo();
            info.Type = type;
            SwitchInfoDict.Add(type, info);
            return info;
        }

        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public WlyTaskInfo GetTaskInfo(string taskID)
        {
            if (TaskInfoDict.ContainsKey(taskID))
            {
                return TaskInfoDict[taskID];
            }

            WlyTaskInfo info = new WlyTaskInfo(taskID);
            TaskInfoDict.Add(taskID, info);
            return info;
        }

        /// <summary>
        /// 武将下野
        /// </summary>
        /// <param name="staff"></param>
        public void RemoveStaffInfo(WlyStaffType staff)
        {
            if (StaffInfoDict.ContainsKey(staff))
            {
                StaffInfoDict.Remove(staff);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            JsonHelper.SaveToXmlFile(this, m_filePath);
        }

        #endregion
    }
}