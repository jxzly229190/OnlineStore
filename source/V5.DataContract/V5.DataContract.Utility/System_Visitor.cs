using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V5.DataContract.Utility
{
    /// <summary>
    /// 系统用户访问
    /// </summary>
    public class System_Visitor
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 部分日期
        /// </summary>
        public int DepartDate { get; set; }
        /// <summary>
        /// 访问数量
        /// </summary>
        public int VisitorCount { get; set; }
        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID { get; set; }
        /// <summary>
        /// 开始进入系统时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 退出系统时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IP4地址
        /// </summary>
        public string IP4Address { get; set; }
        /// <summary>
        /// IP6地址
        /// </summary>
        public string IP6Address { get; set; }

    }
}
