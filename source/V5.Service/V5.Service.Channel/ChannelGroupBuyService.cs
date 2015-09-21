// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelGroupBuyService.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved.
// </copyright>(C)
// <summary>
//   The channel group buy service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Channel
{
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;

    using V5.DataAccess;
    using V5.DataAccess.Channel;
    using V5.DataContract.Product;
    using V5.DataContract.Channel;
    using V5.Library.Storage.DB;

    /// <summary>
    /// The channel group buy service.
    /// </summary>
    public class ChannelGroupBuyService
    {
        #region Constants and Fields

        /// <summary>
        /// 团购频道服务类
        /// </summary>
        private readonly IChannelGroupBuyDA channelGroupBuyDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelGroupBuyService"/> class.
        /// </summary>
        public ChannelGroupBuyService()
        {
            this.channelGroupBuyDA = new DAFactoryChannel().CreateChannelGroupBuyDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="groupBuy">
        /// 团购商品对象
        /// </param>
        /// <returns>
        /// T<see cref="int"/>
        /// 返回参数
        /// </returns>
        public int Insert(Channel_GroupBuy groupBuy)
        {
            return this.channelGroupBuyDA.Insert(groupBuy);
        }

        /// <summary>
        /// 团购商品分页
        /// </summary>
        /// <param name="paging">分页数据对象</param>
        /// <param name="pageCount">分页数</param>
        /// <param name="totalCount">总条数据</param>
        /// <returns>
        /// <see />
        /// </returns>
        public List<View_GroupBuy_Product> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.channelGroupBuyDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 获取商品分页
        /// </summary>
        /// <param name="paging">分页数据对象</param>
        /// <param name="pageCount">分页数</param>
        /// <param name="totalCount">总页数</param>
        /// <returns></returns>
        public List<Product> QueryProduct(Paging paging, out int pageCount, out int totalCount)
        {
            return this.channelGroupBuyDA.PagingProduct(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 根据ProductID查询商品是不参加团购
        /// </summary>
        /// <param name="productId">ProductID</param>
        /// <returns></returns>
        public List<Channel_GroupBuy> QueryGroupBuyByProductId(int productId)
        {
            return this.channelGroupBuyDA.SelectGroupBuyByProductId(productId);
        }

        /// <summary>
        /// 修改团购商品
        /// </summary>
        /// <param name="groupBuy">团购商品对象</param>
        /// <returns></returns>
        public int UpdateGroupBuyProductId(Channel_GroupBuy groupBuy)
        {
            return this.channelGroupBuyDA.Update(groupBuy);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="productId">团商品ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(int productId, int status)
        {
            return this.channelGroupBuyDA.UpdateStatus(productId, status);
        }

        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="ProductId">商品ProductId</param>
        /// <param name="ImgUrl">商品图片路径</param>
        /// <returns></returns>
        public int UpdateImg(int ProductId, string ImgUrl)
        {
            return this.channelGroupBuyDA.UpdateImg(ProductId, ImgUrl);
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="Id">商品Id</param>
        /// <returns></returns>
        public Product QueryProductById(int Id)
        {
            return this.channelGroupBuyDA.SelectProductById(Id);
        }

        public int DeleteGrouBuyProductId(int ProductId)
        {
            return this.channelGroupBuyDA.DeleteGrouBuyProductId(ProductId);
        }

        #endregion
    }
}
