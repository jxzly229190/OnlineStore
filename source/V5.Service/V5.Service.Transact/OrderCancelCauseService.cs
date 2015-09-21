// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCancelCauseService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单取消原因服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Transact.Order;
    using V5.DataContract.Transact.Order;

    /// <summary>
    /// 订单取消原因服务类
    /// </summary>
    public class OrderCancelCauseService
    {
        /// <summary>
        /// 订单取消原因数据访问对象
        /// </summary>
        private readonly IOrderCancelCauseDA orderCancelCauseDA;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCancelCauseService"/> class.
        /// </summary>
        public OrderCancelCauseService()
        {
            this.orderCancelCauseDA = new DAFactoryTransact().CreateOrderCancelCauseDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询所有订单取消原因记录
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Order_Cancel_Cause> QueryAll()
        {
            return this.orderCancelCauseDA.SelectAll();
        }

        #endregion
    }
}