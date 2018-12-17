// *******************************************************************
// * 文件名称： SubEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-22 08:39:21
// *******************************************************************

using System;
using System.Linq;
using System.Threading;

using Wx.App.WlySubAutoPlayer.VM;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyUtility;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.App.WlySubAutoPlayer.Biz
{
    /// <summary>
    /// 小号实体
    /// </summary>
    public class SubEntity : WlyEntity
    {
        #region Fields

        private readonly SubAccountInfo m_subAccountInfo;

        private bool m_loginFlag;

        #endregion

        #region Constructors

        public SubEntity(SubAccountInfo info) : base(info)
        {
            m_subAccountInfo = info;
        }

        #endregion

        #region Public Properties

        public DateTime DevelopTime
        {
            get
            {
                if (!m_subAccountInfo.GetTaskInfo("2eb55829772a490abf2433ce56545e0e").IsComplete)
                {
                    return DateTime.MaxValue;
                }

                var info = m_subAccountInfo.GetTaskInfo("be18cfefa68743c78d6992580360491e");
                return info.NextRunTime;
            }
        }

        /// <summary>
        /// 小号序号
        /// </summary>
        public int Index
        {
            get { return m_subAccountInfo.Index; }
        }

        /// <summary>
        /// 角色信息
        /// </summary>
        public SubAccountInfo Info
        {
            get { return m_subAccountInfo; }
        }

        /// <summary>
        /// 下次登录时间
        /// </summary>
        public DateTime NextLoginTime
        {
            get { return m_subAccountInfo.NextLoginTime; }
        }

        #endregion

        #region Public Methods

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return Info.Name;
        }

        /// <summary>
        /// 重置登录时间
        /// </summary>
        public void Reset()
        {
            m_subAccountInfo.NextLoginTime = DateTime.Now;
            var taskInfo = m_subAccountInfo.GetTaskInfo("1ed1d8def3f445e2ab522661701f740f");
            taskInfo.NextRunTime = DateTime.MinValue;
            taskInfo.IsComplete = false;
            m_subAccountInfo.Save();
        }

        public void SpecialTask()
        {
            try
            {
                WlyUtilityBiz.Login(this);
                BindGameWnd();

                var dmGuid = DMGuid;
                WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_红包);

                // 点击红包
                DMService.Instance.LeftClick(dmGuid, new WxPoint(498, 452), TimeSpan.FromSeconds(2));

                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(434, 203)), 6);
                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(525, 280)), 6);
                FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(485, 349)), 6);
            }
            catch
            {
            }
            finally
            {
                CloseProcess();
            }
        }

        #endregion

        #region Protected Methods

        protected override void InternalAfterLogin()
        {
            SubTaskMgr.Instance.InitEntityTasks(this);
            m_loginFlag = true;

            if (!m_subAccountInfo.Check)
            {
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    var view = WlyViewMgr.GetView(WlyViewType.略_防沉迷验证);
                    var view1 = WlyViewMgr.GetView(WlyViewType.略_跳过);
                    return DMService.Instance.FindPic(DMGuid, WlyPicType.选择国家, WlyUtilityBiz.GameWndRect) || view.IsCurrentView(DMGuid)
                                                                                                          || view1.IsCurrentView(DMGuid);
                }, TimeSpan.FromSeconds(30));
                if (wait && DMService.Instance.FindPic(DMGuid, WlyPicType.选择国家, WlyUtilityBiz.GameWndRect))
                {
                    CreateRole(AutoPlayerVM.Instance.GetAvailableName());
                }
            }

            WxLog.Debug($"SubEntity.InternalAfterLogin AutoPlay Start <{m_subAccountInfo.Name}>");

            // 先检测主公等级
            var level = WlyUtilityBiz.GetMainLevel(DMGuid);
            if (level > m_subAccountInfo.Level)
            {
                WlyMainLevelMgr.Upgrade(DMGuid);
                m_subAccountInfo.Level = level;
                m_subAccountInfo.Save();
            }

            // 检测银币数量是否到达上限
            var result = WlyUtilityBiz.GetPercent(DMGuid, new WxRect(133, 26, 217, 47), "f3f3da-000000", out var percent);
            if (!result)
            {
                throw new InvalidOperationException("Cannot Get Money Amount");
            }

            // 银币超过上限了，想尽办法进行使用
            if (percent >= 0.9)
            {
                GetEuqipments();
                UpgradeEquipments();
            }

            // 如果可以捐国政了，则捐国政
            var now = DateTime.Now;
            var timeNotOK = (now.DayOfWeek == DayOfWeek.Sunday) && (now.Hour >= 4) && (now.Hour < 21);
            if ((percent >= 0.5) && (m_subAccountInfo.GetBuildingInfo(WlyBuildingType.主城).Level >= 130) && !timeNotOK)
            {
                // 捐款2600万获取100政绩
                WlyViewMgr.GoTo(DMGuid, WlyViewType.场景_国政);

                // 点开内政
                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    DMService.Instance.LeftClick(DMGuid, new WxPoint(95, 42));
                    if (DMService.Instance.FindStr(DMGuid, new WxRect(483, 100, 516, 120), "国政", WlyColor.Normal))
                    {
                        return true;
                    }

                    Thread.Sleep(500);
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                // 捐款铸造
                DMService.Instance.LeftClick(DMGuid, new WxPoint(560, 270));
                DMService.Instance.LeftClick(DMGuid, new WxPoint(613, 438));
                DMService.Instance.SendString(DMGuid, WndHwnd, "50000000");

                wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (WlyUtilityBiz.GetAmount(DMGuid, new WxRect(577, 429, 671, 447), "e7e7fd-000000", out var amount))
                    {
                        return true;
                    }

                    Thread.Sleep(500);
                    return false;
                }, TimeSpan.FromSeconds(10));
                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                DMService.Instance.LeftClick(DMGuid, new WxPoint(739, 438));
            }

            // 如果可以进行开发，则马上开发并立刻结束
            //if (DevelopTime < DateTime.Now)
            //{
            //    WlyUtilityBiz.GetAmount(DMGuid, new WxRect(133, 26, 217, 47), "f3f3da-000000", out var amount);
            //    if (amount > 500000)
            //    {
            //        DevelopCity();
            //        //Stop();
            //    }
            //}
        }

        protected override void InternalBackupWorkThread()
        {
            // 小号无其他任务则直接停止
            WxLog.Debug($"SubEntity.InternalBackupWorkThread  No More Tasks <{Account}>");
            var time = WlyUtilityBiz.GetRefreshTime();
            foreach (var runner in TaskList.ToList())
            {
                var info = AccountInfo.GetTaskInfo(runner.Task.ID);
                if (info.NextRunTime < time)
                {
                    time = info.NextRunTime;
                }
            }

            m_subAccountInfo.NextLoginTime = time;

            WxLog.Debug($"SubEntity.InternalBackupWorkThread Name<{m_subAccountInfo.Name}> "
                        + $"NextLoginTime<{m_subAccountInfo.NextLoginTime:yyyy-MM-dd HH:mm:ss}>");
            m_subAccountInfo.Save();
            Stop();
        }

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="errorCount"></param>
        protected override void InternalHandleError(int errorCount)
        {
            if ((errorCount >= 3) && !m_loginFlag)
            {
                m_subAccountInfo.NextLoginTime = DateTime.Now.AddDays(5);
                m_subAccountInfo.Save();
                Stop();
            }
        }

        /// <summary>
        /// 要求计算任务列表
        /// </summary>
        protected override void InternalInitTasks()
        {
            SubTaskMgr.Instance.InitEntityTasks(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="name"></param>
        private void CreateRole(string name)
        {
            // 等待进入创建角色界面
            var wait = FlowLogicHelper.RepeatRun(() => DMService.Instance.FindPic(DMGuid, WlyPicType.选择国家, WlyUtilityBiz.GameWndRect),
                TimeSpan.FromSeconds(30));
            if (!wait)
            {
                throw new InvalidOperationException("进入创建角色界面失败");
            }

            // 选择吴国
            var wu = new WxPoint(630, 400);
            DMService.Instance.LeftClick(DMGuid, wu);

            // 输入名称
            var namePoint = new WxPoint(517, 504);
            DMService.Instance.LeftDoubleClick(DMGuid, namePoint, 100);
            DMService.Instance.LeftDoubleClick(DMGuid, namePoint, 100);

            var guid = DMService.Instance.CreateDMSoft();
            DMService.Instance.BindWindowIME(guid, WndHwnd);
            DMService.Instance.SendStringIme(guid, name);
            Thread.Sleep(1000);
            DMService.Instance.ReleaseDMSoft(guid);

            // 确定
            var okPoint = new WxPoint(493, 556);
            DMService.Instance.LeftClick(DMGuid, okPoint);
            throw new InvalidOperationException("角色创建完毕需要重启");
        }

        /// <summary>
        /// 委派装备
        /// </summary>
        private void GetEuqipments()
        {
            var key = true;
            foreach (var staff in AccountInfo.StaffInfoDict.Values)
            {
                var e = staff.GetEquipInfo(WlyEquipType.战法攻击);
                if (staff.EquipmentRequestDict[e.Type] && (e.EquipLevel < 95))
                {
                    key = false;
                    break;
                }

                e = staff.GetEquipInfo(WlyEquipType.战法防御);
                if (staff.EquipmentRequestDict[e.Type] && (e.EquipLevel < 95))
                {
                    key = false;
                    break;
                }

                e = staff.GetEquipInfo(WlyEquipType.计策防御);
                if (staff.EquipmentRequestDict[e.Type] && (e.EquipLevel < 95))
                {
                    key = false;
                    break;
                }

                e = staff.GetEquipInfo(WlyEquipType.计策攻击);
                if (staff.EquipmentRequestDict[e.Type] && (e.EquipLevel < 95))
                {
                    key = false;
                    break;
                }
            }

            if (key)
            {
                return;
            }

            // 银币十连抽 100次
            WlyViewMgr.GoTo(DMGuid, WlyViewType.功能_委派);

            // 设置自动卖出
            DMService.Instance.LeftClick(DMGuid, new WxPoint(251, 473));
            DMService.Instance.LeftClick(DMGuid, new WxPoint(442, 341));
            DMService.Instance.LeftClick(DMGuid, new WxPoint(493, 395));

            DMService.Instance.LeftClick(DMGuid, new WxPoint(352, 370), TimeSpan.FromSeconds(1));
            if (DMService.Instance.FindStr(DMGuid, new WxRect(434, 340, 476, 367), "确定", WlyColor.Normal))
            {
                DMService.Instance.LeftClick(DMGuid, new WxPoint(462, 316));
                DMService.Instance.LeftClick(DMGuid, new WxPoint(457, 354));
            }

            // 进入委派循环
            for (var i = 0; i < 20; i++)
            {
                // 等待抽取完成
                if (DMService.Instance.FindColor(DMGuid, "f3f3da-000000", new WxRect(404, 460, 461, 481)))
                {
                    DMService.Instance.LeftClick(DMGuid, new WxPoint(390, 472));
                }

                var wait = FlowLogicHelper.RepeatRun(() =>
                {
                    if (DMService.Instance.FindColor(DMGuid, "ffb76f-000000|ffffff-000000", new WxRect(584, 447, 644, 480)))
                    {
                        return true;
                    }

                    Thread.Sleep(1000);
                    return false;
                }, TimeSpan.FromSeconds(10));

                if (!wait)
                {
                    throw new InvalidOperationException();
                }

                DMService.Instance.LeftClick(DMGuid, new WxPoint(612, 467));
            }

            // 退出
            Thread.Sleep(2000);
            DMService.Instance.LeftClick(DMGuid, new WxPoint(727, 477));
        }

        /// <summary>
        /// 升级当前拥有的装备
        /// </summary>
        private void UpgradeEquipments()
        {
            // 强化所有
            WlyEntityBiz.UpgradeAllEquipmenets(this);
        }

        #endregion
    }
}