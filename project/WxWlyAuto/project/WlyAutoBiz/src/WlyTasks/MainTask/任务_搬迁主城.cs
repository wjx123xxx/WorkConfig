// *******************************************************************
// * 文件名称： 任务_搬迁主城.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-22 10:35:09
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyTasks.MainTask
{
    /// <summary>
    /// 搬迁
    /// </summary>
    public class 任务_搬迁主城 : WlyMainTask
    {
        #region Fields

        private WlyCityType m_city;

        #endregion

        #region Constructors

        public 任务_搬迁主城(string id, WlyCityType target, params string[] depends) : base(id, depends)
        {
            m_city = target;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public override string SubTitle => $"搬迁到 {m_city.ToString()}";

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
            if (DMService.Instance.FindStr(dmGuid, new WxRect(530, 323, 565, 340), "取消", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(dmGuid, new WxPoint(455, 330));
            }

            var result = WlyMapMgr.MoveTo(dmGuid, m_city);
            return new WlyTaskInfo(ID, result);
        }

        #endregion
    }
}