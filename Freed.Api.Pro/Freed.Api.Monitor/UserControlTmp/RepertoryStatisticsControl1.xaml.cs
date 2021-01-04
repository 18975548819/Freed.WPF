using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// RepertoryStatisticsControl1.xaml 的交互逻辑
    /// </summary>
    public partial class RepertoryStatisticsControl1 : UserControl
    {
        public RepertoryStatisticsControl1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //MainWindowViewModels.Instance.ChartShowEvent += ChartShowVoid;
            ChartShowStartVoid();
        }

        public void ChartShowStartVoid()
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.GroupTypeInfoModels;

            if (groupTypeInfos.Count > 0)
            {
                RepertoryGrid.Children.Clear();
                List<string> grp = new List<string>();
                foreach (var type in groupTypeInfos)
                {
                    if (!grp.Contains(type.GroupType))
                    {
                        grp.Add(type.GroupType);
                    }
                }

                for (int i = 0; i < grp.Count; i++)
                {
                    List<string> group = new List<string>();
                    List<decimal> values = new List<decimal>();
                    foreach (var item in groupTypeInfos)
                    {
                        if (item.GroupType == grp[i].ToString())
                        {
                            group.Add(item.WmsRepertory);
                            values.Add(Convert.ToDecimal(item.RequestCount));
                        }
                    }
                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(2);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.BorderThickness = new Thickness(1);
                    border.Margin = new Thickness(0, 1, 2, 1);
                    Grid grid = new Grid();
                    border.Child = grid;
                    GroupStatisticsControl groupStatistics = new GroupStatisticsControl();

                    groupStatistics.CreateChartPie(grp[i] + ":请求统计", group, values, "c");

                    grid.Children.Add(groupStatistics);
                    RepertoryGrid.Children.Add(border);
                    MainWindowViewModels.Instance.ChartShowEvent += ChartShowVoid;
                }
            }
        }



        //重新生成图表
        private void ChartShowVoid(bool flag)
        {
            ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.GroupTypeInfoModels;

            if (groupTypeInfos.Count > 0)
            {
                RepertoryGrid.Children.Clear();
                List<string> grp = new List<string>();
                foreach (var type in groupTypeInfos)
                {
                    if (!grp.Contains(type.GroupType))
                    {
                        grp.Add(type.GroupType);
                    }
                }

                for (int i = 0; i < grp.Count; i++)
                {
                    List<string> group = new List<string>();
                    List<decimal> values = new List<decimal>();
                    foreach (var item in groupTypeInfos)
                    {
                        if (item.GroupType == grp[i].ToString())
                        {
                            group.Add(item.WmsRepertory);
                            values.Add(Convert.ToDecimal(item.RequestCount));
                        }
                    }
                    Border border = new Border();
                    border.CornerRadius = new CornerRadius(2);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    //border.Background = new SolidColorBrush(Colors.Transparent);
                    border.BorderThickness = new Thickness(1);
                    border.Margin = new Thickness(0, 1, 2, 1);
                    Grid grid = new Grid();
                    border.Child = grid;
                    //grid.Background = new SolidColorBrush(Colors.Transparent);
                    GroupStatisticsControl groupStatistics = new GroupStatisticsControl();

                    groupStatistics.CreateChartPie(grp[i] + ":请求统计", group, values, "c");

                    grid.Children.Add(groupStatistics);
                    RepertoryGrid.Children.Add(border);
                }
            }
            //App.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    new Thread(delegate ()
            //    {
            //        ObservableCollection<GroupTypeInfoModel> groupTypeInfos = MainWindowViewModels.Instance.GroupTypeInfoModels;

            //        if (groupTypeInfos.Count > 0)
            //        {
            //            RepertoryGrid.Children.Clear();
            //            List<string> grp = new List<string>();
            //            foreach (var type in groupTypeInfos)
            //            {
            //                if (!grp.Contains(type.GroupType))
            //                {
            //                    grp.Add(type.GroupType);
            //                }
            //            }

            //            for (int i = 0; i < grp.Count; i++)
            //            {
            //                List<string> group = new List<string>();
            //                List<decimal> values = new List<decimal>();
            //                foreach (var item in groupTypeInfos)
            //                {
            //                    if (item.GroupType == grp[i].ToString())
            //                    {
            //                        group.Add(item.WmsRepertory);
            //                        values.Add(Convert.ToDecimal(item.RequestCount));
            //                    }
            //                }
            //                Border border = new Border();
            //                border.CornerRadius = new CornerRadius(2);
            //                border.BorderBrush = new SolidColorBrush(Colors.Black);
            //                border.BorderThickness = new Thickness(1);
            //                border.Margin = new Thickness(0, 1, 2, 1);
            //                Grid grid = new Grid();
            //                border.Child = grid;
            //                GroupStatisticsControl groupStatistics = new GroupStatisticsControl();

            //                groupStatistics.CreateChartPie(grp[i] + ":请求统计", group, values, "c");

            //                grid.Children.Add(groupStatistics);
            //                RepertoryGrid.Children.Add(border);
            //            }
            //        }
            //    }).Start();
            //}));
        }
    }
}
