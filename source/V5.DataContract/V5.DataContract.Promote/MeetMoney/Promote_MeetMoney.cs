// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote_MeetMoney.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销活动类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Promote.MeetMoney
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 满就送促销活动类
    /// </summary>
    public class Promote_MeetMoney
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置设置该活动的员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置活动名称．
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
        /// 获取或设置活动描述信息．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置活动状态（1:可用,2:暂停,3:停止）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置活动创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

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
        /// 获取或设置活动范围．
        /// </summary>
        public Promote_MeetMoney_Scope MeetMoneyScope { get; set; }

        /// <summary>
        /// 获取或设置活动规则.
        /// </summary>
        public List<Promote_MeetMoney_Rule> MeetMoneyRules { get; set; }

        #endregion
    }
}
