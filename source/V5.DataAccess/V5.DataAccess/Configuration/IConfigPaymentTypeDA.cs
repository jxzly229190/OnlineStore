// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigPaymentTypeDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付类别 数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    /// <summary>
    /// 支付类别 数据访问接口
    /// </summary>
    public interface IConfigPaymentTypeDA
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        List<Config_Payment_Type> SelectAll();

        /// <summary>
        /// 根据Id查询（暂不支持）
        /// </summary>
        /// <param name="Id">
        /// 查询的ID
        /// </param>
        /// <returns>
        /// 查询结果对象
        /// </returns>
        List<Config_Payment_Type> SelectById(int Id);

        /// <summary>
        /// 新增支付方式
        /// </summary>
        /// <param name="paymentMethod">
        /// 支付方式对象
        /// </param>
        /// <returns>
        /// 新增的ID
        /// </returns>
        int Insert(Config_Payment_Type paymentMethod);

        /// <summary>
        /// 更新支付方式
        /// </summary>
        /// <param name="paymentMethod">
        /// 支付方式
        /// </param>
        void Update(Config_Payment_Type paymentMethod);

        /// <summary>
        /// 根据Id删除支付方式
        /// </summary>
        /// <param name="Id">
        /// 要删除的对象Id
        /// </param>
        /// <returns>
        /// 删除的对象Id
        /// </returns>
        int Delete(int Id);
    }
}