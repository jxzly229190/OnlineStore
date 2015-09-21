// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.User
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.Web.Mvc;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;

    /// <summary>
    /// 会员模型类.
    /// </summary>
    public class UserModel
    {
        #region Constants and Fields

        /// <summary>
        /// The county id.
        /// </summary>
        private int countyID;

        /// <summary>
        /// The city id.
        /// </summary>
        private int cityID;

        #endregion

        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置Cps 合作平台编号．
        /// </summary>
        public int CpsID { get; set; }

        /// <summary>
        ///     获取或设置用户等级编号．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "会员等级不能为空")]
        public int UserLevelID { get; set; }

        /// <summary>
        ///     获取会员等级
        /// </summary>
        public string UserLevelName { get; set; }

        /// <summary>
        ///     获取或设置会员户口所在区县编号．
        /// </summary>
        public int CountyID
        {
            get
            {
                return this.countyID;
            }

            set
            {
                this.countyID = value;

                if (this.countyID > 0)
                {
                    var mongoDbStore1 = new MongoDbStore<County>("Counties");
                    var city = mongoDbStore1.Single(item => item.ID == this.countyID);
                    if (city != null)
                    {
                        this.CityID = city.CityID;
                    }
                }
            }
        }

        /// <summary>
        ///     获取或设置会员户口所在城市编号.
        /// </summary>
        public int CityID
        {
            get
            {
                return this.cityID;
            }

            set
            {
                this.cityID = value;

                if (this.cityID > 0)
                {
                    var mongoDbStore2 = new MongoDbStore<City>("Cities");
                    var province = mongoDbStore2.Single(item => item.ID == this.CityID);
                    if (province != null)
                    {
                        this.ProvinceID = province.ProvinceID;
                    }
                }
            }
        }

        /// <summary>
        ///     获取或设置会员户口所在省市编号.
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        public int ProvinceID { get; set; }

        /// <summary>
        ///     获取或设置用户所在地详细地址．
        /// </summary>
        [StringLength(50, MinimumLength = 0, ErrorMessage = "地址长度不能大于{1}")]
        public string Address { get; set; }

        /// <summary>
        ///     获取或设置电子邮箱．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "电子邮箱不能为空")]
        [RegularExpression(@"^([\w\.\-]+@[\w\.\-]+\.[\w\.\-]+)$", ErrorMessage = "邮箱地址格式不正确")]
        [Remote("IsEmailExists", "User", ErrorMessage = "该邮箱已注册过")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "邮箱地址不能大于{1}")]
        public string Email { get; set; }

        /// <summary>
        ///     获取或设置电子邮箱是否验证．
        /// </summary>
        public bool EmailValidate { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        [Remote("IsMobileExists", "User", HttpMethod = "post", ErrorMessage = "该手机已存在")]
        [RegularExpression(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", ErrorMessage = "手机号码格式不正确")]
        public string Mobile { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        [Remote("IsMobileExists", "User", HttpMethod = "post", ErrorMessage = "该手机已存在")]
        [RegularExpression(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", ErrorMessage = "手机号码格式不正确")]
        public string Tel { get; set; }

        /// <summary>
        ///     获取或设置手机号码是否验证．
        /// </summary>
        public bool MobileValidate { get; set; }

        /// <summary>
        ///     获取或设置用户姓名．
        /// </summary>
        [StringLength(20, MinimumLength = 0, ErrorMessage = "姓名长度不能大于{1} ")]
        [RegularExpression(@"^[a-zA-Z|\d|\u0391-\uFFE5\s]*$", ErrorMessage = "请输入真实姓名")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置用户年龄．
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "请输入正确的年龄")]
        public int Age { get; set; }

        /// <summary>
        ///     获取或设置用户性别．
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        ///     获取或设置会员积分.
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        ///     获取或设置登录名．
        /// </summary>
        [Remote("IsLoginNameExists", "User", HttpMethod = "post", ErrorMessage = "该登录名已存在")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "登录名不能为空")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "输入不正确")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "登录名长度范围{2}~{1}")]
        public string LoginName { get; set; }

        /// <summary>
        ///     获取或设置登录密码．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "密码的长度应在{2}-{1}")]
        public string LoginPassword { get; set; }

        /// <summary>
        ///     获取或设置登录密码．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "密码的长度应在{2}-{1}")]
        [Compare("LoginPassword", ErrorMessage = "密码不一致")]
        public string ConformPassword { get; set; }

        /// <summary>
        ///     获取或设置会员头像URL.
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        ///     获取或设置昵称．
        /// </summary>
        [StringLength(8, MinimumLength = 0, ErrorMessage = "昵称为长度不能大于{1}")]
        public string NickName { get; set; }

        /// <summary>
        ///     获取或设置用户生日．
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     获取或设置用户 QQ 号码．
        /// </summary>
        [StringLength(10, MinimumLength = 5, ErrorMessage = "请填写正确的QQ号码")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "请填写正确的QQ号码")]
        public string QQ { get; set; }

        /// <summary>
        ///     获取或设置用户 MSN 账户．
        /// </summary>
        [StringLength(50, MinimumLength = 0, ErrorMessage = "请填写正确的MSN号码")]
        public string MSN { get; set; }

        /// <summary>
        ///     获取或设置互联登录关联ID．
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        ///     获取或设置用户状态（0：未验证，1：已通过，2：已锁定）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     获取或设置最后登录时间．
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        ///   获取或设置状态名称.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        ///   获取会员账户信息.
        /// </summary>
        public UserAccountModel Account { get; set; }

        /// <summary>
        ///   获取或设置会员的收货地址信息
        /// </summary>
        public string DefaultAddress { get; set; }

        /// <summary>
        ///   获取或设置收货地址的邮政编码.
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        ///     获取或设置省/市/区县编号．
        /// </summary>
        public string CountyName
        {
            get
            {
                if (this.CountyID < 1)
                {
                    return null;
                }

                try
                {
                    var countyList = new MongoDbStore<County>("Counties");
                    var county = countyList.Single(item => item.ID == this.CountyID);

                    var cityList = new MongoDbStore<City>("Cities");
                    var city = cityList.Single(item => item.ID == county.CityID);

                    var provinceList = new MongoDbStore<Province>("Provinces");
                    var provice = provinceList.Single(item => item.ID == city.ProvinceID);

                    return provice.Name + ">>" + city.Name + ">>" + county.Name;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}