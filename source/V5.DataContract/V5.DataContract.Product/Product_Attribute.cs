// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Attribute.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品属性定义类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Product
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     商品属性定义类
    /// </summary>
    public class Product_Attribute
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        ///     获取或设置属性名称．
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        ///     获取或设置排序编号．
        /// </summary>
        public int? Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 输入类型 
        /// </summary>
        public string InputType { get; set; }
        /// <summary>
        ///     获取或设置显示类型
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        ///     获取或设置数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        ///     获取或设置数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        ///     获取或设置属性编码
        /// </summary>
        public string AttributeCode { get; set; }

        #region 新增属性对应多个属性值

        /// <summary>
        /// 属性对应的属性值集合 2013/11/04
        /// </summary>
        public List<Product_AttributeValue> ProductAttributeValues { get; set; }

        #endregion

        #endregion
    }
}