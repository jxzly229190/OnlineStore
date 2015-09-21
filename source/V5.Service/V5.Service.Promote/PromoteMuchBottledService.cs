// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMuchBottledService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销活动服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    ///     多瓶装促销活动服务类.
    /// </summary>
    public class PromoteMuchBottledService
    {
        #region Constants and Fields

        /// <summary>
        /// 多瓶装促销数据访问对象.
        /// </summary>
        private readonly IPromoteMuchBottledDA promoteMuchBottledDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMuchBottledService"/> class.
        /// </summary>
        public PromoteMuchBottledService()
        {
            this.promoteMuchBottledDA = new DAFactoryPromote().CreatePromoteMuchBottledDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加多瓶装促销活动.
        /// </summary>
        /// <param name="promoteMuchBottled">
        /// Promote_MuchBottled的对象实例.
        /// </param>
        /// <returns>
        /// 多瓶装编号.
        /// </returns>
        public int Add(Promote_MuchBottled promoteMuchBottled)
        {
            return this.promoteMuchBottledDA.Insert(promoteMuchBottled);
        }

        /// <summary>
        /// 查询多瓶装促销列表
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
        /// 多瓶装列表
        /// </returns>
        public List<Promote_MuchBottled> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteMuchBottledDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查看指定商品是否已参加多瓶装促销.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// true :已参加，false：未参加.
        /// </returns>
        public bool QueryByProductID(int productID)
        {
            return this.promoteMuchBottledDA.SelectByProductID(productID);
        }

        /// <summary>
        /// 查询指定编号的多瓶装促销.
        /// </summary>
        /// <param name="id">
        /// 编号.
        /// </param>
        /// <returns>
        /// Promote_MuchBottled的对象实例.
        /// </returns>
        public Promote_MuchBottled QueryByID(int id)
        {
            return this.promoteMuchBottledDA.SelectByID(id);
        }

        /// <summary>
        /// 更新多瓶装促销.
        /// </summary>
        /// <param name="promoteMuchBottled">
        /// Promote_MuchBottled的对象实例.
        /// </param>
        public void Update(Promote_MuchBottled promoteMuchBottled)
        {
            this.promoteMuchBottledDA.Update(promoteMuchBottled);
        }

        #endregion
    }
}
