// *******************************************************************
// * 文件名称： WlyStaffMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 21:06:53
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Lib.WlyAutoBiz.WlyViews;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 武将管理器
    /// </summary>
    public static class WlyStaffMgr
    {
        #region Public Methods

        /// <summary>
        /// 顿悟武将到指定的品级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static bool EnvolveStaff(string dmGuid, WlyStaffType staff, WlyQualityType quality)
        {
            SelectStaff(dmGuid, staff, WlyViewType.功能_武将);

            var currentQuality = WlyUtilityBiz.GetQuality(dmGuid, new WxRect(715, 308, 796, 326));
            if (currentQuality >= quality)
            {
                return true;
            }

            // 点击进入顿悟
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(764, 275), TimeSpan.FromSeconds(2));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(477, 96, 523, 120), "顿悟", WlyColor.Normal))
                {
                    return true;
                }

                return false;
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                throw new InvalidOperationException("无法点开顿悟界面");
            }

            while (DMService.Instance.FindColor(WlyColor.Normal, new WxRect(244, 414, 699, 454), dmGuid, out var x, out var y))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(521, 266, 600, 313), staff.ToString(), WlyColor.StaffColor))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(456, 329));
                }
                else
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(543, 329));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(801, 120));
                    return false;
                }
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(769, 415));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(452, 334));

            wait = FlowLogicHelper.RepeatRun(() =>
            {
                var result = DMService.Instance.FindStr(dmGuid, new WxRect(444, 371, 546, 443), "顿悟", WlyColor.Normal);
                if (result)
                {
                    return true;
                }

                Thread.Sleep(1000);
                return false;
            }, TimeSpan.FromSeconds(20));

            if (wait)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(496, 403));
                return true;
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(804, 117));
            return false;
        }

        /// <summary>
        /// 武将转职
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="dmGuid"></param>
        public static void Evolve(WlyStaffType staff, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_武将);
            var result = WlyUtilityBiz.SelectStaffInList(dmGuid, staff);
            if (!result)
            {
                throw new InvalidOperationException($"未能选择到武将{staff.ToString()}");
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(581, 341));
            if (DMService.Instance.FindStr(dmGuid, new WxRect(429, 342, 475, 371), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(454, 360));
            }
        }

        /// <summary>
        /// 招募武将
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        public static bool GetStaff(string dmGuid, WlyStaffType staff)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_招募);

            // 先在左边的列表中确认是否已经存在
            if (DMService.Instance.FindStr(dmGuid, 总成_武将.武将列表, staff.ToString(), WlyColor.StaffColor))
            {
                return true;
            }

            // 开始进行招募
            FlowLogicHelper.RepeatRun(() =>
            {
                // 在当前页寻找武将
                var find = DMService.Instance.FindStr(dmGuid, new WxRect(321, 331, 653, 450), staff.ToString(), WlyColor.StaffColor, out var x,
                    out var y);
                if (find)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y), TimeSpan.FromSeconds(1));
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(714, 305, 801, 329), staff.ToString(), WlyColor.StaffColor))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(612, 462));
                        Thread.Sleep(1000);
                    }

                    return true;
                }

                // 进行翻页
                var pageStr = DMService.Instance.GetWords(dmGuid, new WxRect(468, 453, 514, 476), "ffffff-000000");
                var index = pageStr.IndexOf("/", StringComparison.Ordinal);
                var current = int.Parse(pageStr.Substring(0, index));
                var total = int.Parse(pageStr.Substring(index + 1));
                if (current == total)
                {
                    return true;
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(524, 464));
                return false;
            }, TimeSpan.FromSeconds(10));

            // 确认招募成功情况
            if (DMService.Instance.FindStr(dmGuid, 总成_武将.武将列表, staff.ToString(), WlyColor.StaffColor))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取武将转生等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static int GetStaffGrowLevel(string dmGuid, WlyStaffType staff)
        {
            SelectStaff(dmGuid, staff, WlyViewType.功能_训练);

            var word = DMService.Instance.GetWords(dmGuid, new WxRect(411, 338, 451, 357), "ff6600-000000");
            if (!word.Contains("级"))
            {
                throw new InvalidOperationException("无法解析转生等级");
            }

            return int.Parse(word.Substring(0, word.Length - 1));
        }

        /// <summary>
        /// 获取当前武将等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static int GetStaffLevel(string dmGuid, WlyStaffType staff)
        {
            SelectStaff(dmGuid, staff, WlyViewType.功能_训练);

            var levelStr = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(716, 306, 799, 328), "e9e7cf-000000");
                return !string.IsNullOrEmpty(levelStr);
            }, TimeSpan.FromSeconds(10));
            if (!wait)
            {
                throw new InvalidOperationException("无法解析武将等级");
            }

            var result = int.TryParse(levelStr.Substring(0, levelStr.Length - 1), out var currentLevel);
            if (!result)
            {
                throw new InvalidOperationException("无法解析武将等级");
            }

            return currentLevel;
        }

        /// <summary>
        /// 获取武将的品质
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static WlyQualityType GetStaffQuality(string dmGuid, WlyStaffType staff)
        {
            SelectStaff(dmGuid, staff, WlyViewType.功能_武将);
            return WlyUtilityBiz.GetQuality(dmGuid, new WxRect(715, 308, 796, 326));
        }

        /// <summary>
        /// 获取兵种类型
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static WlySoldierType GetStaffType(string dmGuid, WxRect rect)
        {
            var str = DMService.Instance.GetWords(dmGuid, rect, "ffffff-000000|b3ad9d-000000");
            if (string.IsNullOrEmpty(str))
            {
                return WlySoldierType.Unkonwn;
            }

            var result = Enum.TryParse<WlySoldierType>(str, out var type);
            return result ? type : WlySoldierType.Unkonwn;
        }

        /// <summary>
        /// 武将转生
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static bool Grow(string dmGuid, WlyStaffType staff)
        {
            var currentLevel = GetStaffLevel(dmGuid, staff);
            var growLevel = GetStaffGrowLevel(dmGuid, staff);
            if (currentLevel >= growLevel)
            {
                SelectStaff(dmGuid, staff, WlyViewType.功能_武将);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(491, 472));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 329));
            }

            return true;
        }

        /// <summary>
        /// 训练武将
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="dmGuid"></param>
        public static void PractiseStaff(WlyStaffType staff, string dmGuid)
        {
            SelectStaff(dmGuid, staff, WlyViewType.功能_训练);

            // 确认是否需要开启武将训练位
            while (true)
            {
                var word = DMService.Instance.GetWords(dmGuid, new WxRect(198, 457, 236, 489), "f3f3da-000000");
                if (word.Contains("/") && (word.Split('/')[1] != "5"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(271, 478));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 355), TimeSpan.FromSeconds(2));
                    continue;
                }

                break;
            }

            // 确认武将是否正在训练
            var lastTime = WlyUtilityBiz.GetTime(dmGuid, new WxRect(566, 287, 636, 308), "33ffff-000000");
            if ((lastTime < TimeSpan.FromHours(6)) && (lastTime != TimeSpan.Zero))
            {
                // 结束训练
                DMService.Instance.LeftClick(dmGuid, new WxPoint(580, 331), TimeSpan.FromSeconds(1));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 331), TimeSpan.FromSeconds(1));
                lastTime = WlyUtilityBiz.GetTime(dmGuid, new WxRect(566, 287, 636, 308), "33ffff-000000");
            }

            if (lastTime == TimeSpan.Zero)
            {
                // 选择24小时训练
                if (!DMService.Instance.FindStr(dmGuid, new WxRect(410, 236, 447, 257), "24", "e9e7cf-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(475, 246));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(437, 280));
                }

                // 选择地狱训练
                if (!DMService.Instance.FindStr(dmGuid, new WxRect(408, 261, 448, 285), "地狱", "e9e7cf-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(477, 272));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(438, 338));
                }

                // 点击训练
                DMService.Instance.LeftClick(dmGuid, new WxPoint(580, 331));
            }
        }

        /// <summary>
        /// 选择武将
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <param name="view"></param>
        public static void SelectStaff(string dmGuid, WlyStaffType staff, WlyViewType view)
        {
            // 首先跳转到训练界面
            WlyViewMgr.GoTo(dmGuid, view);
            if (!WlyUtilityBiz.SelectStaffInList(dmGuid, staff))
            {
                GetStaff(dmGuid, staff);
                WlyViewMgr.GoTo(dmGuid, view);
                WlyUtilityBiz.SelectStaffInList(dmGuid, staff);
            }

            if (DMService.Instance.FindStr(dmGuid, new WxRect(529, 321, 562, 341), "取消", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(546, 329));
            }
        }

        /// <summary>
        /// 设置武将兵种类型
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="index"></param>
        /// <param name="dmGuid"></param>
        public static void SetType(WlyStaffType staff, int index, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_武将);
            if (!WlyUtilityBiz.SelectStaffInList(dmGuid, staff))
            {
                throw new InvalidOperationException($"无法选择到武将{staff}");
            }

            if (index == 1)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(370, 254));
            }
            else
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(469, 265));
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(459, 340));
        }

        /// <summary>
        /// 交换两个武将指定的装备
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff1"></param>
        /// <param name="staff2"></param>
        /// <param name="equipType"></param>
        public static void SwitchEquipment(string dmGuid, WlyStaffInfo staff1, WlyStaffInfo staff2, WlyEquipType equipType)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_武将);
            var e1 = staff1.GetEquipInfo(equipType);
            var e2 = staff2.GetEquipInfo(equipType);
            WlyEquipMgr.UnequipStaff(dmGuid, staff1.Name, equipType);
            WlyEquipMgr.UnequipStaff(dmGuid, staff2.Name, equipType);

            if (staff1.Level < e2.EquipLevel)
            {
                Upgrade(staff1.Name, e2.EquipLevel, dmGuid);
            }
            WlyEquipMgr.ChangeEquipmenet(dmGuid, staff1.Name, equipType, e2.EquipLevel);

            if (staff2.Level < e1.EquipLevel)
            {
                Upgrade(staff2.Name, e1.EquipLevel, dmGuid);
            }
            WlyEquipMgr.ChangeEquipmenet(dmGuid, staff2.Name, equipType, e1.EquipLevel);

            staff1.SaveEquipInfo(WlyEquipMgr.GetStaffEquipInfo(dmGuid, staff1.Name, equipType));
            staff2.SaveEquipInfo(WlyEquipMgr.GetStaffEquipInfo(dmGuid, staff2.Name, equipType));
        }

        /// <summary>
        /// 武将下野
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public static bool ThrowStaff(string dmGuid, WlyStaffType staff)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_招募);

            // 无法选中则当做下野已经成功
            if (!WlyUtilityBiz.SelectStaffInList(dmGuid, staff))
            {
                return true;
            }

            // 点击下野
            DMService.Instance.LeftClick(dmGuid, new WxPoint(270, 476));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(453, 332));

            // 操作确认
            return !DMService.Instance.FindStr(dmGuid, 总成_武将.武将列表, staff.ToString(), WlyColor.StaffColor);
        }

        /// <summary>
        /// 突飞指定武将到指定的等级
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="level"></param>
        /// <param name="dmGuid"></param>
        public static bool Upgrade(WlyStaffType staff, int level, string dmGuid)
        {
            // 确认武将正在训练中
            PractiseStaff(staff, dmGuid);

            // 突飞猛进
            return FlowLogicHelper.RunToTarget(() =>
            {
                var levelStr = string.Empty;
                while (string.IsNullOrEmpty(levelStr))
                {
                    SelectStaff(dmGuid, staff, WlyViewType.功能_训练);
                    levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(716, 306, 799, 328), "e9e7cf-000000");
                }

                var result = int.TryParse(levelStr.Substring(0, levelStr.Length - 1), out var currentLevel);
                if (!result)
                {
                    throw new InvalidOperationException("无法解析武将等级");
                }

                return currentLevel;
            }, current => current >= level, () =>
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(490, 460), TimeSpan.FromMilliseconds(50));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(713, 278, 745, 299), "转生", "e9e7cf-000000"))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(545, 329));
                }
            });
        }

        #endregion
    }
}