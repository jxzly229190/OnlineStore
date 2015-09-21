// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteLandingPageDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   LP（LandingPage）数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// LP（LandingPage）数据访问接口.
    /// </summary>
    public interface IPromoteLandingPageDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加LP（LandingPage）.
        /// </summary>
        /// <param name="landingPage">
        /// LP对象实例.
        /// </param>
        /// <returns>
        /// LP编号.
        /// </returns>
        int Insert(Promote_LandingPage landingPage);
        
        /// <summary>
        /// 更新LP（LandingPage）.
        /// </summary>
        /// <param name="landingPage">
        /// LP对象实例.
        /// </param>
        void Update(Promote_LandingPage landingPage);

        /// <summary>
        /// 查询所有LP（编码、父级编码、名称、制作人）.
        /// </summary>
        /// <returns>
        /// LP列表.
        /// </returns>
        List<Promote_LandingPage> SelectAll();

        /// <summary>
        /// 查询指定编号的LP信息.
        /// </summary>
        /// <param name="ID">
        /// LP编号.
        /// </param>
        /// <returns>
        /// LP对象实例.
        /// </returns>
        Promote_LandingPage SelectRowByID(int ID);

        /// <summary>
        /// 查询LP列表（编码、父级编码、名称、制作人）.
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
        /// LP列表
        /// </returns>
        List<Promote_LandingPage> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 删除指定编号的LP信息.
        /// </summary>
        /// <param name="ID">
        /// LP编号.
        /// </param>
        void Delete(int ID);

        #endregion
    }
}