// *******************************************************************
// * 文件名称： 场景_资源区.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-19 17:30:13
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyViews.Scene
{
    /// <summary>
    /// 资源区界面
    /// </summary>
    [WlyView(WlyViewType.场景_资源区)]
    public class 场景_资源区 : 场景_主界面
    {
        #region Public Methods

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public override bool IsCurrentView(string dmGuid)
        {
            return DMService.Instance.FindStr(dmGuid, new WxRect(528, 529, 566, 563), "转到", "ffffcc-000000");

            //return DMService.Instance.FindPic(dmGuid, WlyPicType.资源区, new WxRect(918, 88, 989, 144));
        }

        #endregion
    }
}