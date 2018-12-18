// *******************************************************************
// * 文件名称： SubTaskMgr.cs
// * 作　　者： 王璟星
////// * 创建日期： 2018-04-20 14:59:51
////// *******************************************************************

using System;

using Wx.App.WlySubAutoPlayer.Tasks;
using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyTasks.DailyTask;
using Wx.Lib.WlyAutoBiz.WlyTasks.MainTask;
using Wx.Lib.WlyAutoBiz.WlyViews;

namespace Wx.App.WlySubAutoPlayer.Biz
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class SubTaskMgr : WlyTaskMgr
    {
        #region Fields

        private static readonly object _instanceLocker = new object();

        private static SubTaskMgr _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private SubTaskMgr()
        {
            AddTask(new 任务_购买军令("eddd8f4d28644d2498655b8fe1e1f1df", "549e7319ef2944268bbc5251eb0d4a3a"));
            AddTask(new 任务_手动巡查("1f5484ec82b0454ebdbde7b348aaa0a6", "2738a73cc77a496983647a1717a9ca10"));
            AddTask(new 任务_手动打猎("eb4886d3eaf340c6a55fa0303a11a397", "e4a2cd5898f0485499797ddb916b7267"));
            AddTask(new 任务_酒馆("418e2da1111243d4b0653fa59840f880", "be986ed68f2a40b781a9948e0ac3eb6c"));
            AddTask(new 任务_普通砸罐("03a5738ccee943ef8d53c6aee5f873ed", "e4a2cd5898f0485499797ddb916b7267"));
            AddTask(new 任务_征召士兵("85d00a6e53a04010a90a584c342b8946", "08e6eb1913bc4309832b08c8a031d14e"));
            AddTask(new 任务_开发城池("be18cfefa68743c78d6992580360491e", WlyCityType.江夏, "eb7d89eef3024e9eaf76bc43cb3785df"));
            AddTask(new 任务_兵种重修("8e76068ac10443e3b173d53064709259", "e68d981f387b4172ade3c6cb34b2f106"));
            //AddTask(new 任务_军团日常("efca1b32967a4d6986bfdb52a6fcfcd3", "342f0c65f43142aaa028d5e04cc13fb7"));
            //AddTask(new 任务_攻打城市("28c11437d3de45a49920591d1f3b3345", "eb7d89eef3024e9eaf76bc43cb3785df"));
            AddTask(new 任务_收粮("bc3ab2cdec5d496cb07d2dc1e38f4f8d", "342f0c65f43142aaa028d5e04cc13fb7"));
            AddTask(new 任务_免费军团("1ed1d8def3f445e2ab522661701f740f", "08b88e442e99471bbe2413a8ed4cb7b0"));
            AddTask(new 任务_属性重置("b1810c6795f64153ae104d86ff908730", "e563e19a584f4a579fb5410511608a59"));
            //AddTask(new 任务_活跃度奖励("285bd8d581384762b95c62da233a6ba3", "2eb55829772a490abf2433ce56545e0e"));
            AddTask(new 任务_征收("742c248abea94412b97bf99a430fc6b8", "e4a2cd5898f0485499797ddb916b7267"));

            AddTask(new 任务_委派("e447063326594c43831c91d74aed61d0", "e4a2cd5898f0485499797ddb916b7267"));

            //AddTask(new 任务_游历("6003fe35c6d74e0e91b29127c838602a", "e261539d6a414d408b94b92971c64484"));
            AddTask(new 任务_生产("197074e505ea43c8801a5f44b0e15d12", "2b38c1c1414345b498c75a85a9907b9c"));
            AddTask(new 任务_清理装备("56a30ced745048fb99852dbfed92bd91", "2eb55829772a490abf2433ce56545e0e"));
            //AddTask(new 任务_主公经验("37b0be334d824cdd8069e03e133b8e99", TimeSpan.FromHours(10), "e261539d6a414d408b94b92971c64484"));
            AddTask(new 任务_领取奖励("5e91b2f5faa348e7a354f411ff3526b3", TimeSpan.FromHours(10), "e563e19a584f4a579fb5410511608a59"));
            AddTask(new 任务_竞技场("fb6645ed09e74a21bf7ce1b1dfad1a7b", "2738a73cc77a496983647a1717a9ca10"));
            AddTask(new 任务_领取俸禄("ade8f17ccb8f42139fced994cd157260", "2738a73cc77a496983647a1717a9ca10"));
            AddTask(new 任务_厉兵秣马("2621dc5d69ae4ee9bd535699c1bc61b7"));

            // 主线任务（10级）
            AddTask(new 任务_角色检测("549e7319ef2944268bbc5251eb0d4a3a"));
            AddTask(new 任务_名称检测("ebed05aaae1142adbfdc68a79026bc32", "549e7319ef2944268bbc5251eb0d4a3a"));
            AddTask(new 任务_升级建筑("e563e19a584f4a579fb5410511608a59", WlyBuildingType.主城, 10, "ebed05aaae1142adbfdc68a79026bc32"));
            AddTask(new 任务_升级建筑("7192432b6017458983982c86a1d9840a", WlyBuildingType.策略府, 10, "e563e19a584f4a579fb5410511608a59"));
            AddTask(new 任务_升级科技("f93a1c4941d440ebb526db9f5c441943", 10, "7192432b6017458983982c86a1d9840a"));
            AddTask(new 任务_升级建筑("2031a3eec7fe4d7aba1e60ed9e851009", WlyBuildingType.商铺, 10, "7192432b6017458983982c86a1d9840a"));
            AddTask(new 任务_升级建筑("5ceb0caf56ae49c1a8a2528d8c3a93e4", WlyBuildingType.校场, 10, "2031a3eec7fe4d7aba1e60ed9e851009"));
            AddTask(new 任务_武将突飞("04d091b2611e4ee2becda4cbe7d49ce2", WlyStaffType.文凤卿, 10, "5ceb0caf56ae49c1a8a2528d8c3a93e4"));
            AddTask(new 任务_需求装备("e3236af0c1bc49d89c7c05e0f8b22938", WlyStaffType.文凤卿,
                new[] { WlyEquipType.物理攻击, WlyEquipType.计策攻击, WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 },
                "04d091b2611e4ee2becda4cbe7d49ce2"));
            AddTask(new 任务_推图("c8b9e5aaf90d4fd88fc8194d6f6aaa8f", 1, "f93a1c4941d440ebb526db9f5c441943", "e3236af0c1bc49d89c7c05e0f8b22938"));
            AddTask(new 任务_搬迁主城("b5f455928b1b4746b9a6137b22b5b94c", WlyCityType.长安, "c8b9e5aaf90d4fd88fc8194d6f6aaa8f"));

            // 主线任务（20级）
            AddTask(new 任务_开通队列("ffd832704aac4b92b99abf165565cac0", "b5f455928b1b4746b9a6137b22b5b94c"));
            AddTask(new 任务_购买礼包("f14aff73c81849bc9c53a8d58987d1a2", "e563e19a584f4a579fb5410511608a59"));
            AddTask(new 任务_升级建筑("844571c4075444fe98a90fe3e6aa1eb1", WlyBuildingType.主城, 20, "ffd832704aac4b92b99abf165565cac0"));
            AddTask(new 任务_升级建筑("08e6eb1913bc4309832b08c8a031d14e", WlyBuildingType.兵营, 20, "844571c4075444fe98a90fe3e6aa1eb1"));
            AddTask(new 任务_升级建筑("8c3d41adacfa4c76850f82715ed44b7d", WlyBuildingType.策略府, 20, "08e6eb1913bc4309832b08c8a031d14e"));
            AddTask(new 任务_升级科技("a822faeb15e748688d93bfd1d3a27f69", 20, "8c3d41adacfa4c76850f82715ed44b7d"));
            AddTask(new 任务_升级建筑("fe6bed2b53f2438dad9c1a889a3af65e", WlyBuildingType.商铺, 20, "8c3d41adacfa4c76850f82715ed44b7d"));
            AddTask(new 任务_升级建筑("3c991091c92e41aaae6f1d488c967d1c", WlyBuildingType.校场, 20, "fe6bed2b53f2438dad9c1a889a3af65e"));
            AddTask(new 任务_武将突飞("ba6c41b4bafd4268b121b9397d3d0a9a", WlyStaffType.文凤卿, 20, "3c991091c92e41aaae6f1d488c967d1c"));
            AddTask(new 任务_武将转职("6e2ad8419bd84e32b61168ef28eab47e", WlyStaffType.文凤卿, "ba6c41b4bafd4268b121b9397d3d0a9a"));
            AddTask(new 任务_武将招募("9fe037f4a49c49918b136be5b11ebffb", WlyStaffType.裴元绍, "6e2ad8419bd84e32b61168ef28eab47e"));
            AddTask(new 任务_需求装备("43f700ed7af04825bc5c79aa0b001e2f", WlyStaffType.裴元绍,
                new[] { WlyEquipType.物理攻击, WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 }, "9fe037f4a49c49918b136be5b11ebffb"));
            AddTask(new 任务_武将突飞("1ab3a0d71f874f6eb6bae4570aac66d3", WlyStaffType.裴元绍, 20, "43f700ed7af04825bc5c79aa0b001e2f"));
            AddTask(new 任务_武将转职("e37cafb2c03c44a79ea545e7706a6083", WlyStaffType.裴元绍, "1ab3a0d71f874f6eb6bae4570aac66d3"));
            AddTask(new 任务_设置兵种("c716a3eeafd141818e5f2f9c8efcfd66", WlyStaffType.裴元绍, 2, "e37cafb2c03c44a79ea545e7706a6083"));
            AddTask(new 任务_武将上阵("8e183b724d664b069ed6c60a922679e1", WlyStaffType.裴元绍, WlyFormationType.鱼鳞阵, 2, "c716a3eeafd141818e5f2f9c8efcfd66"));
            AddTask(new 任务_推图("02c980114044463ea2eb5c13c3482436", 2, "8e183b724d664b069ed6c60a922679e1", "a822faeb15e748688d93bfd1d3a27f69"));
            AddTask(new 任务_搬迁主城("be986ed68f2a40b781a9948e0ac3eb6c", WlyCityType.会稽, "02c980114044463ea2eb5c13c3482436"));

            // 主线任务（40级）
            AddTask(new 任务_升级建筑("e4a2cd5898f0485499797ddb916b7267", WlyBuildingType.主城, 40, "be986ed68f2a40b781a9948e0ac3eb6c"));
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
            AddTask(new 任务_武将顿悟("d20d2dfbb21e4ec8825b9eed461252fa", WlyStaffType.裴元绍, WlyQualityType.Blue, "e68d981f387b4172ade3c6cb34b2f106"));
            AddTask(new 任务_武将顿悟("5284cfa5efa445f2ac2fad5c7bce54b8", WlyStaffType.文凤卿, WlyQualityType.Blue, "d20d2dfbb21e4ec8825b9eed461252fa"));
            AddTask(new 任务_推图("d7a35802419d45fab736e9e4a6ef1093", 3, "5284cfa5efa445f2ac2fad5c7bce54b8", "610d196420a64ae78351abf5125dbf99"));
            AddTask(new 任务_推图("751dbfc103614447a964585d2caf232b", 4, "d7a35802419d45fab736e9e4a6ef1093"));
            AddTask(new 任务_搬迁主城("08b88e442e99471bbe2413a8ed4cb7b0", WlyCityType.建业, "751dbfc103614447a964585d2caf232b"));

            // 主线任务（60级）
            AddTask(new 任务_升级建筑("e261539d6a414d408b94b92971c64484", WlyBuildingType.主城, 60, "08b88e442e99471bbe2413a8ed4cb7b0"));
            AddTask(new 任务_主线奖励("cd1223e4c03b47cfb892866a51dd5449", "91b64d11d39e492b92d69f028e8f1666"));
            AddTask(new 任务_升级建筑("8b335c5eb1234f60a50e625d9a1d4b2a", WlyBuildingType.策略府, 60, "e261539d6a414d408b94b92971c64484"));
            AddTask(new 任务_升级科技("7261852fb3ef4c3e8960e43a2195a3aa", 60, "8b335c5eb1234f60a50e625d9a1d4b2a"));
            AddTask(new 任务_升级建筑("edd3d6a9f1574b63adc3f8ded62c2a25", WlyBuildingType.商铺, 60, "8b335c5eb1234f60a50e625d9a1d4b2a"));
            AddTask(new 任务_武将突飞("0ed266837fe64806ab251d96b993545b", WlyStaffType.文凤卿, 60, "edd3d6a9f1574b63adc3f8ded62c2a25"));
            AddTask(new 任务_武将突飞("f0bd1c9ffbb4409cbf3222a413c77553", WlyStaffType.裴元绍, 60, "0ed266837fe64806ab251d96b993545b"));
            AddTask(new 任务_推图("c725e10e61f045c990d57f42e174f48d", 5, "7261852fb3ef4c3e8960e43a2195a3aa", "f0bd1c9ffbb4409cbf3222a413c77553"));
            AddTask(new 任务_推图("e1bc61ada4c148659dea563e358349f1", 6, "c725e10e61f045c990d57f42e174f48d"));
            AddTask(new 任务_推图("4198e3e77f784559b31b03c48e339d68", 7, "e1bc61ada4c148659dea563e358349f1"));
            AddTask(new 任务_推图("0ca5cc6ea68a4cdc8e4d5faa1dcff320", 8, "4198e3e77f784559b31b03c48e339d68"));
            AddTask(new 任务_搬迁主城("e1d217d88576426ca40b8cc15dc7a796", WlyCityType.长沙, "0ca5cc6ea68a4cdc8e4d5faa1dcff320"));

            // 主线任务（80级）
            AddTask(new 任务_升级建筑("5716f585b09e44b0a237ecaa02fda31b", WlyBuildingType.主城, 80, "e1d217d88576426ca40b8cc15dc7a796"));
            AddTask(new 任务_主线奖励("22f0ca26591142aaa6b07ee64d82cbf4", "e1d217d88576426ca40b8cc15dc7a796"));
            AddTask(new 任务_升级建筑("2008fa93bf104241841f2201ac185eb9", WlyBuildingType.策略府, 80, "5716f585b09e44b0a237ecaa02fda31b"));
            AddTask(new 任务_升级科技("5a4610bf6131419bbcf4d9d696fa31d6", 80, "2008fa93bf104241841f2201ac185eb9"));
            AddTask(new 任务_升级建筑("a39af800da44423da48cbe8b54064738", WlyBuildingType.商铺, 80, "2008fa93bf104241841f2201ac185eb9"));
            AddTask(new 任务_武将突飞("c71aa78467d4471b9501128e8efeacaf", WlyStaffType.文凤卿, 80, "a39af800da44423da48cbe8b54064738"));
            AddTask(new 任务_武将突飞("0d393e9c2bdc49109c28120ec58588f7", WlyStaffType.裴元绍, 80, "c71aa78467d4471b9501128e8efeacaf"));
            AddTask(new 任务_推图("4c29dbfdb03b4ee3b1597bead75c9541", 9, "0d393e9c2bdc49109c28120ec58588f7", "5a4610bf6131419bbcf4d9d696fa31d6"));
            AddTask(new 任务_推图("5c6f637669a54517a2b8894dd48fa3b9", 10, "4c29dbfdb03b4ee3b1597bead75c9541"));
            AddTask(new 任务_推图("452364c084c4496781966ec0f3d463d8", 11, "5c6f637669a54517a2b8894dd48fa3b9"));
            AddTask(new 任务_推图("da78ea993d4b41beb8ddf7688d3202de", 12, "452364c084c4496781966ec0f3d463d8"));
            AddTask(new 任务_搬迁主城("2eb55829772a490abf2433ce56545e0e", WlyCityType.江夏, "da78ea993d4b41beb8ddf7688d3202de"));

            // 主线任务（100级）
            AddTask(new 任务_主线奖励("eb7d89eef3024e9eaf76bc43cb3785df", "2eb55829772a490abf2433ce56545e0e"));
            AddTask(new 任务_升级建筑("2738a73cc77a496983647a1717a9ca10", WlyBuildingType.主城, 100, "2eb55829772a490abf2433ce56545e0e"));
            //AddTask(new 任务_加入军团("342f0c65f43142aaa028d5e04cc13fb7", "2738a73cc77a496983647a1717a9ca10"));
            AddTask(new 任务_金币研发("a989d3788be04141ae050185fb749d1b", "342f0c65f43142aaa028d5e04cc13fb7"));
            AddTask(new 任务_升级建筑("5c21fee080604292bfcd2957c2de594a", WlyBuildingType.民居1, 100, "2738a73cc77a496983647a1717a9ca10"));
            AddTask(new 任务_升级建筑("72add6f939c542fda3d94a4732a75ae9", WlyBuildingType.策略府, 100, "5c21fee080604292bfcd2957c2de594a"));
            AddTask(new 任务_升级科技("7f006a6ec1e84af2ba7816a8e883bcc4", 100, "72add6f939c542fda3d94a4732a75ae9"));
            AddTask(new 任务_升级建筑("e0535784fa3b49fe998ddc65f817516c", WlyBuildingType.民居2, 100, "72add6f939c542fda3d94a4732a75ae9"));
            AddTask(new 任务_升级建筑("f5586fa546394ad88c17fcd6e9b6adcb", WlyBuildingType.商铺, 100, "e0535784fa3b49fe998ddc65f817516c"));
            AddTask(new 任务_升级建筑("9259f3bdc92146688bfa8cf747bdc67b", WlyBuildingType.校场, 100, "f5586fa546394ad88c17fcd6e9b6adcb"));
            AddTask(new 任务_获取威望("af72a01867ce406db1dce81b2e4ae95e", 10000, "9259f3bdc92146688bfa8cf747bdc67b"));
            AddTask(new 任务_武将招募("077f0278440f4f0d9712b2b110e71c83", WlyStaffType.小乔, "af72a01867ce406db1dce81b2e4ae95e"));
            AddTask(new 任务_武将突飞("18785b82ec884c51998123529291456a", WlyStaffType.小乔, 40, "077f0278440f4f0d9712b2b110e71c83"));
            AddTask(new 任务_武将转职("e3cc541f7fcd4a4496d20b7226b8dede", WlyStaffType.小乔, "18785b82ec884c51998123529291456a"));
            AddTask(new 任务_武将转职("2cb1ed21d0a84f0ab6265eec24537d5f", WlyStaffType.小乔, "e3cc541f7fcd4a4496d20b7226b8dede"));
            AddTask(new 任务_武将突飞("22c1735af55f4da1917b70f7abac4a2a", WlyStaffType.小乔, 60, "2cb1ed21d0a84f0ab6265eec24537d5f"));
            AddTask(new 任务_武将顿悟("d74b5dfdaf974048872d6e708cf16f90", WlyStaffType.小乔, WlyQualityType.Blue, "2cb1ed21d0a84f0ab6265eec24537d5f"));
            AddTask(new 任务_重修设定("b633d86782504f09a2dfda826fc45e57", WlyStaffType.小乔, WlySoldierType.旗鼓手, "2cb1ed21d0a84f0ab6265eec24537d5f"));
            AddTask(new 任务_设置兵种("7e4cd7c78d3d45dd9862bea751c8c166", WlyStaffType.小乔, 2, "2cb1ed21d0a84f0ab6265eec24537d5f"));

            AddTask(new 任务_获取威望("dddd8300e8a0402c81af751b7106ec4b", 23000, "af72a01867ce406db1dce81b2e4ae95e"));
            AddTask(new 任务_武将招募("a3b99c91b7f44fecb68b038815c608bb", WlyStaffType.张纮, "dddd8300e8a0402c81af751b7106ec4b"));
            AddTask(new 任务_武将突飞("11b6e1b28bd149439ed0085828919348", WlyStaffType.张纮, 40, "a3b99c91b7f44fecb68b038815c608bb"));
            AddTask(new 任务_武将转职("2ab3ca054f2f4af79111fa14740c5de4", WlyStaffType.张纮, "11b6e1b28bd149439ed0085828919348"));
            AddTask(new 任务_武将转职("bdec677eefc44043a8352fc9645247d8", WlyStaffType.张纮, "2ab3ca054f2f4af79111fa14740c5de4"));
            AddTask(new 任务_武将突飞("0236596bef7346c8bc6be82ea113d28e", WlyStaffType.张纮, 60, "bdec677eefc44043a8352fc9645247d8"));
            AddTask(new 任务_武将顿悟("aba0618eb86b4ed3bab67c11270d1263", WlyStaffType.张纮, WlyQualityType.Blue, "0236596bef7346c8bc6be82ea113d28e"));
            AddTask(new 任务_武将顿悟("2add470be6cb46e8beb76ee512a1268c", WlyStaffType.张纮, WlyQualityType.Green, "aba0618eb86b4ed3bab67c11270d1263"));
            AddTask(new 任务_设置兵种("c7188bce968b4aabb22563f3008fc772", WlyStaffType.张纮, 2, "aba0618eb86b4ed3bab67c11270d1263"));
            AddTask(new 任务_重修设定("aaf98cf0d51b447da80db7f13a0b04f1", WlyStaffType.张纮, WlySoldierType.紫电使, "bdec677eefc44043a8352fc9645247d8"));

            AddTask(new 任务_武将招募("612482e71fb24e7688c29a8eb192b98d", WlyStaffType.朱然, "dddd8300e8a0402c81af751b7106ec4b"));
            AddTask(new 任务_武将突飞("0f5abd518a6d4c9cbbfdce36b2816597", WlyStaffType.朱然, 60, "612482e71fb24e7688c29a8eb192b98d"));
            AddTask(new 任务_武将转职("5583830bba29429eabe15973b66abf5a", WlyStaffType.朱然, "0f5abd518a6d4c9cbbfdce36b2816597"));
            AddTask(new 任务_武将转职("3cf7ca222a284ad795bf8b9c174d7589", WlyStaffType.朱然, "5583830bba29429eabe15973b66abf5a"));
            AddTask(new 任务_武将培养("994bdb7191b94adda4813d135a245217", WlyStaffType.朱然, 100, "3cf7ca222a284ad795bf8b9c174d7589"));
            AddTask(new 任务_武将顿悟("78f1e9117fe24403b249a00845ec5fd7", WlyStaffType.朱然, WlyQualityType.Blue, "3cf7ca222a284ad795bf8b9c174d7589"));
            AddTask(new 任务_武将顿悟("e740bd5d3905496ba165ab9ec19d1b7e", WlyStaffType.朱然, WlyQualityType.Green, "78f1e9117fe24403b249a00845ec5fd7"));
            AddTask(new 任务_武将顿悟("afd2c725f6c94dd5aab143b9f25814bc", WlyStaffType.文凤卿, WlyQualityType.Green, "e740bd5d3905496ba165ab9ec19d1b7e"));
            AddTask(new 任务_设置兵种("40a773ff23404eceb152ae1703a986f8", WlyStaffType.朱然, 2, "e740bd5d3905496ba165ab9ec19d1b7e"));
            AddTask(new 任务_重修设定("030cecd633c94b57bbe68be46306f9c4", WlyStaffType.朱然, WlySoldierType.疾风镖手, "3cf7ca222a284ad795bf8b9c174d7589"));

            AddTask(new 任务_获取威望("468d8c7179014e569eed7fef3c4b99f7", 500000, "af72a01867ce406db1dce81b2e4ae95e"));
            AddTask(new 任务_获取威望("d439952557464a31bba36bb7ca7c4447", 2900000, "468d8c7179014e569eed7fef3c4b99f7"));
            AddTask(new 任务_武将招募("7ff7564ea5734f69810c7d76b1fe0f92", WlyStaffType.孙权, "468d8c7179014e569eed7fef3c4b99f7"));
            AddTask(new 任务_武将下野("2e5b1ae727af4c219ac6abe77a040fcc", WlyStaffType.裴元绍, "7ff7564ea5734f69810c7d76b1fe0f92"));
            AddTask(new 任务_武将突飞("6f94f2e47d2e4761b5f84cf8fc0f142f", WlyStaffType.孙权, 40, "7ff7564ea5734f69810c7d76b1fe0f92"));
            AddTask(new 任务_武将转职("70ad76e01d5341ccacd24eeeae3da704", WlyStaffType.孙权, "6f94f2e47d2e4761b5f84cf8fc0f142f"));
            AddTask(new 任务_武将转职("3bb4b14bb4dd4c1cb20a4215bddf170d", WlyStaffType.孙权, "70ad76e01d5341ccacd24eeeae3da704"));
            AddTask(new 任务_武将突飞("ed9bbc7cd9524f0eaea28f4c016b2cde", WlyStaffType.孙权, 60, "3bb4b14bb4dd4c1cb20a4215bddf170d"));
            AddTask(new 任务_武将顿悟("45d873f398c3408ea44cc731d34e579d", WlyStaffType.孙权, WlyQualityType.Blue, "ed9bbc7cd9524f0eaea28f4c016b2cde"));
            AddTask(new 任务_设置兵种("dc490aac83cb434286b636b6a184aa45", WlyStaffType.孙权, 2, "45d873f398c3408ea44cc731d34e579d"));
            AddTask(new 任务_重修设定("9094de2ce207469e8f17fcf37acde3da", WlyStaffType.孙权, WlySoldierType.旗鼓手, "ed9bbc7cd9524f0eaea28f4c016b2cde"));

            AddTask(new 任务_武将培养("ceb5037249c64dae96b93beb8ac763c6", WlyStaffType.文凤卿, 100, "2cb1ed21d0a84f0ab6265eec24537d5f"));
            AddTask(new 任务_武将培养("b3da487b47084e63a9141f71fa7cb8b3", WlyStaffType.小乔, 100, "22c1735af55f4da1917b70f7abac4a2a"));
            AddTask(new 任务_武将培养("c3235ac693ba4dbaa7492923dc8056ab", WlyStaffType.孙权, 100, "ed9bbc7cd9524f0eaea28f4c016b2cde"));
            AddTask(new 任务_武将培养("7fa8d1eb69a6471b8d79f256f7218451", WlyStaffType.张纮, 100, "0236596bef7346c8bc6be82ea113d28e"));

            AddTask(new 任务_升级建筑("28cd9f6f3721404b8f68a5f180cbe6dd", WlyBuildingType.兵营, 100, "9259f3bdc92146688bfa8cf747bdc67b"));
            AddTask(new 任务_升级建筑("13984aec00b843a3808bcde8c1a77b72", WlyBuildingType.工坊, 100, "28cd9f6f3721404b8f68a5f180cbe6dd"));
            AddTask(new 任务_升级建筑("85204f62ef0f47a99a88d49f06864a02", WlyBuildingType.钱庄, 100, "13984aec00b843a3808bcde8c1a77b72"));
            AddTask(new 任务_升级建筑("ea2866cee4ac4aa19052bfe3e385e7d9", WlyBuildingType.商社, 100, "85204f62ef0f47a99a88d49f06864a02"));
            AddTask(new 任务_升级建筑("af58b4f1e17441bea5bdffb0613ba631", WlyBuildingType.账房, 100, "ea2866cee4ac4aa19052bfe3e385e7d9"));
            AddTask(new 任务_升级建筑("e21b75997e10464f9e281d62e7d89d4a", WlyBuildingType.农田, 100, "af58b4f1e17441bea5bdffb0613ba631"));
            AddTask(new 任务_升级建筑("98e33d5a17d44d8880e232dc7213ef72", WlyBuildingType.试炼塔, 100, "e21b75997e10464f9e281d62e7d89d4a"));
            AddTask(new 任务_升级建筑("873f076d10884026b66a5a44df994f86", WlyBuildingType.市场, 100, "98e33d5a17d44d8880e232dc7213ef72"));

            // 继续推图
            AddTask(new 任务_需求装备("5c86fad897b84e1dac203fc81f1e4410", WlyStaffType.朱然,
                new[] { WlyEquipType.物理攻击, WlyEquipType.战法攻击, WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 },
                "612482e71fb24e7688c29a8eb192b98d"));
            AddTask(new 任务_需求装备("e3bcacf5b805455986461de39f693c5a", WlyStaffType.张纮,
                new[] { WlyEquipType.物理攻击, WlyEquipType.计策攻击, WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 },
                "a3b99c91b7f44fecb68b038815c608bb"));
            AddTask(new 任务_需求装备("fca7805efedd4b389b41e7dce7e0870d", WlyStaffType.孙权,
                new[] { WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 }, "7ff7564ea5734f69810c7d76b1fe0f92"));
            AddTask(new 任务_需求装备("f51bdfa12cb047c896f1eb5b7ce27572", WlyStaffType.小乔,
                new[] { WlyEquipType.物理防御, WlyEquipType.战法防御, WlyEquipType.计策防御 }, "077f0278440f4f0d9712b2b110e71c83"));

            // 阵型更改
            AddTask(new 任务_武将上阵("b122ef78e8a84fbd8e031e09e54a447a", WlyStaffType.文凤卿, WlyFormationType.乱剑阵, 5, "9094de2ce207469e8f17fcf37acde3da"));
            AddTask(new 任务_武将上阵("ed5a3a1b672c4b00a3d026a9a8f922d4", WlyStaffType.孙权, WlyFormationType.乱剑阵, 4, "9094de2ce207469e8f17fcf37acde3da"));
            AddTask(new 任务_武将上阵("1a5a7644690c47fc8f8a25a2c1d3e12d", WlyStaffType.朱然, WlyFormationType.乱剑阵, 1, "9094de2ce207469e8f17fcf37acde3da"));
            AddTask(new 任务_武将上阵("c1460fd898f3427b9d972dd525f87696", WlyStaffType.张纮, WlyFormationType.乱剑阵, 7, "9094de2ce207469e8f17fcf37acde3da"));
            AddTask(new 任务_武将上阵("ae76d0b74697474fb214d331d5bece2b", WlyStaffType.小乔, WlyFormationType.乱剑阵, 8, "9094de2ce207469e8f17fcf37acde3da"));

            // 开始推图
            AddTask(new 任务_推图("1925698fde864f5cada4f86f6156af90", 13, "c3235ac693ba4dbaa7492923dc8056ab"));
            AddTask(new 任务_推图("6b9f9e5ffd254c218e2e1d69c2fae590", 14, "1925698fde864f5cada4f86f6156af90"));
            AddTask(new 任务_推图("b10122d6c9904e06a82afd4137fe4115", 15, "6b9f9e5ffd254c218e2e1d69c2fae590"));
            AddTask(new 任务_推图("a8132a21ffa6462ea87cbe3a49c784b3", 16, "b10122d6c9904e06a82afd4137fe4115"));
            AddTask(new 任务_推图("9e580ae8c8374142b12c612fb3cdc185", 17, "a8132a21ffa6462ea87cbe3a49c784b3"));
            AddTask(new 任务_推图("022dab2365fc4cf4ad69791184bdc524", 18, "9e580ae8c8374142b12c612fb3cdc185"));
            AddTask(new 任务_推图("9cf8f9753a7545789f6a6b3295d87ddf", 19, "022dab2365fc4cf4ad69791184bdc524"));

            // 升级到110
            AddTask(new 任务_升级建筑("238f2913bb164fdc933452e2e10da3de", WlyBuildingType.主城, 110, "9cf8f9753a7545789f6a6b3295d87ddf"));
            AddTask(new 任务_升级建筑("d4542e0b4c2045aea37a7bf3c5e1c026", WlyBuildingType.策略府, 110, "238f2913bb164fdc933452e2e10da3de"));
            AddTask(new 任务_升级建筑("6cc00c199e8743e2a23a94adb6de5c02", WlyBuildingType.商铺, 110, "d4542e0b4c2045aea37a7bf3c5e1c026"));
            AddTask(new 任务_升级建筑("71a1d72325ff4396a3341cd715c9fb0a", WlyBuildingType.校场, 110, "6cc00c199e8743e2a23a94adb6de5c02"));
            AddTask(new 任务_升级科技("7cb202bcb97944db9f0bc06e873a5ef9", 110, "d4542e0b4c2045aea37a7bf3c5e1c026"));
            AddTask(new 任务_升级建筑("42c4e7327e554d9195791e5914f0088d", WlyBuildingType.民居1, 110, "71a1d72325ff4396a3341cd715c9fb0a"));
            AddTask(new 任务_升级建筑("cfc4abd898c34ec1a7c757e17c6a3879", WlyBuildingType.账房, 110, "42c4e7327e554d9195791e5914f0088d"));
            AddTask(new 任务_升级建筑("8c50e01c1ba54dda8a2c0b440f43ebcc", WlyBuildingType.民居2, 110, "cfc4abd898c34ec1a7c757e17c6a3879"));
            AddTask(new 任务_升级建筑("fea2681e7dfe4f9d96162e4c4ddc918d", WlyBuildingType.农田, 110, "8c50e01c1ba54dda8a2c0b440f43ebcc"));
            AddTask(new 任务_升级建筑("e7656b7a71684de5bb1379858e8e179a", WlyBuildingType.民居3, 110, "fea2681e7dfe4f9d96162e4c4ddc918d"));
            AddTask(new 任务_升级建筑("cd918c807b384e33b1430a69c2c06cfc", WlyBuildingType.兵营, 110, "e7656b7a71684de5bb1379858e8e179a"));
            AddTask(new 任务_升级建筑("b6b9d12082e24f1b849f1b8640757b38", WlyBuildingType.试炼塔, 110, "cd918c807b384e33b1430a69c2c06cfc"));
            AddTask(new 任务_升级建筑("58cc8e88325145e4892dd2b548bf7ce3", WlyBuildingType.钱庄, 110, "b6b9d12082e24f1b849f1b8640757b38"));
            AddTask(new 任务_升级建筑("a7c2a3a5252d4ad3ac12166dd3044ab6", WlyBuildingType.商社, 110, "58cc8e88325145e4892dd2b548bf7ce3"));
            AddTask(new 任务_升级建筑("7d134abbff31431c8567d89c3b46c876", WlyBuildingType.工坊, 110, "a7c2a3a5252d4ad3ac12166dd3044ab6"));
            AddTask(new 任务_升级建筑("5fd9e1b20c3e41a3bcc84da5686ba6c9", WlyBuildingType.市场, 110, "7d134abbff31431c8567d89c3b46c876"));

            // 助手设置
            AddTask(new 任务_小助手设置("72bed49427004b65b359922a892892ea", "238f2913bb164fdc933452e2e10da3de"));

            // 培养武将
            AddTask(new 任务_武将培养("9534df55ed31409283c1fce3ae111427", WlyStaffType.文凤卿, 110, "238f2913bb164fdc933452e2e10da3de"));
            AddTask(new 任务_武将培养("cff4a3fbd461434b916f6e3dfb5afec3", WlyStaffType.小乔, 110, "238f2913bb164fdc933452e2e10da3de"));
            AddTask(new 任务_武将培养("0ca3213e190b4169bb81cf79e4043999", WlyStaffType.孙权, 110, "238f2913bb164fdc933452e2e10da3de"));
            AddTask(new 任务_武将培养("5034a509190f4082a5625dc7fc43b7cd", WlyStaffType.张纮, 110, "238f2913bb164fdc933452e2e10da3de"));
            AddTask(new 任务_武将培养("1d302ac165714fa39c14ade8ca69b92f", WlyStaffType.朱然, 110, "238f2913bb164fdc933452e2e10da3de"));

            // 推试炼塔
            // 阵型更改
            AddTask(new 任务_武将上阵("8c96473b08014be9a21c93d1b5ed968d", WlyStaffType.文凤卿, WlyFormationType.乱剑阵, 8, "7cb202bcb97944db9f0bc06e873a5ef9"));
            AddTask(new 任务_武将上阵("2c345cbe486d4be2bd3efd2511044dc7", WlyStaffType.孙权, WlyFormationType.乱剑阵, 1, "8c96473b08014be9a21c93d1b5ed968d"));
            AddTask(new 任务_武将上阵("94203877b90e4c09b91a0c6fe9541c33", WlyStaffType.朱然, WlyFormationType.乱剑阵, 7, "2c345cbe486d4be2bd3efd2511044dc7"));
            AddTask(new 任务_武将上阵("199421fc0dc54711b49a38f02b8230e8", WlyStaffType.张纮, WlyFormationType.乱剑阵, 4, "94203877b90e4c09b91a0c6fe9541c33"));
            AddTask(new 任务_武将上阵("4933ac22c89f47b389353cb3869be645", WlyStaffType.小乔, WlyFormationType.乱剑阵, 5, "199421fc0dc54711b49a38f02b8230e8"));
            AddTask(new 任务_推试炼塔("9b1f9ae562784c1381d8aa95e5b9c404", 40, "4933ac22c89f47b389353cb3869be645"));
            AddTask(new 任务_武将转职("cafcca9debd34b61b87f9fa3c054210d", WlyStaffType.文凤卿, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_设置兵种("c45136447a57408992c0233308980605", WlyStaffType.文凤卿, 1, "cafcca9debd34b61b87f9fa3c054210d"));
            AddTask(new 任务_武将转职("ad8f079f76154fb38143e01ab18f982c", WlyStaffType.小乔, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_设置兵种("d1c45904755845cb9ae7b9342410ff7b", WlyStaffType.小乔, 2, "ad8f079f76154fb38143e01ab18f982c"));
            AddTask(new 任务_武将转职("bb7a5dd4540f44788482671cb0845dfb", WlyStaffType.孙权, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_设置兵种("60c58a4caa124e10a436040147947460", WlyStaffType.孙权, 2, "bb7a5dd4540f44788482671cb0845dfb"));
            AddTask(new 任务_武将转职("617292eaf3694fbeb0dea21657d1de36", WlyStaffType.张纮, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_设置兵种("a7d1d9f088c84f3c86bb8e2f3076c18c", WlyStaffType.张纮, 2, "617292eaf3694fbeb0dea21657d1de36"));
            AddTask(new 任务_武将转职("a340a7d7ac96415085632588f5e0810a", WlyStaffType.朱然, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_设置兵种("d85f3ca5753d4fc7b3aa2aae23be1fc3", WlyStaffType.朱然, 2, "a340a7d7ac96415085632588f5e0810a"));

            AddTask(new 任务_重修设定("c376412029c941449ec6c629f973275e", WlyStaffType.张纮, WlySoldierType.引雷术士, "617292eaf3694fbeb0dea21657d1de36"));
            AddTask(new 任务_重修设定("6a85098cb40946138356f6a6cc313d9b", WlyStaffType.朱然, WlySoldierType.皇廷重弩, "a340a7d7ac96415085632588f5e0810a"));
            AddTask(new 任务_重修设定("a43c2fde00074231a1930052cf4a9d9d", WlyStaffType.文凤卿, WlySoldierType.龙头炮, "cafcca9debd34b61b87f9fa3c054210d"));
            AddTask(new 任务_重修设定("5e9d7d78b7384e7dade64fe1f3a7438a", WlyStaffType.孙权, WlySoldierType.擂鼓队, "bb7a5dd4540f44788482671cb0845dfb"));
            AddTask(new 任务_重修设定("e2db8bd074f54790a782f1700122e378", WlyStaffType.小乔, WlySoldierType.擂鼓队, "ad8f079f76154fb38143e01ab18f982c"));

            // 阵型回复
            AddTask(new 任务_武将上阵("773198462d2d474ca7498ccef82273c2", WlyStaffType.文凤卿, WlyFormationType.乱剑阵, 5, "9b1f9ae562784c1381d8aa95e5b9c404"));
            AddTask(new 任务_武将上阵("aa27c22872bf4fcea32502b1aa7f15bf", WlyStaffType.孙权, WlyFormationType.乱剑阵, 4, "773198462d2d474ca7498ccef82273c2"));
            AddTask(new 任务_武将上阵("fc99fa7015e348b09d2e75a21908ebde", WlyStaffType.朱然, WlyFormationType.乱剑阵, 1, "aa27c22872bf4fcea32502b1aa7f15bf"));
            AddTask(new 任务_武将上阵("8c47113119f74a89aff228ef6b52bcf8", WlyStaffType.张纮, WlyFormationType.乱剑阵, 7, "fc99fa7015e348b09d2e75a21908ebde"));
            AddTask(new 任务_武将上阵("6b275234954042e1a50a731bbc85c478", WlyStaffType.小乔, WlyFormationType.乱剑阵, 8, "8c47113119f74a89aff228ef6b52bcf8"));

            // 推图
            AddTask(new 任务_推图("d4c499170fdc4b90a0a24112c5ac4d90", 20, "6b275234954042e1a50a731bbc85c478"));
            AddTask(new 任务_推图("346faa0a335c4dfb958b197ad7bd093f", 21, "d4c499170fdc4b90a0a24112c5ac4d90"));
            AddTask(new 任务_推图("9d2839a4a11942b48aa403f631a28f08", 22, "346faa0a335c4dfb958b197ad7bd093f"));
            AddTask(new 任务_推图("77937694b07443b7b9d27c68f7b963ba", 23, "9d2839a4a11942b48aa403f631a28f08"));

            // 升级到120
            AddTask(new 任务_主线奖励("573474b1946242ee969b3b40f2781149", "77937694b07443b7b9d27c68f7b963ba"));
            AddTask(new 任务_升级建筑("b849bbaa24c34d569b9ba134325873ee", WlyBuildingType.主城, 120, "573474b1946242ee969b3b40f2781149"));
            AddTask(new 任务_升级建筑("63a0dc0fbbdf40049562a30353672c99", WlyBuildingType.策略府, 120, "b849bbaa24c34d569b9ba134325873ee"));
            AddTask(new 任务_升级建筑("9f7213365d464e248ab3d4f875dc7edc", WlyBuildingType.商铺, 120, "63a0dc0fbbdf40049562a30353672c99"));
            AddTask(new 任务_升级建筑("9e1d706cee754802895586389b9ab1dd", WlyBuildingType.校场, 120, "9f7213365d464e248ab3d4f875dc7edc"));
            AddTask(new 任务_升级科技("4ef00b9f018e4f20971b843a3ed8c06d", 120, "63a0dc0fbbdf40049562a30353672c99"));
            AddTask(new 任务_升级建筑("85ec0107135d4ee8b956815104ebedce", WlyBuildingType.民居1, 120, "9e1d706cee754802895586389b9ab1dd"));
            AddTask(new 任务_升级建筑("537c889083ef4747b33d209c1dbe416b", WlyBuildingType.账房, 120, "85ec0107135d4ee8b956815104ebedce"));
            AddTask(new 任务_升级建筑("f65d4805d54445118b34ddce3ecb110c", WlyBuildingType.民居2, 120, "537c889083ef4747b33d209c1dbe416b"));
            AddTask(new 任务_升级建筑("938db8af702c4e4e97728c18f623bc7d", WlyBuildingType.农田, 120, "f65d4805d54445118b34ddce3ecb110c"));
            AddTask(new 任务_升级建筑("7fc51d771c964a5eacb45efaa57330e4", WlyBuildingType.民居3, 120, "938db8af702c4e4e97728c18f623bc7d"));
            AddTask(new 任务_升级建筑("746a3ac40ac34be19826043a9076e5b0", WlyBuildingType.兵营, 120, "7fc51d771c964a5eacb45efaa57330e4"));
            AddTask(new 任务_升级建筑("cdf15ecc9a3245589ed1984a65003895", WlyBuildingType.试炼塔, 120, "746a3ac40ac34be19826043a9076e5b0"));
            AddTask(new 任务_升级建筑("8f6817c900ed4946880a570a4cb89ac8", WlyBuildingType.工坊, 120, "cdf15ecc9a3245589ed1984a65003895"));
            AddTask(new 任务_升级建筑("a52b6891470848b48af4d974bef97e90", WlyBuildingType.市场, 120, "8f6817c900ed4946880a570a4cb89ac8"));

            // 培养武将
            AddTask(new 任务_武将培养("43cb4e3f47704876a82dc66d9ed05d24", WlyStaffType.文凤卿, 120, "b849bbaa24c34d569b9ba134325873ee"));
            AddTask(new 任务_武将培养("12a221df99ae406ea1afe81426eeaa8c", WlyStaffType.小乔, 120, "b849bbaa24c34d569b9ba134325873ee"));
            AddTask(new 任务_武将培养("39f9bb4f0c014972b9061dd137a286b2", WlyStaffType.孙权, 120, "b849bbaa24c34d569b9ba134325873ee"));
            AddTask(new 任务_武将培养("32eff17849b6450fac02cc002cf1c431", WlyStaffType.张纮, 120, "b849bbaa24c34d569b9ba134325873ee"));
            AddTask(new 任务_武将培养("606d260dbc1a433eb85eee40e25f7f80", WlyStaffType.朱然, 120, "b849bbaa24c34d569b9ba134325873ee"));

            // 推图
            AddTask(new 任务_推图("44ebc737a83b4eefbe91d82e7be34f68", 24, "4ef00b9f018e4f20971b843a3ed8c06d"));
            AddTask(new 任务_推图("b2067e3d96c644dcad31d0eb37bca2b9", 25, "44ebc737a83b4eefbe91d82e7be34f68"));
            AddTask(new 任务_推图("6976bba5d1564a67810b585b7c0116e7", 26, "b2067e3d96c644dcad31d0eb37bca2b9"));

            // 升级到130
            AddTask(new 任务_主线奖励("d853993db12e449da9d172e47263ed7b", "6976bba5d1564a67810b585b7c0116e7"));
            AddTask(new 任务_升级建筑("0547397e846d48219e0cc2377638d75d", WlyBuildingType.主城, 130, "d853993db12e449da9d172e47263ed7b"));

            // 捐国政
            //AddTask(new 任务_获取政绩("fac3939a97984d4eb8cee1e7d06081c2", "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_王位争夺战("d317fbe9677648749cf876f19a7fb398", "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_国政日常("6c0bb0869bef4e0e99570f21bbc2ef4a", "0547397e846d48219e0cc2377638d75d"));

            // 助手设置
            AddTask(new 任务_小助手设置("f6a48a8174574362a9d06a440bc3431f", "0547397e846d48219e0cc2377638d75d"));

            // 培养武将
            AddTask(new 任务_武将培养("cb22c31652a64c658d296e56c3748fd7", WlyStaffType.文凤卿, 130, "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_武将培养("ce6db1652feb447dae2255e2d8b336a2", WlyStaffType.小乔, 130, "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_武将培养("31e5a423adde4cd2aca3c701f88b219b", WlyStaffType.孙权, 130, "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_武将培养("d213c2d21a3242fb91001e75e7572e11", WlyStaffType.张纮, 130, "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_武将培养("694da772e6c04d478a33deed1ce407d6", WlyStaffType.朱然, 130, "0547397e846d48219e0cc2377638d75d"));

            // 升级建筑
            AddTask(new 任务_升级建筑("f2d19b9c112d46ddaa6a5c9bd4fc659c", WlyBuildingType.策略府, 130, "0547397e846d48219e0cc2377638d75d"));
            AddTask(new 任务_升级建筑("b3a7dad5ae1f408c9ea83230256da646", WlyBuildingType.商铺, 130, "f2d19b9c112d46ddaa6a5c9bd4fc659c"));
            AddTask(new 任务_升级建筑("b07cf9dc41db4bfa858b7ecf04192d6a", WlyBuildingType.校场, 130, "b3a7dad5ae1f408c9ea83230256da646"));
            AddTask(new 任务_升级科技("d803a15ba99542e8b468197b6fe718b2", 130, "f2d19b9c112d46ddaa6a5c9bd4fc659c"));
            AddTask(new 任务_升级建筑("24eb5fe39cda4c6a92c858e6e12fb546", WlyBuildingType.民居1, 130, "b07cf9dc41db4bfa858b7ecf04192d6a"));
            AddTask(new 任务_升级建筑("0698086fde1b452c80c6ece277708ab2", WlyBuildingType.账房, 130, "24eb5fe39cda4c6a92c858e6e12fb546"));
            AddTask(new 任务_升级建筑("6a48c4ad19fc4223a9c31ebe31e37bcd", WlyBuildingType.民居2, 130, "0698086fde1b452c80c6ece277708ab2"));
            AddTask(new 任务_升级建筑("ee47c38cf3c24a62812613ebd2ae909e", WlyBuildingType.农田, 130, "6a48c4ad19fc4223a9c31ebe31e37bcd"));
            AddTask(new 任务_升级建筑("a3b52f8fac8b47faab47f581a162f9c8", WlyBuildingType.民居3, 130, "ee47c38cf3c24a62812613ebd2ae909e"));
            AddTask(new 任务_升级建筑("5d9e0d9735de4c328cda9024e29158cc", WlyBuildingType.兵营, 130, "a3b52f8fac8b47faab47f581a162f9c8"));
            AddTask(new 任务_升级建筑("669dd70df6c8449b91a205ee57e98db7", WlyBuildingType.民居4, 130, "5d9e0d9735de4c328cda9024e29158cc"));
            AddTask(new 任务_升级建筑("72af99a2c2c7481e9f18488b24e66a26", WlyBuildingType.试炼塔, 130, "669dd70df6c8449b91a205ee57e98db7"));
            AddTask(new 任务_升级建筑("1418e4a53e924597b0ac5c783240b79f", WlyBuildingType.工坊, 130, "72af99a2c2c7481e9f18488b24e66a26"));
            AddTask(new 任务_升级建筑("9e2494dc71d24658a7f892643f1deb53", WlyBuildingType.市场, 130, "1418e4a53e924597b0ac5c783240b79f"));
            AddTask(new 任务_升级建筑("81433e91698b4445b2100c735a0dfdf5", WlyBuildingType.船坞, 130, "9e2494dc71d24658a7f892643f1deb53"));
            AddTask(new 任务_升级建筑("307af698c8be443d9ab058f34c47cc9c", WlyBuildingType.仓库, 130, "81433e91698b4445b2100c735a0dfdf5"));
            AddTask(new 任务_升级建筑("a3114eefae354e66b5b924f2d91f7c3b", WlyBuildingType.商贸码头, 130, "81433e91698b4445b2100c735a0dfdf5"));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 单例
        /// </summary>
        public static SubTaskMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLocker)
                    {
                        if (_instance == null)
                        {
                            _instance = new SubTaskMgr();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion
    }
}