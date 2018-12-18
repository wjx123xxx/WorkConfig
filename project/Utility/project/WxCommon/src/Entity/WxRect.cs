// *******************************************************************
// * 文件名称： WxRect.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 10:53:00
// *******************************************************************

namespace Wx.Utility.WxCommon.Entity
{
    /// <summary>
    /// 代表一个矩形
    /// </summary>
    public class WxRect
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxRect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxRect(WxPoint center, int width, int height)
        {
            Left = center.X - (width / 2);
            Top = center.Y - (height / 2);
            Right = center.X + (width / 2);
            Bottom = center.Y + (height / 2);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WxRect(WxPoint center, int length) : this(center, length, length)
        {
        }

        #endregion

        #region Public Properties

        public int Bottom { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public int Top { get; set; }

        #endregion
    }
}