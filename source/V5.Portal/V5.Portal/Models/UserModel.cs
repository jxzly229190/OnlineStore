// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员模型类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Models
{
    using System;

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
        public int ProvinceID { get; set; }

        /// <summary>
        ///     获取或设置用户所在地详细地址．
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     获取或设置电子邮箱．
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     获取或设置电子邮箱是否验证．
        /// </summary>
        public bool EmailValidate { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     获取或设置手机号码是否验证．
        /// </summary>
        public bool MobileValidate { get; set; }

        /// <summary>
        ///     获取或设置用户姓名．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置用户年龄．
        /// </summary>
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
        public string LoginName { get; set; }

        /// <summary>
        ///     获取或设置登录密码．
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        ///     获取或设置登录密码．
        /// </summary>
        public string ConformPassword { get; set; }

        /// <summary>
        ///     获取或设置会员头像URL.
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        ///     获取或设置昵称．
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     获取或设置用户生日．
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        ///     获取或设置用户 QQ 号码．
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        ///     获取或设置用户 MSN 账户．
        /// </summary>
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