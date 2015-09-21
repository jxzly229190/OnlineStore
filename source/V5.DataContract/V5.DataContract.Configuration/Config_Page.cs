using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V5.DataContract.Configuration
{

    /// <summary>
    /// The Config_Page class.
    /// </summary>
    public class Config_Page
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置父级编号．
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 获取或设置内容标题．
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 获取或设置内容标题．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置内容正文．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        public string Source { get; set; }

        #endregion
    }

}
