// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductConsultReplyModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品咨询回复Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 商品咨询回复Model
    /// </summary>
    public class ProductConsultModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置咨询商品．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置咨询商品．
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置用户手机
        /// </summary>
        public string ConsultPersonMoblie { get; set; }

        /// <summary>
        /// 获取或设置用户邮箱
        /// </summary>
        public string ConsultPersonEmail { get; set; }

        /// <summary>
        /// 获取或设置咨询问题．
        /// </summary>
        public string ConsultContent { get; set; }

        /// <summary>
        /// 获取或设置咨询时间．
        /// </summary>
        public DateTime ConsultTime { get; set; }

        public string ConsultTimeStr
        {
            get
            {
                return this.ConsultTime.ToString("yyyy-M-d hh:mm:ss");
            }
        }

        /// <summary>
        /// 获取或设置咨询编号．
        /// </summary>
        public int ConsultID { get; set; }

        /// <summary>
        /// 获取或设置员工编号．
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 获取或设置员工姓名．
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 获取或设置咨询回复内容．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "回复内容不能为空！")]
        [StringLength(256, ErrorMessage = "字符串长度范围为5~256", MinimumLength = 5)]
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}