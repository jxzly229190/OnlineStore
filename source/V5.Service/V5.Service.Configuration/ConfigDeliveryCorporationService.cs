// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCorporationService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   配送公司服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Configuration
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Configuration;
    using V5.DataContract.Configuration;

    public class ConfigDeliveryCorporationService
    {
        #region  Constants and Fields
        private IConfigDeliveryCorporationDA configDeliveryCorporation; 
        #endregion

        #region  Constructors and Destructors
        public ConfigDeliveryCorporationService()
        {
            this.configDeliveryCorporation = new DAFactoryConfiguration().CreateConfigDeliveryCorporationDA();
        } 
        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 获取所有合作快递公司 列表
        /// </summary>
        /// <returns></returns>
        public List<Config_Delivery_Corporation> QueryAllConfigDeliveryCorporations()
        {
            return this.configDeliveryCorporation.SelectAll();
        }

        /// <summary>
        /// 新增一条送货公司记录
        /// </summary>
        /// <param name="corporation">
        /// 送货公司记录
        /// </param>
        /// <returns>
        /// 新记录的ID
        /// </returns>
        public int Add(Config_Delivery_Corporation Corporation)
        {
            return this.configDeliveryCorporation.Insert(Corporation);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="Id">
        /// 删除的Id
        /// </param>
        /// <returns>
        /// 删除的Id
        /// </returns>
        public int Remove(int Id)
        {
            return this.configDeliveryCorporation.Delete(Id);
        }

        /// <summary>
        /// 修改配送公司记录
        /// </summary>
        /// <param name="corporation">
        /// 配送公司
        /// </param>
        public void Modify(Config_Delivery_Corporation corporation)
        {
            this.configDeliveryCorporation.Update(corporation);
        } 
        #endregion
    }
}