// *******************************************************************
// * 文件名称： WlyViewMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-10 17:36:45
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInterface;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.Lib.WlyAutoBiz.WlyBiz
{
    /// <summary>
    /// 界面跳转器
    /// </summary>
    public static class WlyViewMgr
    {
        #region Fields

        private static readonly IList<IWlyUIView> _viewList = new List<IWlyUIView>();

        public static readonly int LevelMax = 100;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        static WlyViewMgr()
        {
            var types = typeof(WlyViewMgr).Assembly.GetTypes();
            IWlyUIView mainView = null;
            foreach (var t in types)
            {
                if (!typeof(WlyUIViewBase).IsAssignableFrom(t) || t.IsAbstract)
                {
                    continue;
                }

                var attribute = t.GetCustomAttribute<WlyViewAttribute>();
                if ((attribute == null) && !t.IsAbstract)
                {
                    throw new InvalidOperationException($"界面{t.Name}没有附加界面属性");
                }

                var view = Activator.CreateInstance(t) as IWlyUIView;
                _viewList.Add(view);

                if ((view != null) && (view.Type == WlyViewType.场景_主界面))
                {
                    view.Level = 0;
                    mainView = view;
                }
            }

            // 界面检查
            foreach (var v in Enum.GetValues(typeof(WlyViewType)))
            {
                if (Equals(v, WlyViewType.Unknow))
                {
                    continue;
                }

                if (_viewList.FirstOrDefault(o => Equals(o.Type, v)) == null)
                {
                    throw new InvalidOperationException($"没有定义界面 {v}");
                }
            }

            if (mainView == null)
            {
                throw new InvalidOperationException("未定义主界面");
            }

            mainView.Init();
            _viewList = _viewList.OrderByDescending(o => o.Level).ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 所有界面集合
        /// </summary>
        public static IList<IWlyUIView> Views
        {
            get { return _viewList.ToList(); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 退出当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="timeout"></param>
        public static void ExitCurrentView(string dmGuid, TimeSpan timeout)
        {
            IWlyUIView current = null;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                foreach (var v in _viewList)
                {
                    if (v.IsCurrentView(dmGuid))
                    {
                        current = v;
                        return true;
                    }
                }

                return false;
            }, timeout);
            if (!wait)
            {
                throw new InvalidOperationException("无法确定当前所属界面");
            }

            wait = FlowLogicHelper.RepeatRun(() =>
            {
                if (current.IsCurrentView(dmGuid))
                {
                    current.Exit(dmGuid);
                    Thread.Sleep(400);
                    return false;
                }

                return true;
            }, timeout);
            if (!wait)
            {
                throw new InvalidOperationException("无法从当前界面退出");
            }
        }

        /// <summary>
        /// 获取指定的界面对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IWlyUIView GetView(WlyViewType type)
        {
            return _viewList.FirstOrDefault(o => o.Type == type);
        }

        /// <summary>
        /// 跳转到指定的界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="type"></param>
        public static void GoTo(string dmGuid, WlyViewType type)
        {
            GoTo(dmGuid, type, TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// 跳转到指定的界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="type"></param>
        /// <param name="interval"></param>
        public static void GoTo(string dmGuid, WlyViewType type, TimeSpan interval)
        {
            bool Handler()
            {
                // 首先确定当前处于哪个界面，由于可能处于过渡界面，需要一定的等待时间
                IEnumerable<IWlyUIView> views = null;
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    views = _viewList.Where(o => o.IsCurrentView(dmGuid));
                    if (!views.Any())
                    {
                        var res = DMService.Instance.FindPic(dmGuid, WlyPicType.关闭按钮, WlyUtilityBiz.GameWndRect, out var x, out var y);
                        if (res)
                        {
                            DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                        }
                    }

                    return views.Any();
                }, TimeSpan.FromSeconds(30));

                if (!wait)
                {
                    throw new InvalidOperationException("无法确定当前属于哪个界面");
                }

                if (views.Any(o => o.Type == type))
                {
                    return true;
                }

                var groups = views.GroupBy(o => o.CanGoTo(type, dmGuid));
                var ev = groups.FirstOrDefault(o => !o.Key);
                if (ev != null)
                {
                    foreach (var v in ev)
                    {
                        v.Exit(dmGuid);
                    }
                }

                var gv = groups.FirstOrDefault(o => o.Key)?.ToList();
                if (gv != null)
                {
                    var cv = gv.First();
                    var level = LevelMax;
                    foreach (var v in gv)
                    {
                        var length = v.GetLength(type);
                        if (length < level)
                        {
                            level = length;
                            cv = v;
                        }
                    }

                    cv.GoTo(type, dmGuid);
                }

                Thread.Sleep(100);
                return false;
            }

            var result = FlowLogicHelper.RepeatRun(() =>
            {
                if (Handler())
                {
                    return true;
                }

                Thread.Sleep(interval);
                return false;
            }, TimeSpan.FromSeconds(30));
            if (!result)
            {
                throw new InvalidOperationException($"无法切换到界面 {type}");
            }
        }

        #endregion
    }
}