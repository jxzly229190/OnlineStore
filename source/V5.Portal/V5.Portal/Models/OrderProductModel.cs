// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderProductModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单商品Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Models
{
    using System;

    /// <summary>
    /// 订单商品Model类
    /// </summary>
    public class OrderProductModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     获取或设置订单编号．
        /// </summary>
        public string Consignee { get; set; }


        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置商品条形码．
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置商品名称．
        /// </summary>
        public string ProductURL { get; set; }


        /// <summary>
        ///     获取或设置商品库存．
        /// </summary>
        public int InventoryNumber { get; set; }

        /// <summary>
        ///     获取或设置商品主图路径．
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///     获取或设置数量．
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     获取或设置市场价
        /// </summary>
        public double MarketPrice { get; set; }

        /// <summary>
        ///     获取或设置购酒价
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        ///     获取或设置成交价．
        /// </summary>
        public double TransactPrice { get; set; }

        /// <summary>
        ///     获取或设置商品金额
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return this.TransactPrice * this.Quantity;
            } 
        }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime TransactTime { get; set; }

        #endregion 
    }
}