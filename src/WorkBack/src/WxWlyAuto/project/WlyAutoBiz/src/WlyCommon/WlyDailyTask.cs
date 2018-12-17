// *******************************************************************
// * 文件名称： WlyDailyTask.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-16 14:58:41
// *******************************************************************

namespace Wx.Lib.WlyAutoBiz.WlyCommon
{
    /// <summary>
    /// 日常任务
    /// </summary>
    public abstract class WlyDailyTask : WlyTaskBase
    {
        #region Constructors

        public WlyDailyTask(string id, params string[] depends) : base(id, depends)
        {
        }

        #endregion
    }
}