// *******************************************************************
// * 文件名称： WindowService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-13 20:02:57
// *******************************************************************

namespace Wx.Utility.WxForDM.Service
{
    /// <summary>
    /// 窗口服务
    /// </summary>
    public partial class DMService
    {
        #region Public Methods

        /// <summary>
        /// 枚举窗口
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="processID"></param>
        /// <param name="title"></param>
        /// <param name="className"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string EnumWindowByProcessId(string dmGuid, int processID, string title, string className, int filter)
        {
            var dm = GetDM(dmGuid);
            return dm.EnumWindowByProcessId(processID, title, className, filter);
        }

        /// <summary>
        /// 通过进程ID寻找指定窗口句柄
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="processID"></param>
        /// <param name="className"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public int FindWindowByProcessId(string dmGuid, int processID, string className, string title)
        {
            var dm = GetDM(dmGuid);
            return dm.FindWindowByProcessId(processID, className, title);
        }

        /// <summary>
        /// 获取窗口状态
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="hwnd"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int GetWindowState(string dmGuid, int hwnd, int flag)
        {
            var dm = GetDM(dmGuid);
            return dm.GetWindowState(hwnd, flag);
        }

        public void SendPaste(string dmGuid, int hwnd, string message)
        {
            var dm = GetDM(dmGuid);
            dm.SetClipboard(message);
            dm.SendPaste(hwnd);
        }

        /// <summary>
        /// 向窗口发送字符串
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="hwnd"></param>
        /// <param name="message"></param>
        public void SendString(string dmGuid, int hwnd, string message)
        {
            var dm = GetDM(dmGuid);
            dm.SendString(hwnd, message);
        }

        #endregion
    }
}