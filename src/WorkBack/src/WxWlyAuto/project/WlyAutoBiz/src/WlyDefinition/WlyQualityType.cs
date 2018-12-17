// *******************************************************************
// * 文件名称： WlyQualityType.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-10 00:20:04
// *******************************************************************

using System;

namespace Wx.Lib.WlyAutoBiz.WlyDefinition
{
    /// <summary>
    /// 品质类型
    /// </summary>
    [Flags]
    public enum WlyQualityType
    {
        Unknow = 0,

        White = 1,

        Blue = 2,

        Green = 4,

        Yellow = 8,

        Red = 16,

        Purple = 32,

        Orange = 64,

        神皇 = 128,

        灭世 = 512,

        盘古 = 1024,

        混沌 = 2048
    }
}