// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetAmount.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠促销活动类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetAmount
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 满件优惠促销活动类
    /// </summary>
    public class Promote_MeetAmount
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置满件优惠活动名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置活动开始时间．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置活动结束时间．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置活动备注．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置是否需要手机验证（true:手机验证后才可参与）．
        /// </summary>
        public bool IsMobileValidate { get; set; }

        /// <summary>
        /// 获取或设置是否可使用优惠券（true:可以使用）．
        /// </summary>
        public bool IsUseCoupon { get; set; }

        /// <summary>
        /// 获取或设置是否是新会员活动（true:只能新会员参与）．
        /// </summary>
        public bool IsNewUser { get; set; }

        /// <summary>
        /// 获取或设置删除标识．
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 获取或设置活动状态（1：可用，2：暂停，3：停止）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置满件全场优惠规则对象.
        /// </summary>
        public List<Promote_MeetAmount_Rule> MeetAmountRules { get; set; }

        /// <summary>
        /// 获取或设置满件全场优惠活动商品对象.
        /// </summary>
        public Promote_MeetAmount_Scope MeetAmountScope { get; set; }

        #endregion
    }
}
