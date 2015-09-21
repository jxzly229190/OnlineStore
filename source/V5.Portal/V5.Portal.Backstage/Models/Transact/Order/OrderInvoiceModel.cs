// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderInvoiceModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单发票信息Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    /// <summary>
    ///  订单发票信息Model
    /// </summary>
    public class OrderInvoiceModel
    {
        /// <summary>
        /// 获取或设置发票编码
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置订单编码
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置发票类别编码
        /// </summary>
        public int InvoiceTypeID { get; set; }

        /// <summary>
        /// 获取或设置发票类别名称
        /// </summary>
        public string InvoiceTypeName { get; set; }

        /// <summary>
        /// 获取或设置发票内容编码
        /// </summary>
        public int InvoiceContentID { get; set; }

        /// <summary>
        /// 获取或设置发票内容
        /// </summary>
        public string InvoiceContent { get; set; }

        /// <summary>
        /// 获取或设置发票金额
        /// </summary>
        public double InvoiceCost { get; set; }

        /// <summary>
        /// 获取或设置发票抬头
        /// </summary>
        public string InvoiceTitle { get; set; }
    }
}