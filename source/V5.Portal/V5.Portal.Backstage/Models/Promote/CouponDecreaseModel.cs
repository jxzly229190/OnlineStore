// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CouponDecreaseModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   优惠券类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    using global::System;

    /// <summary>
    ///     优惠券类
    /// </summary>
    public class CouponDecreaseModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工编号.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置优惠券名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置优惠券初始数量．
        /// </summary>
        public int InitialNumber { get; set; }

        /// <summary>
        ///     获取或设置现金券剩余数量.
        /// </summary>
        public int Remain { get; set; }

        /// <summary>
        ///     获取或设置现金券绑定数量.
        /// </summary>
        public int Bind { get; set; }

        /// <summary>
        ///     获取或设置现金券消费数量.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        ///     获取或设置优惠券面值．
        /// </summary>
        public double FaceValue { get; set; }

        /// <summary>
        ///     获取或设置需满足金额．
        /// </summary>
        public double MeetAmount { get; set; }

        /// <summary>
        ///     获取或设置生效时间．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     获取或设置失效时间．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     获取或设置优惠券描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     获取或设置使用对象
        /// </summary>
        public string UseObject { get; set; }

        #endregion
    }
}