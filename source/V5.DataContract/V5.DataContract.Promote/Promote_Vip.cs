// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_Vip.cs" company="www.gjw.com">
//   (C) 2014 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 会员促销.
    /// </summary>
    public class Promote_Vip
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置活动名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置创建员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置开始时间．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置结束时间．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置活动备注．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置活动状态（1：可用，2：暂停，3：停止）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 获取或设置是否新会员．
        /// </summary>
        public bool IsNewUser { get; set; }

        /// <summary>
        /// 获取或设置是否手机验证．
        /// </summary>
        public bool IsMobileValidate { get; set; }

        /// <summary>
        /// 获取或设置能否使用优惠券．
        /// </summary>
        public bool IsUseCoupon { get; set; }

        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<Promote_Vip_Scope> Scopes { get; set; }

        #endregion
    }
}