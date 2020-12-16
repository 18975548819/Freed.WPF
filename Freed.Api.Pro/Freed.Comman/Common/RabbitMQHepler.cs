using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Comman.Common
{
    /// <summary>
    /// RabbitMQ帮助类
    /// </summary>
    public class RabbitMQHepler
    {
        public delegate void RabbitMQDelegate(object sender, RabbitMQGetMsgEventArges e);

        public event RabbitMQDelegate RabbitMQEvent;
        /// <summary>
        /// 消费者  获取队列消息
        /// </summary>
        /// <returns></returns>
        public string ProductionConsumer()
        {
            var message = "";
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
                                var consumer = new EventingBasicConsumer(channel);
                                consumer.Received += (model, ea) => //如果有消息需要处理就触发这个事件
                                {
                                    var body = ea.Body;
                                    message = Encoding.UTF8.GetString(body.ToArray());

                                    RabbitMQGetMsgEventArges rabbitMQGetMsgEvent = new RabbitMQGetMsgEventArges();
                                    rabbitMQGetMsgEvent.MassagerStr = message;
                                    if (this.RabbitMQEvent != null)
                                    {
                                        this.RabbitMQEvent(this,rabbitMQGetMsgEvent);
                                    }
                                    //Console.WriteLine($"消费者01 接受消息: {message}");
                                };
                                channel.BasicConsume(queue: "OnlyProducerMessage",
                                                                         autoAck: true,
                                                                         consumer: consumer);
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
            return message;
        }
    }


    public class RabbitMQGetMsgEventArges : EventArgs
    {
        public string MassagerStr { get; set; }
    }
}
