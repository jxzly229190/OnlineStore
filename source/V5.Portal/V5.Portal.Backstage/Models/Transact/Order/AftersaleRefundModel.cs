// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AftersaleRefundModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   退单Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    /// <summary>
    /// 退单Model
    /// </summary>
    public class AftersaleRefundModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置退款来源编号（1：取消订单，2：退货，3：换货）．
        /// </summary>
        public int RefundSourceID { get; set; }

        /// <summary>
        /// 获取或设置订单编码．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置订单编号．
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 获取或设置退款方式编号（1：退至虚拟账户，2：人工退款至指定帐号）．
        /// </summary>
        public int RefundMethodID { get; set; }

        /// <summary>
        /// 获取或设置订单金额金额．
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 获取或设置已支付金额．
        /// </summary>
        public double PaymentMoney { get; set; }


        /// <summary>
        /// 获取或设置实际退款金额．
        /// </summary>
        public double ActualRefundMoney { get; set; }

        /// <summary>
        /// 获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置退款状态（1：审核中，2：退款中，3：已退款）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public string RefundDescription { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}