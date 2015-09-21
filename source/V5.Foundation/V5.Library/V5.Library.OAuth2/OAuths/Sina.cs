// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sina.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the Sina type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.OAuth2.OAuths
{
    using System;
    using System.Net;
    using System.Text;
    using System.Web.Script.Serialization;

    /// <summary>
    /// ����.
    /// </summary>
    public class Sina 
    {
        /// <summary>
        /// ���صĿ���ID��
        /// </summary>
        public string openID = string.Empty;

        /// <summary>
        /// ���ʵ�Token
        /// </summary>
        public string token = string.Empty;

        /// <summary>
        ///  ����ʱ��
        /// </summary>
        public DateTime expiresTime;

        /// <summary>
        /// �������˺��ǳ�
        /// </summary>
        public string nickName = string.Empty;

        /// <summary>
        /// �������˺�ͷ���ַ
        /// </summary>
        public string headUrl = string.Empty;

        public string OAuthUrl
        {
            get
            {
                return string.Format("https://api.weibo.com/oauth2/authorize?response_type=code&client_id={0}&redirect_uri={1}&type={2}", this.AppKey, this.CallbackUrl, "sina");
            }
        }

        private string TokenUrl
        {
            get
            {
                return "https://api.weibo.com/oauth2/access_token";
            }
        }

        public string AppKey
        {
            get
            {
                return "2629489548";
            }
        }

        public string AppSercet
        {
            get
            {
                return "f77f06a87d46a9803759b8b29b77f8aa";
            }
        }

        public string CallbackUrl
        {
            get
            {
                return "http://192.168.2.134:81/login/OAuthReturn?type=sina";
            }
        }

        private string UserInfoUrl = "https://api.weibo.com/2/users/show.json?access_token={0}&uid={1}";

        public bool Authorize(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string result = this.GetToken("POST", code); // {"access_token":"2.004JqI7Ck2ExrC299b8f6030VMFg6E","remind_in":"157679999","expires_in":157679999,"uid":"2218557493"}

                // �ֽ�result;
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
                        var wc = new WebClient { Encoding = Encoding.UTF8 };
                        wc.Headers.Add("Pragma", "no-cache");
                        if (!string.IsNullOrEmpty(this.token) && !string.IsNullOrEmpty(this.openID))
                        {
                            // ��ȡ΢���ǳƺ�ͷ��
                            result = wc.DownloadString(string.Format(this.UserInfoUrl, this.token, this.openID));
 
                            // ���أ�/*{"id":2218557493,"idstr":"2218557493","class":1,"screen_name":"����","name":"����","province":"31","city":"12","location":"�Ϻ� ������","description":"","url":"","profile_image_url":"http://tp2.sinaimg.cn/2218557493/50/0/1","profile_url":"u/2218557493","domain":"","weihao":"","gender":"m","followers_count":16,"friends_count":22,"statuses_count":94,"favourites_count":0,"created_at":"Tue Jul 05 16:27:53 +0800 2011","following":false,"allow_all_act_msg":false,"geo_enabled":true,"verified":false,"verified_type":-1,"remark":"","status":{"created_at":"Fri Jan 24 00:01:03 +0800 2014","id":3670055428577660,"mid":"3670055428577660","idstr":"3670055428577660","text":"��˯����ƻ���ʧ���ˣ�����ӡ�","source":"<a href=\"http://app.weibo.com/t/feed/48iM0v\" rel=\"nofollow\">��Ҫ��ѧ��</a>","favorited":false,"truncated":false,"in_reply_to_status_id":"","in_reply_to_user_id":"","in_reply_to_screen_name":"","pic_urls":[],"geo":null,"reposts_count":0,"comments_count":0,"attitudes_count":0,"mlevel":0,"visible":{"type":0,"list_id":0}},"ptype":0,"allow_all_comment":true,"avatar_large":"http://tp2.sinaimg.cn/2218557493/180/0/1","avatar_hd":"http://tp2.sinaimg.cn/2218557493/180/0/1","verified_reason":"","follow_me":false,"online_status":0,"bi_followers_count":0,"lang":"zh-cn","star":0,"mbtype":0,"mbrank":0,"block_word":0} */
                            if (!string.IsNullOrEmpty(result)) 
                            {
                                data = serializer.Deserialize<object>(result);
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
