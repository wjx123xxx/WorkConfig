// *******************************************************************
// * 文件名称： WlyFinalTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-20 23:43:11
// *******************************************************************

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 日常收尾任务
    /// </summary>
    public abstract class WlyFinalTask : WlyDailyTask
    {
        #region Constructors

        public WlyFinalTask(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion
    }
}