using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using V5.DataContract.Product;

namespace V5.DataAccess.Product
{
    public interface IBrandInformationDA
    {
        /// <summary>
        /// 执行插入操作
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        int Insert(Brand_Information brand);
        /// <summary>
        /// 执行修改操作 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        int Update(Brand_Information brand);
        /// <summary>
        /// 批量添加事务操作
        /// </summary>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int InsertBatch(List<Brand_Information> list, SqlTransaction transaction);

        Brand_Information QueryByID(int id);
        Brand_Information QueryByBrandID(int brandId);
        int UpdateByBrandID(Brand_Information brand);
        int UpdateLogo(int brandId, string logo);
        int UpdteProductId(int brandId, string productString);
    }
}
