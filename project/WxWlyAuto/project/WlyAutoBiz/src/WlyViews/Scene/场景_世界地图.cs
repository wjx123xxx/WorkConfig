// *******************************************************************
// * 文件名称： 场景_世界地图.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 16:11:53
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 世界地图
    /// </summary>
    [WlyView(WlyViewType.场景_世界地图)]
    public class 场景_世界地图 : 场景_主界面
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(544, 258, 579, 283), "洛", "f3f3da-000000");
        }

        #endregion
    }
}