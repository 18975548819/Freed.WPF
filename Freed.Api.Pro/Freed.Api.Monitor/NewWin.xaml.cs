using Freed.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Freed.Api.Monitor
{
    /// <summary>
    /// NewWin.xaml 的交互逻辑
    /// </summary>
    public partial class NewWin : Window
    {
        public double TopFrom
        {
            get; set;
        }
        public NewWin()
        {
            InitializeComponent();
            this.Loaded += NotificationWindow_Loaded;
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyData data = this.DataContext as NotifyData;
            if (data != null)
            {
                tbContent.Text = data.Content;
                tbTitle.Text = data.Title;
            }
            NewWin self = sender as NewWin;
            if (self != null)
            {
                double right = SystemParameters.WorkArea.Right - 10;//工作区最右边的值                        
                self.Top = TopFrom - 160;
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                animation.From = right;
                animation.To = right - self.ActualWidth;
                self.BeginAnimation(Window.LeftProperty, animation);

                Task.Factory.StartNew(delegate
                {
                    int seconds = 5;//通知持续5s后消失
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    //Invoke到主进程中去执行
                    this.Dispatcher.Invoke(delegate
                    {
                        animation = new DoubleAnimation();
                        animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                        animation.Completed += (s, a) => { self.Close(); };//动画执行完毕，关闭当前窗体
                        animation.From = right - self.ActualWidth;
                        animation.To = right;//通知从左往右收回
                        self.BeginAnimation(Window.LeftProperty, animation);
                    });
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double right = SystemParameters.WorkArea.Right;
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            animation.Completed += (s, a) => { this.Close(); };
            animation.From = right - this.ActualWidth;
            animation.To = right;
            this.BeginAnimation(Window.LeftProperty, animation);
        }
    }
}
