using Freed.MQTT.Server.Model;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
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

namespace Freed.MQTT.Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private IMqttServer server;  //MQTT服务

        public MainWindow()
        {
            InitializeComponent();
        }


        private void mqttServerWin_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private ConnectModel _connectModel = new ConnectModel();
        public ConnectModel ConnectModel
        {
            get { return _connectModel; }
            set { _connectModel = value; }
        }

        private ObservableCollection<string> allClients = new ObservableCollection<string>();

        /// <summary>
        /// 客户端连接
        /// </summary>
        public ObservableCollection<string> AllClients
        {
            get { return allClients; }
            set
            {
                if (allClients != value)
                {
                    allClients = value;
                    this.OnPropertyChanged("AllClients");
                }

            }
        }


        private ObservableCollection<TopicModel> allTopics = new ObservableCollection<TopicModel>();

        /// <summary>
        /// 订阅主题集合
        /// </summary>
        public ObservableCollection<TopicModel> AllTopics
        {
            get { return allTopics; }
            set
            {
                if (allTopics != value)
                {
                    allTopics = value;
                    this.OnPropertyChanged("AllTopics");
                }

            }
        }


        private string addTopic;
        /// <summary>
        /// 添加的主题
        /// </summary>
        public string AddTopic
        {
            get { return addTopic; }
            set
            {
                if (addTopic != value)
                {
                    addTopic = value;
                    this.OnPropertyChanged("AddTopic");
                }

            }
        }

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
                    int count = richMsg.Document.Blocks.Count;
                    if (count >= 1000)
                        richMsg.Document.Blocks.Clear();
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
                    richMsg.Document.Blocks.Add(p);

                    richMsg.ScrollToEnd();
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
                    int count = richMsg.Document.Blocks.Count;
                    if (count >= 1000)
                        richMsg.Document.Blocks.Clear();

                    Paragraph p = new Paragraph();
                    p.Foreground = new SolidColorBrush(fontColor); ;
                    p.FontSize = fontSize;
                    Run Runtext = new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + text);
                    p.Inlines.Add(Runtext);
                    richMsg.Document.Blocks.Add(p);

                    richMsg.ScrollToEnd();
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
                    int count = richMsg.Document.Blocks.Count;
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
                    richMsg.Document.Blocks.Add(p);

                    richMsg.ScrollToEnd();
                };

                App.Current.Dispatcher.Invoke(changedText);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 开始/停止服务
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartServserClick(object sender, RoutedEventArgs e)
        {
            var optionBuilder = new MqttServerOptionsBuilder().WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Parse(this.ConnectModel.HostIP)).WithDefaultEndpointPort(this.ConnectModel.HostPort).WithDefaultCommunicationTimeout(TimeSpan.FromMilliseconds(this.ConnectModel.Timeout)).WithConnectionValidator(t =>
            {
                if (t.Username != this.ConnectModel.UserName || t.Password != this.ConnectModel.Password)
                {
                    t.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                }
                t.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
            });
            var option = optionBuilder.Build();

            server = new MqttFactory().CreateMqttServer();
            server.ApplicationMessageReceived += Server_ApplicationMessageReceived;//绑定消息接收事件
            server.ClientConnected += Server_ClientConnected;//绑定客户端连接事件
            server.ClientDisconnected += Server_ClientDisconnected;//绑定客户端断开事件
            server.ClientSubscribedTopic += Server_ClientSubscribedTopic;//绑定客户端订阅主题事件
            server.ClientUnsubscribedTopic += Server_ClientUnsubscribedTopic;//绑定客户端退订主题事件
            server.Started += Server_Started;//绑定服务端启动事件
            server.Stopped += Server_Stopped;//绑定服务端停止事件
            //启动
            await server.StartAsync(option);
        }

        /// <summary>
        /// 停止MQTT服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StopServerClick(object sender, RoutedEventArgs e)
        {
            if (server != null)
            {
                await server.StopAsync();
            }
        }

        #endregion

        #region IMqttServer事件
        /// <summary>
        /// 服务停止事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_Stopped(object sender, EventArgs e)
        {
            this.AppendTextForegroundBrush("服务端已停止！");
        }

        /// <summary>
        /// 服务启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_Started(object sender, EventArgs e)
        {
            this.AppendTextForegroundBrush("服务启动成功！", Colors.Green);
        }

        /// <summary>
        /// 退订阅主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ClientUnsubscribedTopic(object sender, MqttClientUnsubscribedTopicEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (this.AllTopics.Any(t => t.Topic == e.TopicFilter))
                {
                    TopicModel model = this.AllTopics.First(t => t.Topic == e.TopicFilter);
                    this.AllTopics.Remove(model);
                    model.Clients.Remove(e.ClientId);
                    model.Count--;
                    if (model.Count > 0)
                    {
                        this.AllTopics.Add(model);
                    }
                }
            });
            this.AppendTextForegroundBrush("客户端" + e.ClientId + "退订主题" + e.TopicFilter);
        }

        /// <summary>
        /// 客户端订阅主题事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ClientSubscribedTopic(object sender, MqttClientSubscribedTopicEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (this.AllTopics.Any(t => t.Topic == e.TopicFilter.Topic))
                {
                    TopicModel model = this.AllTopics.First(t => t.Topic == e.TopicFilter.Topic);
                    this.AllTopics.Remove(model);
                    model.Clients.Add(e.ClientId);
                    model.Count++;
                    this.AllTopics.Add(model);
                }
                else
                {
                    TopicModel model = new TopicModel(e.TopicFilter.Topic, e.TopicFilter.QualityOfServiceLevel)
                    {
                        Clients = new List<string> { e.ClientId },
                        Count = 1
                    };
                    this.AllTopics.Add(model);
                }
            });

            this.AppendTextForegroundBrush("客户端" + e.ClientId + "订阅主题" + e.TopicFilter.Topic);
        }

        /// <summary>
        /// 客户端端口连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.AllClients.Remove(e.ClientId);
                var query = this.AllTopics.Where(t => t.Clients.Contains(e.ClientId));
                if (query.Any())
                {
                    var tmp = query.ToList();
                    foreach (var model in tmp)
                    {
                        this.AllTopics.Remove(model);
                        model.Clients.Remove(e.ClientId);
                        model.Count--;
                        this.AllTopics.Add(model);
                    }
                }
            });
            this.AppendTextForegroundBrush("客户端：" + e.ClientId + "断开连接");
        }

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.AllClients.Add(e.ClientId);
            });
            this.AppendTextForegroundBrush("客户端："+e.ClientId+"连接",Colors.Blue);
        }

        /// <summary>
        /// 接收消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            if (e.ApplicationMessage.Topic == "/environ/temp")
            {
                string str = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                double tmp;
                bool isdouble = double.TryParse(str, out tmp);
                if (isdouble)
                {
                    string result = "";
                    if (tmp > 40)
                    {
                        result = "温度过高！";
                    }
                    else if (tmp < 10)
                    {
                        result = "温度过低！";
                    }
                    else
                    {
                        result = "温度正常！";
                    }
                    MqttApplicationMessage message = new MqttApplicationMessage()
                    {
                        Topic = e.ApplicationMessage.Topic,
                        Payload = Encoding.UTF8.GetBytes(result),
                        QualityOfServiceLevel = e.ApplicationMessage.QualityOfServiceLevel,
                        Retain = e.ApplicationMessage.Retain
                    };
                    server.PublishAsync(message);
                }
            }
            this.AppendTextForegroundBrush("收到消息" + e.ApplicationMessage.ConvertPayloadToString() + ",来自客户端" + e.ClientId + ",主题为" + e.ApplicationMessage.Topic);
        }
        #endregion


        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTopic_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.AddTopic) && server != null)
            {
                TopicModel topic = new TopicModel(this.AddTopic, MqttQualityOfServiceLevel.AtLeastOnce);
                foreach (string clientId in this.AllClients)
                {
                    server.SubscribeAsync(clientId, new List<TopicFilter> { topic });
                }
                this.AllTopics.Add(topic);
            }
        }

        /// <summary>
        /// 清除日志记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuClear_Click(object sender, RoutedEventArgs e)
        {
            richMsg.Document.Blocks.Clear();
        }
    }
}
