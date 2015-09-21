namespace V5.Portal.Models
{
    using System.Web;
    using System;
    using V5.Library;

    public class UserSessionManager
    {
        /// <summary>
        /// 获取Session编号.
        /// </summary>
        public static string SessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return !(UserID == 0);
            }
        }

        /// <summary>
        /// 获取或设置会员编号.
        /// </summary>
        public static int UserID
        {
            get
            {
                return Utils.ToInteger(Get("UserID"), 0);
            }

            set
            {
                Set("UserID", value);
            }
        }

        /// <summary>
        /// 获取或设置登录成功后显示的名称（昵称-登录名-邮箱-手机）
        /// </summary>
        public static string ShowName
        {
            get
            {
                return Utils.ToString(Get("ShowName"));
            }

            set
            {
                Set("ShowName", value);
            }
        }

        /// <summary>
        /// 获取或设置登录名称
        /// </summary>
        public static string LoginName
        {
            get
            {
                return Utils.ToString(Get("LoginName"));
            }

            set
            {
                Set("LoginName", value);
            }
        }

        /// <summary>
        /// 获取或设置用户姓名
        /// </summary>
        public static string Name
        {
            get
            {
                return Utils.ToString(Get("Name"));
            }

            set
            {
                Set("Name", value);
            }
        }

        /// <summary>
        /// 获取或设置会员登录失败次数.
        /// </summary>
        public static int LoginErrCount
        {
            get
            {
                return Utils.ToInteger(Get("LoginErrCount"),0);
            }

            set
            {
                Set("LoginErrCount", value);
            }
        }

        /// <summary>
        /// 获取或设置会员登录时间.
        /// </summary>
        public static DateTime LoginTime
        {
            get
            {
                return Convert.ToDateTime(Get("LoginTime"));
            }

            set
            {
                Set("LoginTime", value);
            }
        }

        /// <summary>
        /// 获取或设置扩展字段
        /// </summary>
        public static string ExtObject
        {
            get
            {
                return Utils.ToString(Get("ExtObject"));
            }

            set
            {
                Set("ExtObject", value);
            }
        }

        /// <summary>
        /// 获取或设置验证码
        /// </summary>
        public static string SecurityCode
        {
            get
            {
                return Utils.ToString(Get("SecurityCode"));
            }

            set
            {
                Set("SecurityCode", value);
            }
        }

        /// <summary>
        /// UserSession对象
        /// </summary>
        /// <returns></returns>
        public static UserSession UserSession
        {
            get
            {
                return new UserSession()
                {
                    SessionId = SessionID,
                    VisitorKey = SessionID,
                    UserID = UserID
                };
            }
        }

        /// <summary>
        /// 获取Session值
        /// </summary>
        /// <returns></returns>
        public static object Get(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 设置Session值
        /// </summary>
        public static void Set(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
        }

        /// <summary>
        /// 移除Session值
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }
                
        /// <summary>
        /// 初始化Session 参数
        /// </summary>
        public static void Start()
        {
            SecurityCode = string.Empty;
            UserID = 0;
            ShowName = string.Empty;
            LoginName = string.Empty;
            Name = string.Empty;
            ExtObject = string.Empty;
        }
    }
}