// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentInfoModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付信息Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    /// <summary>
    /// 支付信息Model
    /// </summary>
    public class PaymentInfoModel
    {
        /// <summary>
        ///  获取或设置订单编码
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置支付编码
        /// </summary>
        public int PaymentID { get; set; }

        /// <summary>
        /// 获取或设置支付方式编码
        /// </summary>
        public int PaymentMethodID { get; set; }

        /// <summary>
        /// 设置或获取支付方式名称
        /// </summary>
        public string PaymentMethodName 
        {
            get
            {
                if (this.PaymentMethodID == 0)
                {
                    return "在线支付";
                }
                else if (this.PaymentMethodID == 1)
                {
                    return "货到付款";
                }
				else if (this.PaymentMethodID == 2)
				{
					return "货到付款（POS刷卡）";
				}
                else
                {
                    return "未知状态";
                }
            }
        }

        /// <summary>
        /// 获取或设置商品总金额
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 获取或设置优惠金额
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// 获取或设置实际需支付金额
        /// </summary>
        public double ActualMoney { get; set; }

        /// <summary>
        /// 获取或设置实际已支付金额
        /// </summary>
        public double ActualPaid { get; set; }

        /// <summary>
        /// 获取或设置运费
        /// </summary>
        public double DeliveryCost { get; set; }
    }
}