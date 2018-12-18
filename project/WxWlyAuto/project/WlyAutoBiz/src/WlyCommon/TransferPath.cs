// *******************************************************************
// * 文件名称： TransferPath.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 22:54:36
// *******************************************************************

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 切换路径
    /// </summary>
    public struct TransferPath
    {
        public WlyViewType Target { get; set; }

        public int Length { get; set; }
    }
}