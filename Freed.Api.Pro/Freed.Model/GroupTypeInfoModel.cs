using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Model
{
    /// <summary>
    /// 分组统计数据
    /// </summary>
    public class GroupTypeInfoModel
    {
        /// <summary>
        /// 刻度标识
        /// </summary>
        public string Scale { get; set; }
        public string GroupType { get; set; }
        public string WmsRepertory { get; set; }
        public int RequestCount { get; set; }
    }
}
