namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    public class DouBan
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
                return string.Format("https://www.douban.com/service/auth2/auth?response_type=code&client_id={0}&redirect_uri={1}&type={2}", this.AppKey, this.CallbackUrl,"douban"); // 必须GET
            }
        }

        private string TokenUrl
        {
            get
            {
                return "https://www.douban.com/service/auth2/token"; // 必须POST
            }
        }

        public string AppKey
        {
            get
            {
                return "019133e5cca7a5c204b39bb165f6f146";
            }
        }

        public string AppSercet
        {
            get
            {
                return "e1fed91ef62a0a3c";
            }
        }

        public string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=douban";
            }
        }

        internal string UserInfoUrl = "https://api.douban.com/v2/user/~me"; // 根据Token 获取用户信息

        public bool Authorize(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                // {"access_token":"3fd99148d709bebbbf8cb3da851d3c31","douban_user_name":"大苕","douban_user_id":"45972387","expires_in":604800,"refresh_token":"76c9f53ad81fa7de645ddfb0885ef79a"}
                string result = this.GetToken("POST", code); // 一次性返回数据。

                // 分解result;
                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        var serializer = new JavaScriptSerializer();
                        serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                        dynamic data = serializer.Deserialize<object>(result);
                        this.token = data.access_token;
                        double d;
                        if (double.TryParse(data.expires_in.ToString(), out d) && d > 0)
                        {
                            this.expiresTime = DateTime.Now.AddSeconds(d);
                        }

                        this.openID = data.douban_user_id;
                        this.nickName = data.douban_user_name;
                        return true;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return false;
        }

        public string GetToken(string method, string code)
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
