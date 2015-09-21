// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteLandingPageModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   LP（LandingPage）管理类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Promote
{
    using global::System;

    /// <summary>
    ///     LP（LandingPage）管理类
    /// </summary>
    public class PromoteLandingPageModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置父级编号
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        ///     获取或设置LP主题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置制作人
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     获取或设置开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     获取或设置截止时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     获取或设置状态(0 未启用 1 启用 2 关闭 3 暂停)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     获取或设置状态文本
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        ///     获取或设置链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     获取或设置LP内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     获取或设置主图
        /// </summary>
        public string MasterPicture { get; set; }

        /// <summary>
        ///     获取或设置副图01
        /// </summary>
        public string Picture01 { get; set; }

        /// <summary>
        ///     获取或设置副图02
        /// </summary>
        public string Picture02 { get; set; }

        /// <summary>
        ///     获取或设置副图03
        /// </summary>
        public string Picture03 { get; set; }

        /// <summary>
        ///     获取或设置副图04
        /// </summary>
        public string Picture04 { get; set; }

        /// <summary>
        ///     获取或设置副图05
        /// </summary>
        public string Picture05 { get; set; }

        /// <summary>
        ///     获取或设置创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}