// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigPaymentTypeModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付类别Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 支付类别 Model
    /// </summary>
    public class ConfigPaymentTypeModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        private int paymentMethodID;
        /// <summary>
        /// 获取或设置支付方式编号:0.在线支付，1.货到付款
        /// </summary>
        public int PaymentMethodID {
            get
            {
                return this.paymentMethodID;
            }
            set
            {
                if (value == 0) this.PaymentMethodName = "在线支付";
                else if(value==1)
                {
                    this.PaymentMethodName = "货到付款";
                }
				else if (value == 2)
				{
					this.PaymentMethodName = "货到付款（Pos刷卡）";
				}
                this.paymentMethodID = value;
            }
        }

        /// <summary>
        /// 获取或设置支付方式名称．
        /// </summary>
        public string PaymentMethodName { get; set; }

        /// <summary>
        /// 获取或设置支付名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不允许为空！")]
        [StringLength(16, ErrorMessage = "长度不允许超过16个字符！")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}