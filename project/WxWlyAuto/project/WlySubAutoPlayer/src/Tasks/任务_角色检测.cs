// *******************************************************************
// * 文件名称： 任务_角色检测.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-21 12:38:37
// *******************************************************************

using System;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 角色检测
    /// </summary>
    public class 任务_角色检测 : WlyMainTask
    {
        #region Constructors

        public 任务_角色检测(string id, params string[] depends) : base(id, depends)
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
            if (sub.Info.Check)
            {
                return new WlyTaskInfo(ID, true);
            }

            // 开始检测
            var dmGuid = sub.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_军团);
            var result = DMService.Instance.FindPic(dmGuid, WlyPicType.吴国军团, new WxRect(189, 287, 794, 443), out var x, out var y);
            if (result)
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                if (DMService.Instance.FindStr(dmGuid, new WxRect(390, 182, 443, 211), "吴之团", "e9e7cf-000000"))
                {
                    sub.Info.Check = true;
                    return new WlyTaskInfo(ID, true);
                }
            }

            throw new InvalidOperationException("认证失败");
        }

        #endregion
    }
}