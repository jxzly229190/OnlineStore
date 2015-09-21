// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetailModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单详情Model 类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System.Collections.Generic;
    using V5.Portal.Backstage.Models.User;

    /// <summary>
    /// 订单详情Model 类
    /// </summary>
    public class OrderDetailModel
    {
        /// <summary>
        /// 获取或设置订单信息
        /// </summary>
        public OrderModel OrderInfo { get; set; }

        /// <summary>
        /// 获取或设置当前订单状态
        /// </summary>
        public OrderState CurrentOrderState { get; set; }

        /// <summary>
        /// 获取或设置订单商品信息
        /// </summary>
        public List<OrderProductModel> OrderProducts { get; set; }

        /// <summary>
        /// 获取或设置退货订单商品信息
        /// </summary>
        public List<OrderProductModel> OrderReturnProducts { get; set; }

        /// <summary>
        /// 获取或设置订单物流流转明细
        /// </summary>
        public List<OrderTrackDetailModel> OrderTrackDetails { get; set; }

        /// <summary>
        /// 获取或设置用户信息
        /// </summary>
        public UserModel UserInfo { get; set; }

        /// <summary>
        /// 获取或设置支付明细
        /// </summary>
        public PaymentInfoModel PaymentInfo { get; set; }

        /// <summary>
        /// 获取或设置收货人信息
        /// </summary>
        public UserReceiveAddressModel ReceiverInfo { get; set; }

        /// <summary>
        /// 获取或设置发票信息
        /// </summary>
        public OrderInvoiceModel InvoiceInfo { get; set; }
    }
}