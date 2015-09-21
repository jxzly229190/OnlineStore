using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataAccess;
using V5.DataAccess.Configuration;
using V5.DataContract.Configuration;
using V5.Library.Storage.DB;

namespace V5.Service.Configuration
{
    public class ConfigPageService
    {
        #region  Constants and Fields
        private IConfigPageDA configPageDA;
        #endregion

        #region  Constructors and Destructors
        public ConfigPageService()
        {
            this.configPageDA = new DAFactoryConfiguration().CreateConfigPageDA();
        }
        #endregion
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Config_Page> Query(int type)
        {
            return this.configPageDA.Query(type);
        }
        /// <summary>
        /// 根据主键查内容
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <returns></returns>
        public Config_Page QueryByID(int id)
        {
            return this.configPageDA.QueryByID(id);
        }

        /// <summary>
        /// 修改内容
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <param name="content">内容</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int UpdateContent(int id, string content, string name)
        {
            return this.configPageDA.UpdateContent(id, content, name);
        }
        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns></returns>
        public int Insert(Config_Page model)
        {
            return this.configPageDA.Insert(model);
        }

        public List<Config_Page> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.configPageDA.Paging(paging, out pageCount, out totalCount);
        }
        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <returns></returns>
        public int DeleteRow(int id)
        {
            return this.configPageDA.DeleteRow(id);
        }

        public int UpdateLink(Config_Page config)
        {
            return this.configPageDA.UpdateLink(config);
        }
    }
}
