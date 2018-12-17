// *******************************************************************
// * 文件名称： 功能_训练.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 14:26:53
// *******************************************************************

using System;
using System.Text.RegularExpressions;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews
{
    /// <summary>
    /// 功能_训练
    /// </summary>
    [WlyView(WlyViewType.功能_训练)]
    public class 功能_训练 : 总成_武将
    {

        #region Fields

        private readonly Regex m_regex = new Regex(@"(\d)/(\d)");

        #endregion

        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(284, 152, 314, 169), "训练", WlyColor.Normal);
        }

        /// <summary>
        /// 获取可用训练位置数量
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public int GetAvailableSpace(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_训练);

            var countStr = DMService.Instance.GetWords(dmGuid, new WxRect(209, 467, 236, 483), "f3f3da-000000");
            if (!m_regex.IsMatch(countStr))
            {
                throw new InvalidOperationException($"Invalid Str <{countStr}>");
            }

            return int.Parse(m_regex.Match(countStr).Groups[2].ToString());
        }

        /// <summary>
        /// 获取训练位置总数
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public int GetTotalSpace(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_训练);

            var countStr = DMService.Instance.GetWords(dmGuid, new WxRect(209, 467, 236, 483), "f3f3da-000000");
            if (!m_regex.IsMatch(countStr))
            {
                throw new InvalidOperationException($"Invalid Str <{countStr}>");
            }

            return int.Parse(m_regex.Match(countStr).Groups[1].ToString());
        }

        #endregion
    }
}