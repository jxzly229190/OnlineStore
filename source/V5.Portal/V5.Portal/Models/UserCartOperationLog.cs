using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Models
{
    /// <summary>
    /// 用户购物车操作日志
    /// </summary>
    public class UserCartOperationLog
    {
        public int UserID { get; set; }

        public string VisitorKey { get; set; }

        public string SessionKey { get; set; }

        public string OperationType { get; set; }

        public string Message { get; set; }

        public DateTime OperateTime { get; set; }

        public UserCartModel UserCart { get; set; }
    }
}