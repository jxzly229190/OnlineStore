// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigDeliveryCostModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   快递运费配置 MVC Model类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Configuration
{
    using global::System;
    using global::System.ComponentModel.DataAnnotations;

    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;

    /// <summary>
    /// 快递运费配置 MVC Model类
    /// </summary>
    public class ConfigDeliveryCostModel
    {
        private int cityID;

        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货公司编号．
        /// </summary>
        [Required(ErrorMessage = "快递公司不能为空！！")]
        public int DeliveryCorporationID { get; set; }

        public string DeliveryCorporationName { get; set; }

        /// <summary>
        /// 获取或设置城市编号．
        /// </summary>
        [Required(ErrorMessage = "城市不能为空！！")]
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
                    var city = mongoDbStore2.Single(item => item.ID == this.CityID);
                    cityName = city.Name;
                    if (city != null)
                    {
                        this.ProvinceID = city.ProvinceID;
                    }
                }
            }
        }

        private string cityName;

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get
            {
                return cityName;
            }
        }

        public int ProvinceID { get; set; }

        /// <summary>
        /// 获取或设置预计持续时长（单位：天）．
        /// </summary>
        [Required(ErrorMessage = "运输耗时不能为空！！", AllowEmptyStrings = false)]
        public int Duration { get; set; }

        /// <summary>
        /// 获取或设置运费金额．
        /// </summary>
        [Required(ErrorMessage = "运费不能为空！！", AllowEmptyStrings = false)]
        public double Cost { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion 
    }
}