// *******************************************************************
// * 文件名称： 任务_开发城池.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-26 16:00:12
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 开发城池
    /// </summary>
    public class 任务_开发城池 : WlyMainTask
    {
        #region Fields

        private readonly WlyCityType m_city;

        #endregion

        #region Constructors

        public 任务_开发城池(string id, WlyCityType city, params string[] depends) : base(id, depends)
        {
            m_city = city;
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
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_世界地图);

            // 确认投资冷却时间
            if (DMService.Instance.FindStr(dmGuid, new WxRect(100, 212, 145, 234), "可投资", "20ef4c-000000"))
            {
                WlyMapMgr.OpenCity(dmGuid, m_city);
                var s1 = DMService.Instance.GetWords(dmGuid, new WxRect(470, 352, 565, 370), "e9e7cf-000000");
                var v1 = int.Parse(s1.Split('/')[0]);
                WlyUtilityBiz.SystemInfo.Develop1 = v1;

                var s2 = DMService.Instance.GetWords(dmGuid, new WxRect(470, 374, 565, 939), "e9e7cf-000000");
                var v2 = int.Parse(s2.Split('/')[0]);
                WlyUtilityBiz.SystemInfo.Develop2 = v2;
                WlyUtilityBiz.SystemInfo.Save();

                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(470, 214, 494, 236), "城", WlyColor.Normal))
                    {
                        return true;
                    }

                    DMService.Instance.LeftClick(dmGuid, new WxPoint(660, 277));
                    return false;
                }, TimeSpan.FromSeconds(5));
                if (!wait)
                {
                    throw new InvalidOperationException("无法打开城池进行开发");
                }

                if (v1 > v2)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(583, 267));
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(536, 301));
                }

                DMService.Instance.LeftClick(dmGuid, new WxPoint(440, 367), TimeSpan.FromSeconds(2));
            }

            if (!DMService.Instance.FindColor(dmGuid, "33ffff-000000", new WxRect(100, 212, 160, 234)))
            {
                return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
            }

            var coolTime = WlyUtilityBiz.GetTime(dmGuid, new WxRect(100, 212, 160, 234), "33ffff-000000");
            return new WlyTaskInfo(ID, DateTime.Now.Add(coolTime));
        }

        #endregion
    }
}