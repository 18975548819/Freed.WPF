using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Model
{
    /// <summary>
    /// Socket接收信息
    /// </summary>
    public class SocketReceiveDataMsg
    {
        public string Msg { get; set; }
        public string Guids { get; set; }
        public string RequestTime { get; set; }
        public string ActionName { get; set; }
        public string RequestUrl { get; set; }
        public string GroupType { get; set; }
        public string WmsRepertory { get; set; }
        public int RequestCount { get; set; }
    }
}
