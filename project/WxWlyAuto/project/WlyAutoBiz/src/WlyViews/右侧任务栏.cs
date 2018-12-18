// *******************************************************************
// * 文件名称： 右侧任务栏.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-18 21:40:22
// *******************************************************************

using System;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 任务界面
    /// </summary>
    [WlyView(WlyViewType.右侧任务栏)]
    public class 右侧任务栏 : WlyUIViewBase
    {
        #region Fields

        private readonly WxPoint m_exit = new WxPoint(817, 178);

        private readonly WxRect m_rect = new WxRect(882, 167, 945, 190);

        private readonly string m_strColor = "f2f2da-000000";

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override void Exit(string dmGuid)
        {
            FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(803, 220, 863, 241), "领取奖励", "1fed4a-000000", WlyColor.White))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(829, 232), TimeSpan.FromSeconds(1));
                    if (DMService.Instance.FindStr(dmGuid, new WxRect(475, 360, 535, 380), "领取奖励", WlyColor.Normal))
                    {
                        DMService.Instance.LeftClick(dmGuid, new WxPoint(504, 371));
                    }
                    return false;
                }

                return true;
            }, TimeSpan.FromSeconds(15));

            DMService.Instance.LeftClick(dmGuid, m_exit);
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, m_rect, "主线任务", "ffffff-000000");
        }

        #endregion
    }
}