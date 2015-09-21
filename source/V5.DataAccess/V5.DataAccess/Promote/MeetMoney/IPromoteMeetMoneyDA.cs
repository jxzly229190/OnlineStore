// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetMoneyDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetMoney
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetMoney;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满就送促销数据访问接口.
    /// </summary>
    public interface IPromoteMeetMoneyDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 查询满就送促销列表
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
        /// 满就送列表
        /// </returns>
        List<Promote_MeetMoney> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 添加满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        /// <returns>
        /// 满就送促销活动的编号.
        /// </returns>
        int Insert(Promote_MeetMoney promoteMeetMoney, out SqlTransaction transaction);

        /// <summary>
        /// 修改满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据库事务.
        /// </param>
        void Update(Promote_MeetMoney promoteMeetMoney, out SqlTransaction transaction);

        /// <summary>
        /// 查询指定的满就送促销.
        /// </summary>
        /// <param name="id">
        /// 满额优惠编号.
        /// </param>
        /// <returns>
        /// Promote_MeetMoney的对象实例.
        /// </returns>
        Promote_MeetMoney SelectByID(int id);

        /// <summary>
        /// 修改促销活动状态.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号
        /// </param>
        /// <param name="status">
        /// 促销活动状态.
        /// </param>
        void UpdateStatus(int id, int status);

        /// <summary>
        /// 删除促销活动.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号
        /// </param>
        void Delete(int id);

        #endregion
    }
}
