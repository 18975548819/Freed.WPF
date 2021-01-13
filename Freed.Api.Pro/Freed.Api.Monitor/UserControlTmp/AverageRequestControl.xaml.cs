using Freed.Api.Monitor.ViewModel;
using Freed.Model;
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

namespace Freed.Api.Monitor.UserControlTmp
{
    /// <summary>
    /// AverageRequestControl.xaml 的交互逻辑
    /// </summary>
    public partial class AverageRequestControl : UserControl, INotifyPropertyChanged
    {
        #region 属性触发事件
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
        #endregion

        private decimal _AveregeRequestConut = 0;

        public decimal AveregeRequestConut
        {
            get { return _AveregeRequestConut; }
            set { _AveregeRequestConut = value;this.RaisePropertyChanged("AveregeRequestConut"); }
        }
        public AverageRequestControl()
        {
            InitializeComponent();
        }

        private void ctrAvaregeGroup_Loaded(object sender, RoutedEventArgs e)
        {
            GetAvaregeRequest();
        }


        private void GetAvaregeRequest()
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.ScaleStatisticsModels;
            int totalRequstCount = MainWindowViewModels.Instance.RequestTotaltNum;
            if (groupTypeInfos.Count() > 0)
            {
                AveregeRequestConut = totalRequstCount / groupTypeInfos.Count();
                MainWindowViewModels.Instance.RequestCountEvent += GetAvaregeRequestVoid;
            }
        }



        private void GetAvaregeRequestVoid(bool flag)
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.ScaleStatisticsModels;
            int totalRequstCount = MainWindowViewModels.Instance.RequestTotaltNum;
            if (groupTypeInfos.Count() > 0)
            {
                AveregeRequestConut = totalRequstCount / groupTypeInfos.Count();
            }
        }
    }
}
