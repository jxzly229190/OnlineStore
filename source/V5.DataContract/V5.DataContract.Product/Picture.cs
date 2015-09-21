// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Picture.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   图片类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;

    /// <summary>
    ///     图片类
    /// </summary>
    public class Picture
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        public int ParentCategoryID { get; set; }

        public int? ProductCategoryID { get; set; }

        public int? ParentBrandID { get; set; }

        public int? ProductBrandID { get; set; }

        /// <summary>
        ///     获取或设置图片显示路径名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置图片名称．
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     获取或设置图片类型．
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     获取或设置图片路径．
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置缩略图路径
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        ///     获取或设置图片大小（单位：KB）．
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     获取或设置图片高度．
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     获取或设置图片宽度．
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     获取或设置图片状态（0：未引用，1：已引用）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置上传时间．
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }


        #region 给实体添加的属性

        public int ProductID { get; set; }

        #endregion

        #endregion
    }
}