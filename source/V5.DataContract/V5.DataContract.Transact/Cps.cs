// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cps.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Cps 合作平台类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Transact
{
    using System;

    /// <summary>
    ///     Cps 合作平台类
    /// </summary>
    public class Cps
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置Cps 合作平台名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置Cps 用户名．
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     获取或设置网址．
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        ///     获取或设置联系人．
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        ///     获取或设置手机号码．
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     获取或设置联系电话．
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     获取或设置电子邮箱．
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     获取或设置QQ 号码．
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        ///     获取或设置公司名称．
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        ///     获取或设置公司地址．
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        ///     获取或设置邮政号码．
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        ///     获取或设置跟踪地址．
        /// </summary>
        public string TrackingURL { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}