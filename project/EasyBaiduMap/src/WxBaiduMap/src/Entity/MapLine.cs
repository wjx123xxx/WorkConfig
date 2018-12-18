// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： MapLine.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-03-21 19:29:10
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

namespace Wx.Common.BaiduMap.Entity
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 地图上一条线
    /// </summary>
    public class MapLine : IEnumerable<MapTile>
    {
        #region Public Properties

        /// <summary>
        /// 结束瓦片X坐标
        /// </summary>
        public long EndX { get; internal set; }

        /// <summary>
        /// 开始瓦片X坐标
        /// </summary>
        public long StartX { get; internal set; }

        /// <summary>
        /// 获取任务数量
        /// </summary>
        public long TaskCount => (EndX - StartX) + 1;

        /// <summary>
        /// 对应Y坐标
        /// </summary>
        public long Y { get; internal set; }

        /// <summary>
        /// Zoom
        /// </summary>
        public int Zoom { get; internal set; }

        #endregion

        #region Public Methods

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>用于循环访问集合的枚举数。</returns>
        public IEnumerator<MapTile> GetEnumerator()
        {
            var list = new List<MapTile>();
            for (var x = StartX; x <= EndX; x++)
            {
                list.Add(new MapTile
                {
                    X = x,
                    Y = Y,
                    Zoom = Zoom
                });
            }

            return list.GetEnumerator();
        }

        #endregion

        #region Private Methods

        /// <summary>返回循环访问集合的枚举数。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}