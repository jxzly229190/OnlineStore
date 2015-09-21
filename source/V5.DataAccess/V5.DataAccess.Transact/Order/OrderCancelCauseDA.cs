// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancelCauseDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消原因数据访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact.Order
{
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Transact.Order;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 订单取消原因数据访问类
    /// </summary>
    public class OrderCancelCauseDA : IOrderCancelCauseDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        /// <summary>
        /// 查询所有订单取消原因
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Cancel_Cause> SelectAll()
        {
            return
                this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Order_Cancel_Cause_Select",
                    null,
                    null).ToList<Order_Cancel_Cause>();
        }
    }
}