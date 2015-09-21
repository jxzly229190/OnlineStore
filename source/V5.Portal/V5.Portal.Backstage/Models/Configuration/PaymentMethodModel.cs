// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentMethodModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货公司访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    /// <summary>
    /// 支付方式 Model
    /// </summary>
    public class PaymentMethodModel
    {
        public int PaymentMethodId { get; set; }

        public string PaymentMethodName { get; set; }
    }
}