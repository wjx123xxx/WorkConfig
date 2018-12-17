// *******************************************************************
// * 文件名称： WlyBuildingMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-28 23:42:25
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 建筑升级器
    /// </summary>
    public static class WlyBuildingMgr
    {
        #region Fields

        /// <summary>
        /// 建筑类型与视图类型映射
        /// </summary>
        private static readonly IDictionary<WlyBuildingType, WlyViewType> _typeMap = new Dictionary<WlyBuildingType, WlyViewType>();

        #endregion

        #region Constructors

        static WlyBuildingMgr()
        {
            var views = typeof(WlyUIViewBase).Assembly.GetTypes();
            foreach (var viewType in views)
            {
                var bt = viewType.GetCustomAttribute<WlyBuildingAttribute>();
                if (bt == null)
                {
                    continue;
                }

                _typeMap.Add(bt.Type, viewType.GetCustomAttribute<WlyViewAttribute>().Type);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取指定建筑的等级
        /// </summary>
        /// <param name="buildingType"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static int GetBuildingLevel(WlyBuildingType buildingType, string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, _typeMap[buildingType]);

            // 获取建筑等级
            var levelStr = DMService.Instance.GetWords(dmGuid, new WxRect(291, 308, 518, 333), "ffffff-000000", 1);
            var indexOf = levelStr.IndexOf("级", StringComparison.Ordinal);
            if (indexOf == -1)
            {
                return 0;
            }

            bool result = int.TryParse(levelStr.Substring(0, indexOf), out var level);
            if (!result)
            {
                throw new InvalidOperationException("错误的等级格式");
            }

            return level;
        }

        /// <summary>
        /// 升级指定的建筑到指定的等级
        /// </summary>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <param name="dmGuid"></param>
        /// <returns>消除冷却需要花费的金币数量</returns>
        public static int Upgrade(WlyBuildingType type, int level, string dmGuid)
        {
            FlowLogicHelper.RunToTarget(() => GetBuildingLevel(type, dmGuid), current =>
            {
                if (!string.IsNullOrEmpty(DMService.Instance.GetWords(dmGuid, new WxRect(530, 244, 611, 289), "ff6600-000000")))
                {
                    return true;
                }

                return current >= level;
            }, () => DMService.Instance.LeftClick(dmGuid, new WxPoint(663, 228), TimeSpan.FromMilliseconds(200)));

            // 升级建筑
            var costStr = DMService.Instance.GetWords(dmGuid, new WxRect(530, 244, 611, 289), "ff6600-000000");
            if (!string.IsNullOrEmpty(costStr))
            {
                if (!costStr.Contains("金币"))
                {
                    throw new InvalidOperationException("无效的金币数量");
                }

                return int.Parse(costStr.Substring(0, costStr.Length - 2));
            }

            return 0;
        }

        #endregion
    }
}