// *******************************************************************
// * 文件名称： MainWindowVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-27 17:17:49
// *******************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Utility.UICommon;

namespace Wx.App.WlyUIViewViewer.VM
{
    /// <summary>
    /// 主视图VM
    /// </summary>
    public class MainWindowVM : WxUIEntity
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private WlyUIViewWrapper m_selectedView;

        #endregion

        #region Constructors

        public MainWindowVM()
        {
            var viewDict = new Dictionary<WlyViewType, WlyUIViewWrapper>();
            foreach (var view in WlyViewMgr.Views)
            {
                var wrapper = new WlyUIViewWrapper(view);
                ViewList.Add(wrapper);
                viewDict.Add(view.Type, wrapper);
            }

            foreach (var wrapper in viewDict.Values.ToList())
            {
                foreach (var sub in wrapper.SubViewTypes)
                {
                    var subWrapper = viewDict[sub];
                    wrapper.Children.Add(subWrapper);
                }
            }

            foreach (var group in viewDict.Values.GroupBy(o => o.Row))
            {
                var list = group.ToList().OrderByDescending(o => o.ChildLength).ToList();
                for (var i = 0; i < group.Count(); i++)
                {
                    list[i].Column = i;
                }
            }

            // 添加链接线段
            foreach (var view in viewDict.Values)
            {
                foreach (var line in view.GetLinks())
                {
                    ViewList.Add(line);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public WlyUIViewWrapper SelectedView
        {
            get { return m_selectedView; }
            set
            {
                if (m_selectedView != value)
                {
                    if (m_selectedView != null)
                    {
                        foreach (var child in m_selectedView.Children)
                        {
                            child.Brush = Brushes.Black;
                        }
                    }

                    m_selectedView = value;

                    if (m_selectedView != null)
                    {
                        foreach (var child in m_selectedView.Children)
                        {
                            child.Brush = Brushes.Blue;
                        }
                    }

                    OnPropertyChanged(nameof(SelectedView));
                }
            }
        }

        public ObservableCollection<WxUIEntity> ViewList { get; } = new ObservableCollection<WxUIEntity>();

        #endregion
    }
}