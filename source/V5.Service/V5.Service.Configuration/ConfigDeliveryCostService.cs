// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCostService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   快递运费服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 快递运费 配置 服务类
    /// </summary>
    public class ConfigDeliveryCostService
    {
        #region Constants and Fields

        /// <summary>
        /// The config delivery cost da.
        /// </summary>
        private IConfigDeliveryCostDA configDeliveryCostDA;

        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigDeliveryCostService"/> class.
        /// </summary>
        public ConfigDeliveryCostService()
        {
            this.configDeliveryCostDA = new DAFactoryConfiguration().CreateConfigDeliveryCostDA();
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 分页查询运费信息
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
        public List<Config_Delivery_Cost> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.configDeliveryCostDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询所有 快递运费配置 信息
        /// </summary>
        /// <returns>运费配置列表</returns>
        public List<Config_Delivery_Cost> QueryAllConfigDeliveryCosts()
        {
            return this.configDeliveryCostDA.SelectAll();
        }

        /// <summary>
        /// 新增一条运费配置
        /// </summary>
        /// <param name="deliveryCost">运费配置对象</param>
        /// <returns>新增运费配置ID</returns>
        public int Add(Config_Delivery_Cost deliveryCost)
        {
            return this.configDeliveryCostDA.Insert(deliveryCost);
        }

        /// <summary>
        /// 删除一条运费配置对象
        /// </summary>
        /// <param name="id">
        /// 配置ID
        /// </param>
        /// <returns>
        /// 删除对象的ID
        /// </returns>
        public int Remove(int id)
        {
            return this.configDeliveryCostDA.Delete(id);
        }

        /// <summary>
        /// 修改运费配置 对象
        /// </summary>
        /// <param name="deliveryCost">
        /// 要修改的运费配置 对象
        /// </param>
        public void Modify(Config_Delivery_Cost deliveryCost)
        {
            this.configDeliveryCostDA.Update(deliveryCost);
        }

        /// <summary>
        /// 根据快递公司ID查询运费列表
        /// </summary>
        /// <param name="corporationId"></param>
        /// <returns></returns>
        public List<Config_Delivery_Cost> QueryByCorporationId(int corporationId)
        {
            return this.configDeliveryCostDA.SelectByCorporationId(corporationId);
        }  
        #endregion
    }
}