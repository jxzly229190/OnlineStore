// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserLevelPriceModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员等级价格模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 会员等级价格模型类.
    /// </summary>
    public class UserLevelPriceModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工登录编号.
        /// </summary>
        public int SystemUserID { get; set; }

        /// <summary>
        ///     获取或设置员工编号.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置员工姓名.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        ///     获取或设置用户等级编号．
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        ///     获取或设置用户等级名称.
        /// </summary>
        public string UserLevelName { get; set; }

        /// <summary>
        ///     获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        ///     获取或设置商品条形码.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "条形码不能为空")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "条形码长度不能大于{1}")]
        public string ProductBarcode { get; set; }

        /// <summary>
        ///     获取或设置商品名称.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     获取或设置商品等级价格.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     获取或设置状态（0：正常，1：已停止）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置状态（0：正常，1：已停止）．
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}