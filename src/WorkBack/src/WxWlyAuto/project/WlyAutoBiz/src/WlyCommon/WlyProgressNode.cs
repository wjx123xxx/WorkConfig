// *******************************************************************
// * 文件名称： WlyProgressNode.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-06-22 22:38:44
// *******************************************************************

using Wx.Utility.WxCommon.Entity;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 副本进度节点
    /// </summary>
    public class WlyProgressNode
    {
        #region Public Properties

        /// <summary>
        /// 节点主键
        /// </summary>
        public string ID => $"wly{Progress}_{Location.X}_{Location.Y}";

        /// <summary>
        /// 节点序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否为最后一个节点
        /// </summary>
        public bool IsEnd { get; set; }

        /// <summary>
        /// 节点坐标
        /// </summary>
        public WxPoint Location { get; set; }

        /// <summary>
        /// 下一个节点
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// 上一个节点
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// 主进度
        /// </summary>
        public int Progress { get; set; }

        #endregion
    }
}