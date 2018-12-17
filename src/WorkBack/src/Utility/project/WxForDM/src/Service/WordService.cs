// *******************************************************************
// * 文件名称： WordService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 15:10:42
// *******************************************************************

using Wx.Utility.WxCommon.Entity;

namespace Wx.Utility.WxForDM.Service
{
    /// <summary>
    /// 文字服务
    /// </summary>
    public partial class DMService
    {
        #region Public Methods

        /// <summary>
        /// 开启全局字库
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="enable"></param>
        public void EnableShareDict(string dmGuid, bool enable)
        {
            var dm = GetDM(dmGuid);
            dm.EnableShareDict(enable ? 1 : 0);
        }

        /// <summary>
        /// 抓取指定的文字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool FetchWords(string dmGuid, WxRect rect, string color, string word, int index, string filePath)
        {
            var dm = GetDM(dmGuid);
            var info = dm.FetchWord(rect.Left, rect.Top, rect.Right, rect.Bottom, color, word);
            var result = dm.AddDict(index, info);
            if (result == 1)
            {
                return dm.SaveDict(index, filePath) == 1;
            }

            return false;
        }

        /// <summary>
        /// 寻找指定颜色的字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="target"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool FindStr(string dmGuid, WxRect rect, string target, string color, out int x, out int y)
        {
            var dm = GetDM(dmGuid);
            var find = dm.FindStr(rect.Left, rect.Top, rect.Right, rect.Bottom, target, color, 1, out var xx, out var yy);
            if (find > -1)
            {
                x = (int)xx;
                y = (int)yy;
                return true;
            }

            x = -1;
            y = -1;
            return false;
        }

        /// <summary>
        /// 寻找指定颜色的字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="target"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool FindStr(string dmGuid, WxRect rect, string target, string color)
        {
            return FindStr(dmGuid, rect, target, color, out var _, out var _);
        }

        /// <summary>
        /// 寻找指定颜色的字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="target"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        public bool FindStr(string dmGuid, WxRect rect, string target, params string[] colors)
        {
            var color = string.Join("|", colors);
            return FindStr(dmGuid, rect, target, color, out var _, out var _);
        }

        /// <summary>
        /// 找字扩展
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="target"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public string FindStrEx(string dmGuid, WxRect rect, string target, string color)
        {
            var dm = GetDM(dmGuid);
            return dm.FindStrEx(rect.Left, rect.Top, rect.Right, rect.Bottom, target, color, 1);
        }

        public bool FindStrFast(string dmGuid, WxRect rect, string target, string color, out int x, out int y)
        {
            var dm = GetDM(dmGuid);
            var find = dm.FindStrFast(rect.Left, rect.Top, rect.Right, rect.Bottom, target, color, 1, out var xx, out var yy);
            if (find > -1)
            {
                x = (int)xx;
                y = (int)yy;
                return true;
            }

            x = -1;
            y = -1;
            return false;
        }

        /// <summary>
        /// 获取指定范围内指定颜色的字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetWords(string dmGuid, WxRect rect, string color, int index = 0)
        {
            var dm = GetDM(dmGuid);
            var resultStr = dm.GetWords(rect.Left, rect.Top, rect.Right, rect.Bottom, color, 1);
            if (!string.IsNullOrEmpty(resultStr))
            {
                return dm.GetWordResultStr(resultStr, index);
            }

            return string.Empty;
        }

        public void SendStringIme(string dmGuid, string message)
        {
            var dm = GetDM(dmGuid);
            dm.SendStringIme(message);
        }

        /// <summary>
        /// 设置字典
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="dictPath"></param>
        /// <param name="index"></param>
        public void SetDict(string dmGuid, string dictPath, int index)
        {
            var dm = GetDM(dmGuid);
            dm.SetDict(index, dictPath);
        }

        #endregion
    }
}