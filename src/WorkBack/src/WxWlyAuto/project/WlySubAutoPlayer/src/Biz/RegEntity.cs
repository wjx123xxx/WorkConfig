// *******************************************************************
// * 文件名称： RegEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 16:43:52
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Extension;

namespace Wx.App.WlySubAutoPlayer.Biz
{
    /// <summary>
    /// 注册用实体
    /// </summary>
    public class RegEntity
    {
        #region Public Methods

        public SubEntity GetNewEntity()
        {
            WlyUtilityBiz.Reg();


            SubAccountInfo info = new SubAccountInfo(MathHelper.GetNewGuid());
            info.Account = "123";
            info.Password = "456";
            return new SubEntity(info);
        }

        #endregion
    }
}