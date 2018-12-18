// *******************************************************************
// * 文件名称： WlyBuildingViewBase.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-07 16:21:38
// *******************************************************************

using System.Reflection;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 建筑界面基类
    /// </summary>
    public abstract class WlyBuildingViewBase : WlyUIViewBase
    {
        #region Protected Properties

        /// <summary>
        /// 建筑名称的颜色
        /// </summary>
        protected virtual string BuildingColor => WlyColor.Normal;

        /// <summary>
        /// 建筑名称
        /// </summary>
        protected virtual string BuildingName
        {
            get
            {
                var attribute = GetType().GetCustomAttribute<WlyBuildingAttribute>();
                return attribute.Type.ToString();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public override sealed void Exit(string dmGuid)
        {
            DMService.Instance.LeftClick(dmGuid, new WxPoint(740, 158));
        }

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override sealed bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(298, 310, 360, 330), BuildingName, BuildingColor);
        }

        #endregion
    }
}