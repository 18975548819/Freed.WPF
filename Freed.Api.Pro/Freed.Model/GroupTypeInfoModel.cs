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
        public string GroupType { get; set; }
        public string WmsRepertory { get; set; }
        public int RequestCount { get; set; }
    }
}
