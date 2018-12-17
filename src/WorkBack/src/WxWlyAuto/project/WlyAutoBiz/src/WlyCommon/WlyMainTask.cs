// *******************************************************************
// * 文件名称： WlyMainTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-16 15:56:57
// *******************************************************************

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 主线任务
    /// </summary>
    public abstract class WlyMainTask : WlyTaskBase
    {
        #region Constructors

        protected WlyMainTask(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 过滤检测
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool InternalFilter(WlyEntity entity)
        {
            var infoDict = entity.AccountInfo.TaskInfoDict;
            if (infoDict.ContainsKey(ID) && infoDict[ID].IsComplete)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}