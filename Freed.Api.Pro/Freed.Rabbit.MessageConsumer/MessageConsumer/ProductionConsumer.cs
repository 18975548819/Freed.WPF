using Freed.Comman.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Rabbit.MessageConsumer.MessageConsumer
{
    public class ProductionConsumer
    {
        public static Client client;

        public  static void Show()
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
                            Console.ForegroundColor = ConsoleColor.Green;
                            try
                            {
                                var consumer = new EventingBasicConsumer(channel);
                                consumer.Received += (model, ea) => //如果有消息需要处理就触发这个事件
                                {
                                    var body = ea.Body;
                                    var message = Encoding.UTF8.GetString(body.ToArray());
                                    Console.WriteLine($"WMS接口有新的请求: {message}");

                                    //启动Socket通讯
                                    try
                                    {
                                        if (client == null)
                                        {
                                            //client = new Client(null, "10.2.138.129", "36889");
                                            client = new Client(null, AppConfig.SocketServerIp, AppConfig.SocketServerProt);
                                        }
                                        if (!client.connected)
                                        {
                                            client.start();
                                        }
                                        Task.Run(() =>
                                        {
                                            client.Send(message.ToString());
                                        });

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Socket服务器连接失败");
                                    }
                                };
                                channel.BasicConsume(queue: "WMSProducerMessage",
                                             autoAck: true,
                                             consumer: consumer);
                                Console.WriteLine(" Press [enter] to exit.");
                                Console.ReadLine();
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

        private static void clientConnEvent(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(msg);
            Console.ReadLine();
        }
    } 
}
