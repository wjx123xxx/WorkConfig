﻿// *******************************************************************
// * 文件名称： RunningState.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-08 21:51:28
// *******************************************************************

namespace Wx.App.BizCore
{
    /// <summary>
    /// 窗口运行状态
    /// </summary>
    public enum RunningState
    {
        // 等待开始
        Wait,

        // 开始运行
        Running,

        // 停止运行
        Stopped
    }
}