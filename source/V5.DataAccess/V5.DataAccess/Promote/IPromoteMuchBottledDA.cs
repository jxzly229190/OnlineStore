// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMuchBottledDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 多瓶装促销数据访问接口.
    /// </summary>
    public interface IPromoteMuchBottledDA
    {
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
        int Insert(Promote_MuchBottled promoteMuchBottled);

        /// <summary>
        /// 更新多瓶装促销.
        /// </summary>
        /// <param name="promoteMuchBottled">
        /// Promote_MuchBottled的对象实例.
        /// </param>
        void Update(Promote_MuchBottled promoteMuchBottled);

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
        List<Promote_MuchBottled> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查看指定商品是否已参加多瓶装促销.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// true :已参加，false：未参加.
        /// </returns>
        bool SelectByProductID(int productID);

        /// <summary>
        /// 查询指定编号的多瓶装促销.
        /// </summary>
        /// <param name="id">
        /// 编号.
        /// </param>
        /// <returns>
        /// Promote_MuchBottled的对象实例.
        /// </returns>
        Promote_MuchBottled SelectByID(int id);

        #endregion
    }
}
