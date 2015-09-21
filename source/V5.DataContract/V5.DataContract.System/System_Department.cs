// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Department.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   部门类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     部门类
    /// </summary>
    public class System_Department
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置部门名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置部门总人数．
        /// </summary>
        public int Headcount { get; set; }

        /// <summary>
        ///     获取或设置部门负责人．
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        ///     获取或设置部门负责人手机号码．
        /// </summary>
        public string PrincipalMobile { get; set; }

        /// <summary>
        ///     获取或设置部门描述．
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}