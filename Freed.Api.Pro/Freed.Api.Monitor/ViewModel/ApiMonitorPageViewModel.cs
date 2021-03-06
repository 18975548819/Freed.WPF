﻿using Freed.Comman.Common;
using Freed.Controls.Foundation;
using Freedom.Controls;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Forms;
using static Freed.Comman.Common.RabbitMQHepler;
using System.Threading;
using Freedom.Controls.Foundation;
using Freed.Model;

namespace Freed.Api.Monitor.ViewModel
{
    public class ApiMonitorPageViewModel: ObservableObject
    {
        private static ApiMonitorPageViewModel _Instance;

        private readonly QueryControl queryControl = new QueryControl();

        private System.Windows.Controls.RichTextBox _richTextBox = null;

        private string newTitle ="消息提示";

        private string newContent = "";

        private Server server = null; //Socket服务

        public int i = 0;
        public static List<NewWin> _dialogs = new List<NewWin>();

        public static ApiMonitorPageViewModel Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ApiMonitorPageViewModel();
                }
                return ApiMonitorPageViewModel._Instance;
            }
        }


        public string NewTitle
        {
            get { return newTitle; }
            set { newTitle = value; RaisePropertyChanged("NewTitle"); }
        }

        public string NewContent
        {
            get { return newContent; }
            set { newContent = value; RaisePropertyChanged("NewContent"); }
        }


        public RelayCommand ClickNewCloseCommand
        {
            get { return new RelayCommand(ClickNewCloseVoid); }
        }


        /// <summary>
        ///界面初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void DoInitFunction(object obj)
        {
            var layOut = (Grid)obj;
            _richTextBox = queryControl.GetChildObject<System.Windows.Controls.RichTextBox>(layOut, "richMsg") as System.Windows.Controls.RichTextBox;
            AppendTextForegroundBrush("WMS接口监控启动！");
            //MainWindowViewModels.Instance.StatsSevice();
            try
            {
                if (MainWindowViewModels.Instance.Server != null)
                {
                    AppendTextForegroundBrush("WMS接口请求监控打开", Colors.Blue, 14);
                    MainWindowViewModels.Instance.Server.ShowNewsEvent += ShowInfo;
                }
                else
                {
                    MainWindowViewModels.Instance.StatsSevice();
                    AppendTextForegroundBrush("WMS接口请求监控打开", Colors.Blue, 14);
                    MainWindowViewModels.Instance.Server.ShowNewsEvent += ShowInfo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MainWindowViewModels.Instance.SetFrameTraget("MenuMainPage");
            }

        }

        private void ShowInfo(string msg)
        {
            //Task.Run(() =>
            //{
            //    AppendTextForegroundBrush(msg, Colors.Green, 14);
            //});

            new Thread(delegate () {
                AppendTextForegroundBrush(msg, Colors.Green, 14);
            }).Start();


        }


        /// <summary>
        /// 启动服务
        /// </summary>
        private void StatsService()
        {
            if (server == null) server = new Server(null, "10.19.87.203", 36889);
            if (!server.started) server.start();
        }

        #region 消息弹窗提示
        private void ClickNewCloseVoid()
        {
            //try
            //{
            //    if (MainWindowViewModels.Instance.Server != null)
            //    {
            //        MainWindowViewModels.Instance.StopService();
            //        MainWindowViewModels.Instance.Server = null;
            //        AppendTextForegroundBrush("WMS接口请求监控关闭", Colors.Red, 14);
            //    }
            //}
            //catch(Exception)
            //{
            //}
            this.NewTitle = "服务关闭提示";
            this.newContent = "WMS接口请求监控服务关闭！";
            NotifyData data = new NotifyData();
            data.Title = this.NewTitle;
            data.Content = this.newContent;
            NewWin dialog = new NewWin();//new 一个通知  
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            dialog.DataContext = data;//设置通知里要显示的数据            
            dialog.Show();
            _dialogs.Add(dialog);
            MainWindowViewModels.Instance.SetFrameTraget("MenuMainPage");
        }

        private double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            while (isContinueFind)
            {
                topFrom = topFrom - 160;//此处100是NotifyWindow的高 160-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }
            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            return topFrom;
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NewWin;
            _dialogs.Remove(closedDialog);
        }
        #endregion

        #region 信息显示操作
        /// <summary>
        /// 将数据写入RichTextBox中，且奇偶行前景为不同颜色
        /// </summary>
        /// <param name="text"></param>
        /// <param name="warning">告警标志，true告警，需红色标记，否则普通信息，使用默认颜色标记</param>
        /// <param name="fontSize">字体大小</param>
        private void AppendTextForegroundBrush(string text, bool warning = false, int fontSize = 12)
        {
            try
            {
                Action changedText = () =>
                {
                    int count = _richTextBox.Document.Blocks.Count;
                    if (count >= 1000)
                        _richTextBox.Document.Blocks.Clear();
                    SolidColorBrush brush = null;
                    if (warning == true)
                    {
                        brush = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        if (count % 2 == 0)
                        {
                            brush = new SolidColorBrush(Colors.Gray);
                        }
                        else
                        {
                            brush = new SolidColorBrush(Colors.Black);
                        }
                    }
                    Paragraph p = new Paragraph();
                    p.Foreground = brush;
                    p.FontSize = fontSize;
                    Run Runtext = new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + text);
                    p.Inlines.Add(Runtext);
                    _richTextBox.Document.Blocks.Add(p);

                    _richTextBox.ScrollToEnd();
                };
                App.Current.Dispatcher.Invoke(changedText);
            }
            catch (Exception ex)
            {
                
            }
        }
        /// <summary>
        /// 将数据写入RichTextBox中，指定行前景设和字体大小
        /// </summary>
        /// <param name="text"></param>
        /// <param name="warning">告警标志，true告警，需红色标记，否则普通信息，使用默认颜色标记</param>
        /// <param name="fontSize">字体大小</param>
        private void AppendTextForegroundBrush(string text, Color fontColor, int fontSize = 12)
        {
            try
            {
                Action changedText = () =>
                {
                    int count = _richTextBox.Document.Blocks.Count;
                    if (count >= 1000)
                        _richTextBox.Document.Blocks.Clear();

                    Paragraph p = new Paragraph();
                    p.Foreground = new SolidColorBrush(fontColor); ;
                    p.FontSize = fontSize;
                    Run Runtext = new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + text);
                    p.Inlines.Add(Runtext);
                    _richTextBox.Document.Blocks.Add(p);

                    _richTextBox.ScrollToEnd();
                };

                App.Current.Dispatcher.Invoke(changedText);
            }
            catch (Exception ex)
            {
                
            }
        }
        /// <summary>
        /// 将数据写入RichTextBox中，指定行前景设和字体大小
        /// </summary>
        /// <param name="text"></param>
        private void AppendTextParityBackground(string text, int fontSize = 15)
        {
            try
            {
                Action changedText = () =>
                {
                    int count = _richTextBox.Document.Blocks.Count;
                    SolidColorBrush brush = null;
                    if (count % 2 == 0)
                    {
                        brush = new SolidColorBrush(Colors.LightGreen);
                    }
                    else
                    {
                        brush = new SolidColorBrush(Colors.White);
                    }
                    Paragraph p = new Paragraph();
                    p.Background = brush;
                    p.FontSize = fontSize;
                    Run Runtext = new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + text);
                    p.Inlines.Add(Runtext);
                    _richTextBox.Document.Blocks.Add(p);

                    _richTextBox.ScrollToEnd();
                };

                App.Current.Dispatcher.Invoke(changedText);
            }
            catch (Exception ex)
            { 
            }
        }
        #endregion

        #region RabbitMQ
        public void ProductionConsumer()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "10.0.40.16";//RabbitMQ服务在本地运行
            factory.UserName = "zeng.tao";//用户名
            factory.Password = "qwer.123456";//密码 
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    {
                        {
                            ///Console.ForegroundColor = ConsoleColor.Green;
                            try
                            {
                                AppendTextForegroundBrush("获取消息队列", Colors.Green, 20);
                                //channel.QueueDeclare("OnlyProducerMessage", false, false, false, null);
                                var consumer = new EventingBasicConsumer(channel);
                                channel.BasicConsume(queue: "OnlyProducerMessage",
                                         autoAck: true,
                                         consumer: consumer);
                                consumer.Received += (model, ea) => //如果有消息需要处理就触发这个事件
                                {
                                    var body = ea.Body;
                                    var message = Encoding.UTF8.GetString(body.ToArray());
                                    new Thread(delegate ()
                                    {
                                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                                        {
                                            AppendTextForegroundBrush(message.ToString(), Colors.Green, 20);
                                        }));
                                    }).Start();
                                    //Console.WriteLine($"消费者01 接受消息: {message}");
                                };
                                //channel.BasicConsume(queue: "OnlyProducerMessage",
                                //                                         autoAck: true,
                                //                                         consumer: consumer);
                                //Console.WriteLine(" Press [enter] to exit.");
                                //Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                    }
                }
            }
        }


        public void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "10.0.40.16";//RabbitMQ服务在本地运行
            factory.UserName = "zeng.tao";//用户名
            factory.Password = "qwer.123456";//密码 

            using (var connection = factory.CreateConnection())  //创建连接
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "OnlyProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null); //创建一个队列

                    channel.ExchangeDeclare(exchange: "OnlyProducerMessageExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

                    channel.QueueBind(queue: "OnlyProducerMessage", exchange: "OnlyProducerMessageExChange", routingKey: string.Empty, arguments: null);

                    for (int i = 0; i < 10; i++)
                    {
                        string message = $"消息{i}";
                        byte[] body = Encoding.UTF8.GetBytes(message);

                        new Thread(delegate ()
                        {
                            App.Current.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                AppendTextForegroundBrush("发送消息队列！");
                            }));
                        }).Start();

                        channel.BasicPublish(exchange: "OnlyProducerMessageExChange",
                                        routingKey: string.Empty,
                                        basicProperties: null,
                                        body: body);
                    }
                }
            }
        }
        #endregion
    }
}
