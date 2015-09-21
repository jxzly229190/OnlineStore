// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigDeliveryMethodDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货方式配置 数据访问接口类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    public interface IConfigDeliveryMethodDA
    {
        /// <summary>
        /// 查询所有送货方式配置信息
        /// </summary>
        /// <returns>查询结果列表</returns>
        List<Config_Delivery_Method> SelectAll();

        /// <summary>
        /// 插入一条送货方式类配置信息
        /// </summary>
        /// <param name="deliveryMethod">送货方式配置</param>
        /// <returns>新增的配置ID</returns>
        int Insert(Config_Delivery_Method deliveryMethod);

        /// <summary>
        /// 删除一条送货方式配置信息
        /// </summary>
        /// <param name="id">
        /// 送货方式Id
        /// </param>
        /// <returns>
        /// 删除的配置ID
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 更新一条送货方式配置信息
        /// </summary>
        /// <param name="deliveryMethod">
        /// 送货方式配置
        /// </param>
        void Update(Config_Delivery_Method deliveryMethod); 
    }
}