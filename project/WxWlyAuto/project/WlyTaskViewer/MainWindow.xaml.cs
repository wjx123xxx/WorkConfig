using System.Windows;

using Wx.App.WlyTaskViewer.VM;

namespace Wx.App.WlyTaskViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            DataContext = new MainWindowVM();
            InitializeComponent();

            // 加入到画布
            //foreach (var wrapper in m_dict.Values)
            //{
            //    //canvas.Children.Add(wrapper.GetUI());
            //}

            //foreach (var wrapper in m_dict.Values)
            //{
            //    foreach (var line in wrapper.GetLinks())
            //    {
            //        //canvas.Children.Add(line);
            //    }
            //}
        }

        #endregion
    }
}