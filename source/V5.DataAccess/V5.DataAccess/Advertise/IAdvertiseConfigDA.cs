using System.Collections.Generic;
using V5.DataContract.Advertise;
using V5.Library.Storage.DB;
using System.Data.SqlClient;

namespace V5.DataAccess.Advertise
{
    public interface IAdvertiseConfigDA
    {
        List<Advertise_Config> Query(int count, string search);

        List<Advertise_Config> QueryPid(int pid);
        /// <summary>
        /// 查出所有
        /// </summary>
        /// <returns></returns>
        List<Advertise_Config> QueryAll();
        /// <summary>
        /// 插入操作 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        int Insert(Advertise_Config config);
        /// <summary>
        /// 加载树节点
        /// </summary>
        /// <returns></returns>
        List<LandingPageTree> QueryLPTree();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="pageCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Advertise_Config> Paging(Paging paging, out int pageCount, out int totalCount);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(Advertise_Config model);
        /// <summary>
        /// 根据IDs查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Advertise_Config QueryID(int id);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int BatchInsert(List<Advertise_Config> list, SqlTransaction transaction);
        /// <summary>
        /// 删除一列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteRow(int id);

        /// <summary>
        /// 修改排序字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        int UpdateIsOrder(int id,int pid);

        /// <summary>
        /// 更新过滤
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        void UpdateFilter(int id, int filter);
    }
}
