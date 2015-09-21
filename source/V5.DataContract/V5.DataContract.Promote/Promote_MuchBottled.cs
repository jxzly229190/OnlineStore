// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MuchBottled.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品多瓶装促销类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    using System;

    /// <summary>
    /// 商品多瓶装促销活动类.
    /// </summary>
    public class Promote_MuchBottled
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置员工编号.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置商品编号.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置商品价格.
        /// </summary>
        public double GoujiuPrice { get; set; }

        /// <summary>
        /// 获取或设置商品名称.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置活动名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置是否仅限在线支付.
        /// </summary>
        public bool IsOnlinePayment { get; set; }

        /// <summary>
        /// 获取或设置活动生效时间.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置活动结束时间.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置是否显示时间.
        /// </summary>
        public bool IsDisplayTime { get; set; }
        
        /// <summary>
        /// 获取或设置活动创建时间.
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}
