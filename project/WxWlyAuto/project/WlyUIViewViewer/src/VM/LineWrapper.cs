// *******************************************************************
// * 文件名称： LineWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 18:16:30
// *******************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using Wx.Utility.UICommon;
using Wx.Utility.WxCommon.Entity;

namespace Wx.App.WlyUIViewViewer.VM
{
    /// <summary>
    /// 线段Wrapper
    /// </summary>
    public class LineWrapper : WxUIEntity
    {
        #region Fields

        /// <summary>
        /// Comment
        /// </summary>
        private SolidColorBrush m_brush = Brushes.Black;

        #endregion

        #region Public Properties

        /// <summary>
        /// Comment
        /// </summary>
        public SolidColorBrush Brush
        {
            get { return m_brush; }
            set
            {
                if (m_brush != value)
                {
                    m_brush = value;
                    OnPropertyChanged(nameof(Brush));
                }
            }
        }

        /// <summary>
        /// 是否跨级
        /// </summary>
        public bool Cross
        {
            get { return To.Row > From.Row + 1; }
        }

        /// <summary>
        /// Comment
        /// </summary>
        public Geometry Data
        {
            get
            {
                

                var width = WlyUIViewWrapper.Width;
                var height = WlyUIViewWrapper.Height;
                var tw = WlyUIViewWrapper.TWidth;
                var th = WlyUIViewWrapper.THeight;
                var interval = 4 * (To.Column - From.Column);
                var startX = From.X + (width / 2);
                var startY = From.Y + height / 2;
                var endX = To.X + (width / 2);
                var endY = To.Y + height / 2;
                return Geometry.Parse($"M {startX} {startY} L {endX} {endY}");

                var list = new List<WxPoint>();
                var x1 = From.X + (width / 2) + interval;
                var y1 = From.Y + height;
                var x2 = (To.X + (width / 2)) - interval;
                var y2 = To.Y - (th / 2) - interval;
                var y3 = To.Y;

                var point1 = new WxPoint((int)x1, (int)y1);
                var point2 = new WxPoint((int)x1, (int)y2);
                var point3 = new WxPoint((int)x2, (int)y2);
                var point4 = new WxPoint((int)x2, (int)y3);

                list.Add(point1);
                list.Add(point2);
                list.Add(point3);
                list.Add(point4);

                return Geometry.Parse($"M {point1.X} {point1.Y} {string.Join(" ", list.Select(o => $"L {o.X} {o.Y}"))}");
            }
        }

        public WlyUIViewWrapper From { get; set; }

        public WlyUIViewWrapper To { get; set; }

        /// <summary>
        /// 是否朝向右边
        /// </summary>
        public bool ToRight
        {
            get { return To.Column >= From.Column; }
        }

        #endregion
    }
}