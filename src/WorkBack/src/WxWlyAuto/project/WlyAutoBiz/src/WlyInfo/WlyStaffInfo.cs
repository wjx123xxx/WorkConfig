// *******************************************************************
// * 文件名称： WlyStaffInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 21:46:22
// *******************************************************************

using System.Collections.Generic;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 武将信息
    /// </summary>
    public class WlyStaffInfo
    {
        #region Public Properties

        /// <summary>
        /// 当前兵种
        /// </summary>
        public WlySoldierType CurrentType { get; set; }

        public IDictionary<WlyEquipType, WlyEquipInfo> EquipInfoDict { get; set; } = new Dictionary<WlyEquipType, WlyEquipInfo>();

        /// <summary>
        /// 装备需求字典
        /// </summary>
        public IDictionary<WlyEquipType, bool> EquipmentRequestDict { get; set; } = new Dictionary<WlyEquipType, bool>
        {
            { WlyEquipType.带兵量, false },
            { WlyEquipType.战法攻击, false },
            { WlyEquipType.战法防御, false },
            { WlyEquipType.物理攻击, false },
            { WlyEquipType.物理防御, false },
            { WlyEquipType.计策攻击, false },
            { WlyEquipType.计策防御, false }
        };

        /// <summary>
        /// 转生等级
        /// </summary>
        public int GrowLevel { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 武将名称
        /// </summary>
        public WlyStaffType Name { get; set; }

        /// <summary>
        /// 武将品质
        /// </summary>
        public WlyQualityType Quality { get; set; }

        /// <summary>
        /// 目标兵种
        /// </summary>
        public WlySoldierType TargetType { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取装备信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public WlyEquipInfo GetEquipInfo(WlyEquipType type)
        {
            if (EquipInfoDict.ContainsKey(type))
            {
                return EquipInfoDict[type];
            }

            var info = new WlyEquipInfo
            {
                Type = type,
                Level = 1,
                EquipLevel = 1,
                Quality = WlyQualityType.Unknow
            };
            EquipInfoDict.Add(type, info);
            return info;
        }

        /// <summary>
        /// 保存装备信息
        /// </summary>
        /// <param name="info"></param>
        public void SaveEquipInfo(WlyEquipInfo info)
        {
            var current = GetEquipInfo(info.Type);
            current.Level = info.Level;
            current.Quality = info.Quality;
            current.EquipLevel = info.EquipLevel;
        }

        #endregion
    }
}