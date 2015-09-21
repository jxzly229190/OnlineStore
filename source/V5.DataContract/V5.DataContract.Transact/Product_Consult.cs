// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product_Consult.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    /// 商品咨询回复类
    /// </summary>
    [Serializable]
    public class Product_Consult
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置咨询商品．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置咨询商品．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品图片地址
        /// </summary>
        public string ProductPicPath { get; set; }

        /// <summary>
        /// 获取或设置用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置咨询问题．
        /// </summary>
        public string ConsultContent { get; set; }

        /// <summary>
        /// 获取或设置咨询时间
        /// </summary>
        public DateTime ConsultTime { get; set; }

        /// <summary>
        /// 获取或设置咨询编号．
        /// </summary>
        public int ConsultID { get; set; }

        /// <summary>
        /// 获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置员工名称．
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 获取或设置咨询回复内容．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}