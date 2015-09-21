// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserCollectRecordDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员收藏商品记录数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员收藏商品记录数据访问接口.
    /// </summary>
    public interface IUserCollectRecordDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加会员收藏记录.
        /// </summary>
        /// <param name="userCollectRecord">
        /// The user collect record.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(User_CollectRecord userCollectRecord);

        /// <summary>
        /// 删除会员收藏记录.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="productId">
        /// The product id.
        /// </param>
        void Delete(int userId, int productId);

        /// <summary>
        /// 批量删除会员收藏记录.
        /// </summary>
        /// <param name="id">
        /// 收藏记录编号集合.
        /// </param>
        void DeleteBatch(string id);

        /// <summary>
        /// 查询指定会员，指定商品的收藏记录.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="productId">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="User_CollectRecord"/>.
        /// </returns>
        User_CollectRecord SelectRow(int userId, int productId);

        /// <summary>
        /// 查询会员的收藏商品列表.
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 收藏商品列表
        /// </returns>
        List<User_CollectRecord> Paging(Paging paging, out int pageCount, out int totalCount);

        #endregion
    }
}
