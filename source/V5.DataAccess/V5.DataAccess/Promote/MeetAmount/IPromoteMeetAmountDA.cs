// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMeetAmountDA.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   满件优惠促销活动数据访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote.MeetAmount
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote.MeetAmount;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满件优惠促销活动数据访问接口
    /// </summary>
    public interface IPromoteMeetAmountDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 满件优惠的编号.
        /// </returns>
        int Insert(Promote_MeetAmount promoteMeetAmount, out SqlTransaction transaction);

        /// <summary>
        /// 修改满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Update(Promote_MeetAmount promoteMeetAmount, out SqlTransaction transaction);

        /// <summary>
        /// 启动、停止满件优惠.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动编号.
        /// </param>
        /// <param name="status">
        /// 活动状态（1：可用，2：停用）.
        /// </param>
        void UpdateStatus(int id, int status);

        /// <summary>
        /// 查询满件优惠列表
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
        /// 满件优惠列表
        /// </returns>
        List<Promote_MeetAmount> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查询指定的满件优惠活动.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount.
        /// </returns>
        Promote_MeetAmount SelectByID(int id);

        /// <summary>
        /// 删除促销活动.
        /// </summary>
        /// <param name="id">
        /// 满件促销活动的编号
        /// </param>
        void Delete(int id);

        #endregion
    }
}
