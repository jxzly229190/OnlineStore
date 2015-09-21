// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChannelGroupBuyDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ChannelGroupBuyDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Channel
{
    using global::System.Collections.Generic;
    using V5.DataContract.Channel;
    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The ChannelGroupBuyDA interface.
    /// </summary>
    public interface IChannelGroupBuyDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加团购活动
        /// </summary>
        /// <param name="groupBuy">
        /// The channel.groupBy
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// 返回参数
        /// </returns>
        int Insert(Channel_GroupBuy groupBuy);

        /// <summary>
        /// 团购商品分页
        /// </summary>
        /// <param name="paging">
        ///     分页数据对象
        /// </param>
        /// <param name="pageCount">
        ///     分页数
        /// </param>
        /// <param name="totalCount">
        ///     总条数
        /// </param>
        /// <returns>
        /// The <see>
        /// <cref>List</cref>
        /// </see>
        ///     .
        /// 返回参数
        /// </returns>
        List<View_GroupBuy_Product> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 商品表分页
        /// </summary>
        /// <param name="paging">分页数据对象</param>
        /// <param name="pageCount"> 分页数</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        List<Product> PagingProduct(Paging paging, out int pageCount, out int totalCount);

        Product SelectProductById(int id);

        List<Channel_GroupBuy> SelectGroupBuyByProductId(int productId);

        int Update(Channel_GroupBuy groupBuy);

        int UpdateStatus(int productId, int status);

        int UpdateImg(int productId, string imgUrl);

        int DeleteGrouBuyProductId(int productId);

        #endregion
    }
}
