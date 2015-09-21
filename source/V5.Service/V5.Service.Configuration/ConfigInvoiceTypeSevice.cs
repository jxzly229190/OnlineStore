// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigInvoiceTypeSevice.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票类型服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;

    /// <summary>
    /// 发票类型服务类
    /// </summary>
    public class ConfigInvoiceTypeSevice
    {
        #region  Constants and Fields
        private IConfigInvoiceTypeDA configInvoiceTypeDA; 
        #endregion

        #region  Constructors and Destructors
        /// <summary>
        /// 实例化服务对象
        /// </summary>
        public ConfigInvoiceTypeSevice()
        {
            this.configInvoiceTypeDA = new DAFactoryConfiguration().CreateConfigInvoiceTypeDA();
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 查询所有发票类型
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Invoice_Type> QueryAll()
        {
            return configInvoiceTypeDA.SelectAll();
        }

        /// <summary>
        /// 增加一条发票类型记录
        /// </summary>
        /// <param name="invoiceType">
        /// 增加的发票类型
        /// </param>
        /// <returns>
        /// 增加的对象的Id
        /// </returns>
        public int Add(Config_Invoice_Type invoiceType)
        {
            return configInvoiceTypeDA.Insert(invoiceType);
        }

        /// <summary>
        /// 删除一条发票类型
        /// </summary>
        /// <param name="id">
        /// 需删除的对象ID
        /// </param>
        /// <returns>
        /// 删除的对象ID
        /// </returns>
        public int Remove(int id)
        {
            return this.configInvoiceTypeDA.Delete(id);
        }

        /// <summary>
        /// 更新一条发票类型
        /// </summary>
        /// <param name="configInvoiceType">
        /// 需更新的发票类型
        /// </param>
        public void Modify(Config_Invoice_Type configInvoiceType)
        {
            this.configInvoiceTypeDA.Update(configInvoiceType);
        }     
        #endregion
    }
}