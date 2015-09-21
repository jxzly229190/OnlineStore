// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config_Delivery_Cost.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   送货费用类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Configuration
{
    using System;

    /// <summary>
    /// 送货费用类
    /// </summary>
    public class Config_Delivery_Cost
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置送货公司编号．
        /// </summary>
        public int DeliveryCorporationID { get; set; }

        /// <summary>
        /// 获取或设置城市编号．
        /// </summary>
        public int CityID { get; set; }

        /// <summary>
        /// 获取或设置预计持续时长（单位：天）．
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 获取或设置运费金额．
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}