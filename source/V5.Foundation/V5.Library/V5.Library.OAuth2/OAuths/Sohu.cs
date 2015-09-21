namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    public class Sohu
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

        public  string OAuthUrl
        {
            get
            {
                return string.Format("https://api.t.sohu.com/oauth2/authorize?response_type=code&client_id={0}&redirect_uri={1}&scope=basic&type={2}", this.AppKey, this.CallbackUrl , "sohu");
            }
        }

        private string TokenUrl
        {
            get
            {
                return "https://api.t.sohu.com/oauth2/access_token";
            }
        }

        private string AppKey
        {
            get
            {
                return "vUFBOhwtnETdAWSYC9I4";
            }
        }

        private string AppSercet
        {
            get
            {
                return "O0nrM1ms$fqbzQ3sxlc$CHk(kY^rTo-8Owrx(nQZ";
            }
        }

        private string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=sohu";
            }
        }

        private string UserInfoUrl = "https://api.t.sohu.com/account/verify_credentials.json?access_token={0}";

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

                        if (!string.IsNullOrEmpty(this.token))
                        {
                            // 获取微博昵称和头像
                            var wc = new WebClient { Encoding = Encoding.UTF8 };
                            wc.Headers.Add("Pragma", "no-cache");
                            result = wc.DownloadString(string.Format(this.UserInfoUrl, this.token));
                            /*
                             {"id":"765538828","screen_name":"fybc90","name":"","location":"-,-","description":"","url":"","gender":"0","profile_image_url":"http://s5.cr.itc.cn/mblog/icon/39/3e/m_12821828868810.png","protected":false,"followers_count":1,"profile_background_color":"","profile_text_color":"","profile_link_color":"","profile_sidebar_fill_color":"","profile_sidebar_border_color":"","friends_count":55,"created_at":"Tue Sep 04 14:58:49 +0800 2012","favourites_count":0,"utc_offset":"","time_zone":"","profile_background_image_url":"","notifications":"","geo_enabled":false,"statuses_count":0,"following":true,"verified":false,"verified_reason":"","lang":"zh_cn","contributors_enabled":false}
                             */
                            if (!string.IsNullOrEmpty(result))
                            {
                                data = serializer.Deserialize<object>(result);
                                this.openID = data.id;
                                this.nickName = data.screen_name;
                                this.headUrl = data.profile_image_url;
                                return true;
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

        public string GetToken(string method, string code)
        {
            string result;
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(this.AppSercet);
                string SohuAppSercet = Convert.ToBase64String(bytes);
                string para = "grant_type=authorization_code&client_id=" + this.AppKey + "&client_secret="
                              + SohuAppSercet + "&code=" + code;
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
