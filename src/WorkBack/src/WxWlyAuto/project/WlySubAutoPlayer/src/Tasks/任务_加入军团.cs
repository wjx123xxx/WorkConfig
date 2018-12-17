// *******************************************************************
// * 文件名称： 任务_加入军团.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-18 23:59:25
// *******************************************************************

using System;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 加入军团
    /// </summary>
    public class 任务_加入军团 : WlyMainTask
    {
        #region Constructors

        public 任务_加入军团(string id, params string[] depends) : base(id, depends)
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
            var index = ((SubEntity)entity).Index;
            var name = $"海潮{(index / 200) + 1:D2}番队";
            var result = WlyGroupMgr.Join(dmGuid, name);
            if (result)
            {
                return new WlyTaskInfo(ID, true);
            }

            throw new InvalidOperationException("加入军团失败");
        }

        #endregion
    }
}