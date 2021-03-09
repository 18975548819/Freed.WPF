using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Api.Monitor
{
    public static class AppConfig
    {
        private static AppSettingsSection appSettings;
        private static Configuration config;
        static AppConfig()
        {
            string assemblyConfigFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            config = ConfigurationManager.OpenExeConfiguration(assemblyConfigFile);
            //获取appSettings节点    
            appSettings = (AppSettingsSection)config.GetSection("appSettings");
        }
        /// <summary>
        /// Socket服务器IP
        /// </summary>
        public static string SocketServerIp
        {
            get
            {
                return appSettings.Settings["SocketServerIp"].Value;
            }
            set
            {
                appSettings.Settings.Remove("SocketServerIp");
                appSettings.Settings.Add("SocketServerIp", value);
                config.Save();
            }
        }


        /// <summary>
        /// Socket服务器端口
        /// </summary>
        public static string SocketServerProt
        {
            get
            {
                return appSettings.Settings["SocketServerProt"].Value;
            }
            set
            {
                appSettings.Settings.Remove("SocketServerProt");
                appSettings.Settings.Add("SocketServerProt", value);
                config.Save();
            }
        }

    }
}
