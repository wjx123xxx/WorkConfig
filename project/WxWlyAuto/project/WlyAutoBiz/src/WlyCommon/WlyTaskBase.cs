// *******************************************************************
// * 文件名称： WlyTaskBase.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-15 23:06:23
// *******************************************************************

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public abstract class WlyTaskBase
    {
        #region Fields

        private readonly string[] m_depends;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="depends">依赖的任务</param>
        public WlyTaskBase(string id, params string[] depends)
        {
            ID = id;
            m_depends = depends ?? new string[0];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 副标题
        /// </summary>
        public virtual string SubTitle => "";

        /// <summary>
        /// 依赖项
        /// </summary>
        public string[] Depends => m_depends;

        /// <summary>
        /// 任务标识
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string MainTitle => GetType().Name.Substring(3);

        #endregion

        #region Public Methods

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return GetType().Name;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual WlyTaskInfo Run(WlyEntity entity)
        {
            return InternalRun(entity);
        }

        /// <summary>
        /// 根据角色信息，确定任务是否可执行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Filter(WlyEntity entity)
        {
            var infoDict = entity.AccountInfo.TaskInfoDict;
            foreach (var depend in m_depends)
            {
                if (!infoDict.ContainsKey(depend))
                {
                    return false;
                }

                if (!infoDict[depend].IsComplete)
                {
                    return false;
                }
            }

            return InternalFilter(entity);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract WlyTaskInfo InternalRun(WlyEntity entity);

        /// <summary>
        /// 过滤检测
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool InternalFilter(WlyEntity entity)
        {
            return true;
        }

        #endregion
    }
}