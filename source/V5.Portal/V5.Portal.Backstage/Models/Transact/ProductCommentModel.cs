// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评论Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 商品评论Model类
    /// </summary>
    public class ProductCommentModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置评论商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置评论商品编号．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置评论订单编号．
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 获取或设置评论用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置评论用户名称．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置评论分数．
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 获取或设置评论内容．
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置评论审核状态（1：未审核，2：已通过，3：已锁定）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取评论审核状态名称（1：未审核，2：已通过，3：已锁定）．
        /// </summary>
        public string StatusName
        {
            get
            {
                switch (this.Status)
                {
                    case 1:
                        return "未审核";
                    case 2:
                        return "已通过";
                    case 3:
                        return "已锁定";
                    default:
                       return "未知状态";
                }
            }
        }

        /// <summary>
        /// 获取或设置评论用户编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置评论用户编号．
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 获取或设置审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }

        /// <summary>
        /// 获取或设置审核备注
        /// </summary>
        [StringLength(128, ErrorMessage = "长度不能超过128")]
        public string AuditDescription { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}