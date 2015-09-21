// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Coupon_Query.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   优惠券查询类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    using System;

    /// <summary>
    ///     优惠券查询类
    /// </summary>
    public class Coupon_Query
    {
        #region Public Properties

        /// <summary>
        ///    获取或设置类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     获取或设置类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        ///     获取或设置优惠券名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置优惠券初始数量．
        /// </summary>
        public int InitialNumber { get; set; }
        
        /// <summary>
        ///     获取或设置优惠券面值．
        /// </summary>
        public double FaceValue { get; set; }

        /// <summary>
        ///     获取或设置优惠券描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     获取或设置生效时间．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     获取或设置失效时间．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}