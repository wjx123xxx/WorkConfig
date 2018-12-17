namespace Wx.App.EasyBaiduMap.Wnd
{
    using System.Windows;

    using Wx.App.EasyBaiduMap.Common;

    /// <summary>
    /// SetRegionWnd.xaml 的交互逻辑
    /// </summary>
    public partial class SetRegionWnd : Window
    {
        #region Constructors

        public SetRegionWnd()
        {
            InitializeComponent();

            var okCommand = CommandFactory.CreateCommand(OkExecute);
            OKBtn.Command = okCommand;
        }

        #endregion

        #region Private Methods

        private void OkExecute(object obj)
        {
            DialogResult = true;
        }

        #endregion
    }
}