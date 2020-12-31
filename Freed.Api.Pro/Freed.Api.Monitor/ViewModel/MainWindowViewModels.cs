using Freed.Comman.Common;
using Freed.Controls.Foundation;
using Freed.Model;
using Freedom.Controls.Foundation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Freed.Api.Monitor.ViewModel
{
    public delegate void ChartShowDlg(bool flag);  //定义委托
    public class MainWindowViewModels : ObservableObject
    {
        public event ChartShowDlg ChartShowEvent;  //定义数据更新事件

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
        /// <summary>
        /// Socket消息集合
        /// </summary>
        private ObservableCollection<SocketReceiveDataMsg> _SocketReceiveDataMsgs;

        /// <summary>
        /// 统计每个账套的请求次数
        /// </summary>
        private ObservableCollection<GroupTypeInfoModel> _GroupTypeInfoModels;

        private RelayCommand<object> _LeftMeunClickCommand;
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

        public RelayCommand<object> LeftMeunClickCommand
        {
            get
            {
                if (_LeftMeunClickCommand == null)
                    _LeftMeunClickCommand = new RelayCommand<object>((obj) => LeftMeunCommandFunction(obj));
                return _LeftMeunClickCommand;
            }
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

        /// <summary>
        /// Socket客户端消息集合
        /// </summary>
        public ObservableCollection<SocketReceiveDataMsg> SocketReceiveDataMsgs
        {
            get { 
                if (_SocketReceiveDataMsgs == null)
                {
                    _SocketReceiveDataMsgs = new ObservableCollection<SocketReceiveDataMsg>() ;
                }
                return _SocketReceiveDataMsgs;
            }
            //set { _SocketReceiveDataMsgs = value; RaisePropertyChanged("SocketReceiveDataMsgs"); }
        }

        /// <summary>
        /// 统计每个账套的请求
        /// </summary>
        public ObservableCollection<GroupTypeInfoModel> GroupTypeInfoModels
        {
            get
            {
                if (_GroupTypeInfoModels == null)
                {
                    _GroupTypeInfoModels = new ObservableCollection<GroupTypeInfoModel>();
                }
                return _GroupTypeInfoModels;
            }
            //set { _SocketReceiveDataMsgs = value; RaisePropertyChanged("SocketReceiveDataMsgs"); }
        }
        #endregion


        #region  重载方法
        public override void DoInitFunction(object obj)
        {
            SetFrameTraget("MenuMainPage");
            UpdateSystemDataTime();
            StatsSevice();  //启动Socket服务器Receive
            if (Server != null)
            {
                Server.ShowNewsEvent += ReceiveEvenVoid;
            }
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


        #region Socket操作
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

        /// <summary>
        /// 停止
        /// </summary>
        public void StopService()
        {
            if (Server != null)
            {
                Server.stop();
            }
        }
        #endregion

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
        /// 左侧菜单按钮点击
        /// </summary>
        /// <param name="obj"></param>
        private void LeftMeunCommandFunction(object obj)
        {
            if (obj.IsEmpty())
                return;
            var nflag = obj.ToInt();
            var pagename = "";
            switch (nflag)
            {
                case 0:
                    pagename = "MenuMainPage";
                    break;
                case 1:
                    pagename = "ApiMonitorPage";
                    break;
                case 2:
                    pagename = "SocketDataPage";
                    break;
                case 3:
                    pagename = "DataStatisticsPage";
                    break;
            }
            this.SetFrameTraget(pagename);
        }

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

        /// <summary>
        /// 接收Socket客户端发送的消息
        /// </summary>
        /// <param name="msg"></param>
        private void ReceiveEvenVoid(string msg)
        {
            Action ac = () =>
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (msg.Contains("接口请求"))
                    {
                        string[] msgInfo = msg.Split('接');
                        for (int i = 1; i < msgInfo.Length; i++)
                        {
                            if (msgInfo[i].Length > 0)
                            {
                                try
                                {
                                    string[] msgStr = msgInfo[i].Split('|');
                                    if (msgStr[0].ToString().Contains("开始"))
                                    {
                                        SocketReceiveDataMsg socketReceiveData = new SocketReceiveDataMsg();
                                        socketReceiveData.Msg = msgStr[0];
                                        socketReceiveData.Guids = msgStr[1];
                                        socketReceiveData.RequestTime = msgStr[2];
                                        socketReceiveData.ActionName = msgStr[3];
                                        socketReceiveData.RequestUrl = msgStr[4];
                                        socketReceiveData.GroupType = msgStr[5];
                                        socketReceiveData.WmsRepertory = msgStr[6];
                                        var sock = SocketReceiveDataMsgs.Where(s => s.ActionName == msgStr[3] && s.RequestUrl == msgStr[4]).FirstOrDefault();
                                        if (sock != null)
                                        {
                                            sock.RequestCount += 1;
                                            socketReceiveData.RequestCount = sock.RequestCount;
                                            SocketReceiveDataMsgs.Remove(sock);
                                            SocketReceiveDataMsgs.Add(socketReceiveData);
                                        }
                                        else
                                        {
                                            socketReceiveData.RequestCount = 1;
                                            SocketReceiveDataMsgs.Add(socketReceiveData);
                                        }

                                        //账套请求分组统计
                                        GroupTypeInfoModel groupTypeInfo = new GroupTypeInfoModel();
                                        groupTypeInfo.GroupType = msgStr[5];
                                        groupTypeInfo.WmsRepertory = msgStr[6];
                                        var groupType = GroupTypeInfoModels.Where(g => g.GroupType == msgStr[5] &&  g.WmsRepertory == msgStr[6]).FirstOrDefault();
                                        if (groupType != null)
                                        {
                                            groupType.RequestCount += 1;
                                        }
                                        else
                                        {
                                            groupTypeInfo.RequestCount = 1;
                                            GroupTypeInfoModels.Add(groupTypeInfo);
                                        }
                                        //开启数据更新事件
                                        if (ChartShowEvent != null)
                                        {
                                            ChartShowEvent(true);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    
                                }
                            }
                        }
                    }
                }));
            };
            Thread thread = new Thread(new ThreadStart(ac));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion
    }
}
