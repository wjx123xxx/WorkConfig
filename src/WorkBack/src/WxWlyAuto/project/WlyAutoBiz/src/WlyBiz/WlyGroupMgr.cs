// *******************************************************************
// * 文件名称： WlyGroupMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 00:00:29
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 军团管理器
    /// </summary>
    public static class WlyGroupMgr
    {
        #region Public Methods

        /// <summary>
        /// 加入指定名称的军团
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Join(string dmGuid, string name)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团);

            while (true)
            {
                // 查找当页的海潮军团
                var result = DMService.Instance.FindStrEx(dmGuid, new WxRect(195, 293, 798, 448), "海潮军团", "ffffb3-000000");
                if (!string.IsNullOrEmpty(result))
                {
                    foreach (var group in result.Split('|'))
                    {
                        var pos = group.Split(',');
                        var point = new WxPoint(int.Parse(pos[1]), int.Parse(pos[2]));
                        DMService.Instance.LeftClick(dmGuid, point, TimeSpan.FromSeconds(2));

                        // 确认点击到的军团名称是否符合
                        if (DMService.Instance.FindStr(dmGuid, new WxRect(384, 182, 452, 209), name, "e9e7cf-000000"))
                        {
                            return JoinCore(dmGuid, name);
                        }
                    }
                }

                // 翻页
                var pageStr = DMService.Instance.GetWords(dmGuid, new WxRect(482, 466, 512, 487), "ffffff-000000");
                var pageStrs = pageStr.Split('/');
                if (pageStrs[0] == pageStrs[1])
                {
                    throw new InvalidOperationException($"未找到军团{name}");
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(533, 475));
            }
        }

        /// <summary>
        /// 升级军团科技
        /// </summary>
        /// <param name="dmGuid"></param>
        public static bool Upgrade(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团科技);

            // 检测科技等级
            var res = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(739, 340, 783, 364), "20ef4c-000000", out var l);
            if (res && (l < 100))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(516, 474));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(609, 476));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(587, 287));
                DMService.Instance.LeftClick(dmGuid, new WxPoint(436, 364));
                return false;
            }

            var startPoint = new WxPoint(237, 256);
            for (int i = 0; i < 6; i++)
            {
                DMService.Instance.LeftClick(dmGuid, startPoint.Shift(74 * i, 0));

                // 检测科技等级
                var result = WlyUtilityBiz.GetAmount(dmGuid, new WxRect(739, 340, 783, 364), "20ef4c-000000", out var level);
                if (!result)
                {
                    throw new InvalidOperationException("无法检测到科技等级");
                }

                if (level < 100)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(516, 474));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(609, 476));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(587, 287));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(436, 364));
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 考虑已经加入和军团人数已满的情况
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool JoinCore(string dmGuid, string name)
        {
            if (DMService.Instance.FindStr(dmGuid, new WxRect(667, 465, 728, 490), "加入军团", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(695, 479), TimeSpan.FromSeconds(1));
            }

            // 确认加入成功
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团信息);
            if (DMService.Instance.FindStr(dmGuid, new WxRect(384, 183, 454, 208), name, "e9e7cf-000000"))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}