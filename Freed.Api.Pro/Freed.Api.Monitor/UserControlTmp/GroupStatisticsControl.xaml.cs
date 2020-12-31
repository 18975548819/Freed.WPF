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
using Visifire.Charts;

namespace Freed.Api.Monitor.UserControlTmp
{
    /// <summary>
    /// GroupStatisticsControl.xaml 的交互逻辑
    /// </summary>
    public partial class GroupStatisticsControl : UserControl
    {
        public GroupStatisticsControl()
        {
            InitializeComponent();
        }


        #region 图表展示

        #region  折线图
        public void CreateChartSpline_Day(string name, List<string> valuex, List<decimal> valuey)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

                //创建一个图标
                Chart chart = new Chart();

                //设置图标的宽度和高度
                chart.Width = 350;
                chart.Height = 250;
                chart.Margin = new Thickness(10, 5, 10, 5);
                //是否启用打印和保持图片
                chart.ToolBarEnabled = false;

                //设置图标的属性
                chart.ScrollingEnabled = false;//是否启用或禁用滚动
                chart.View3D = true;//3D效果显示

                //创建一个标题的对象
                Title title = new Title();

                //设置标题的名称
                title.Text = name;
                title.Padding = new Thickness(0, 10, 5, 0);

                //向图标添加标题
                chart.Titles.Add(title);

                Axis yAxis = new Axis();
                //设置图标中Y轴的最小值永远为0           
                yAxis.AxisMinimum = 0;
                //设置图表中Y轴的后缀          
                yAxis.Suffix = "%";
                chart.AxesY.Add(yAxis);

                // 创建一个新的数据线。               
                DataSeries dataSeries = new DataSeries();

                // 设置数据线的格式
                dataSeries.RenderAs = RenderAs.Spline;//柱状Stacked


                // 设置数据点              
                DataPoint dataPoint;
                for (int i = 0; i < valuex.Count; i++)
                {
                    // 创建一个数据点的实例。                   
                    dataPoint = new DataPoint();
                    // 设置X轴点                    
                    dataPoint.AxisXLabel = valuex[i];
                    //设置Y轴点 
                    dataPoint.YValue = Convert.ToDouble(valuey[i]);
                    //添加一个点击事件        
                    dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                    //添加数据点                   
                    dataSeries.DataPoints.Add(dataPoint);
                }

                // 添加数据线到数据序列。                
                chart.Series.Add(dataSeries);

                //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
                Grid gr = new Grid();
                gr.Children.Add(chart);
                g_pic1.Children.Add(gr);
            }));
        }

        private void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region 饼状图
        public void CreateChartPie(string name, List<string> valuex, List<decimal> valuey, string type)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                //创建一个图标
                Chart chart = new Chart();

                //设置图标的宽度和高度
                //chart.Width = 500;
                //chart.Height = 500;
                //chart.Margin = new Thickness(50, 5, 10, 5);
                //是否启用打印和保持图片
                chart.ToolBarEnabled = false;

                //设置图标的属性
                chart.ScrollingEnabled = false;//是否启用或禁用滚动
                chart.View3D = true;//3D效果显示

                //创建一个标题的对象
                Title title = new Title();
                SolidColorBrush solidColor = new SolidColorBrush(Colors.Red);
                title.FontColor = solidColor;
                title.FontWeight = FontWeights.Bold;
                //设置标题的名称
                title.Text = name;
                title.Padding = new Thickness(0, 10, 5, 0);

                //向图标添加标题
                chart.Titles.Add(title);

                //Axis yAxis = new Axis();
                ////设置图标中Y轴的最小值永远为0           
                //yAxis.AxisMinimum = 0;
                ////设置图表中Y轴的后缀          
                //yAxis.Suffix = "斤";
                //chart.AxesY.Add(yAxis);

                // 创建一个新的数据线。               
                DataSeries dataSeries = new DataSeries();

                // 设置数据线的格式
                dataSeries.RenderAs = RenderAs.Pie;//柱状Stacked


                // 设置数据点              
                DataPoint dataPoint;
                for (int i = 0; i < valuex.Count; i++)
                {
                    // 创建一个数据点的实例。                   
                    dataPoint = new DataPoint();
                    // 设置X轴点                    
                    dataPoint.AxisXLabel = valuex[i];

                    dataPoint.LegendText = "##" + valuex[i];
                    //设置Y轴点     
                    dataPoint.YValue = Convert.ToDouble(valuey[i]);
                    //dataPoint.YValue = double.Parse(valuey[i]);
                    //添加一个点击事件        
                    dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown1);
                    //添加数据点                   
                    dataSeries.DataPoints.Add(dataPoint);
                }

                // 添加数据线到数据序列。                
                chart.Series.Add(dataSeries);

                //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
                Grid gr = new Grid();
                gr.Children.Add(chart);
                if (type == "c")
                {
                    g_pic1.Children.Add(gr);
                }
                else
                {
                    g_pic1.Children.Add(gr);
                }
            }));
        }

        private void dataPoint_MouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
