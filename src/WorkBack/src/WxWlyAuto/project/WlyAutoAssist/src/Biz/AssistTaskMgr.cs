// *******************************************************************
// * 文件名称： AssistTaskMgr.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-05-12 22:49:12
// *******************************************************************

using Wx.App.WlyAutoAssist.Tasks;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyTasks.MainTask;

namespace Wx.App.WlyAutoAssist.Biz
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class AssistTaskMgr : WlyTaskMgr
    {
        #region Fields

        private static readonly object _instanceLocker = new object();

        private static AssistTaskMgr _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private AssistTaskMgr()
        {
            // 主线任务（10级）
            AddTask(new 任务_升级建筑("e563e19a584f4a579fb5410511608a59", WlyBuildingType.主城, 10));
            AddTask(new 任务_升级建筑("7192432b6017458983982c86a1d9840a", WlyBuildingType.策略府, 10, "e563e19a584f4a579fb5410511608a59"));
            AddTask(new 任务_升级科技("f93a1c4941d440ebb526db9f5c441943", 10, "7192432b6017458983982c86a1d9840a"));
            AddTask(new 任务_升级建筑("2031a3eec7fe4d7aba1e60ed9e851009", WlyBuildingType.商铺, 10, "7192432b6017458983982c86a1d9840a"));
            AddTask(new 任务_升级建筑("5ceb0caf56ae49c1a8a2528d8c3a93e4", WlyBuildingType.校场, 10, "2031a3eec7fe4d7aba1e60ed9e851009"));
            AddTask(new 任务_武将突飞("04d091b2611e4ee2becda4cbe7d49ce2", WlyStaffType.文凤卿, 10, "5ceb0caf56ae49c1a8a2528d8c3a93e4"));
            AddTask(new 任务_推图("c8b9e5aaf90d4fd88fc8194d6f6aaa8f", 1, "04d091b2611e4ee2becda4cbe7d49ce2", "f93a1c4941d440ebb526db9f5c441943"));
            AddTask(new 任务_搬迁主城("b5f455928b1b4746b9a6137b22b5b94c", WlyCityType.长安, "c8b9e5aaf90d4fd88fc8194d6f6aaa8f"));

            // 主线任务（20级）
            AddTask(new 任务_开通队列("ffd832704aac4b92b99abf165565cac0", "b5f455928b1b4746b9a6137b22b5b94c"));
            AddTask(new 任务_升级建筑("844571c4075444fe98a90fe3e6aa1eb1", WlyBuildingType.主城, 20, "ffd832704aac4b92b99abf165565cac0"));
            AddTask(new 任务_升级建筑("08e6eb1913bc4309832b08c8a031d14e", WlyBuildingType.兵营, 20, "844571c4075444fe98a90fe3e6aa1eb1"));
            AddTask(new 任务_升级建筑("8c3d41adacfa4c76850f82715ed44b7d", WlyBuildingType.策略府, 20, "08e6eb1913bc4309832b08c8a031d14e"));
            AddTask(new 任务_升级科技("a822faeb15e748688d93bfd1d3a27f69", 20, "8c3d41adacfa4c76850f82715ed44b7d"));
            AddTask(new 任务_升级建筑("fe6bed2b53f2438dad9c1a889a3af65e", WlyBuildingType.商铺, 20, "8c3d41adacfa4c76850f82715ed44b7d"));
            AddTask(new 任务_升级建筑("3c991091c92e41aaae6f1d488c967d1c", WlyBuildingType.校场, 20, "fe6bed2b53f2438dad9c1a889a3af65e"));
            AddTask(new 任务_武将突飞("ba6c41b4bafd4268b121b9397d3d0a9a", WlyStaffType.文凤卿, 20, "3c991091c92e41aaae6f1d488c967d1c"));
            AddTask(new 任务_武将转职("6e2ad8419bd84e32b61168ef28eab47e", WlyStaffType.文凤卿, "ba6c41b4bafd4268b121b9397d3d0a9a"));
            AddTask(new 任务_武将突飞("1ab3a0d71f874f6eb6bae4570aac66d3", WlyStaffType.裴元绍, 20, "6e2ad8419bd84e32b61168ef28eab47e"));
            AddTask(new 任务_武将转职("e37cafb2c03c44a79ea545e7706a6083", WlyStaffType.裴元绍, "1ab3a0d71f874f6eb6bae4570aac66d3"));
            AddTask(new 任务_设置兵种("c716a3eeafd141818e5f2f9c8efcfd66", WlyStaffType.裴元绍, 2, "e37cafb2c03c44a79ea545e7706a6083"));
            AddTask(new 任务_武将上阵("8e183b724d664b069ed6c60a922679e1", WlyStaffType.裴元绍, WlyFormationType.鱼鳞阵, 1, "c716a3eeafd141818e5f2f9c8efcfd66"));
            //AddTask(new 任务_强化装备("7f96ce242a0a4721866f55b66fcab0e3", WlyStaffType.裴元绍, WlyEquipType.物理防御, 20, "8e183b724d664b069ed6c60a922679e1"));
            //AddTask(new 任务_强化装备("20720ba70df84bba9c1eb2b0453d0a2d", WlyStaffType.裴元绍, WlyEquipType.物理攻击, 20, "7f96ce242a0a4721866f55b66fcab0e3"));
            //AddTask(new 任务_强化装备("199bd18992284df4b9d3989079918d14", WlyStaffType.文凤卿, WlyEquipType.物理防御, 20, "20720ba70df84bba9c1eb2b0453d0a2d"));
            //AddTask(new 任务_强化装备("e3a458a69862498d891c29d8e8f308bd", WlyStaffType.文凤卿, WlyEquipType.物理攻击, 20, "199bd18992284df4b9d3989079918d14"));
            //AddTask(new 任务_强化装备("1f67202711364d259e9e747fc10b11e4", WlyStaffType.文凤卿, WlyEquipType.带兵量, 20, "e3a458a69862498d891c29d8e8f308bd"));
            AddTask(new 任务_推图("02c980114044463ea2eb5c13c3482436", 2, "1f67202711364d259e9e747fc10b11e4", "a822faeb15e748688d93bfd1d3a27f69"));

            // 主线任务（40级）
            AddTask(new 任务_升级建筑("e4a2cd5898f0485499797ddb916b7267", WlyBuildingType.主城, 40, "02c980114044463ea2eb5c13c3482436"));
            AddTask(new 任务_升级建筑("59cbfb5745cd4b79ab8af6857d148cb8", WlyBuildingType.策略府, 40, "e4a2cd5898f0485499797ddb916b7267"));
            AddTask(new 任务_升级科技("610d196420a64ae78351abf5125dbf99", 40, "59cbfb5745cd4b79ab8af6857d148cb8"));
            AddTask(new 任务_升级建筑("70856baeead64a8298b6aa73a494ce55", WlyBuildingType.商铺, 40, "59cbfb5745cd4b79ab8af6857d148cb8"));
            AddTask(new 任务_升级建筑("2b38c1c1414345b498c75a85a9907b9c", WlyBuildingType.工坊, 20, "59cbfb5745cd4b79ab8af6857d148cb8"));

            AddTask(new 任务_武将突飞("c31d226e350c44e3abd8d585bc394078", WlyStaffType.文凤卿, 40, "70856baeead64a8298b6aa73a494ce55"));
            AddTask(new 任务_武将转职("fb64984f948e4a88b16c0c587e848f10", WlyStaffType.文凤卿, "c31d226e350c44e3abd8d585bc394078"));
            AddTask(new 任务_武将突飞("b2ddf2b541a44d42a3c95475bca1c67f", WlyStaffType.裴元绍, 40, "fb64984f948e4a88b16c0c587e848f10"));
            AddTask(new 任务_武将转职("c80bcbabd73d421dad3eb3c94ad8eb29", WlyStaffType.裴元绍, "b2ddf2b541a44d42a3c95475bca1c67f"));
            AddTask(new 任务_重修设定("92fd5fe7542940abb84811b1b1bbfcf9", WlyStaffType.文凤卿, WlySoldierType.烈火长弓, "c80bcbabd73d421dad3eb3c94ad8eb29"));
            AddTask(new 任务_重修设定("e68d981f387b4172ade3c6cb34b2f106", WlyStaffType.裴元绍, WlySoldierType.短戟刺客, "92fd5fe7542940abb84811b1b1bbfcf9"));
            //AddTask(new 任务_强化装备("28754d315d6548a288b0d6634f2aeb52", WlyStaffType.裴元绍, WlyEquipType.物理防御, 40, "c80bcbabd73d421dad3eb3c94ad8eb29"));
            //AddTask(new 任务_强化装备("fda73d537e8d44cf8e62b0821476c828", WlyStaffType.裴元绍, WlyEquipType.物理攻击, 40, "28754d315d6548a288b0d6634f2aeb52"));
            //AddTask(new 任务_强化装备("1d702611eb3e469b9a1da4fad43428f4", WlyStaffType.文凤卿, WlyEquipType.物理防御, 40, "fda73d537e8d44cf8e62b0821476c828"));
            //AddTask(new 任务_强化装备("c651b32cf16b45d5ab5e8224f1f00897", WlyStaffType.文凤卿, WlyEquipType.物理攻击, 40, "1d702611eb3e469b9a1da4fad43428f4"));
            //AddTask(new 任务_强化装备("28a01962fb494ed2af20227ab8d16851", WlyStaffType.文凤卿, WlyEquipType.带兵量, 40, "c651b32cf16b45d5ab5e8224f1f00897"));
            AddTask(new 任务_武将顿悟("d20d2dfbb21e4ec8825b9eed461252fa", WlyStaffType.裴元绍, WlyQualityType.Blue, "28a01962fb494ed2af20227ab8d16851"));
            AddTask(new 任务_武将顿悟("5284cfa5efa445f2ac2fad5c7bce54b8", WlyStaffType.文凤卿, WlyQualityType.Blue, "d20d2dfbb21e4ec8825b9eed461252fa"));
            AddTask(new 任务_推图("d7a35802419d45fab736e9e4a6ef1093", 3, "5284cfa5efa445f2ac2fad5c7bce54b8", "610d196420a64ae78351abf5125dbf99"));
            AddTask(new 任务_推图("751dbfc103614447a964585d2caf232b", 4, "d7a35802419d45fab736e9e4a6ef1093"));

            AddTask(new 任务_小号辅助("1a199a4d12064ecbb485d9402fc5708b", "751dbfc103614447a964585d2caf232b"));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static AssistTaskMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new AssistTaskMgr();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion
    }
}