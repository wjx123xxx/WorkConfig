// *******************************************************************
// * 文件名称： WlyUIViewWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 17:19:13
// *******************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInterface;
using Wx.Utility.UICommon;

namespace Wx.App.WlyUIViewViewer.VM
{
    /// <summary>
    /// 视图包装器
    /// </summary>
    public class WlyUIViewWrapper : WxUIEntity
    {
        #region Fields

        private readonly IWlyUIView m_view;

        /// <summary>
        /// Comment
        /// </summary>
        private SolidColorBrush m_brush = Brushes.Black;

        private int m_childLength = -1;

        /// <summary>
        /// 列
        /// </summary>
        private int m_column;

        /// <summary>
        /// 排
        /// </summary>
        private int m_row;

        #endregion

        #region Constructors

        public WlyUIViewWrapper(IWlyUIView view)
        {
            m_view = view;
            var level = m_view.Level;
            if (level == WlyViewMgr.LevelMax)
            {
                m_row = 0;
            }
            else
            {
                m_row = level + 1;
            }
        }

        #endregion

        #region Public Properties

        public static double Height { get; } = 50;

        public static double THeight { get; } = 60;

        public static double TWidth { get; } = 60;

        public static double Width { get; } = 140;

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

        public int ChildLength
        {
            get
            {
                if (!Children.Any())
                {
                    return 0;
                }

                if (m_childLength != -1)
                {
                    return m_childLength;
                }

                //m_childLength = Children.Max(o => o.ChildLength) + 1;
                return m_childLength;
            }
        }

        public IList<WlyUIViewWrapper> Children { get; } = new List<WlyUIViewWrapper>();

        /// <summary>
        /// 列
        /// </summary>
        public int Column
        {
            get { return m_column; }
            set
            {
                if (m_column != value)
                {
                    m_column = value;
                    OnPropertyChanged(nameof(Column));
                }
            }
        }

        public string Name
        {
            get { return m_view.Type.ToString(); }
        }

        public WlyUIViewWrapper Parent { get; set; }

        /// <summary>
        /// 排
        /// </summary>
        public int Row
        {
            get { return m_row; }
            set
            {
                if (m_row != value)
                {
                    m_row = value;
                    OnPropertyChanged(nameof(Row));
                }
            }
        }

        public IEnumerable<WlyViewType> SubViewTypes
        {
            get { return m_view.Children; }
        }

        public double X => (Width + TWidth) * Column;

        public double Y => (THeight + Height) * Row;

        #endregion

        #region Public Methods

        public IEnumerable<LineWrapper> GetLinks()
        {
            foreach (var child in Children)
            {
                yield return new LineWrapper
                {
                    From = this,
                    To = child,
                    Brush = Brushes.Brown
                };
            }
        }

        #endregion
    }
}