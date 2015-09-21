// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigInvoiceTypeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票类型配置 数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    /// <summary>
    /// 发票类型配置 数据访问接口
    /// </summary>
    public interface IConfigInvoiceTypeDA
    {
        /// <summary>
        /// 查询所有配置信息
        /// </summary>
        /// <returns>
        /// 查询的配置列别
        /// </returns>
        List<Config_Invoice_Type> SelectAll();

        /// <summary>
        /// 新增一条发票类别配置
        /// </summary>
        /// <param name="invoiceType">
        /// 发票类别配置
        /// </param>
        /// <returns>
        /// 新增的ID
        /// </returns>
        int Insert(Config_Invoice_Type invoiceType);        
        
        /// <summary>
        /// 删除一条发票类别配置
        /// </summary>
        /// <param name="id">删除配置的ID</param>
        /// <returns>删除的ID</returns>
        int Delete(int id);

        /// <summary>
        /// 更新一条发票类别配置
        /// </summary>
        /// <param name="invoiceType">发票类别配置</param>
        void Update(Config_Invoice_Type invoiceType);
    }
}