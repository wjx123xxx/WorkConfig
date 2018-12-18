// *******************************************************************
// * 文件名称： MouseService.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-03-12 14:44:50
// *******************************************************************

using System;
using System.Threading;

using Wx.Utility.WxCommon.Entity;

namespace Wx.Utility.WxForDM.Service
{
    /// <summary>
    /// 鼠标服务
    /// </summary>
    public partial class DMService
    {
        #region Public Methods

        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="sleep"></param>
        public void LeftClick(string dmGuid, WxPoint point, TimeSpan sleep)
        {
            var dm = GetDM(dmGuid);
            dm.MoveTo(point.X, point.Y);
            dm.LeftClick();
            Thread.Sleep(sleep);
        }

        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        public void LeftClick(string dmGuid, WxPoint point)
        {
            LeftClick(dmGuid, point, TimeSpan.FromMilliseconds(400));
        }

        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="sleep"></param>
        public void LeftDoubleClick(string dmGuid, WxPoint point, TimeSpan sleep)
        {
            var dm = GetDM(dmGuid);
            dm.MoveTo(point.X, point.Y);
            dm.LeftDoubleClick();
            Thread.Sleep(sleep);
        }

        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        public void LeftDoubleClick(string dmGuid, WxPoint point)
        {
            LeftDoubleClick(dmGuid, point, TimeSpan.Zero);
        }

        /// <summary>
        /// 左键双击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="sleep">单位：毫秒</param>
        public void LeftDoubleClick(string dmGuid, WxPoint point, int sleep)
        {
            LeftDoubleClick(dmGuid, point, TimeSpan.FromMilliseconds(sleep));
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        public void LeftDown(string dmGuid, WxPoint point)
        {
            var dm = GetDM(dmGuid);
            dm.MoveTo(point.X, point.Y);
            dm.LeftDown();
            Thread.Sleep(500);
        }

        /// <summary>
        /// 左键弹起
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        public void LeftUp(string dmGuid, WxPoint point)
        {
            var dm = GetDM(dmGuid);
            dm.MoveTo(point.X, point.Y);
            dm.LeftUp();
            Thread.Sleep(500);
        }

        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="sleep"></param>
        public void MoveTo(string dmGuid, WxPoint point, int sleep)
        {
            var dm = GetDM(dmGuid);
            dm.MoveTo(point.X, point.Y);
            Thread.Sleep(sleep);
        }

        /// <summary>
        /// 重复点击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="times"></param>
        public void RepeatLeftClick(string dmGuid, WxPoint point, int times)
        {
            for (int i = 0; i < times; i++)
            {
                LeftClick(dmGuid, point);
            }
        }

        /// <summary>
        /// 重复点击
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="point"></param>
        /// <param name="times"></param>
        public void RepeatLeftClick(string dmGuid, WxPoint point, int times, int sleep)
        {
            for (int i = 0; i < times; i++)
            {
                LeftClick(dmGuid, point, TimeSpan.FromMilliseconds(sleep));
            }
        }

        /// <summary>
        /// 滚轮
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="sleep"></param>
        public void WheelDown(string dmGuid, int sleep)
        {
            var dm = GetDM(dmGuid);
            dm.WheelDown();
            Thread.Sleep(sleep);
        }

        /// <summary>
        /// 滚轮
        /// </summary>
        /// <param name="dmGuid"></param>
        /// <param name="sleep"></param>
        public void WheelUp(string dmGuid, int sleep)
        {
            var dm = GetDM(dmGuid);
            dm.WheelUp();
            Thread.Sleep(sleep);
        }

        #endregion
    }
}