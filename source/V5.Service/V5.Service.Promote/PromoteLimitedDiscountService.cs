// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteLimitedDiscountService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   限时打折促销活动服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 限时打折促销活动服务类.
    /// </summary>
    public class PromoteLimitedDiscountService
    {
        #region Constants and Fields

        /// <summary>
        /// /// <summary>
        /// 限时打折促销数据访问对象.
        /// </summary>.
        /// </summary>
        private readonly IPromoteLimitedDiscountDA promoteLimitedDiscountDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteLimitedDiscountService"/> class.
        /// </summary>
        public PromoteLimitedDiscountService()
        {
            this.promoteLimitedDiscountDA = new DAFactoryPromote().CreatePromoteLimitedDiscountDA();
        }

        #endregion

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
        public int Add(Promote_Limited_Discount promoteLimitedDiscount)
        {
            return this.promoteLimitedDiscountDA.Insert(promoteLimitedDiscount);
        }

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
        public List<Promote_Limited_Discount> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteLimitedDiscountDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 修改限时抢购促销.
        /// </summary>
        /// <param name="promoteLimitedDiscount">
        /// Promote_Limited_Discount的对象的实例.
        /// </param>
        public void Modify(Promote_Limited_Discount promoteLimitedDiscount)
        {
            this.promoteLimitedDiscountDA.Update(promoteLimitedDiscount);
        }

        /// <summary>
        /// 修改活动状态.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        /// <param name="status">
        /// 状态数值（1：正常，2，暂停，3，停止）.
        /// </param>
        public void ModifyStatus(int id, int status)
        {
            this.promoteLimitedDiscountDA.UpdateStatus(id, status);
        }

        /// <summary>
        /// 删除指定活动.
        /// </summary>
        /// <param name="id">
        /// 活动编号.
        /// </param>
        public void Remove(int id)
        {
            this.promoteLimitedDiscountDA.Delete(id);
        }

        /// <summary>
        /// 当前商品中已参加限时抢购的商品.
        /// </summary>
        /// <param name="productIDs">
        /// 商品编号例如（1，2，3，）.
        /// </param>
        /// <returns>
        /// 当前商品中已参加限时抢购的商品信息.
        /// </returns>
        public List<ProductSearchResult> QueryByPromoteProduct(string productIDs)
        {
            return this.promoteLimitedDiscountDA.SelectByPromoteProduct(productIDs);
        }

        /// <summary>
        /// 查询指定编号的限时打折.
        /// </summary>
        /// <param name="id">
        /// 限制打折编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_Limited_Discount"/>.
        /// </returns>
        public Promote_Limited_Discount QueryByID(int id)
        {
            return this.promoteLimitedDiscountDA.SelectByID(id);
        }

        #endregion
    }
}
