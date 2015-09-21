namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    using V5.Library.OAuth2;

    public class RenRen
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
                return string.Format("https://graph.renren.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}&type={2}", this.AppKey, this.CallbackUrl, "renren");
            }
        }

        public string TokenUrl
        {
            get
            {
                return "https://graph.renren.com/oauth/token";
            }
        }

        public string AppKey
        {
            get
            {
                return "33b66eb3dabd470caa8d92bda6a96224";
            }
        }

        public string AppSercet
        {
            get
            {
                return "74591aa22bce452781d3575b953358c3";
            }
        }

        public string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=renren";
            }
        }

        public string UserInfoUrl = "https://api.renren.com/v2/user/get?access_token={0}&userId={1}";

        public bool Authorize(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
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

                        this.openID = data.user.id.ToString();
                        this.nickName = data.user.name;
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
                string para = "grant_type=authorization_code&client_id=" + this.AppKey + "&client_secret=" + this.AppSercet + "&code="
                              + code;
                para += "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(this.CallbackUrl) + "&rnd=" + DateTime.Now.Second;
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
