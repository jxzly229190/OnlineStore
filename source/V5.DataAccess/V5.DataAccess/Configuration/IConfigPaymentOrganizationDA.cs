// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigPaymentOrganizationDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付机构 数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    /// <summary>
    /// 支付机构 数据访问接口
    /// </summary>
    public interface IConfigPaymentOrganizationDA
    {
        /// <summary>
        /// 查询所有机构信息
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        List<Config_Payment_Organization> SelectAll();

        /// <summary>
        /// 根据类别Id查询机构信息
        /// </summary>
        /// <param name="paymentTypeId">付款类别ID</param>
        /// <returns>支付机构信息列表</returns>
        List<Config_Payment_Organization> SelectByPaymentTypeId(int paymentTypeId);

        /// <summary>
        /// 根据机构Id查询机构信息
        /// </summary>
        /// <param name="Id">
        /// 机构ID
        /// </param>
        /// <returns>
        /// 机构信息列表
        /// </returns>
        List<Config_Payment_Organization> SelectById(int Id);

        /// <summary>
        /// 新增一条机构信息
        /// </summary>
        /// <param name="paymentType">
        /// 机构信息
        /// </param>
        /// <returns>
        /// 新增机构ID
        /// </returns>
        int Insert(Config_Payment_Organization paymentType);

        /// <summary>
        /// 更新一条机构信息
        /// </summary>
        /// <param name="paymentType">
        /// 需更新的机构信息
        /// </param>
        void Update(Config_Payment_Organization paymentType);

        /// <summary>
        /// 删除一条机构信息
        /// </summary>
        /// <param name="Id">
        /// 删除的ID
        /// </param>
        /// <returns>
        /// 删除的ID
        /// </returns>
        int Delete(int Id);
    }
}