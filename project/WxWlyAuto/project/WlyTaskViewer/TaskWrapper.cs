// *******************************************************************
// * 文件名称： TaskWrapper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-23 11:26:19
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Wx.Lib.WlyAutoBiz.WlyCommon;

namespace WlyTaskViewer
{
    /// <summary>
    /// 任务包装器
    /// </summary>
    public class TaskWrapper
    {
        #region Constants

        public const double Height = 40;

        #endregion

        #region Fields

        private static int _interval = 4;

        public static int m_maxColumn = 0;

        private static double TWidth = 60;

        private static double Offset = TWidth / 2;

        public static double THeight = 60;

        private static double Width = 140;

        private readonly SolidColorBrush[] m_brushes =
            { Brushes.DodgerBlue, Brushes.OrangeRed, Brushes.BlueViolet, Brushes.ForestGreen, Brushes.Fuchsia, Brushes.Brown, Brushes.DeepSkyBlue };

        private readonly IList<TaskWrapper> m_children = new List<TaskWrapper>();

        private readonly IList<TaskWrapper> m_parents = new List<TaskWrapper>();

        private readonly WlyTaskBase m_task;

        private int m_row = -1;

        #endregion

        #region Constructors

        public TaskWrapper(WlyTaskBase task)
        {
            m_task = task;
        }

        #endregion

        #region Public Properties

        public int ChildLength
        {
            get
            {
                if (!m_children.Any())
                {
                    return 0;
                }

                return m_children.Max(o => o.ChildLength) + 1;
            }
        }

        public IList<TaskWrapper> Children => m_children;

        public int Column { get; set; }

        public string[] Depends => m_task.Depends;

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

        public double X => (Width + TWidth) * Column;

        public double Y => (THeight + Height) * Row;

        #endregion

        #region Public Methods

        /// <summary>
        /// 任务之间的连接关系
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UIElement> GetLinks()
        {
            IList<UIElement> links = new List<UIElement>();

            foreach (var child in Children)
            {
                var brush = m_brushes[((child.Column - Column) + m_maxColumn) - 1];
                if (child.Column == Column && child.Row - Row <= 1)
                {
                    links.Add(GetLine(X + (Width / 2), Y + Height + 1, X + (Width / 2), child.Y, brush));
                    continue;
                }

                var startX = X + (Width / 2) + ((child.Column - Column) * _interval);
                var startY = Y + Height + 1;
                var endY = child.Y;
                var endX = (child.X + (Width / 2)) - ((child.Column - Column) * _interval);

                var x1 = startX + (Width / 2) + (TWidth / 2);
                if (startX > endX)
                {
                    x1 = startX - (Width / 2) - (TWidth / 2);
                }

                var y1 = (startY + Offset) - (Math.Abs(child.Column - Column) * _interval);
                var y2 = (endY - Offset) + (Math.Abs(child.Column - Column) * _interval);

                if (Math.Abs(y1 - y2) < THeight)
                {
                    y2 = y1;
                }

                var line = GetLine(startX, startY, startX, y1, brush);
                if (line != null)
                {
                    links.Add(line);
                }

                line = GetLine(startX, y1, x1, y1, brush);
                if (line != null)
                {
                    links.Add(line);
                }

                line = GetLine(x1, y1, x1, y2, brush);
                if (line != null)
                {
                    links.Add(line);
                }

                line = GetLine(x1, y2, endX, y2, brush);
                if (line != null)
                {
                    links.Add(line);
                }

                line = GetLine(endX, y2, endX, endY, brush);
                if (line != null)
                {
                    links.Add(line);
                }
            }

            return links;
        }

        public UIElement GetUI()
        {
            var stack = new StackPanel
            {
                Width = Width,
                Height = Height
            };
            stack.Children.Add(new TextBlock
            {
                Text = m_task.MainTitle,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Foreground = Brushes.Navy
            });
            stack.Children.Add(new TextBlock
            {
                Text = m_task.SubTitle,
                Foreground = Brushes.Navy,
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            var brush = Brushes.DarkGreen;
            if (m_task is WlyDailyTask)
            {
                brush = Brushes.Blue;
            }
            var border = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = brush,
                Child = stack,
                CornerRadius = new CornerRadius(5)
            };

            border.SetValue(Canvas.LeftProperty, X);
            border.SetValue(Canvas.TopProperty, Y);
            return border;
        }

        #endregion

        #region Private Methods

        private Line GetLine(double x1, double y1, double x2, double y2, Brush brush)
        {
            if ((Math.Abs(x1 - x2) < 0.1) && (Math.Abs(y1 - y2) < 0.1))
            {
                return null;
            }

            return new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = 1,
                Stroke = brush
            };
        }

        #endregion
    }
}