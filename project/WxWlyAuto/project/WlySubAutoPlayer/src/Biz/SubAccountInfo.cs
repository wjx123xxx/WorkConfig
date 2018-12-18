// *******************************************************************
// * 文件名称： SubAccountInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 15:01:14
// *******************************************************************

using System;
using System.IO;

using Newtonsoft.Json;

using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Utility.WxCommon.Extension;

namespace Wx.App.WlySubAutoPlayer.Biz
{
    /// <summary>
    /// 小号信息
    /// </summary>
    public class SubAccountInfo : WlyAccountInfo
    {
        #region Constructors

        public SubAccountInfo(string id)
        {
            m_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "sub", $"{id}.data");
            UID = id;
        }

        [JsonConstructor]
        private SubAccountInfo()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 是否通过认证
        /// </summary>
        public bool Check { get; set; }

        /// <summary>
        /// 角色序列号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 主公等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 下次登录时间
        /// </summary>
        public DateTime NextLoginTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 加载账号
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public static SubAccountInfo LoadAccount(string dataFile)
        {
            var accountInfo = JsonHelper.LoadFromXmlFile<SubAccountInfo>(dataFile);
            accountInfo.m_filePath = dataFile;
            return accountInfo;
        }

        #endregion

        #region Public Methods

        public override void ResetLoginTime()
        {
            NextLoginTime = DateTime.Now;
            Save();
        }

        public override void ResetMainLevel()
        {
            Level = 0;
        }

        #endregion
    }
}