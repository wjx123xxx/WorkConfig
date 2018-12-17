// *******************************************************************
// * 文件名称： WlySystemInfo.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-25 14:54:43
// *******************************************************************

using System;
using System.IO;
using System.Timers;

using Newtonsoft.Json;

using Wx.Utility.UICommon;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Lib.WlyAutoBiz.WlyInfo
{
    /// <summary>
    /// 与卧龙吟系统相关的一些参数
    /// </summary>
    public class WlySystemInfo : WxUIEntity
    {
        #region Fields

        private static readonly DateTime _gameStart = new DateTime(2017, 10, 3, 4, 0, 0);

        private static readonly string[] _seasons = { "春", "夏", "秋", "冬" };

        private readonly Timer m_timer = new Timer(400);

        private string m_configFile;

        /// <summary>
        /// Comment
        /// </summary>
        private int m_currentIndex;

        /// <summary>
        /// 当前时间
        /// </summary>
        private DateTime m_currentTime;

        /// <summary>
        /// 开发进度1
        /// </summary>
        private int m_develop1;

        /// <summary>
        /// 开发进度2
        /// </summary>
        private int m_develop2;

        /// <summary>
        /// 粮价
        /// </summary>
        private double m_foodPrice;

        /// <summary>
        /// 粮价描述信息
        /// </summary>
        private string m_foodPriceDescription;

        /// <summary>
        /// 游戏时间
        /// </summary>
        private string m_gameTime;

        /// <summary>
        /// 下次卖粮时间
        /// </summary>
        private DateTime m_nextFoodTime;

        #endregion

        #region Constructors

        [JsonConstructor]
        public WlySystemInfo()
        {
            m_timer.Elapsed += (sender, args) => CurrentTime = DateTime.Now;
            m_timer.Start();
        }

        private WlySystemInfo(string file)
        {
            m_configFile = file;
            m_timer.Elapsed += (sender, args) => CurrentTime = DateTime.Now;
            m_timer.Start();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Comment
        /// </summary>
        public int CurrentIndex
        {
            get { return m_currentIndex; }
            set
            {
                if (m_currentIndex != value)
                {
                    m_currentIndex = value;
                    OnPropertyChanged(nameof(CurrentIndex));
                }
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return m_currentTime; }
            set
            {
                if (m_currentTime != value)
                {
                    m_currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        /// <summary>
        /// 开发进度1
        /// </summary>
        public int Develop1
        {
            get { return m_develop1; }
            set
            {
                if (m_develop1 != value)
                {
                    m_develop1 = value;
                    OnPropertyChanged(nameof(Develop1));
                }
            }
        }

        /// <summary>
        /// 开发进度2
        /// </summary>
        public int Develop2
        {
            get { return m_develop2; }
            set
            {
                if (m_develop2 != value)
                {
                    m_develop2 = value;
                    OnPropertyChanged(nameof(Develop2));
                }
            }
        }

        /// <summary>
        /// 粮价
        /// </summary>
        public double FoodPrice
        {
            get { return m_foodPrice; }
            set
            {
                m_foodPrice = value;
                OnPropertyChanged(nameof(FoodPrice));
            }
        }

        /// <summary>
        /// 粮价描述信息
        /// </summary>
        public string FoodPriceDescription
        {
            get { return m_foodPriceDescription; }
            set
            {
                if (m_foodPriceDescription != value)
                {
                    m_foodPriceDescription = value;
                    OnPropertyChanged(nameof(FoodPriceDescription));
                }
            }
        }

        /// <summary>
        /// 游戏时间
        /// </summary>
        public string GameTime
        {
            get { return m_gameTime; }
            set
            {
                if (m_gameTime != value)
                {
                    m_gameTime = value;
                    OnPropertyChanged(nameof(GameTime));
                }
            }
        }

        /// <summary>
        /// 下次卖粮时间
        /// </summary>
        public DateTime NextFoodTime
        {
            get { return m_nextFoodTime; }
            set
            {
                if (m_nextFoodTime != value)
                {
                    m_nextFoodTime = value;
                    OnPropertyChanged(nameof(NextFoodTime));
                }
            }
        }

        #endregion

        #region Public Methods

        public static WlySystemInfo Load(string file)
        {
            WlySystemInfo info = null;
            try
            {
                if (File.Exists(file))
                {
                    info = JsonHelper.LoadFromXmlFile<WlySystemInfo>(file);
                    info.m_configFile = file;
                }
                else
                {
                    info = new WlySystemInfo(file);
                }
            }
            catch (Exception ex)
            {
                WxLog.Error($"WlySystemInfo.Load Error <{ex}>");
            }

            var gameInterval = (int)(DateTime.Now - _gameStart).TotalDays;
            info.m_gameTime = $"{180 + (gameInterval / 4)}年 {_seasons[gameInterval % 4]}";
            return info;
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            JsonHelper.SaveToXmlFile(this, m_configFile, "SystemInfo");
        }

        #endregion
    }
}