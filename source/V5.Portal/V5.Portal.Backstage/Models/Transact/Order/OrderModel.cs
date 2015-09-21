// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货公司访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact.Order
{
    using global::System;

    using NPOI.SS.Formula.Functions;

    /// <summary>
    /// 送货公司访问类
    /// </summary>
    public class OrderModel
    {
        #region Constants and Fields

        /// <summary>
        /// 用户名字段
        /// </summary>
        private string userName;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置会员编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置用户手机
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 获取或设置会员邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 获取或设置会员名称．
        /// </summary>
        public string UserName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.userName))
                {
                    // 若用户名为空，则优先使用用户手机号作为用户名，否则使用邮箱地址
                    if (!string.IsNullOrWhiteSpace(this.UserMobile))
                    {
                        return this.UserMobile;
                    }

                    return this.UserEmail;
                }

                return this.userName;
            }

            set
            {
                this.userName = value;
            }
        }

        /// <summary>
        /// 获取或设置收货地址编号．
        /// </summary>
        public int RecieveAddressID { get; set; }

        /// <summary>
        /// 获取或设置收货人．
        /// </summary>
        public string Consignee { get; set; }

		/// <summary>
		/// 获取或设置收货人手机号码．
		/// </summary>
		public string ReceiverMoblie { get; set; }

        /// <summary>
        /// 获取或设置CPS 编号．
        /// </summary>
        public int CpsID { get; set; }

        /// <summary>
        /// 订单来源字段
        /// </summary>
        private string cpsName;

        /// <summary>
        /// 获取或设置Cps 名称(订单来源)
        /// </summary>
        public string CpsName
        {
            get
            {
                if (this.CpsID == 0)
                {
                    this.cpsName = "官网";
                }

                return this.cpsName;
            }

            set
            {
                this.cpsName = value;
            }
        }

        /// <summary>
        /// 获取或设置支付方式编号．
        /// </summary>
        public int PaymentMethodID { get; set; }

        /// <summary>
        /// 获取或设置支付方式名称
        /// </summary>
        public string PaymentMethodName
        {
            get
            {
                switch (this.PaymentMethodID)
                {
                    case 0:
                        return "在线支付";
                    case 1:
                    case 2:
                        return "货到付款";
                    default:
                        return "在线支付";
                }
            }
        }

        /// <summary>
        /// 获取或设置订单编码（按一定生成规则生成）．
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 获取或设置订单编号（原单号，当天时间 + 订单索引，例如：201311070001）
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 获取或设置订单总金额．
        /// </summary>
        public double TotalMoney { get; set; }

        /// <summary>
        /// 总金额（包含运费）
        /// </summary>
        public double TotalAmount {
            get
            {
                return this.DeliveryCost + this.TotalMoney;
            }
        }

        /// <summary>
		/// 获取或设置订单优惠总金额．
		/// </summary>
		public double Discount { get; set; }

        /// <summary>
        /// 获取或设置订单总积分．
        /// </summary>
        public int TotalIntegral { get; set; }

        /// <summary>
        /// 获取或设置订单运费．
        /// </summary>
        public double DeliveryCost { get; set; }

        /// <summary>
        /// 获取或设置支付状态（0：未支付，1：已支付）．
        /// </summary>
        public int PaymentStatus { get; set; }

        /// <summary>
        /// 获取支付状态名称（0：未支付，1：已支付）．
        /// </summary>
        public string PaymentStatusName
        {
            get
            {
                return this.PaymentStatus != 1 ? "未支付" : "已支付";
            }
        }

        /// <summary>
        /// 获取或设置是否开发票（0：不开，1：开）．
        /// </summary>
        public bool IsRequireInvoice { get; set; }

        /// <summary>
        /// 获取或设置订单状态．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置订单状态
        /// （100：待付款，0：待确认，1：已确认，2：已发货，3：已签收，4：ERP 作废，5：已损失，6：已取消，8：官网作废）
        /// </summary>
        public string StatusName
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "待确认";
                    case 1:
                        return "已确认";
                    case 2:
                        return "已发货";
                    case 3:
                        return "已签收";
                    case 4:
                        return "ERP 作废";
                    case 5:
                        return "已损失";
                    case 6:
                        return "已取消";
                    case 8:
                        return "官网作废";
                    case 100:
                        return "待付款";
                    default:
                        return "未知状态";
                }
            }
        }

        /// <summary>
        /// 获取或设置订单客服备注。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置订单补充说明．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取创建时间字符串．
        /// </summary>
        public string CreateTimeString 
        {
            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        #endregion
    }
}