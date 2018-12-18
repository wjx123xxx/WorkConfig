// *******************************************************************
// * 文件名称： WlyUIViewBase.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 10:33:03
// *******************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Wx.Lib.WlyAutoBiz.WlyAttribute;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInterface;

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 卧龙吟界面基类
    /// </summary>
    public abstract class WlyUIViewBase : IWlyUIView
    {
        #region Fields

        /// <summary>
        /// 跳转逻辑列表
        /// </summary>
        private readonly ConcurrentDictionary<WlyViewType, Action<string>> m_transferHandlers =
            new ConcurrentDictionary<WlyViewType, Action<string>>();

        private readonly ConcurrentDictionary<WlyViewType, TransferPath> m_viewMap = new ConcurrentDictionary<WlyViewType, TransferPath>();

        private bool m_isInit;

        private WlyViewType m_type = WlyViewType.Unknow;

        #endregion

        #region Public Properties

        /// <summary>
        /// 子界面列表
        /// </summary>
        public IEnumerable<WlyViewType> Children
        {
            get { return m_viewMap.Values.Select(o => o.Target).Distinct().ToList(); }
        }

        /// <summary>
        /// 界面层级
        /// </summary>
        public int Level { get; set; } = WlyViewMgr.LevelMax;

        /// <summary>
        /// 界面类型
        /// </summary>
        public WlyViewType Type
        {
            get
            {
                if (m_type == WlyViewType.Unknow)
                {
                    var attribute = GetType().GetCustomAttribute<WlyViewAttribute>();
                    m_type = attribute.Type;
                }

                return m_type;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前界面退出
        /// </summary>
        /// <param name="dmGuid"></param>
        public abstract void Exit(string dmGuid);

        /// <summary>
        /// 判断是否为当前界面
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public abstract bool IsCurrentView(string dmGuid);

        /// <summary>
        /// 是否能从本界面前往指定的界面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public bool CanGoTo(WlyViewType type, string dmGuid)
        {
            return m_viewMap.ContainsKey(type);
        }

        /// <summary>
        /// 与指定界面的距离
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetLength(WlyViewType type)
        {
            if (m_viewMap.ContainsKey(type))
            {
                return m_viewMap[type].Length;
            }

            return WlyViewMgr.LevelMax;
        }

        /// <summary>
        /// 从当前界面前往指定的界面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public bool GoTo(WlyViewType type, string dmGuid)
        {
            if (!m_viewMap.ContainsKey(type))
            {
                Exit(dmGuid);
                return true;
            }

            var result = m_transferHandlers.TryGetValue(m_viewMap[type].Target, out var handler);
            if (!result)
            {
                throw new InvalidOperationException($"无法前往界面{type}");
            }

            handler(dmGuid);
            return true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (m_isInit)
            {
                return;
            }

            m_isInit = true;

            foreach (var type in m_transferHandlers.Keys)
            {
                var view = WlyViewMgr.GetView(type);
                if (view == null)
                {
                    throw new InvalidOperationException($"未定义界面_{type}");
                }

                if (view.Level > Level)
                {
                    view.Level = Level + 1;
                }
            }

            // 构建跳转地图
            foreach (var type in m_transferHandlers.Keys)
            {
                var view = WlyViewMgr.GetView(type);
                if (view == null)
                {
                    throw new InvalidOperationException($"未定义界面_{type}");
                }

                view.Init();
                foreach (var subType in ((WlyUIViewBase)view).m_viewMap.Keys)
                {
                    var v = WlyViewMgr.GetView(subType);
                    if (v.Level <= Level)
                    {
                        continue;
                    }

                    var current = new TransferPath
                    {
                        Target = type,
                        Length = ((WlyUIViewBase)view).m_viewMap[subType].Length + 1
                    };

                    if (m_viewMap.ContainsKey(subType))
                    {
                        var old = m_viewMap[subType];
                        if (old.Length <= current.Length)
                        {
                            continue;
                        }

                        m_viewMap.TryRemove(subType, out var _);
                    }

                    m_viewMap.TryAdd(subType, current);
                }
            }

            m_isInit = true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 添加跳转逻辑
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        protected void AddHandler(WlyViewType type, Action<string> handler)
        {
            if (!m_transferHandlers.ContainsKey(type))
            {
                m_transferHandlers.TryAdd(type, handler);
                m_viewMap.TryAdd(type, new TransferPath
                {
                    Target = type,
                    Length = 1
                });
            }
        }

        #endregion
    }
}