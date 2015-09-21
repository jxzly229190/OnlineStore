// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICpsDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact;
    using V5.Library.Storage.DB;

    /// <summary>
    /// Cps数据访问接口.
    /// </summary>
    public interface ICpsDA
    {
        /// <summary>
        /// 添加CPS.
        /// </summary>
        /// <param name="cps">
        /// CPS的对象实例.
        /// </param>
        /// <returns>
        /// Cps编号.
        /// </returns>
        int Insert(Cps cps);

        /// <summary>
        /// 查询Cps列表
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
        /// Cps列表
        /// </returns>
        List<Cps> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 更新Cps信息.
        /// </summary>
        /// <param name="cps">
        /// CPS的对象实例.
        /// </param>
        void Update(Cps cps);

        /// <summary>
        /// 查询所有CPS信息
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        List<Cps> SelectAll();
    }
}