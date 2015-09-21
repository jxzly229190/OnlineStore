// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统登录模型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Login
{
    /// <summary>
    /// 系统登录模型
    /// </summary>
    public class LoginModel
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置登陆名称
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 获取或设置登录密码
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        /// 获取或设置安全码
        /// </summary>
        public string SecurityCode { get; set; }

        #endregion
    }
}