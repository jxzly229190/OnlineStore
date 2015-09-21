using System;

namespace V5.DataContract.Utility
{
    public class Code
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string PrefixName { get; set; }
        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// 编码长度
        /// </summary>
        public int TransactLength { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Transaction { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public string CodeFormat { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIterator { get; set; }
        /// <summary>
        /// 自增字段
        /// </summary>
        public int Iterator { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 用户编号自增
        /// </summary>
        public int UserIterator { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireDate { get; set; }
    }
}
