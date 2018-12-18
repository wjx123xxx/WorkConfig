// *******************************************************************
// * 文件名称： MainAccountInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-04-20 15:52:39
// *******************************************************************

using System.IO;

using Newtonsoft.Json;

using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Utility.WxCommon.Extension;

namespace Wx.App.WlyAutoUI.Biz
{
    /// <summary>
    /// 主账号信息
    /// </summary>
    public class MainAccountInfo : WlyAccountInfo
    {
        #region Constructors

        private MainAccountInfo(string filePath)
        {
            m_filePath = filePath;
            UID = MathHelper.GetNewGuid();
        }

        [JsonConstructor]
        private MainAccountInfo()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 加载一个账号信息
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public static WlyAccountInfo LoadAccount(string dataFile)
        {
            if (File.Exists(dataFile))
            {
                var accountInfo = JsonHelper.LoadFromXmlFile<MainAccountInfo>(dataFile);
                if (accountInfo == null)
                {
                    accountInfo = new MainAccountInfo(dataFile);
                }

                accountInfo.m_filePath = dataFile;
                return accountInfo;
            }

            return new MainAccountInfo(dataFile);
        }

        #endregion
    }
}