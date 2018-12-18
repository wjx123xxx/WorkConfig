// *******************************************************************
// * 文件名称： WlySwitchInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-19 10:04:57
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 任务开关
    /// </summary>
    public class WlySwitchInfo
    {
        #region Public Properties

        /// <summary>
        /// 启用状态
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 开关类型
        /// </summary>
        public WlySwitchType Type { get; set; }

        #endregion
    }
}