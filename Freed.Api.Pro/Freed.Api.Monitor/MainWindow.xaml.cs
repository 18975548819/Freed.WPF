using Freed.Api.Monitor.ViewModel;
using Microsoft.Win32;
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

namespace Freed.Api.Monitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModels.Instance;
            this.IsEnabledChanged += LoginWindow_IsEnabledChanged;
        }

        private void LoginWindow_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                this.Close();
                //System.Environment.Exit(0); // 这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            Point position = e.GetPosition(wb);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < wb.ActualWidth && position.Y >= 0 && position.Y < wb.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void max_Click(object sender, RoutedEventArgs e)
        {
            ChangedWinSize();
        }

        private void HideClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OnNotifyIcon();
            //this.WindowState = WindowState.Minimized;
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
            Application.Current.Shutdown(0);
        }


        #region UI页面size控制
        /// <summary>
        /// 当前窗口状态
        /// </summary>
        public System.Windows.WindowState CurrentWinowState
        { get; set; }

        private void ChangedWinSize()
        {
            if (this.CurrentWinowState == System.Windows.WindowState.Maximized)
            {
                this.CurrentWinowState = System.Windows.WindowState.Normal;
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                System.Drawing.Rectangle rectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                this.Height = 780;
                this.Width = 1000;
                this.Left = (rectangle.Width - this.Width) / 2;
                this.Top = (rectangle.Height - this.Height) / 2;
            }
            else if (this.CurrentWinowState == System.Windows.WindowState.Normal)
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                System.Drawing.Rectangle rectangle = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                this.Height = rectangle.Height;
                this.Width = rectangle.Width;
                this.Left = 0;
                this.Top = 0;
                this.CurrentWinowState = System.Windows.WindowState.Maximized;
            }
        }
        #endregion


        #region 最小托盘
        private System.Windows.Forms.NotifyIcon _NotifyIcon;  //最小托盘类
        private void OnNotifyIcon()
        {
            string icoPath = System.AppDomain.CurrentDomain.BaseDirectory + "Images" + "\\" + "3.ico";
            this.WindowState = System.Windows.WindowState.Minimized;
            if (_NotifyIcon == null)
            {
                System.Windows.Forms.MenuItem meunItem = new System.Windows.Forms.MenuItem();
                meunItem.Tag = "1";
                meunItem.Click += meunItem_Click;
                meunItem.Text = "显示";
                System.Windows.Forms.MenuItem meunItem1 = new System.Windows.Forms.MenuItem();
                meunItem1.Tag = "2";
                meunItem1.Click += meunItem_Click;
                meunItem1.Text = "退出";

                System.Windows.Forms.MenuItem[] items = new System.Windows.Forms.MenuItem[] { meunItem, meunItem1 };
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu(items);

                _NotifyIcon = new System.Windows.Forms.NotifyIcon();
                _NotifyIcon.ContextMenu = menu;
                _NotifyIcon.BalloonTipText = "隐身状态";
                _NotifyIcon.Text = "苏州冲压看板监控";
                _NotifyIcon.Visible = true;
                _NotifyIcon.Icon = new System.Drawing.Icon(icoPath);
                _NotifyIcon.Click += _NotifyIcon_Click;
                _NotifyIcon.ShowBalloonTip(1000);
            }
        }

        private void _NotifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Normal;
        }

        private void meunItem1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //托盘菜单
        private void meunItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MenuItem meunItem = (System.Windows.Forms.MenuItem)sender;
            switch (meunItem.Tag.ToString())
            {
                case "1":
                    _NotifyIcon_Click(sender, e);
                    break;
                case "2":
                    if (System.Windows.MessageBox.Show("确认退出", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                    {
                        if (_NotifyIcon != null)
                            _NotifyIcon.Visible = false;

                        //StopCollectTimerThread();

                        this.Close();
                        System.Environment.Exit(0); // 这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
                    }
                    break;
            }
        }
        #endregion


        #region 注册表
        /// <summary>
        /// 注册表注册系统启动自动运行
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <param name="autoRun">true自动运行，false取消自动运行</param>
        void RegRun(string appName, bool autoRun)
        {
            RegistryKey HKCU = Registry.CurrentUser;
            RegistryKey Run = HKCU.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            bool b = false;
            foreach (string i in Run.GetValueNames())
            {
                if (i == appName)
                {
                    b = true;
                    break;
                }
            }
            try
            {
                if (autoRun == true)
                {
                    //Run.SetValue(appName, AppConfig.StartupPath);
                    Run.SetValue(appName, System.Windows.Forms.Application.ExecutablePath);  //获取当前文件路径
                }
                else
                {
                    Run.DeleteValue(appName);
                }
            }
            catch
            { }
            HKCU.Close();
        }
        bool AutoRunState(string appName)
        {
            RegistryKey HKCU = Registry.CurrentUser;
            RegistryKey Run = HKCU.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            bool autoRun = false;
            foreach (string name in Run.GetValueNames())
            {
                if (name == appName)
                {
                    autoRun = true;
                    break;
                }
            }

            HKCU.Close();
            return autoRun;

        }
        #endregion
    }
}
