using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace V5.DataContract.Advertise
{
    public class Advertise_Config
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
        /// 获取或设置名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置来源(1:产品 2:LP 3:其他)．
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 获取或设置URL地址．
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 获取或设置图片地址．
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 获取或设置描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置是否启用．
        /// </summary>
        public int Enabled { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 获取或设置索引ID
        /// </summary>
        public int IndexID { get; set; }

        /// <summary>
        /// 获取或设置图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 获取或设置图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 获取或设置缩略图地址
        /// </summary>
        public string ThumbnailImagePath { get; set; }

        /// <summary>
        /// 获取或设置图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 获取或设置图片背景颜色
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int IsOrder { get; set; }

        /// <summary>
        /// 过滤字段
        /// </summary>
        public int filter { get; set; }

        /// <summary>
        /// 是否为父级节点
        /// </summary>
        public bool isParent { get; set; }

        #endregion
    }
}
