// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserBrowseHistoryDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员浏览历史数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员浏览历史数据访问接口.
    /// </summary>
    public interface IUserBrowseHistoryDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加会员浏览记录.
        /// </summary>
        /// <param name="userBrowseHistory">
        /// The user browse history.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(User_BrowseHistory userBrowseHistory);

        /// <summary>
        /// 删除会员浏览商品记录.
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
        /// 查询会员的浏览商品列表.
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
        /// 浏览商品列表
        /// </returns>
        List<User_BrowseHistory> Paging(Paging paging, out int pageCount, out int totalCount);

        #endregion
    }
}
