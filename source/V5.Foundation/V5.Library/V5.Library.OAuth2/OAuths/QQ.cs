namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;

    public class QQ
    {
        /// <summary>
        /// 返回的开放ID。
        /// </summary>
        public string openID = string.Empty;

        /// <summary>
        /// 访问的Token
        /// </summary>
        public string token = string.Empty;

        /// <summary>
        ///  过期时间
        /// </summary>
        public DateTime expiresTime;

        /// <summary>
        /// 第三方账号昵称
        /// </summary>
        public string nickName = string.Empty;

        /// <summary>
        /// 第三方账号头像地址
        /// </summary>
        public string headUrl = string.Empty;

        public string OAuthUrl
        {
            get
            {
                return string.Format("https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id={0}&redirect_uri={1}&type={2}", this.AppKey, this.CallbackUrl, "qq");
            }
        }

        private string TokenUrl
        {
            get
            {
                return "https://graph.qq.com/oauth2.0/token";
            }
        }

        private string AppKey
        {
            get
            {
                return "101012146";
            }
        }

        private string AppSercet
        {
            get
            {
                return "f519f5fc33ac1f09d2e39850230727a5";
            }
        }

        private string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=qq";
            }
        }

        private string OpenIDUrl = "https://graph.qq.com/oauth2.0/me?access_token={0}";

        private string UserInfoUrl =
            "https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}";

        public bool Authorize(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var result = this.GetToken("GET", code); // 返回: access_token=EC8A803FC695559AF557CD3DB5B80DBF&expires_in=7776000&refresh_token=DF9FDF859B22832F5FECFFC00B4D21A6

                // 分解result 
                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        this.token = OperateString.QueryString(result, "access_token");
                        
                        if (!string.IsNullOrEmpty(this.token))
                        {
                            double d;
                            if (double.TryParse(OperateString.QueryString(result, "expires_in"), out d))
                            {
                                this.expiresTime = DateTime.Now.AddSeconds(d);
                            }

                            var wc = new WebClient { Encoding = Encoding.UTF8 };
                            wc.Headers.Add("Pragma", "no-cache");

                            // 读取OpenID
                            result = wc.DownloadString(string.Format(this.OpenIDUrl, this.token)); // 返回callback( {"client_id":"101012146","openid":"31895EE2E3CEEE258CA5919E62A647E0"} );
                            if (!string.IsNullOrEmpty(result))
                            {
                                this.openID = OperateString.GetJosnValue(result, "openid");
                            }

                            if (!string.IsNullOrEmpty(this.openID))
                            {
                                // 读取QQ账号和头像
                                result = wc.DownloadString(string.Format(this.UserInfoUrl, this.token, this.AppKey, this.openID)); 
                                /*{
                                    "ret": 0,
                                    "msg": "",
                                    "is_lost":0,
                                    "nickname": "惊丞翰海",
                                    "gender": "男",
                                    "figureurl": "http:\/\/qzapp.qlogo.cn\/qzapp\/101012146\/31895EE2E3CEEE258CA5919E62A647E0\/30",
                                    "figureurl_1": "http:\/\/qzapp.qlogo.cn\/qzapp\/101012146\/31895EE2E3CEEE258CA5919E62A647E0\/50",
                                    "figureurl_2": "http:\/\/qzapp.qlogo.cn\/qzapp\/101012146\/31895EE2E3CEEE258CA5919E62A647E0\/100",
                                    "figureurl_qq_1": "http:\/\/q.qlogo.cn\/qqapp\/101012146\/31895EE2E3CEEE258CA5919E62A647E0\/40",
                                    "figureurl_qq_2": "http:\/\/q.qlogo.cn\/qqapp\/101012146\/31895EE2E3CEEE258CA5919E62A647E0\/100",
                                    "is_yellow_vip": "0",
                                    "vip": "0",
                                    "yellow_vip_level": "0",
                                    "level": "0",
                                    "is_yellow_year_vip": "0"
                                }*/

                                if (!string.IsNullOrEmpty(result))
                                {
                                    this.nickName = OperateString.GetJosnValue(result, "nickname");
                                    this.headUrl = OperateString.GetJosnValue(result, "figureurl");
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The get token.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetToken(string method, string code)
        {
            string result;
            try
            {
                string para = "grant_type=authorization_code&client_id=" + this.AppKey + "&client_secret="
                              + this.AppSercet + "&code=" + code;
                para += "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(this.CallbackUrl) + "&rnd="
                        + DateTime.Now.Second;
                var wc = new WebClient { Encoding = Encoding.UTF8 };
                wc.Headers.Add("Pragma", "no-cache");
                if (method == "POST")
                {
                    if (string.IsNullOrEmpty(wc.Headers["Content-Type"]))
                    {
                        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    }

                    result = wc.UploadString(this.TokenUrl, method, para);
                }
                else
                {
                    result = wc.DownloadString(this.TokenUrl + "?" + para);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return result;
        }
    }
}
