// *******************************************************************
// * 文件名称： 任务_名称检测.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 13:51:25
// *******************************************************************

using System;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 名称检测
    /// </summary>
    public class 任务_名称检测 : WlyMainTask
    {
        #region Constructors

        public 任务_名称检测(string id, params string[] depends) : base(id, depends)
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
            var sub = (SubEntity)entity;
            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);

            // 获取名称
            var name = DMService.Instance.GetWords(dmGuid, new WxRect(54, 0, 134, 24), "eae7d0-000000");
            if (!name.Contains("海潮"))
            {
                WxLog.Error($"任务_名称检测.InternalRun Invalid Name <{name}> Account <{entity.Account}> Psw <{entity.Password}> UID<{entity.AccountInfo.UID}>");
                return new WlyTaskInfo(ID)
                {
                    NextRunTime = DateTime.Now.AddDays(7)
                };
            }

            try
            {
                sub.Info.Name = name;
                sub.Info.Index = int.Parse(name.Substring(2));
                entity.Stop();
                return new WlyTaskInfo(ID, true);
            }
            catch (Exception ex)
            {
                WxLog.Error($"任务_名称检测.InternalRun Name<{name}> Error <{ex}>");
                return new WlyTaskInfo(ID, DateTime.Now.AddDays(7));
            }
        }

        #endregion
    }
}