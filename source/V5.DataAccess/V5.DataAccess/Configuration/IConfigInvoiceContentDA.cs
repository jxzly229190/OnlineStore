// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigInvoiceContentDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   发票内容配置 数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    /// <summary>
    /// 发票内容配置 数据访问接口
    /// </summary>
    public interface IConfigInvoiceContentDA
    {
        /// <summary>
        /// 查询所有发票内容配置信息
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        List<Config_Invoice_Content> SelectAll();

        /// <summary>
        /// 新增一条发票内容配置
        /// </summary>
        /// <param name="invoiceContent">
        /// 发票内容配置
        /// </param>
        /// <returns>
        /// 新增对象ID
        /// </returns>
        int Insert(Config_Invoice_Content invoiceContent);

        /// <summary>
        /// 删除一条发票配置信息
        /// </summary>
        /// <param name="id">
        /// 删除的配置ID
        /// </param>
        /// <returns>
        /// 删除的配置ID
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 更新一条发票内容配置
        /// </summary>
        /// <param name="invoiceContent">
        /// 发票内容配置
        /// </param>
        void Update(Config_Invoice_Content invoiceContent); 
    }
}