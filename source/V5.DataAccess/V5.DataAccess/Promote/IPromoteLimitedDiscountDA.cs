// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteLimitedDiscountDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   限时打折促销数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 限时打折促销数据访问接口.
    /// </summary>
    public interface IPromoteLimitedDiscountDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加限时打折促销活动.
        /// </summary>
        /// <param name="promoteLimitedDiscount">
        /// Promote_Limited_Discount的对象实例.
        /// </param>
        /// <returns>
        /// 限时打折促销活动的编号.
        /// </returns>
        int Insert(Promote_Limited_Discount promoteLimitedDiscount);

        /// <summary>
        /// 查询限时打折促销列表
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
        /// 限时打折列表
        /// </returns>
        List<Promote_Limited_Discount> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 修改限时抢购促销.
        /// </summary>
        /// <param name="promoteLimitedDiscount">
        /// Promote_Limited_Discount的对象的实例.
        /// </param>
        void Update(Promote_Limited_Discount promoteLimitedDiscount);

        /// <summary>
        /// 修改活动状态.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <param name="status">
        /// 状态数值（1：正常，2，暂停，3，停止）.
        /// </param>
        void UpdateStatus(int id, int status);

        /// <summary>
        /// 删除指定活动.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        void Delete(int id);

        /// <summary>
        /// 当前商品中已参加限时抢购的商品.
        /// </summary>
        /// <param name="productIDs">
        /// 商品编号例如（1，2，3，）.
        /// </param>
        /// <returns>
        /// 当前商品中已参加限时抢购的商品信息.
        /// </returns>
        List<ProductSearchResult> SelectByPromoteProduct(string productIDs);

        /// <summary>
        /// 查询指定编号的限时打折.
        /// </summary>
        /// <param name="id">
        /// 限制打折编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_Limited_Discount"/>.
        /// </returns>
        Promote_Limited_Discount SelectByID(int id);

        #endregion
    }
}
