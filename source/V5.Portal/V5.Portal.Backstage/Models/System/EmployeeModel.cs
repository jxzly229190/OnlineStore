// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The employee model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.System
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;

    /// <summary>
    /// The employee model.
    /// </summary>
    public class EmployeeModel
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
        ///     获取或设置员工所在部门编号．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择员工所在部门")]
        public int DepartmentID { get; set; }

        /// <summary>
        ///     获取或设置员工户口所在区县编号．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择员工户口所在地")]
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
        /// Gets or sets the city id.
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
        /// Gets or sets the province id.
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        ///     获取或设置员工身份证号码．
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        ///     获取或设置员工户口地址．
        /// </summary>
        public string IdentityCardAddress { get; set; }

        /// <summary>
        ///     获取或设置员工工资卡卡号．
        /// </summary>
        public string BankCard { get; set; }

        /// <summary>
        ///     获取或设置员工姓名．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入员工姓名")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "员工姓名长度范围为：2 ~ 32")]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置员工年龄．
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        ///     获取或设置员工性别．
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///     获取或设置员工手机号码．
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     获取或设置员工家庭住址．
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        ///     获取或设置状态（0：在职，1：离职）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}