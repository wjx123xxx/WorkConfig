// *******************************************************************
// * 文件名称： MainTaskMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-16 10:55:56
// *******************************************************************

using System;

using Wx.App.WlyAutoUI.View;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask;
using Wx.Lib.WlyAutoBiz.WlyTasks.TimeTask;

namespace Wx.App.WlyAutoUI.Biz
{
    /// <summary>
    /// 大号任务管理器
    /// </summary>
    public class MainTaskMgr : WlyTaskMgr
    {
        #region Fields

        private static readonly object _instanceLocker = new object();

        private static MainTaskMgr _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private MainTaskMgr()
        {
            // 初始化时间任务
            AddTask(new 任务_活动副本("683550ce7a4f4f9395fb46c5b3f0d8d9", new TimeSpan(9, 29, 10), new TimeSpan(9, 31, 0)));
            AddTask(new 任务_海鲜副本("0b2ef83cb7524e728634bc0c6b0209a6", new TimeSpan(9, 29, 10), new TimeSpan(9, 31, 0)));
            AddTask(new 任务_购物("09d66775b43b4a04ad6ebc21fe46b8cd", new TimeSpan(9, 31, 10), new TimeSpan(12, 29, 0)));
            AddTask(new 任务_擂台赛报名("5a10dac4b27c44f5b734801a2fc8c926", new TimeSpan(10, 1, 0), new TimeSpan(10, 4, 50)));
            AddTask(new 任务_擂台助威("3e739fdad930498c945f91c3cd6059d8", new TimeSpan(10, 8, 30), new TimeSpan(10, 9, 50)));
            AddTask(new 任务_活动副本("10bd067dc995419485622474aa729bd6", new TimeSpan(12, 29, 10), new TimeSpan(12, 31, 0)));
            AddTask(new 任务_海鲜副本("c63c8ef9445540b49ec67b0380a30fa4", new TimeSpan(12, 29, 10), new TimeSpan(12, 31, 0)));
            AddTask(new 任务_购物("64ecf8996877496baadefd1c8ea78620", new TimeSpan(12, 31, 10), new TimeSpan(18, 59, 0)));
            AddTask(new 任务_擂台赛报名("bb8d1602135c477c8b3f250774d3481d", new TimeSpan(15, 1, 0), new TimeSpan(15, 4, 50)));
            AddTask(new 任务_擂台助威("730cbddbe24549ae873fc8afcc21f257", new TimeSpan(15, 8, 30), new TimeSpan(15, 9, 50)));
            AddTask(new 任务_活动副本("0ae3da40fd354a278f3c6c02e0caec23", new TimeSpan(18, 59, 10), new TimeSpan(19, 1, 0)));
            AddTask(new 任务_海鲜副本("e85dd15c5c904986a20a7c40c558a8a8", new TimeSpan(18, 59, 10), new TimeSpan(19, 1, 0)));
            AddTask(new 任务_购物("851509682d464763944c31e78a7f9624", new TimeSpan(19, 1, 10), new TimeSpan(20, 59, 0)));
            AddTask(new 任务_活动副本("2fb9da1abf0449de87bc41cce6fa6b0b", new TimeSpan(20, 59, 10), new TimeSpan(21, 1, 0)));
            AddTask(new 任务_海鲜副本("061225a48b514dfc91477395874adeac", new TimeSpan(20, 59, 10), new TimeSpan(21, 1, 0)));
            AddTask(new 任务_擂台赛报名("af2786dd42ae402bb9c518a8609556b3", new TimeSpan(21, 1, 0), new TimeSpan(21, 4, 50)));
            AddTask(new 任务_擂台助威("b5a8a6fe06bb496fafd28c8fea0f40ca", new TimeSpan(21, 8, 30), new TimeSpan(21, 9, 50)));
            AddTask(new 任务_购物("fbcdc501d404436cb4828703b8e14d3f", new TimeSpan(21, 1, 10), new TimeSpan(1, 3, 59, 0)));
            //AddTask(new 任务_海盗活动("fd1867f6d7c74d7094ad6604801002c3", new TimeSpan(16, 0, 40), new TimeSpan(16, 6, 30)));
            //AddTask(new 任务_海盗活动("f22d35fa2abc41abaa36f4caa15a5e4c", new TimeSpan(21, 30, 40), new TimeSpan(21, 36, 30)));
            AddTask(new 任务_群雄逐鹿("b0af2ce3e5f74bb29f2d8715b7d068e0", new TimeSpan(10, 30, 30), new TimeSpan(21, 30, 30)));
            AddTask(new 任务_虎牢关战役("fd8f475a4793487889563251b1db44a4", new TimeSpan(10, 10, 30), new TimeSpan(23, 30, 30)));
            AddTask(new 任务_古代战场("6f55d3e22bd84390b5183a2c362ca5f9", new TimeSpan(10, 10, 30), new TimeSpan(23, 0, 0)));
            AddTask(new 任务_装备重铸("536ecdf16c724de7a51d9a312a2b8dcb"));
            AddTask(new 任务_游历("f9b02e3818de4aca9cd376f1b4166ae0"));

            // 初始化日常任务
            AddTask(new 任务_购买军令("63fe2018a9bf431e8b0d29ae611e02a1"));
            AddTask(new 任务_田矿占领("0b94e04bef764291ac4b67d238ab797a"));
            AddTask(new 任务_修理船只("56028a9a5fb84aa18d287636fe2abd18"));
            AddTask(new 任务_卖粮("b2ed466f8be24e8fa13f009989f8eec7"));
            AddTask(new 任务_巡查("d48615f1d5854eca8c7af16bbbf11df1"));
            AddTask(new 任务_领取奖励("3f2069ee51854b66bf5818366d8ebcb3", TimeSpan.FromMinutes(30)));
            //AddTask(new 任务_打猎("ba133d721bfd4f43bd6dd3a9aa4a4e2f"));
            AddTask(new 任务_古城探险("a9942edf86b34286a6b83cd7eeef77c9"));
            //AddTask(new 任务_城池争夺战("eebf75c92bcc4caf91cd8ae9663572e3"));

            AddTask(new 任务_寻访("7126f7a1deab4145b10744c35e027476"));
            AddTask(new 任务_强征("42f98dff437849f3b5433bc3b0c78921"));
            AddTask(new 任务_游戏助手("609c0ed388ca43a081254c131faa4bd5"));
            AddTask(new 任务_客串海盗("a916c7da68ab4b5e9cffe8081015ecc5"));
            AddTask(new 任务_清理仓库("9d559acbfea8486d8cc6619ed3a6b1fd"));
            AddTask(new 任务_收取贡品("4da939b288d24cfaada08ffa37f578b4"));
            AddTask(new 任务_商旅派遣("7b352dbfbc62486ebb8934b9bf2f3adc"));
            AddTask(new 任务_商城刷新("3d279fd5f78c4a8da8418976bdcd7772"));
            AddTask(new 任务_重登陆("b358788ae1e74d27a537508a30830cde"));

            //AddTask(new 任务_活跃度奖励("31dd8eba8b8040faaa44b4ef1b450ae5"));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static MainTaskMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new MainTaskMgr();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion
    }
}