// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigDeliveryCorporationDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//    送货公司 数据库 访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Configuration
{
    using global::System.Collections.Generic;

    using V5.DataContract.Configuration;

    /// <summary>
    /// 送货公司 数据库 访问接口
    /// </summary>
    public interface IConfigDeliveryCorporationDA
    {
        /// <summary>
        /// 查询所有合作送货公司列表
        /// </summary>
        /// <returns>
        /// 所有送货公司列表
        /// </returns>
        List<Config_Delivery_Corporation> SelectAll();

        /// <summary>
        /// 新增快递公司
        /// </summary>
        /// <param name="corporation">
        /// 快递公司
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(Config_Delivery_Corporation corporation);

        /// <summary>
        /// 删除快递公司
        /// </summary>
        /// <param name="id">
        /// 公司编码
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Delete(int id);

        /// <summary>
        /// 更新快递公司信息
        /// </summary>
        /// <param name="corporation">
        /// 快递公司
        /// </param>
        void Update(Config_Delivery_Corporation corporation);
    }
}
