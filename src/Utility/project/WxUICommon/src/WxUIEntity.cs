// *******************************************************************
// * 版权所有： 王璟星
// * 文件名称： WxUIEntity.cs
// * 作　　者： 王璟星
// * 创建日期： 2017-01-24 15:31:21
// * 文件版本： 1.0.0.0
// * 修改时间：             修改人：                修改内容：
// *******************************************************************

using System.ComponentModel;

namespace Wx.Utility.UICommon
{
    /// <summary>
    /// 与界面进行绑定的实体基类
    /// </summary>
    public class WxUIEntity : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// 属性更改通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// 属性更改通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}