// *******************************************************************
// * 文件名称： TaskWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-23 11:26:19
// *******************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Utility.UICommon;

namespace Wx.App.WlyTaskViewer.VM
{
    /// <summary>
    /// 任务包装器
    /// </summary>
    public class TaskWrapper : WxUIEntity
    {
        #region Fields

        private static int _interval = 4;

        public static int m_maxColumn = 0;

        private readonly SolidColorBrush[] m_brushes =
            { Brushes.DodgerBlue, Brushes.OrangeRed, Brushes.BlueViolet, Brushes.Fuchsia, Brushes.Brown, Brushes.DeepSkyBlue, Brushes.ForestGreen };

        private readonly IList<TaskWrapper> m_children = new List<TaskWrapper>();

        private readonly IList<TaskWrapper> m_parents = new List<TaskWrapper>();

        private readonly WlyTaskBase m_task;

        private int m_childLength = -1;

        private int m_column;

        private int m_row = -1;

        #endregion

        #region Constructors

        public TaskWrapper(WlyTaskBase task)
        {
            m_task = task;
        }

        #endregion

        #region Public Properties

        public static double Height { get; } = 50;

        public static double THeight { get; } = 60;

        public static double TWidth { get; } = 60;

        public static double Width { get; } = 140;

        public SolidColorBrush Brush
        {
            get
            {
                if (m_task is WlyDailyTask)
                {
                    return Brushes.Blue;
                }

                return Brushes.Green;
            }
        }

        public int ChildLength
        {
            get
            {
                if (!m_children.Any())
                {
                    return 0;
                }

                if (m_childLength != -1)
                {
                    return m_childLength;
                }

                m_childLength = m_children.Max(o => o.ChildLength) + 1;
                return m_childLength;
            }
        }

        public IList<TaskWrapper> Children => m_children;

        public int Column
        {
            get => m_column;
            set
            {
                m_column = value;
                if (Parents.Any() && Parents.All(o => o.Column > m_column))
                {
                    m_column = Parents.Min(o => o.Column);
                }
            }
        }

        public string[] Depends => m_task.Depends;

        public string MainTitle
        {
            get { return m_task.MainTitle; }
        }

        public IList<TaskWrapper> Parents => m_parents;

        public int Row
        {
            get
            {
                if (m_row == -1)
                {
                    if (!Parents.Any())
                    {
                        m_row = 0;
                    }
                    else
                    {
                        m_row = Parents.Max(o => o.Row) + 1;
                    }
                }

                return m_row;
            }
            set => m_row = value;
        }

        public string SubTitle
        {
            get { return m_task.SubTitle; }
        }

        public double X => (Width + TWidth) * Column;

        public double Y => (THeight + Height) * Row;

        #endregion

        #region Public Methods

        /// <summary>
        /// 任务之间的连接关系
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineWrapper> GetLinks()
        {
            foreach (var child in Children)
            {
                yield return new LineWrapper
                {
                    From = this,
                    To = child,
                    Brush = Brushes.Brown
                    //Brush = m_brushes[Column > child.Column ? Column : child.Column]
                };
            }
        }

        #endregion
    }
}