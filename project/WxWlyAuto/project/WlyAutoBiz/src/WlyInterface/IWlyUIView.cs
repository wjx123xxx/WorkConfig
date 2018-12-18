// *******************************************************************
// * 文件名称： IWlyUIView.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-10 17:27:24
// *******************************************************************

using System.Collections.Generic;

using Wx.Lib.WlyAutoBiz.WlyDefinition;

namespace Wx.Lib.WlyAutoBiz.WlyInterface
{
    /// <summary>
    /// 卧龙吟界面UI接口
    /// </summary>
    public interface IWlyUIView
    {
        #region Public Properties

        /// <summary>
        /// 子界面列表
        /// </summary>
        IEnumerable<WlyViewType> Children { get; }

        /// <summary>
        /// 界面层级
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// 界面类型
        /// </summary>
        WlyViewType Type { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 是否能从本界面前往指定的界面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        bool CanGoTo(WlyViewType type, string dmGuid);

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        void Exit(string dmGuid);

        /// <summary>
        /// 与指定界面的距离
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetLength(WlyViewType type);

        /// <summary>
        /// 从当前界面前往指定的界面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        bool GoTo(WlyViewType type, string dmGuid);

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        bool IsCurrentView(string dmGuid);

        #endregion
    }
}