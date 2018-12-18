// *******************************************************************
// * 文件名称： WxPoint.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 10:53:25
// *******************************************************************

namespace Wx.Utility.WxCommon.Entity
{
    /// <summary>
    /// 代表一个点
    /// </summary>
    public class WxPoint
    {
        #region Constructors

        public WxPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public Properties

        public int X { get; set; }

        public int Y { get; set; }

        #endregion

        #region Public Methods

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((WxPoint)obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public WxPoint Shift(int x, int y)
        {
            return new WxPoint(X + x, Y + y);
        }

        #endregion

        #region Protected Methods

        protected bool Equals(WxPoint other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        #endregion
    }
}