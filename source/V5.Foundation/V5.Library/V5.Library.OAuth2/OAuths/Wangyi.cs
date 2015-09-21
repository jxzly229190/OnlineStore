namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    public class Wangyi
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
                return string.Format("https://api.t.163.com/oauth2/authorize?response_type=code&client_id={0}&redirect_uri={1}&type={2}", this.AppKey, this.CallbackUrl, "163");
            }
        }

        public string TokenUrl
        {
            get
            {
                return "https://api.t.163.com/oauth2/access_token";
            }
        }

        public string AppKey
        {
            get
            {
                return "UZRwfBHkdyMLaVB0";
            }
        }

        public string AppSercet
        {
            get
            {
                return "ExGrCvUtG4ZEa1anb5KO8cBhjNRE0z6k";
            }
        }

        public string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=163";
            }
        }

        private string UserInfoUrl = "https://api.t.163.com/users/show.json?access_token={0}";

        public bool Authorize(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                // 返回数据 {"uid":"-6031527548317371740","expires_in":"86400","refresh_token":"92fb7e3b7a951c7f7d25e6fac1256d55","access_token":"52b1e94002ff342e78e7b05649c8278e"}
                string result = this.GetToken("GET", code); 

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

                        this.openID = data.uid; 
                        if (!string.IsNullOrEmpty(this.token))
                        {
                            // 获取微博昵称和头像
                            var wc = new WebClient { Encoding = Encoding.UTF8 };
                            wc.Headers.Add("Pragma", "no-cache");
                            result = wc.DownloadString(string.Format(this.UserInfoUrl, this.token));
                            /*
                             {"status":{"id":"4607144266070774910","source":"网易邮箱","text":"转发微博。","created_at":"Mon Oct 08 14:00:31 +0800 2012","videoInfos":null,"musicInfos":null,"songInfos":null,"in_reply_to_screen_name":"youdianguai","in_reply_to_status_id":"627757708239180849","in_reply_to_user_id":"-4360481398884639011","in_reply_to_user_name":"宅男宅女爱冷笑话","truncated":false,"previewLinkInfos":null},"following":false,"blocking":false,"followed_by":false,"name":"瑶头","location":"上海市,闵行区","id":"-6031527548317371740","description":"","email":"fybc90@163.com","gender":"1","verified":false,"url":"","screen_name":"0197922482","profile_image_url":"http://oimagea5.ydstatic.com/image?w=48&h=48&url=http%3A%2F%2F126.fm%2F410VNT","created_at":"Tue Apr 17 13:45:47 +0800 2012","columnIdNameWithCounts":null,"darenRec":"","favourites_count":"0","followers_count":"1","friends_count":"2","geo_enable":false,"icorp":"0","realName":null,"statuses_count":"1","sysTag":null,"userTag":null,"in_groups":[]}
                             */
                            if (!string.IsNullOrEmpty(result))
                            {
                                data = serializer.Deserialize<object>(result);
                                this.openID = data.id;
                                this.nickName = data.name;
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
