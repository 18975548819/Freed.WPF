using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Freed.Api.Monitor.ViewModel;

namespace Freed.Api.Monitor.UserControlTmp
{
    /// <summary>
    /// SocketDataGridControl.xaml 的交互逻辑
    /// </summary>
    public partial class SocketDataGridControl : UserControl
    {
        public SocketDataGridControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Scoket_DataGrid.ItemsSource = MainWindowViewModels.Instance.SocketReceiveDataMsgs;
            }));
        }
    }
}
