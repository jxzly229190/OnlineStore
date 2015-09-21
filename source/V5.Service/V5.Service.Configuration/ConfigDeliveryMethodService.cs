// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryMethodService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   配送方式服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;

    /// <summary>
    /// 配送方式服务类
    /// </summary>
    public class ConfigDeliveryMethodService
    {
        #region  Constants and Fields
        private IConfigDeliveryMethodDA configDeliveryMethodDA; 
        #endregion

        #region  Constructors and Destructors
        /// <summary>
        /// 初始化服务对象
        /// </summary>
        public ConfigDeliveryMethodService()
        {
            this.configDeliveryMethodDA = new DAFactoryConfiguration().CreateConfigDeliveryMethodDA();
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 查询所有配送方式
        /// </summary>
        /// <returns>
        /// 查询结果列表
        /// </returns>
        public List<Config_Delivery_Method> QueryAll()
        {
            return configDeliveryMethodDA.SelectAll();
        }

        /// <summary>
        /// 添加一条配送方式
        /// </summary>
        /// <param name="deliveryMethod">
        /// 需要添加的配送方式
        /// </param>
        /// <returns>
        /// 新增的配送ID
        /// </returns>
        public int Add(Config_Delivery_Method deliveryMethod)
        {
            return configDeliveryMethodDA.Insert(deliveryMethod);
        }

        /// <summary>
        /// 删除一条配送方式
        /// </summary>
        /// <param name="id">
        /// 要删除的ID
        /// </param>
        /// <returns>
        /// 删除的ID
        /// </returns>
        public int Remove(int id)
        {
            return this.configDeliveryMethodDA.Delete(id);
        }

        /// <summary>
        /// 更新一条配送方式
        /// </summary>
        /// <param name="configDeliveryMethod">
        /// 需更新的配送方式
        /// </param>
        public void Modify(Config_Delivery_Method configDeliveryMethod)
        {
            this.configDeliveryMethodDA.Update(configDeliveryMethod);
        }      
        #endregion
    }
}