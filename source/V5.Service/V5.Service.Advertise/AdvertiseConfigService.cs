using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using V5.DataAccess;
using V5.DataAccess.Advertise;
using V5.DataContract.Advertise;
using V5.DataContract.Promote;
using V5.Library.Storage.DB;
using System.Data.SqlClient;

namespace V5.Service.Advertise
{
    public class AdvertiseConfigService
    {
        #region Constants and Fields

        /// <summary>
        /// 广告配置
        /// </summary>
        private readonly IAdvertiseConfigDA advertiseConfigDA;

        #endregion

        #region Constructors and Destructors

        public AdvertiseConfigService()
        {
            this.advertiseConfigDA = new DAFactoryAdvertise().CreateAdvertiseConfigDA();
        }

        #endregion

        #region Method

        /// <summary>
        /// 查询数据方法
        /// </summary>
        /// <returns></returns>
        public List<Advertise_Config> Query(int count, string search)
        {
            return this.advertiseConfigDA.Query(count, search);
        }

        /// <summary>
        /// 查询数据方法
        /// </summary>
        /// <returns></returns>
        public List<Advertise_Config> QueryPid(int pid)
        {
            return this.advertiseConfigDA.QueryPid(pid);
        }

        /// <summary>
        /// 查询所有的节点
        /// </summary>
        /// <returns></returns>
        public List<Advertise_Config> QueryAll()
        {
            return this.advertiseConfigDA.QueryAll();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="config">数据实体对象</param>
        /// <returns></returns>
        public int Insert(Advertise_Config config)
        {
            return this.advertiseConfigDA.Insert(config);
        }

        /// <summary>
        /// 获取LandingPage所有节点
        /// </summary>
        /// <returns></returns>
        public List<LandingPageTree> QueryLPTree()
        {
            return this.advertiseConfigDA.QueryLPTree();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Advertise_Config> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.advertiseConfigDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Advertise_Config model)
        {
            return this.advertiseConfigDA.Update(model);
        }

        /// <summary>
        /// 根据IDs查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advertise_Config QueryID(int id)
        {
            return this.advertiseConfigDA.QueryID(id);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int BatchInsert(List<Advertise_Config> list, SqlTransaction transaction)
        {
            return this.advertiseConfigDA.BatchInsert(list, transaction);
        }

        /// <summary>
        /// 根据ID删除一列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteRow(int id)
        {
            return this.advertiseConfigDA.DeleteRow(id);
        }

        /// <summary>
        /// 修改排序字段
        /// </summary>
        /// <param name="id">排序ID</param>
        /// <param name="pid">父ID</param>
        /// <returns></returns>
        public int UpdateIsOrder(int id,int pid)
        {
            return this.advertiseConfigDA.UpdateIsOrder(id,pid);
        }

        /// <summary>
        /// 更新过滤
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        public void UpdateFilter(int id, int filter)
        {
            this.advertiseConfigDA.UpdateFilter(id, filter);
        }

        #endregion
    }
}
