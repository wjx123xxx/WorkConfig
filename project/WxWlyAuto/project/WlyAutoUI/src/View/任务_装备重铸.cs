// *******************************************************************
// * 文件名称： 任务_装备重铸.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-30 21:58:24
// *******************************************************************

using System;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlyAutoUI.View
{
    /// <summary>
    /// 装备重铸
    /// </summary>
    [WlyTask(WlyTaskType.装备重铸)]
    public class 任务_装备重铸 : WlyDailyTask
    {
        #region Constructors

        public 任务_装备重铸(string id, params string[] depends) : base(id, depends)
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
            if (DMService.Instance.FindStr(dmGuid, new WxRect(511, 176, 548, 199), "物品", WlyColor.Normal))
            {
                var equip = WlyEquipMgr.GetEquipInfo(dmGuid);
                // 每次重铸执行100次
                for (var i = 0; i < 1000; i++)
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(555, 428));
                    FlowLogicHelper.RepeatRun(() =>
                    {
                        if (equip.Type == WlyEquipType.带兵量)
                        {
                            if (DMService.Instance.FindColor(dmGuid, "20ef4c-000000", new WxRect(780, 412, 798, 427)))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(584, 425), TimeSpan.FromSeconds(1));
                                return true;
                            }

                            if (DMService.Instance.FindColor(dmGuid, "ff0000-000000", new WxRect(780, 412, 798, 427)))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 428));
                                return true;
                            }
                        }
                        else
                        {
                            if (DMService.Instance.FindColor(dmGuid, "20ef4c-000000", new WxRect(780, 437, 798, 453)))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(584, 425), TimeSpan.FromSeconds(1));
                                return true;
                            }



                            if (DMService.Instance.FindColor(dmGuid, "ff0000-000000", new WxRect(780, 437, 798, 453)))
                            {
                                DMService.Instance.LeftClick(dmGuid, new WxPoint(502, 428));
                                return true;
                            }
                        }

                        Thread.Sleep(500);

                        return true;
                    }, TimeSpan.FromSeconds(5));
                }
            }

            return new WlyTaskInfo(ID, true);
        }

        #endregion
    }
}