// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthConfig.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   验证配置类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.App_Start
{
    /// <summary>
    /// 验证配置类
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// 注册验证配置项
        /// </summary>
        public static void RegisterAuth()
        {
            // 若要允许此站点的用户使用他们在其他站点(例如 Microsoft、Facebook 和 Twitter)上拥有的帐户登录，
            // 必须更新此站点。有关详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
