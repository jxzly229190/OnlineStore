// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Employee.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   员工类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     员工类
    /// </summary>
    public class System_Employee
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置员工所在部门编号．
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        ///     获取或设置员工户口所在区县编号．
        /// </summary>
        public int CountyID { get; set; }

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