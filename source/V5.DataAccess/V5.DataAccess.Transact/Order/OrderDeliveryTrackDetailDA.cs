// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDeliveryTrackDtrailDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单配送物流流转数据操作类
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
    /// 订单配送物流流转数据操作类
    /// </summary>
    public class OrderDeliveryTrackDetailDA : IOrderDeliveryTrackDetailDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 根据订单编码查询订单流程信息
        /// </summary>
        /// <param name="orderId">
        /// 订单编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public Order_Delivery_Tracking SelectByOrderId(int orderId)
        {
            var list = this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_Order_Delivery_Tracking_Details_SelectByOrderID",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "orderID",
                                SqlDbType.Int,
                                orderId,
                                ParameterDirection.Input)
                        },
					null).ToList<Order_Delivery_Tracking>();

	        if (list != null && list.Count > 0)
	        {
		        return list[0];
	        }

	        return null;
        }

        #endregion
    }
}