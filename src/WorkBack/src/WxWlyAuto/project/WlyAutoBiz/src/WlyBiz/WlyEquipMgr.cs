// *******************************************************************
// * 文件名称： WlyEquipMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-09 00:14:22
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 装备管理器
    /// </summary>
    public static class WlyEquipMgr
    {
        #region Fields

        /// <summary>
        /// 装备部位位置映射
        /// </summary>
        private static readonly IDictionary<WlyEquipType, WxPoint> _equipSpaceMap = new Dictionary<WlyEquipType, WxPoint>();

        private static readonly string _equipStr;

        #endregion

        #region Constructors

        static WlyEquipMgr()
        {
            _equipSpaceMap.Add(WlyEquipType.战法防御, new WxPoint(411, 222));
            _equipSpaceMap.Add(WlyEquipType.战法攻击, new WxPoint(356, 373));
            _equipSpaceMap.Add(WlyEquipType.计策攻击, new WxPoint(471, 376));
            _equipSpaceMap.Add(WlyEquipType.计策防御, new WxPoint(470, 274));
            _equipSpaceMap.Add(WlyEquipType.物理攻击, new WxPoint(357, 276));
            _equipSpaceMap.Add(WlyEquipType.物理防御, new WxPoint(410, 329));
            _equipSpaceMap.Add(WlyEquipType.带兵量, new WxPoint(412, 428));

            var list = new List<string>();
            foreach (var v in Enum.GetValues(typeof(WlyEquipType)))
            {
                list.Add(v.ToString());
            }

            _equipStr = string.Join("|", list);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 更换指定部位的装备
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="type"></param>
        /// <param name="staffLevel"></param>
        public static void ChangeEquipmenet(string dmGuid, WlyStaffType staff, WlyEquipType type, int staffLevel)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_装备);
            WlyUtilityBiz.SelectStaffInList(dmGuid, staff);

            var p = _equipSpaceMap[type];
            DMService.Instance.LeftClick(dmGuid, p);
            var currentInfo = GetEquipInfo(dmGuid);

            var startPoint = new WxPoint(551, 240);
            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 2; i++)
                {
                    var clickPoint = startPoint.Shift(i * 60, j * 60);
                    if (DMService.Instance.FindColor(dmGuid, "0a1215-000000", new WxRect(clickPoint, 10)))
                    {
                        return;
                    }

                    DMService.Instance.LeftClick(dmGuid, clickPoint);
                    var equipInfo = GetEquipInfo(dmGuid);

                    if (equipInfo.EquipLevel > staffLevel)
                    {
                        continue;
                    }

                    // 符合更换条件则进行更换
                    if ((equipInfo.Quality > currentInfo.Quality)
                        || ((equipInfo.Quality == currentInfo.Quality) && (equipInfo.EquipLevel > currentInfo.EquipLevel)))
                    {
                        // 点击更换
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(590, 473));
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 获取装备信息
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static WlyEquipInfo GetEquipInfo(string dmGuid)
        {
            var info = new WlyEquipInfo
            {
                Quality = WlyUtilityBiz.GetQuality(dmGuid, new WxRect(710, 290, 801, 312))
            };

            if (info.Quality > WlyQualityType.Unknow)
            {
                info.Type = GetEquipTypeCore(dmGuid, new WxRect(712, 339, 808, 358));
                var levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(713, 289, 812, 313), "e9e7cf-000000");
                info.Level = int.Parse(levelStr.Substring(0, levelStr.Length - 1));

                levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(711, 314, 807, 336), $"{WlyColor.White}|ff0000-000000", 1);
                info.EquipLevel = int.Parse(levelStr.Substring(2));
            }

            return info;
        }

        /// <summary>
        /// 获取指定武将指定部位的装备信息
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="equipType"></param>
        /// <returns></returns>
        public static WlyEquipInfo GetStaffEquipInfo(string dmGuid, WlyStaffType staff, WlyEquipType equipType)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_装备);
            WlyUtilityBiz.SelectStaffInList(dmGuid, staff);
            DMService.Instance.LeftClick(dmGuid, _equipSpaceMap[equipType], TimeSpan.FromSeconds(1));
            var info = GetEquipInfo(dmGuid);
            return info;
        }

        /// <summary>
        /// 判断是否为一件装备
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool IsEquipment(string dmGuid, WxRect rect)
        {
            return DMService.Instance.FindStr(dmGuid, rect, _equipStr, "f3f3da-000000");
        }

        /// <summary>
        /// 为指定英雄卸下装备
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        public static bool UnequipStaff(string dmGuid, WlyStaffType staff)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_装备);
            if (!WlyUtilityBiz.SelectStaffInList(dmGuid, staff))
            {
                return false;
            }

            // 点击全部卸载
            DMService.Instance.LeftClick(dmGuid, new WxPoint(372, 470));

            // 确认装备已经全部卸载
            foreach (var p in _equipSpaceMap.Values)
            {
                DMService.Instance.LeftClick(dmGuid, p);
                var type = GetEquipInfo(dmGuid);
                if (type.Quality > WlyQualityType.Unknow)
                {
                    return false;
                }
            }

            return true;
        }

        public static void UnequipStaff(string dmGuid, WlyStaffType staff, WlyEquipType equipType)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_装备);
            if (!WlyUtilityBiz.SelectStaffInList(dmGuid, staff))
            {
                return ;
            }

            // 点击全部卸载
            DMService.Instance.LeftClick(dmGuid, _equipSpaceMap[equipType]);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(460, 469));
        }

        /// <summary>
        /// 强化装备到指定的等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="equip"></param>
        /// <param name="targetLevel"></param>
        public static WlyEquipInfo UpgradeEquip(string dmGuid, WlyStaffType staff, WlyEquipType equip, int targetLevel)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_强化装备);
            var result = SelectUpgradeEquipCore(dmGuid, staff, equip);
            if (!result)
            {
                WlyViewMgr.ExitCurrentView(dmGuid, TimeSpan.FromSeconds(10));
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_强化装备);
                result = SelectUpgradeEquipCore(dmGuid, staff, equip);
            }

            if (!result)
            {
                return new WlyEquipInfo
                {
                    Quality = WlyQualityType.Unknow
                };
            }

            // 点击强化
            FlowLogicHelper.RunToTarget(() => GetEquipLevelCore(dmGuid), current => current >= targetLevel,
                () => DMService.Instance.LeftClick(dmGuid, new WxPoint(563, 354), TimeSpan.FromMilliseconds(50)));
            return GetEquipInfo(dmGuid);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取装备等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        private static int GetEquipLevelCore(string dmGuid)
        {
            var levelStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(713, 289, 812, 313), "e9e7cf-000000");
                return !string.IsNullOrEmpty(levelStr);
            }, TimeSpan.FromSeconds(10));

            if (!wait || !levelStr.Contains("级") || !int.TryParse(levelStr.Substring(0, levelStr.Length - 1), out var currentLevel))
            {
                throw new InvalidOperationException("无法解析装备等级");
            }

            return currentLevel;
        }

        /// <summary>
        /// 获得当前部位的类型
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private static WlyEquipType GetEquipTypeCore(string dmGuid, WxRect rect)
        {
            var index = DMService.Instance.FindStrEx(dmGuid, rect, _equipStr, "f3f3da-000000");
            return (WlyEquipType)int.Parse(index[0].ToString());
        }

        /// <summary>
        /// 在强化界面选择武将指定部位的装备
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="equip"></param>
        private static bool SelectUpgradeEquipCore(string dmGuid, WlyStaffType staff, WlyEquipType equip)
        {
            var result = DMService.Instance.FindStrEx(dmGuid, new WxRect(188, 201, 417, 439), staff.ToString(), WlyColor.White);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var points = result.Split('|')
                                   .Select(o =>
                                   {
                                       var ps = o.Split(',');
                                       return new WxPoint(int.Parse(ps[1]), int.Parse(ps[2]));
                                   });

                foreach (var p in points)
                {
                    DMService.Instance.LeftClick(dmGuid, p);
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(712, 339, 808, 358), equip.ToString(), "f3f3da-000000"))
                    {
                        return true;
                    }
                }
            }

            DMService.Instance.RepeatLeftClick(dmGuid, new WxPoint(426, 427), 14, 100);
            result = DMService.Instance.FindStrEx(dmGuid, new WxRect(188, 201, 417, 439), staff.ToString(), WlyColor.White);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var points = result.Split('|')
                                   .Select(o =>
                                   {
                                       var ps = o.Split(',');
                                       return new WxPoint(int.Parse(ps[1]), int.Parse(ps[2]));
                                   });

                foreach (var p in points)
                {
                    DMService.Instance.LeftClick(dmGuid, p);
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(712, 339, 808, 358), equip.ToString(), "f3f3da-000000"))
                    {
                        return true;
                    }
                }
            }

            throw new InvalidOperationException($"无法找到{staff.ToString()}的装备{equip.ToString()}");
        }

        #endregion
    }
}