// *******************************************************************
// * 文件名称： MainWindowVM.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-24 16:21:41
// *******************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Wx.App.WlySubAutoPlayer.Biz;
using Wx.Utility.UICommon;

namespace Wx.App.WlyTaskViewer.VM
{
    /// <summary>
    /// 主窗口VM
    /// </summary>
    public class MainWindowVM : WxUIEntity
    {
        #region Fields

        private IDictionary<string, TaskWrapper> m_dict = new Dictionary<string, TaskWrapper>();

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindowVM()
        {
            var tasks = SubTaskMgr.Instance.TaskList;
            foreach (var task in tasks)
            {
                m_dict.Add(task.ID, new TaskWrapper(task));
            }

            // 构建关系树
            foreach (var wrapper in m_dict.Values)
            {
                foreach (var depend in wrapper.Depends)
                {
                    var parent = m_dict[depend];
                    parent.Children.Add(wrapper);
                    wrapper.Parents.Add(parent);
                }

                TaskList.Add(wrapper);
            }

            // 计算序号
            CalculateColumn();

            // 添加链接线段
            foreach (var task in m_dict.Values)
            {
                foreach (var line in task.GetLinks())
                {
                    TaskList.Add(line);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 任务列表
        /// </summary>
        public ObservableCollection<WxUIEntity> TaskList { get; } = new ObservableCollection<WxUIEntity>();

        #endregion

        #region Private Methods

        private void CalculateColumn()
        {
            var max = m_dict.Values.Max(o => o.Row);
            for (int i = 0; i <= max; i++)
            {
                var row = i;
                var wrappers = m_dict.Values.Where(o => o.Row == row).ToList();
                wrappers = wrappers.OrderByDescending(o =>
                                   {
                                       if (!o.Children.Any())
                                       {
                                           return o.Column;
                                       }

                                       return o.Children.Max(s => s.Column);
                                   })
                                   .ToList();
                wrappers = wrappers.OrderByDescending(o => o.ChildLength).ToList();
                for (int j = 0; j < wrappers.Count; j++)
                {
                    wrappers[j].Column = j;
                }

                if (TaskWrapper.m_maxColumn < wrappers.Count)
                {
                    TaskWrapper.m_maxColumn = wrappers.Count;
                }
            }

            //canvas.Height = (max + 1) * (TaskWrapper.Height + TaskWrapper.THeight);
        }

        #endregion
    }
}