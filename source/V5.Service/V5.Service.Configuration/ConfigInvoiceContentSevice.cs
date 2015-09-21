// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigInvoiceContentSevice.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票内容 服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;

    public class ConfigInvoiceContentSevice
    {
        #region  Constants and Fields
        private IConfigInvoiceContentDA configInvoiceContentDA; 
        #endregion

        #region  Constructors and Destructors
        /// <summary>
        /// 实例化服务类对象
        /// </summary>
        public ConfigInvoiceContentSevice()
        {
            this.configInvoiceContentDA = new DAFactoryConfiguration().CreateConfigInvoiceContentDA();
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 新增发票内容
        /// </summary>
        /// <param name="invoiceContent">
        /// 需新增的发票内容
        /// </param>
        /// <returns>
        /// 新增的ID
        /// </returns>
        public int Add(Config_Invoice_Content invoiceContent)
        {
            return configInvoiceContentDA.Insert(invoiceContent);
        }

        /// <summary>
        /// 删除一条发票内容记录
        /// </summary>
        /// <param name="id">
        /// 需删除发票内容的ID
        /// </param>
        /// <returns>
        /// 删除的对象ID
        /// </returns>
        public int Remove(int id)
        {
            return this.configInvoiceContentDA.Delete(id);
        }

        /// <summary>
        /// 修改一条发票内容
        /// </summary>
        /// <param name="configInvoiceContent">
        /// 需修改的发票内容
        /// </param>
        public void Modify(Config_Invoice_Content configInvoiceContent)
        {
            this.configInvoiceContentDA.Update(configInvoiceContent);
        }

        /// <summary>
        /// 查询所有发票内容
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Invoice_Content> QueryAll()
        {
            return this.configInvoiceContentDA.SelectAll();
        } 
        #endregion
    }
}