// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps 合作平台模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    ///     Cps 合作平台模型类.
    /// </summary>
    public class CpsModel
    {
        #region Constants and Fields

        /// <summary>
        /// The tracking url.
        /// </summary>
        private string trackingURL;

        /// <summary>
        /// The zip code.
        /// </summary>
        private string zipCode;

        /// <summary>
        /// The qq.
        /// </summary>
        private string qq;

        /// <summary>
        /// The email.
        /// </summary>
        private string email;

        /// <summary>
        /// The tel.
        /// </summary>
        private string tel;

        /// <summary>
        /// The company address.
        /// </summary>
        private string companyAddress;

        /// <summary>
        /// The mobile.
        /// </summary>
        private string mobile;
        #endregion

        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置Cps 合作平台名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "平台名称不能为空")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "平台名称不能超过{1}位")]
        [RegularExpression(@"^[a-zA-Z|\d|\u0391-\uFFE5]*$", ErrorMessage = "输入不正确")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置Cps 用户名．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "请填写正确的用户名")]
        [RegularExpression(@"^[a-zA-Z|\d|\u0391-\uFFE5]*$", ErrorMessage = "输入不正确")]
        public string UserName { get; set; }

        /// <summary>
        ///     获取或设置网址．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "网址不能为空")]
        [RegularExpression(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", ErrorMessage = "输入不正确")]
        public string URL { get; set; }

        /// <summary>
        ///     获取或设置联系人．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "联系人不能为空")]
        [StringLength(10, MinimumLength = 0, ErrorMessage = "请填写正确的联系人")]
        public string Linkman { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        [RegularExpression(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", ErrorMessage = "手机号码格式不正确")]
        public string Mobile
        {
            get
            {
                return this.mobile ?? string.Empty;
            }

            set
            {
                this.mobile = value;
            }
        }

        /// <summary>
        ///     获取或设置联系电话．
        /// </summary>
        [StringLength(20, MinimumLength = 0, ErrorMessage = "请填写正确的联系电话")]
        [RegularExpression(@"^(0[0-9]{2,3}-)?([2-9][0-9]{6,7})+(-[0-9]{1,4})?$", ErrorMessage = "请填写正确的联系电话")]
        public string Tel
        {
            get
            {
                return this.tel ?? string.Empty;
            }

            set
            {
                this.tel = value;
            }
        }

        /// <summary>
        ///     获取或设置电子邮箱．
        /// </summary>
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9]+\.[A-Za-z]{2,4}", ErrorMessage = "邮箱地址格式不正确")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "邮箱地址长度不能大于{1}")]
        public string Email
        {
            get
            {
                return this.email ?? string.Empty;
            }

            set
            {
                this.email = value;
            }
        }

        /// <summary>
        ///     获取或设置QQ 号码．
        /// </summary>
        [StringLength(10, MinimumLength = 0, ErrorMessage = "请填写正确的1QQ号码")]
        [RegularExpression(@"[1-9][0-9]{4,}", ErrorMessage = "请填写正确的QQ号码")]
        public string QQ
        {
            get
            {
                return this.qq ?? string.Empty;
            }

            set
            {
                this.qq = value;
            }
        }

        /// <summary>
        ///     获取或设置公司名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "公司名称不能为空")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "公司名称的长度不能大于{1}")]
        [RegularExpression(@"^[a-zA-Z|\d|\u0391-\uFFE5]*$", ErrorMessage = "输入不正确")]
        public string Company { get; set; }

        /// <summary>
        ///     获取或设置公司地址．
        /// </summary>
        [StringLength(50, MinimumLength = 0, ErrorMessage = "公司地址的长度不能大于{1}")]
        public string CompanyAddress
        {
            get
            {
                return this.companyAddress ?? string.Empty;
            }

            set
            {
                this.companyAddress = value;
            }
        }

        /// <summary>
        ///     获取或设置邮政号码．
        /// </summary>
        [RegularExpression(@"^[1-9]\d{5}(?!\d)", ErrorMessage = "请填写正确的邮政编码")]
        public string ZipCode
        {
            get
            {
                return this.zipCode ?? string.Empty;
            }

            set
            {
                this.zipCode = value;
            }
        }

        /// <summary>
        ///     获取或设置跟踪地址．
        /// </summary>
        [RegularExpression(@"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", ErrorMessage = "输入不正确")]
        public string TrackingURL
        {
            get
            {
                return this.trackingURL ?? string.Empty;
            }

            set
            {
                this.trackingURL = value;
            }
        }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}