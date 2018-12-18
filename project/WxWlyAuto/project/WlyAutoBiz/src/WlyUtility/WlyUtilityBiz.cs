// *******************************************************************
// * 文件名称： WlyUtilityBiz.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-02-21 23:52:44
// *******************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Wx.Lib.WlyAutoBiz.WlyBiz;
using Wx.Lib.WlyAutoBiz.WlyCommon;
using Wx.Lib.WlyAutoBiz.WlyDefinition;
using Wx.Lib.WlyAutoBiz.WlyInfo;
using Wx.Utility.WxCommon.Entity;
using Wx.Utility.WxCommon.Extension;
using Wx.Utility.WxForDM.Service;
using Wx.Utility.WxFramework.Common.Log;

namespace Wx.Lib.WlyAutoBiz.WlyUtility
{
    /// <summary>
    /// 卧龙吟业务封装提供类
    /// </summary>
    public static class WlyUtilityBiz
    {
        #region Fields

        /// <summary>
        /// 登录锁
        /// </summary>
        private static readonly object _loginLocker = new object();

        /// <summary>
        ///  品质映射表
        /// </summary>
        private static readonly IDictionary<WlyQualityType, string> _qualityMap = new Dictionary<WlyQualityType, string>();

        public static readonly string BaseProgramPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app", "uqee.exe.old");

        /// <summary>
        /// 游戏程序路径
        /// </summary>
        public static readonly string ProgramPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app", "uqee.exe");

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        static WlyUtilityBiz()
        {
            _qualityMap.Add(WlyQualityType.White, "ffffff-000000");
            _qualityMap.Add(WlyQualityType.Blue, "33ccff-000000");
            _qualityMap.Add(WlyQualityType.Green, "66ff33-000000");
            _qualityMap.Add(WlyQualityType.Yellow, "ffff00-000000");
            _qualityMap.Add(WlyQualityType.Red, "ff3333-000000");
            _qualityMap.Add(WlyQualityType.Purple, "9900ff-000000");
            _qualityMap.Add(WlyQualityType.Orange, "ffcc00-000000");
            _qualityMap.Add(WlyQualityType.神皇, WlyColor.神皇);
            _qualityMap.Add(WlyQualityType.灭世, WlyColor.灭世);
            _qualityMap.Add(WlyQualityType.混沌, WlyColor.混沌);
            _qualityMap.Add(WlyQualityType.盘古, WlyColor.盘古);

            // 初始化图片资源
            var assembly = typeof(WlyUtilityBiz).Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                var stream = assembly.GetManifestResourceStream(res);
                var names = res.Split('.');
                var type = Enum.Parse(typeof(WlyPicType), names[names.Length - 2]);
                DMService.Instance.RegisterPicResource(type, stream);
            }

            // 设置全局字库
            //var dmGuid = DMService.Instance.CreateDMSoft(false);
            //DMService.Instance.SetDict(dmGuid, DictPath, 0);
            //DMService.Instance.EnableShareDict(dmGuid, true);
            //DMService.Instance.ReleaseDMSoft(dmGuid);
        }

        #endregion

        #region Public Properties

        public static string DictPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resource", "WLYWord.txt");

        /// <summary>
        /// 首攻检测标识
        /// </summary>
        public static bool FreeAttack { get; set; }

        /// <summary>
        /// 首攻锁
        /// </summary>
        public static object FreeAttackLocker { get; } = new object();

        /// <summary>
        /// 游戏窗口矩形框
        /// </summary>
        public static WxRect GameWndRect { get; } = new WxRect(0, 0, 1000, 600);

        /// <summary>
        /// 游戏系统全局信息
        /// </summary>
        public static WlySystemInfo SystemInfo { get; } =
            WlySystemInfo.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "main", "systemInfo.xml"));

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取指定矩形块中的数字
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="amountRect"></param>
        /// <param name="color"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool GetAmount(string dmGuid, WxRect amountRect, string color, out int amount)
        {
            var amountStr = DMService.Instance.GetWords(dmGuid, amountRect, color);
            if (amountStr.Contains("/"))
            {
                amountStr = amountStr.Substring(0, amountStr.IndexOf("/", StringComparison.Ordinal));
            }

            return WordToAmount(amountStr, out amount);
        }

        /// <summary>
        /// 获取主城等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static int GetCityLevel(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);
            return int.Parse(DMService.Instance.GetWords(dmGuid, new WxRect(0, 90, 48, 133), WlyColor.Normal));
        }

        /// <summary>
        /// 获取威望数量
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static int GetCreditAmount(string dmGuid)
        {
            // 首先跳转到主界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);
            var rect = new WxRect(276, 73, 323, 90);
            var result = GetAmount(dmGuid, rect, "e9e7cf-000000", out var amount);
            return amount;
        }

        /// <summary>
        /// 获取主公等级
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static int GetMainLevel(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);
            return int.Parse(DMService.Instance.GetWords(dmGuid, new WxRect(20, 69, 115, 103), "66ccff-000000").Substring(3));
        }

        /// <summary>
        /// 获取占比
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool GetPercent(string dmGuid, WxRect rect, string color, out double percent)
        {
            percent = 0;
            var amountStr = DMService.Instance.GetWords(dmGuid, rect, color);
            string currentStr;
            string totalStr;
            if (amountStr.Contains("/"))
            {
                var index = amountStr.IndexOf("/", StringComparison.Ordinal);
                currentStr = amountStr.Substring(0, index);
                totalStr = amountStr.Substring(index + 1, amountStr.Length - index - 1);
            }
            else
            {
                return false;
            }

            var result = WordToAmount(currentStr, out var current);
            if (!result)
            {
                return false;
            }

            result = WordToAmount(totalStr, out var total);
            if (!result)
            {
                return false;
            }

            percent = current / (double)total;
            return true;
        }

        /// <summary>
        /// 获取军令数量
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <returns></returns>
        public static int GetPoint(string dmGuid)
        {
            // 首先跳转到主界面
            WlyViewMgr.GoTo(dmGuid, WlyViewType.场景_主界面);
            var pointRect = new WxRect(119, 106, 163, 123);
            var words = string.Empty;
            var wait = FlowLogicHelper.RepeatRun(() =>
            {
                words = DMService.Instance.GetWords(dmGuid, pointRect, "f3f3da-202020");
                return !string.IsNullOrEmpty(words);
            }, TimeSpan.FromSeconds(20));

            if (!wait)
            {
                throw new InvalidOperationException("无法获取军令信息");
            }

            var result = int.TryParse(words.Split('/')[0], out var point);
            if (!result)
            {
                WxLog.Debug($"WlyUtilityBiz.GetPoint  <{words}>");
                return 0;
            }

            return point;
        }

        /// <summary>
        /// 获取指定区域中文字所属的品质
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static WlyQualityType GetQuality(string dmGuid, WxRect rect)
        {
            foreach (var pair in _qualityMap)
            {
                if (DMService.Instance.FindColor(dmGuid, pair.Value, rect))
                {
                    return pair.Key;
                }
            }

            return WlyQualityType.Unknow;
        }

        /// <summary>
        /// 获取指定品质颜色的字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetQualityStr(WlyQualityType type)
        {
            var list = new List<string>();
            foreach (var v in Enum.GetValues(typeof(WlyQualityType)))
            {
                var q = (WlyQualityType)v;
                if ((type & q) != 0)
                {
                    list.Add(_qualityMap[q]);
                }
            }

            return string.Join("|", list);
        }

        /// <summary>
        /// 获取下一个刷新时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRefreshTime()
        {
            var time = DateTime.Today.AddHours(4);
            if (DateTime.Now < time)
            {
                return time;
            }

            return time.AddDays(1);
        }

        /// <summary>
        /// 征召士兵
        /// </summary>
        /// <param name="dmGuid"></param>
        public static void GetSoldier(string dmGuid)
        {
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_征兵);
            FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(634, 410, 704, 439), "募集", $"{WlyColor.Normal}|{WlyColor.White}"))
                {
                    FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(664, 425), TimeSpan.FromMilliseconds(100)), 10);
                    return false;
                }

                return true;
            }, TimeSpan.FromSeconds(10));

            DMService.Instance.LeftClick(dmGuid, new WxPoint(562, 261));
            DMService.Instance.LeftClick(dmGuid, new WxPoint(666, 292));
        }

        /// <summary>
        /// 获取武将名字品质的颜色
        /// </summary>
        /// <returns></returns>
        public static string GetStaffQualityStr()
        {
            return GetQualityStr(WlyQualityType.White | WlyQualityType.Blue | WlyQualityType.Green | WlyQualityType.Red);
        }

        /// <summary>
        /// 获取指定矩形中时间
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="timeRect"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static TimeSpan GetTime(string dmGuid, WxRect timeRect, string color)
        {
            var timeStr = DMService.Instance.GetWords(dmGuid, timeRect, color);
            var result = TimeSpan.TryParseExact(timeStr, new[] { @"hh\:mm\:ss", @"mm\:ss", @"h\:mm\:ss" }, null, out var time);
            return result ? time : TimeSpan.Zero;
        }

        /// <summary>
        /// 登录到服务器
        /// </summary>
        /// <returns>返回null表示登录失败，需要重新尝试</returns>
        public static void Login(WlyEntity entity)
        {
            // 登录并发保护
            lock (_loginLocker)
            {
                var guid = DMService.Instance.CreateDMSoft();
                var hwndStr = string.Empty;

                // 程序保护
                var fileInfo = new FileInfo(ProgramPath);
                if (fileInfo.Length < 10240)
                {
                    File.Delete(ProgramPath);
                    File.Copy(BaseProgramPath, ProgramPath);
                }

                // 启动程序
                var process = Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = ProgramPath
                });

                try
                {
                    // 获取窗口句柄，进程有时不会立即打开，做一个循环获取，保证成功率
                    Thread.Sleep(400);
                    if (!FlowLogicHelper.RepeatRun(() =>
                    {
                        hwndStr = DMService.Instance.EnumWindowByProcessId(guid, process.Id, "", "Internet Explorer_Server", 2);
                        if (string.IsNullOrEmpty(hwndStr))
                        {
                            Thread.Sleep(300);
                            return false;
                        }

                        return true;
                    }, TimeSpan.FromSeconds(5)))
                    {
                        throw new InvalidOperationException("打开游戏进程失败");
                    }

                    // 绑定窗口，开始登录
                    var loginHwnd = int.Parse(hwndStr);
                    DMService.Instance.BindWindow(guid, loginHwnd);

                    // 确认登录界面完全显示
                    if (!FlowLogicHelper.RepeatRun(() =>
                    {
                        if (!DMService.Instance.FindPic(guid, WlyPicType.进入游戏, GameWndRect))
                        {
                            Thread.Sleep(500);
                            return false;
                        }

                        return true;
                    }, TimeSpan.FromSeconds(5)))
                    {
                        WxLog.Debug($"WlyUtilityBiz.Login Cannot Find Login Wnd <{loginHwnd}>");
                        throw new InvalidOperationException("登录界面不显示");
                    }

                    // 输入账号
                    var accountPoint = new WxPoint(324, 258);
                    DMService.Instance.LeftDoubleClick(guid, accountPoint, 200);
                    DMService.Instance.SendString(guid, loginHwnd, entity.Account);
                    Thread.Sleep(200);

                    // 输入密码
                    var pswPoint = new WxPoint(234, 291);
                    DMService.Instance.LeftDoubleClick(guid, pswPoint, 200);
                    DMService.Instance.SendString(guid, loginHwnd, entity.Password);
                    Thread.Sleep(200);

                    // 点击登录，等待登录成功
                    var loginPoint = new WxPoint(482, 322);
                    if (!FlowLogicHelper.RepeatRun(() =>
                    {
                        if (!DMService.Instance.FindPic(guid, WlyPicType.登录成功, GameWndRect))
                        {
                            if (DMService.Instance.FindPic(guid, WlyPicType.进入游戏, GameWndRect))
                            {
                                DMService.Instance.LeftClick(guid, loginPoint);
                            }

                            Thread.Sleep(500);
                            return false;
                        }

                        return true;
                    }, TimeSpan.FromSeconds(5)))
                    {
                        throw new InvalidOperationException("Need Restart");
                    }

                    DMService.Instance.LeftClick(guid, new WxPoint(230, 159));
                    DMService.Instance.SendString(guid, loginHwnd, "6117");
                    Thread.Sleep(400);
                    DMService.Instance.LeftClick(guid, new WxPoint(365, 158));

                    entity.EntityProcess = process;
                }
                catch
                {
                    if (process != null)
                    {
                        process.Kill();
                        process.Dispose();
                    }

                    throw;
                }
                finally
                {
                    DMService.Instance.ReleaseDMSoft(guid);
                }
            }
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        public static WlyAccountInfo Reg()
        {
            lock (_loginLocker)
            {
                while (true)
                {
                    var guid = DMService.Instance.CreateDMSoft();
                    var hwndStr = string.Empty;

                    // 启动程序
                    var process = Process.Start(new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = ProgramPath
                    });

                    try
                    {
                        // 获取窗口句柄，进程有时不会立即打开，做一个循环获取，保证成功率
                        Thread.Sleep(400);
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            hwndStr = DMService.Instance.EnumWindowByProcessId(guid, process.Id, "", "Internet Explorer_Server", 2);
                            if (string.IsNullOrEmpty(hwndStr))
                            {
                                Thread.Sleep(300);
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(5)))
                        {
                            throw new InvalidOperationException("打开游戏进程失败");
                        }

                        // 绑定窗口，开始登录
                        var hwnd = int.Parse(hwndStr);
                        DMService.Instance.BindWindow(guid, hwnd);

                        // 开始注册
                        var rand = new Random();
                        var account = MathHelper.GetNewGuid().Replace("-", "").Substring(0, rand.Next(6, 9));
                        var psw = MathHelper.GetNewGuid().Replace("-", "").Substring(0, rand.Next(6, 9));

                        FlowLogicHelper.RepeatRun(() => DMService.Instance.FindPic(guid, WlyPicType.新用户注册, new WxRect(5, 192, 113, 228)),
                            TimeSpan.FromSeconds(10));

                        // 点击新用户注册
                        FlowLogicHelper.RepeatRun(() =>
                        {
                            Thread.Sleep(100);
                            var result = DMService.Instance.FindPic(guid, WlyPicType.新用户注册, new WxRect(5, 192, 113, 228));
                            if (result)
                            {
                                DMService.Instance.LeftClick(guid, new WxPoint(51, 207));
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(5));

                        // 输入注册信息
                        DMService.Instance.LeftClick(guid, new WxPoint(323, 257));
                        DMService.Instance.SendString(guid, hwnd, account);

                        DMService.Instance.LeftClick(guid, new WxPoint(300, 290));
                        DMService.Instance.SendString(guid, hwnd, psw);

                        DMService.Instance.LeftClick(guid, new WxPoint(300, 330));
                        DMService.Instance.SendString(guid, hwnd, psw);

                        // 点击进入游戏
                        DMService.Instance.LeftClick(guid, new WxPoint(478, 323), TimeSpan.FromSeconds(2));

                        // 确认账号有效
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            Thread.Sleep(500);
                            return DMService.Instance.FindPic(guid, WlyPicType.登录成功, GameWndRect);
                        }, TimeSpan.FromSeconds(5)))
                        {
                            throw new InvalidOperationException("Need Restart");
                        }

                        // 注销
                        DMService.Instance.LeftClick(guid, new WxPoint(377, 11));

                        // 账号确认
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            if (!DMService.Instance.FindPic(guid, WlyPicType.进入游戏, GameWndRect))
                            {
                                Thread.Sleep(500);
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(5)))
                        {
                            throw new InvalidOperationException("登录界面不显示");
                        }

                        // 输入账号
                        DMService.Instance.LeftDoubleClick(guid, new WxPoint(324, 258), 200);
                        DMService.Instance.SendString(guid, hwnd, account);
                        Thread.Sleep(200);

                        // 输入密码
                        DMService.Instance.LeftDoubleClick(guid, new WxPoint(234, 291), 200);
                        DMService.Instance.SendString(guid, hwnd, psw);
                        Thread.Sleep(200);

                        // 点击登录，等待登录成功
                        if (!FlowLogicHelper.RepeatRun(() =>
                        {
                            if (!DMService.Instance.FindPic(guid, WlyPicType.登录成功, GameWndRect))
                            {
                                if (DMService.Instance.FindPic(guid, WlyPicType.进入游戏, GameWndRect))
                                {
                                    DMService.Instance.LeftClick(guid, new WxPoint(482, 322));
                                }

                                Thread.Sleep(500);
                                return false;
                            }

                            return true;
                        }, TimeSpan.FromSeconds(5)))
                        {
                            throw new InvalidOperationException("Need Restart");
                        }

                        // 验证成功
                        return new WlyAccountInfo
                        {
                            Account = account,
                            Password = psw
                        };
                    }
                    catch (Exception ex)
                    {
                        WxLog.Error($"WlyUtilityBiz.Reg Error <{ex}>");
                    }
                    finally
                    {
                        if (process != null)
                        {
                            process.Kill();
                            process.Dispose();
                        }

                        DMService.Instance.ReleaseDMSoft(guid);
                    }
                }
            }
        }

        /// <summary>
        /// 在武将列表中选择指定的武将
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="staff"></param>
        public static bool SelectStaffInList(string dmGuid, WlyStaffType staff)
        {
            var name = staff.ToString();
            return FlowLogicHelper.RepeatRun(() =>
            {
                if (DMService.Instance.FindStr(dmGuid, new WxRect(193, 205, 304, 462), name, WlyColor.Selected))
                {
                    return true;
                }

                if (DMService.Instance.FindStr(dmGuid, new WxRect(193, 205, 304, 462), name, GetStaffQualityStr(), out var x, out var y))
                {
                    DMService.Instance.LeftClick(dmGuid, new WxPoint(x, y));
                }

                Thread.Sleep(500);
                return false;
            }, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// 领取红包的任务
        /// </summary>
        /// <param name="entity"></param>
        public static void SpecialTask(WlyEntity entity)
        {
            Login(entity);

            var dmGuid = entity.DMGuid;
            WlyViewMgr.GoTo(dmGuid, WlyViewType.功能_红包);

            // 点击红包
            DMService.Instance.LeftClick(dmGuid, new WxPoint(498, 452), TimeSpan.FromSeconds(2));

            FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(434, 203)), 6);
            FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(525, 280)), 6);
            FlowLogicHelper.RepeatRun(() => DMService.Instance.LeftClick(dmGuid, new WxPoint(485, 349)), 6);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 转换万，亿文字为数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static bool WordToAmount(string str, out int amount)
        {
            var b = 1;
            if (str.Contains("万"))
            {
                str = str.Substring(0, str.Length - 1);
                b = 10000;
            }
            else if (str.Contains("亿"))
            {
                str = str.Substring(0, str.Length - 1);
                b = 100000000;
            }

            var result = int.TryParse(str, out amount);
            if (!result)
            {
                amount = 0;
            }

            amount = amount * b;
            return result;
        }

        #endregion
    }
}