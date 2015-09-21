// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductConsultDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System.Collections.Generic;
    using V5.DataContract.Transact;

    /// <summary>
    ///  商品咨询访问接口
    /// </summary>
    public interface IProductConsultDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 回复咨询
        /// </summary>
        /// <param name="consult">咨询回复对象</param>
        void ReplyConsult(Product_Consult consult);

        /// <summary>
        /// 更新回复咨询
        /// </summary>
        /// <param name="reply">商品咨询回复对象</param>
        void UpdateConsultReply(Product_Consult reply);

        /// <summary>
        /// 删除咨询
        /// </summary>
        /// <param name="id">需删除的对象编码</param>
        /// <returns>已删除的对象编码</returns>
        int DeleteConsult(int id);

        /// <summary>
        /// 删除回复咨询
        /// </summary>
        /// <param name="id">需删除的对象编码</param>
        /// <returns>已删除的对象编码</returns>
        int DeleteConsultReply(int id);

        /// <summary>
        /// 分页查询回复咨询信息
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Product_Consult> PagingReplies(Library.Storage.DB.Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 分页查询咨询信息
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Product_Consult> QueryConsult(Library.Storage.DB.Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 添加商品咨询.
        /// </summary>
        /// <param name="productConsult">
        /// Product_Consult.
        /// </param>
        /// <returns>
        /// 咨询的编号.
        /// </returns>
        int Insert(Product_Consult productConsult);

        /// <summary>
        /// 查询所有商品咨询.
        /// </summary>
        /// <returns>
        /// 商品咨询列表.
        /// </returns>
        List<Product_Consult> SelectAll();

        #endregion
    }
}