using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Advertise
{
    public class AdvertiseConfigModel
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
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置来源(1:产品 2:LP 3:其他)．
        /// </summary>
        public string SourceName
        {
            get {
                switch (Source)
                {
                    case "1":
                        return "产品";
                    case "2":
                        return "LP";
                    case "3":
                        return "其他";
                }
                return "";
            }
        }

        /// <summary>
        /// 获取或设置图片
        /// </summary>
        public string OriginalImage
        {
            get
            {
                if (!string.IsNullOrEmpty(ImagePath))
                {
                    return ImagePath.Replace("/Thumbnail/", "/");
                }
                return "";
            }
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailImagePath { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        public int filter { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public bool isParent { get; set; }

        #endregion
    }
}