// *******************************************************************
// * 文件名称： WlyFormationMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 14:00:18
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 阵型管理器
    /// </summary>
    public static class WlyFormationMgr
    {
        #region Fields

        private static IDictionary<WlyFormationType, WxPoint> _formationMap = new ConcurrentDictionary<WlyFormationType, WxPoint>();

        private static IDictionary<int, WxPoint> _locationMap = new ConcurrentDictionary<int, WxPoint>();

        #endregion

        #region Constructors

        static WlyFormationMgr()
        {
            _formationMap.Add(WlyFormationType.鱼鳞阵, new WxPoint(572, 231));
            _formationMap.Add(WlyFormationType.乱剑阵, new WxPoint(572, 274));

            // 上阵定位表
            _locationMap.Add(1, new WxPoint(500, 253));
            _locationMap.Add(2, new WxPoint(500, 321));
            _locationMap.Add(3, new WxPoint(500, 388));
            _locationMap.Add(4, new WxPoint(429, 253));
            _locationMap.Add(5, new WxPoint(429, 321));
            _locationMap.Add(6, new WxPoint(429, 388));
            _locationMap.Add(7, new WxPoint(363, 253));
            _locationMap.Add(8, new WxPoint(363, 321));
            _locationMap.Add(9, new WxPoint(363, 388));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 设置阵型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dmGuid"></param>
        public static void SetFormation(WlyFormationType type, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_阵型);

            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(386, 432, 438, 453), type.ToString(), "f3f3da-000000"))
                {
                    return true;
                }

                DMService.Instance.LeftClick(dmGuid, _formationMap[type]);
                DMService.Instance.LeftClick(dmGuid, new WxPoint(599, 463));
                return false;
            }, TimeSpan.FromSeconds(5));

            if (!wait)
            {
                throw new InvalidOperationException("无法设置指定阵型");
            }
            DMService.Instance.LeftClick(dmGuid, new WxPoint(500, 322));
        }

        /// <summary>
        /// 武将上阵
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="formation"></param>
        /// <param name="index"></param>
        /// <param name="dmGuid"></param>
        public static void SetStaff(WlyStaffType staff, WlyFormationType formation, int index, string dmGuid)
        {
            SetFormation(formation, dmGuid);

            WlyUtilityBiz.SelectStaffInList(dmGuid, staff);
            if (DMService.Instance.FindStr(dmGuid, new WxRect(228, 458, 267, 487), "下阵", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(251, 471));
                WlyUtilityBiz.SelectStaffInList(dmGuid, staff);
            }

            DMService.Instance.LeftClick(dmGuid, new WxPoint(248, 474));
            DMService.Instance.LeftClick(dmGuid, _locationMap[index]);
            DMService.Instance.LeftClick(dmGuid, new WxPoint(386, 449));

            // 检测
            DMService.Instance.LeftClick(dmGuid, _locationMap[index]);
            var result = DMService.Instance.FindStr(dmGuid, new WxRect(718, 307, 796, 328), staff.ToString(), WlyUtilityBiz.GetStaffQualityStr());
            if (!result)
            {
                throw new InvalidOperationException("武将上阵失败");
            }
        }

        #endregion
    }
}