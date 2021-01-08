using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Freed.Model;

namespace Freed.Api.Monitor.UserControlTmp
{
    /// <summary>
    /// MaxRequestControl.xaml 的交互逻辑
    /// </summary>
    public partial class MaxRequestControl : UserControl, INotifyPropertyChanged
    {
        public MaxRequestControl()
        {
            InitializeComponent();
        }

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

        private string _ActionName = "";
        private int _RequestConut = 0;

        public string ActionName
        {
            get { return _ActionName; }
            set { _ActionName = value; RaisePropertyChanged("ActionName"); }
        }

        public int RequestConut
        {
            get { return _RequestConut; }
            set { _RequestConut = value; RaisePropertyChanged("RequestConut"); }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetMaxRequest();
        }
        private void GetMaxRequest()
        {
            ObservableCollection<SocketReceiveDataMsg> groupTypeInfos = MainWindowViewModels.Instance.SocketReceiveDataMsgs;
            if (groupTypeInfos.Count() > 0)
            {
                SocketReceiveDataMsg groupTypeInfo = groupTypeInfos.OrderByDescending(g => g.RequestCount).FirstOrDefault();
                ActionName = groupTypeInfo.ActionName;
                RequestConut = groupTypeInfo.RequestCount;
                MainWindowViewModels.Instance.RequestCountEvent += GetMaxRequestVoid;
            }
        }



        private void GetMaxRequestVoid(bool flag)
        {
            ObservableCollection<SocketReceiveDataMsg> groupTypeInfos = MainWindowViewModels.Instance.SocketReceiveDataMsgs;
            if (groupTypeInfos.Count() > 0)
            {
                SocketReceiveDataMsg groupTypeInfo = groupTypeInfos.OrderByDescending(g => g.RequestCount).FirstOrDefault();
                ActionName = groupTypeInfo.ActionName;
                RequestConut = groupTypeInfo.RequestCount;
            }
        }
    }
}
