using Freed.Api.Monitor.ViewModel;
using Freed.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ScaleStatisticsControl.xaml 的交互逻辑
    /// </summary>
    public partial class ScaleStatisticsControl : UserControl
    {
        /// <summary>
        /// 每小时接口请求次数统计
        /// </summary>
        public ScaleStatisticsControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ChartShowStartVoid();
        }

        public void ChartShowStartVoid()
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.ScaleStatisticsModels;

            if (groupTypeInfos.Count > 0)
            {
                RepertoryGrid.Children.Clear();
                List<string> group = new List<string>();
                List<decimal> values = new List<decimal>();
                foreach (var item in groupTypeInfos)
                {
                    group.Add(item.Scale);
                    values.Add(Convert.ToDecimal(item.RequestCount));
                }
                Border border = new Border();
                border.CornerRadius = new CornerRadius(2);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(0, 1, 2, 1);
                Grid grid = new Grid();
                border.Child = grid;
                GroupStatisticsControl groupStatistics = new GroupStatisticsControl();

                groupStatistics.CreateChartSpline_Day("WMS接口每小时请求统计", group, values);

                grid.Children.Add(groupStatistics);
                RepertoryGrid.Children.Add(border);
                MainWindowViewModels.Instance.ChartShowEvent += ChartShowVoid;
            }
        }



        //重新生成图表
        private void ChartShowVoid(bool flag)
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.ScaleStatisticsModels;

            if (groupTypeInfos.Count > 0)
            {
                RepertoryGrid.Children.Clear();
                List<string> group = new List<string>();
                List<decimal> values = new List<decimal>();
                foreach (var item in groupTypeInfos)
                {
                    group.Add(item.Scale);
                    values.Add(Convert.ToDecimal(item.RequestCount));
                }
                Border border = new Border();
                border.CornerRadius = new CornerRadius(2);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(0, 1, 2, 1);
                Grid grid = new Grid();
                border.Child = grid;
                GroupStatisticsControl groupStatistics = new GroupStatisticsControl();

                groupStatistics.CreateChartSpline_Day("WMS接口每小时请求统计", group, values);

                grid.Children.Add(groupStatistics);
                RepertoryGrid.Children.Add(border);
            }
        }
    }
}
