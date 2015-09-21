// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserReceiveAddressModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   收货地址Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace V5.Portal.Backstage.Models.User
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.Web.Mvc;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;
    using V5.Service.User;

    /// <summary>
    /// 收货地址Model类
    /// </summary>
    public class UserReceiveAddressModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置用户编号．
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        ///     获取或设置用户名称．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     获取或设置省份编号．
        /// </summary>
        public int ProvinceID 
        {
            get
            {
                if (this.CityID < 1)
                {
                    return 0;
                }

                var cityList = new MongoDbStore<City>("Cities");
                var city = cityList.Single(item => item.ID == this.CityID);
                return city == null ? -1 : city.ProvinceID;
            } 
        }

        /// <summary>
        ///     获取或设置城市编号．
        /// </summary>
        public int CityID
        {
            get
            {
                if (this.CountyID < 1)
                {
                    return 0;
                }

                var countyList = new MongoDbStore<County>("Counties");
                var county = countyList.Single(item => item.ID == this.CountyID);
                return county == null ? -1 : county.CityID;
            }
        }

        /// <summary>
        ///     获取或设置区县编号．
        /// </summary>
        public int CountyID { get; set; }

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

                var countyList = new MongoDbStore<County>("Counties");
                var county = countyList.Single(item => item.ID == this.CountyID);

                var cityList = new MongoDbStore<City>("Cities");
                var city = cityList.Single(item => item.ID == county.CityID);

                var provinceList = new MongoDbStore<Province>("Provinces");
                var provice = provinceList.Single(item => item.ID == city.ProvinceID);

                return (provice == null ? "" : provice.Name) + "，" + (city == null ? "" : city.Name) + "，" + (county == null ? "" : county.Name);
            }
        }

        /// <summary>
        ///     获取或设置收货详细地址．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不能为空")]
        public string Address { get; set; }

        /// <summary>
        ///     获取或设置收货人．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "收货人姓名不能为空！")]
        public string Consignee { get; set; }

        /// <summary>
        ///     获取收货人姓名．
        /// </summary>
        public string ReceiverName 
        {
            get
            {
                return this.Consignee;
            } 
        }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        [RegularExpression(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", ErrorMessage = "手机号码格式不正确")]
        public string Mobile { get; set; }

        /// <summary>
        ///     获取或设置电话号码．
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     获取或设置是否为默认收货地址．
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取邮政编码
        /// </summary>
        public string PostCode
        {
            get
            {
                return new UserReceiveAddressService().QueryPostCodeByID(this.CountyID);
            } 
        }

        #endregion
    }
}