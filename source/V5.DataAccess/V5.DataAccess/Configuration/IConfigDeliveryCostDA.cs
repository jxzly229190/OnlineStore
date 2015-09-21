// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigDeliveryCostDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   快递运费 数据操作接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;
    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 快递运费 数据操作接口
    /// </summary>
    public interface IConfigDeliveryCostDA
    {
        /// <summary>
        /// 查询所有合作送货运费列表
        /// </summary>
        /// <returns>
        /// 所有快递运费
        /// </returns>
        List<Config_Delivery_Cost> SelectAll();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Config_Delivery_Cost> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 根据配送公司Id查询配送费用列表
        /// </summary>
        /// <param name="corporationId">
        /// 配送公司Id
        /// </param>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        List<Config_Delivery_Cost> SelectByCorporationId(int corporationId);

        /// <summary>
        /// 新增一条运费
        /// </summary>
        /// <param name="deliveryCost">
        /// 运费对象
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(Config_Delivery_Cost deliveryCost);

        /// <summary>
        /// 删除一条运费
        /// </summary>
        /// <param name="Id">
        /// 标示ID
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete(int Id);

        /// <summary>
        /// 编辑运费
        /// </summary>
        /// <param name="deliveryCost">
        /// 运费对象
        /// </param>
        void Update(Config_Delivery_Cost deliveryCost);
    }
}