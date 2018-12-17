// *******************************************************************
// * 文件名称： MainEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-11 23:19:59
// *******************************************************************

using System;
using System.Linq;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;

namespace Wx.App.WlyAutoUI.Biz
{
    /// <summary>
    /// 主号挂机实体
    /// </summary>
    public class MainEntity : WlyEntity
    {
        #region Constructors

        public MainEntity(WlyAccountInfo info) : base(info)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return "海潮";
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 程序挂机线程，退出该函数表示本次挂机结束
        /// </summary>
        protected override void InternalBackupWorkThread()
        {
            OnDescriptionChanged("首攻检测");
            var chatView = WlyViewMgr.GetView(WlyViewType.聊天);
            if (chatView.IsCurrentView(DMGuid))
            {
                chatView.Exit(DMGuid);
            }

            WlyUtilityBiz.FreeAttack = true;
            FindFirstAttack();
            WlyUtilityBiz.FreeAttack = false;
        }

        /// <summary>
        /// 要求计算任务列表
        /// </summary>
        protected override void InternalInitTasks()
        {
            MainTaskMgr.Instance.InitEntityTasks(this);
        }

        protected override void InternalRestart()
        {
            MainTaskMgr.Instance.InitEntityTasks(this);
            CloseProcess();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 寻找首攻
        /// </summary>
        private bool FindFirstAttack()
        {
            // 处理加入失败，关闭军团战界面，防止被bug消耗军令
            var view = WlyViewMgr.GetView(WlyViewType.略_加入失败);
            if (view.IsCurrentView(DMGuid))
            {
                view.Exit(DMGuid);
                WlyViewMgr.ExitCurrentView(DMGuid, TimeSpan.FromSeconds(5));
            }

            // 组队框
            var teamRect = new WxRect(632, 206, 807, 449);

            string GetTargetStr()
            {
                if (AccountInfo.Point > AccountInfo.PointReserved)
                {
                    return $"加|区|(首|攻)|不耗|首攻|{AccountInfo.SelectedGroupType.ToString()}";
                }

                return "加|区|(首|攻)|不耗|首攻";
            }

            bool Handler()
            {
                // 在聊天框寻找首攻
                var chatRect = new WxRect(0, 470, 277, 600);
                var str = DMService.Instance.FindStrEx(DMGuid, chatRect, GetTargetStr(), "fff71c-000000|fcffa9-000000|00ff00-000000");
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }

                // 字符串分析
                var sp = str.Split('|');
                if (sp.Count(p => p[0] == '0') > 1)
                {
                    // 加字数量过多，需要关闭聊天窗
                    return false;
                }

                // 激活
                if (sp.Any(p => p[0] == '1'))
                {
                    KeepAlive();
                }

                // 找到了一次军团副本
                if (sp.Any(p => p[0] == '6'))
                {
                    AccountInfo.Point--;
                }

                // 有首攻标识，进行加入
                if (sp.Any(p => int.Parse(p[0].ToString()) > 1))
                {
                    var join = sp.FirstOrDefault(o => o[0] == '0');
                    if (!string.IsNullOrEmpty(join))
                    {
                        var pos = join.Split(',');
                        var point = new WxPoint(int.Parse(pos[1]) + 5, int.Parse(pos[2]) + 5);
                        DMService.Instance.LeftClick(DMGuid, point, TimeSpan.FromSeconds(2));
                    }
                }

                Thread.Sleep(1000);
                if (DMService.Instance.FindStr(DMGuid, teamRect, "海潮", "e9e7cf-000000"))
                {
                    return true;
                }

                return false;
            }

            var result = FlowLogicHelper.RepeatRun(Handler, TimeSpan.FromSeconds(5));
            if (result)
            {
                FlowLogicHelper.RepeatRun(() =>
                {
                    Thread.Sleep(200);
                    return DMService.Instance.FindStr(DMGuid, teamRect, "海潮", "e9e7cf-000000");
                }, TimeSpan.FromSeconds(5));
                return true;
            }

            return false;
        }

        #endregion
    }
}