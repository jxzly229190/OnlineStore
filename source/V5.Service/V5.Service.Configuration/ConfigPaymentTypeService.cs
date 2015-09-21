// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigPaymentTypeService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付类别服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;

    /// <summary>
    /// 支付类别服务类
    /// </summary>
    public class ConfigPaymentTypeService
    {
        #region  Constants and Fields
        private IConfigPaymentTypeDA _paymentTypeDA;

        private IConfigPaymentTypeDA paymentTypeDA
        {
            get
            {
                if (this._paymentTypeDA == null)
                {
                    this._paymentTypeDA = new DAFactoryConfiguration().CreateConfigPaymentTypeDA();
                }

                return this._paymentTypeDA;
            }
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 查询所有支付类别
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Payment_Type> QueryAll()
        {
            return this.paymentTypeDA.SelectAll();
        }

        /// <summary>
        /// 新增支付类别
        /// </summary>
        /// <param name="paymentType">
        /// 支付类别
        /// </param>
        /// <returns>
        /// 新增对象的ID
        /// </returns>
        public int Add(Config_Payment_Type paymentType)
        {
            return this.paymentTypeDA.Insert(paymentType);
        }

        /// <summary>
        /// 更新支付类别
        /// </summary>
        /// <param name="paymentType">
        /// 需更新的支付类别
        /// </param>
        public void Modify(Config_Payment_Type paymentType)
        {
            this.paymentTypeDA.Update(paymentType);
        }

        /// <summary>
        /// 删除支付类别
        /// </summary>
        /// <param name="Id">
        /// 支付类别Id
        /// </param>
        /// <returns>
        /// 删除的对象Id
        /// </returns>
        public int Remove(int Id)
        {
            return this.paymentTypeDA.Delete(Id);
        } 
        #endregion
    }
}