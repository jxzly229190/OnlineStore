using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Utility
{
    public class SystemVisitorChartJson
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 图表数据
        /// </summary>
        public List<int> value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string line_width { get; set; }

        /// <summary>
        /// 图表下标
        /// </summary>
        public int[] labels { get; set; }

        /// <summary>
        /// 图表容器
        /// </summary>
        public string render { get; set; }

        /// <summary>
        /// 图表标题
        /// </summary>
        public string vptitle { get; set; }

        /// <summary>
        /// 图表单位
        /// </summary>
        public string dateString { get; set; }

        /// <summary>
        /// 纵轴单位
        /// </summary>
        public int start_scale { get; set; }
        public int end_scale { get; set; }
        public int scale_space { get; set; }
        public List<SystemVisitorLineDay> systemVisitorLineDay { get; set; }
        public List<SystemVisitorLineMonth> systemVisitorLineMonth { get; set; }
        public List<SystemVisitorLineSeason> systemVisitorLineSeason { get; set; }
        public List<SystemVisitorLineYear> systemVisitorLineYear { get; set; }
    }
}