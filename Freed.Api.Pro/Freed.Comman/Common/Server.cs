namespace Freed.Comman.Common
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public delegate void SocketSeviceDlg(string msg);  //定义委托
    public class Server
    {
        public event SocketSeviceDlg ShowNewsEvent;  //定义消息接收事件

        public string ipString = "10.19.87.203";   // 服务器端ip
        public int port = 36888;                // 服务器端口
        
        public Socket socket;
        public Print print;                     // 运行时的信息输出方法

        public Dictionary<string, Socket> clients = new Dictionary<string, Socket>();   // 存储连接到服务器的客户端信息
        public bool started = false;            // 标识当前是否启动了服务

        public Server(Print print = null, string ipString = null, int port = -1)
        {
            this.print = print;
            if (ipString != null) this.ipString = ipString;
            if (port >= 0)this.port = port;
        }

        public Server(Print print = null, string ipString = null, string port = "-1")
        {
            this.print = print;
            if (ipString != null) this.ipString = ipString;

            int port_int = Int32.Parse(port);
            if (port_int >= 0) this.port = port_int;
        }

        /// <summary>
        /// Print用于输出Server的输出信息
        /// </summary>
        public delegate void Print(string info);

        /// <summary>
        /// 启动服务
        /// </summary>
        public void start()
        {
            try
            {
                IPAddress address = IPAddress.Parse(ipString);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(address, port));   
                socket.Listen(10000);

                if (print != null)
                {
                    try { print("启动服务【" + socket.LocalEndPoint.ToString() + "】成功"); }
                    catch { print = null; }
                }
                started = true;

                new Thread(listenClientConnect).Start(socket);  // 在新的线程中监听客户端连接
            }
            catch (Exception exception)
            {
                if (print != null)
                {
                    print("启动服务失败 " + exception.ToString());
                }
                started = false;
            }
        }

        /// <summary>
        /// 监听客户端的连接
        /// </summary>
        private void listenClientConnect(object obj)
        {
            Socket socket2 = (Socket) obj;
            while (true)
            {
                try
                {
                    Socket clientScoket = socket2.Accept();
                    if (print != null)
                    {
                        print("客户端" + clientScoket.RemoteEndPoint.ToString() + "已连接");
                    }
                    if (ShowNewsEvent != null)
                    {
                        ShowNewsEvent("客户端" + clientScoket.RemoteEndPoint.ToString() + "已连接");
                    }
                    new Thread(receiveData).Start(clientScoket);   // 在新的线程中接收客户端信息

                    Thread.Sleep(1000);                            // 延时1秒后，接收连接请求
                    if (!started) return;
                }
                catch (Exception)
                {
                    socket2.Close();
                    socket.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        public void Send(string info, string id)
        {
            if (clients.ContainsKey(id))
            {
                Socket socket = clients[id];

                try 
                { 
                    Send(socket, info); 
                }
                catch(Exception ex)
                {
                    clients.Remove(id);
                    if (print != null) print("客户端已断开，【" + id + "】");
                }
            }
        }

        /// <summary>
        /// 通过socket发送数据data
        /// </summary>
        private void Send(Socket socket, string data)
        {
            if (socket != null && data != null && !data.Equals(""))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);   // 将data转化为byte数组
                socket.Send(bytes);                            // 
            }
        }

        private string clientIp = "";
        /// <summary>
        /// 输出Server的输出信息到客户端
        /// </summary>
        public void PrintOnClient(string info)
        {
            Send(info, clientIp);
        }

        /// <summary>
        /// 接收通过socket发送过来的数据
        /// </summary>
        private void receiveData(object obj)
        {
            Socket socket3 = (Socket) obj;

            string clientIp = socket3.RemoteEndPoint.ToString();                 // 获取客户端标识 ip和端口
            if (!clients.ContainsKey(clientIp)) clients.Add(clientIp, socket3);  // 将连接的客户端socket添加到clients中保存
            else clients[clientIp] = socket3;

            while (true)
            {
                try
                {
                    string str = Receive(socket3);
                    if (!str.Equals(""))
                    {
                        if (ShowNewsEvent != null)
                        {
                            ShowNewsEvent(str);
                        }

                        if (str.Equals("[.Echo]"))
                        {
                            this.clientIp = clientIp;
                            print = new Print(PrintOnClient);     // 在客户端显示服务器输出信息
                        }
                        if (print != null) print("【" + clientIp + "】" + str);

                        if (str.Equals("[.Shutdown]")) Environment.Exit(0); // 服务器退出
                        else if (str.StartsWith("[.RunCmd]")) runCMD(str);  // 执行cmd命令
                    }
                }
                catch (Exception exception)
                {
                    if (print != null) print("连接已自动断开，【" + clientIp + "】" + exception.Message);

                    socket3.Shutdown(SocketShutdown.Both);
                    socket3.Close();

                    if (clients.ContainsKey(clientIp)) clients.Remove(clientIp);
                    return;
                }

                if (!started) return;
                Thread.Sleep(200);      // 延时0.2秒后再接收客户端发送的消息
            }
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        private void runCMD(string cmd)
        {
            new Thread(runCMD_0).Start(cmd);
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        private void runCMD_0(object obj)
        {
            string cmd = (string)obj;
            string START = "[.RunCmd]";
            if (cmd.StartsWith(START))
            {
                cmd = cmd.Substring(START.Length);  // 获取cmd信息
                Cmd.Run(cmd, print);                // 执行cmd，输出执行结果到print
            }
        }

        /// <summary>
        /// 从socket接收数据
        /// </summary>
        private string Receive(Socket socketRe)
        {
            string data = "";

            byte[] bytes = null;
            int len = socketRe.Available;
            if (len > 0)
            {
                bytes = new byte[len];
                int receiveNumber = socketRe.Receive(bytes);
                data = Encoding.UTF8.GetString(bytes, 0, receiveNumber);
            }

            return data;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void stop()
        {
            started = false;
            socket.Close();
            GC.Collect();
        }
    }
}
