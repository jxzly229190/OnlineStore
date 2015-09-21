// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCorporationDAL.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   支付机构 Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;


    public class ConfigPaymentOrganizationModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置支付方式编号:1.在线付款，2.货到付款
        /// </summary>
        [Required(ErrorMessage = "请选择支付方式！")]
        public int PaymentTypeID { get; set; }

        /// <summary>
        /// 获取或设置支付名称．
        /// </summary>
        [Required(ErrorMessage = "名称不能为空！")]
        [StringLength(32, ErrorMessage = "长度不允许超过32个字符！！")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置网银支付网址．
        /// </summary>
        [RegularExpression(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", ErrorMessage = "非法的URL")]
        [StringLength(50, ErrorMessage = "长度不允许超过50个字符！！")]
        public string URL { get; set; }

        /// <summary>
        /// 获取或设置网银显示图片网址．
        /// </summary>
        [RegularExpression(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", ErrorMessage = "非法的URL")]
        [StringLength(50, ErrorMessage = "长度不允许超过50个字符！！")]
        public string ImageURL { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the sorting.
        /// </summary>
        public int Sorting { get; set; }

        #endregion
    }
}