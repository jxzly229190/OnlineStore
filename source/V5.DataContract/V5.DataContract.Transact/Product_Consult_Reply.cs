// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Consult_Reply.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询回复类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    ///     商品咨询回复类
    /// </summary>
    public class Product_Consult_Reply
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置商品咨询编号．
        /// </summary>
        public int ConsultID { get; set; }

        /// <summary>
        ///     获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置咨询回复内容．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}