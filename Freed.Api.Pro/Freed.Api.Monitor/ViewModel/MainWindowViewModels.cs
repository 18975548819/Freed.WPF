using Freed.Comman.Common;
using Freed.Controls.Foundation;
using Freed.Model;
using Freedom.Controls.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Freed.Api.Monitor.ViewModel
{
    public class MainWindowViewModels : ObservableObject
    {
        /// <summary>
        /// 全局静态属性
        /// </summary>
        public static MainWindowViewModels Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MainWindowViewModels();
                }
                return MainWindowViewModels._Instance;
            }
        }

        #region  私有变量
        /// 全局静态变量
        /// </summary>
        private static MainWindowViewModels _Instance;
        /// <summary>
        /// 当前时间
        /// </summary>
        private string _NowTime = "";
        /// <summary>
        /// 当日是星期几
        /// </summary>
        private string _NowWeek;
        /// <summary>
        /// 当前程序版本号
        /// </summary>
        private string _Versions = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        /// <summary>
        /// 跳转界面路径
        /// </summary>
        private Uri _MainTraget = null;
        /// <summary>
        /// 左侧菜单显示
        /// </summary>
        private Visibility _LeftMenuVisibility = Visibility.Collapsed;
        #endregion


        #region 公共变量
        //Socket服务
        public Server Server = null;
        #endregion


        #region RelayCommand
        public RelayCommand ClickMenuCommand
        {
            get { return new RelayCommand(ClickMenuVoid); }
        }
        #endregion



        #region 属性定义
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public string NowTime
        {
            get { return _NowTime; }
            set { _NowTime = value; RaisePropertyChanged("NowTime"); }
        }

        /// <summary>
        /// 当前星期
        /// </summary>
        public string NowWeek
        {
            get { return _NowWeek; }
            set { _NowWeek = value; RaisePropertyChanged("NowWeek"); }
        }

        public string Versions
        {
            get { return _Versions; }
            set { _Versions = value; RaisePropertyChanged("Versions"); }
        }

        //跳转页面地址
        public Uri MainTraget
        {
            get { return _MainTraget; }
            set
            {
                _MainTraget = value;
                if (value != null)
                {
                    base.RaisePropertyChanged("MainTraget");
                }
            }
        }

        /// <summary>
        /// 左侧菜单显示
        /// </summary>
        public Visibility LeftMenuVisibility
        {
            get { return _LeftMenuVisibility; }
            set { _LeftMenuVisibility = value;RaisePropertyChanged("LeftMenuVisibility"); }
        }
        #endregion


        #region  重载方法
        public override void DoInitFunction(object obj)
        {
            SetFrameTraget("MenuMainPage");
            UpdateSystemDataTime();
        }
        #endregion


        #region 公共方法
        /// <summary>
        /// 界面跳转
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="topTitle"></param>
        /// <param name="topvisable"></param>
        public void SetFrameTraget(string pageName, string topTitle = "", bool topvisable = false)
        {
            LeftMenuVisibility = Visibility.Collapsed;
            if (pageName.Trim() == "")
                return;
            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                this.MainTraget = new Uri(string.Format("/Freed.Api.Monitor;component/View/{0}.xaml", pageName), UriKind.RelativeOrAbsolute);
            }));
        }

        /// <summary>
        /// 启动Socket服务
        /// </summary>
        public void StatsSevice()
        {
            if (Server == null)
            {
                Server = new Server(null, "10.19.87.203", 36889);
            }
            if (!Server.started)
            {
                Server.start();
            }
        }

        public void StopService()
        {
            if (Server != null)
            {
                Server.stop();
            }
        }
        #endregion


        #region 私有方法
        #region  获取日期时间
        /// <summary>
        /// 获取最新系统时间
        /// </summary>
        private void UpdateSystemDataTime()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += GetSystemDataTime;
            timer.Start();
        }

        private void GetSystemDataTime(object sender, EventArgs e)
        {
            NowTime = DateTime.Now.ToString();
            string wk = DateTime.Now.DayOfWeek.ToString();
            if (wk.Equals(IsWeek.Monday.ToString()))
            {
                NowWeek = "星期一";
            }
            if (wk.Equals(IsWeek.Tuesday.ToString()))
            {
                NowWeek = "星期二";
            }
            if (wk.Equals(IsWeek.Wednesday.ToString()))
            {
                NowWeek = "星期三";
            }
            if (wk.Equals(IsWeek.Thursday.ToString()))
            {
                NowWeek = "星期四";
            }
            if (wk.Equals(IsWeek.Friday.ToString()))
            {
                NowWeek = "星期五";
            }
            if (wk.Equals(IsWeek.Saturday.ToString()))
            {
                NowWeek = "星期六";
            }
            if (wk.Equals(IsWeek.Sunday.ToString()))
            {
                NowWeek = "星期日";
            }
        }
        #endregion

        /// <summary>
        /// 显示或者隐藏左侧菜单
        /// </summary>
        private void ClickMenuVoid()
        {
            if (this.LeftMenuVisibility == Visibility.Visible)
            {
                this.LeftMenuVisibility = Visibility.Collapsed;
            }
            else
            {
                this.LeftMenuVisibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
