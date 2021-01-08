using Freed.Controls.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// RequestCountShowControl.xaml 的交互逻辑
    /// </summary>
    public partial class RequestCountShowControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        private int _RequestCount = 0;

        public int RequestCount { 
            get { return _RequestCount; }
            set { _RequestCount = value; RaisePropertyChanged("RequestCount"); }
        }

        public RequestCountShowControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModels.Instance.RequestCountEvent += RequestCountVoid;
        }

        private void RequestCountVoid(bool flag)
        {
            this.RequestCount = MainWindowViewModels.Instance.RequestTotaltNum;
        }
    }
}
