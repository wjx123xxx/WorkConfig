// *******************************************************************
// * 文件名称： 任务_军团日常.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-19 00:37:55
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyUtility;

namespace Wx.App.WlySubAutoPlayer.Tasks
{
    /// <summary>
    /// 军团日常
    /// </summary>
    public class 任务_军团日常 : WlyMainTask
    {
        #region Constructors

        public 任务_军团日常(string id, params string[] depends) : base(id, depends)
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
            // 升级军团科技
            var dmGuid = entity.DMGuid;
            if (WlyGroupMgr.Upgrade(dmGuid))
            {
                return new WlyTaskInfo(ID, true);
            }

            return new WlyTaskInfo(ID, WlyUtilityBiz.GetRefreshTime());
        }

        #endregion
    }
}