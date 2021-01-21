using System;
using System.Collections.Generic;
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
using Freed.MQTT.Client.Model;
using MQTTnet;
using MQTTnet.Client;

namespace Freed.MQTT.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private IMqttClient _client;

        private List<TopicModel> _allTopics = new List<TopicModel>();

        public List<TopicModel> AllTopics
        {
            get { return _allTopics; }
            set
            {
                if (_allTopics != value)
                {
                    _allTopics = value;
                    OnPropertyChanged("AllTopics");
                }
            }
        }

        private List<TopicFilter> _selectedTopics;
        /// <summary>
        /// 主题集合
        /// </summary>
        public List<TopicFilter> SelectedTopics
        {
            get { return _selectedTopics; }
            set
            {
                if (_selectedTopics != value)
                {
                    _selectedTopics = value;
                    OnPropertyChanged("SelectedTopics");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.AllTopics = InitTopics();
        }

        #region 客户端打开/断开
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connClick(object sender, RoutedEventArgs e)
        {
            string clientId = Guid.NewGuid().ToString("N");
            if (!string.IsNullOrEmpty(txtIp.Text) && !string.IsNullOrEmpty(txtPort.Text))
            {
                InitClient(clientId, txtIp.Text,Convert.ToInt32(txtPort.Text));
            }
            else
            {
                this.AppendTextForegroundBrush("服务端地址或端口号不能为空！", Colors.Red);
                return;
            }
        }

        private  void stopClick(object sender, RoutedEventArgs e)
        {
            if (_client != null)
            {
                _client.DisconnectAsync();
            }
        }
        #endregion

        #region MQTT事件
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="port"></param>
        private void InitClient(string id, string url = "127.0.0.1", int port = 12345)
        {
            var options = new MqttClientOptions()
            {
                ClientId = id
            };
            options.ChannelOptions = new MqttClientTcpOptions()
            {
                Server = url,
                Port = port
            };
            options.Credentials = new MqttClientCredentials()
            {
                Username = txtIp.Text,
                Password = txtPort.Text
            };
            options.CleanSession = true;
            options.KeepAlivePeriod = TimeSpan.FromSeconds(100);
            options.KeepAliveSendInterval = TimeSpan.FromSeconds(10000);
            if (_client != null)
            {
                _client.DisconnectAsync();
                _client = null;
            }
            _client = new MQTTnet.MqttFactory().CreateMqttClient();
            _client.ApplicationMessageReceived += _client_ApplicationMessageReceived;
            _client.Connected += _client_Connected;
            _client.Disconnected += _client_Disconnected;
            _client.ConnectAsync(options);
        }

        private void _client_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _client_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            this.AppendTextForegroundBrush("服务器连接成功！", Colors.Blue);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _client_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            this.AppendTextForegroundBrush("收到来自客户端" + e.ClientId + "，主题为" + e.ApplicationMessage.Topic + "的消息：" + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        }
        #endregion

        #region 数据初始化
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        private List<TopicModel> InitTopics()
        {
            List<TopicModel> topics = new List<TopicModel>();
            topics.Add(new TopicModel("/environ/temp", "环境-温度"));
            topics.Add(new TopicModel("/environ/hum", "环境-湿度"));
            //topics.Add(new TopicModel("/environ/pm25", "环境-PM2.5"));
            //topics.Add(new TopicModel("/environ/CO2", "环境-二氧化碳"));
            //topics.Add(new TopicModel("/energy/electric", "能耗-电"));
            //topics.Add(new TopicModel("/energy/water", "环境-水"));
            //topics.Add(new TopicModel("/energy/gas", "环境-电"));
            topics.Add(new TopicModel("/data/alarm", "数据-报警"));
            topics.Add(new TopicModel("/data/message", "数据-消息"));
            topics.Add(new TopicModel("/data/notify", "数据-通知"));
            return topics;
        }

        /// <summary>
        /// 数据模型转换
        /// </summary>
        /// <param name="topics"></param>
        /// <returns></returns>
        private List<TopicFilter> ConvertTopics(List<TopicModel> topics)
        {
            //MQTTnet.TopicFilter
            List<TopicFilter> filters = new List<TopicFilter>();
            foreach (TopicModel model in topics)
            {
                TopicFilter filter = new TopicFilter(model.Topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
                filters.Add(filter);

            }
            return filters;
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

        /// <summary>
        /// 清除日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuClear_Click(object sender, RoutedEventArgs e)
        {
            richMsg.Document.Blocks.Clear();
        }
        #endregion

        #region 消息发布
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (_client != null)
            {
                if (this.comboTopics.SelectedIndex < 0)
                {
                    this.AppendTextForegroundBrush("请选择要发布的主题！");
                    return;
                }
                if (string.IsNullOrEmpty(txtContent.Text))
                {
                    this.AppendTextForegroundBrush("消息内容不能为空！");
                    return;
                }
                string topic = comboTopics.SelectedValue as string;
                string content = txtContent.Text;
                MqttApplicationMessage msg = new MqttApplicationMessage
                {
                    Topic = topic,
                    Payload = Encoding.UTF8.GetBytes(content),
                    QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = false
                };
                _client.PublishAsync(msg);
                this.AppendTextForegroundBrush("成功发布主题为" + topic + "的消息！");
            }
            else
            {
                this.AppendTextForegroundBrush("请连接服务端后发布消息！");
                return;
            }
        }
        #endregion

        #region 订阅主题
        /// <summary>
        /// 保存订阅主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<TopicModel> topics = this.AllTopics.Where(t => t.IsSelected == true).ToList();
            this.SelectedTopics = ConvertTopics(topics);
            SubscribeTopics(this.SelectedTopics);
        }

        private void SubscribeTopics(List<TopicFilter> filters)
        {
            if (_client != null)
            {
                _client.SubscribeAsync(filters);
                string tmp = "";
                foreach (var filter in filters)
                {
                    tmp += filter.Topic;
                    tmp += ",";
                }
                if (tmp.Length > 1)
                {
                    tmp = tmp.Substring(0, tmp.Length - 1);
                }
                this.AppendTextForegroundBrush("成功订阅主题：" + tmp);
            }
            else
            {
                this.AppendTextForegroundBrush("请连接服务端后订阅主题！");
            }
        }
        #endregion
    }
}
