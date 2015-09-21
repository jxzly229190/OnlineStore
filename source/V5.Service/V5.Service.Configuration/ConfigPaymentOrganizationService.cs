// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigPaymentOrganizationService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付机构服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;
    using System.Collections.Generic;

    /// <summary>
    /// 支付机构服务类
    /// </summary>
    public class ConfigPaymentOrganizationService
    {
        #region  Constants and Fields
        private IConfigPaymentOrganizationDA _paymentOrganizationDA;
        #endregion

        #region  Constructors and Destructors
        private IConfigPaymentOrganizationDA paymentOrganizationDA
        {
            get
            {
                if (this._paymentOrganizationDA == null)
                {
                    this._paymentOrganizationDA = new DAFactoryConfiguration().CreateConfigPaymentOrganizationDA();
                }

                return this._paymentOrganizationDA;
            }
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 查询所有支付机构信息
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Payment_Organization> QueryAll()
        {
            return this.paymentOrganizationDA.SelectAll();
        }

        /// <summary>
        /// 根据支付类别Id查询支付机构信息
        /// </summary>
        /// <param name="paymentTypeId">
        /// 支付类别ID
        /// </param>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Payment_Organization> QueryByPaymentTypeId(int paymentTypeId)
        {
            return this.paymentOrganizationDA.SelectByPaymentTypeId(paymentTypeId);
        }

        /// <summary>
        /// 新增支付机构
        /// </summary>
        /// <param name="paymentOrganization">
        /// 需添加的支付机构
        /// </param>
        /// <returns>
        /// 新增的支付机构ID
        /// </returns>
        public int Add(Config_Payment_Organization paymentOrganization)
        {
            return this.paymentOrganizationDA.Insert(paymentOrganization);
        }

        /// <summary>
        /// 修改支付机构记录
        /// </summary>
        /// <param name="paymentOrganization">
        /// 需修改的支付机构
        /// </param>
        public void Modify(Config_Payment_Organization paymentOrganization)
        {
            this.paymentOrganizationDA.Update(paymentOrganization);
        }

        /// <summary>
        /// 删除支付机构
        /// </summary>
        /// <param name="Id">
        /// 支付机构Id
        /// </param>
        /// <returns>
        /// 已删除机构Id
        /// </returns>
        public int Remove(int Id)
        {
            return this.paymentOrganizationDA.Delete(Id);
        } 
        #endregion
    }
}
