// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Payment_Type.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付类型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// 支付类型类
    /// </summary>
    public class Config_Payment_Type
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置支付方式编号．
        /// </summary>
        public int PaymentMethodID { get; set; }

        /// <summary>
        /// 获取或设置支付名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}