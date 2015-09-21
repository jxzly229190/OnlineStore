using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.JScript;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace V5.Library
{
    using System.Web;
    using System.Xml.Serialization;
    using System.Xml;

    public static class Utils
    {
        /// <summary>
        /// 获取主机名称
        /// </summary>
        public static string HostName
        {
            get
            {
                return System.Net.Dns.GetHostName();
            }
        }

        public static string GetDiskPath
        {
            get
            {
                return ToString(HttpContext.Current.Server.MapPath("~"));
            }
        }

        /// <summary>
        /// 获取前台站点地址
        /// </summary>
        public static string GetWebSiteUrl
        {
            get
            {
                return ToString(System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"]);
            }
        }

        /// <summary>
        /// 获取内网访问地址 add by yaosy 2014-04-03 10：32
        /// </summary>
        public static string WebSiteLocalUrl
        {
            get
            {
                return ToString(System.Configuration.ConfigurationManager.AppSettings["WebSiteLocalUrl"]);
            }
        }
        
        /// <summary>
        /// 不支持配送区域
        /// </summary>
        public static string UnsupportRegion
        {
            get
            {
                return GetAppSettings("UnsupportRegion");
            }
        }

		/// <summary>
		/// 是否推送ERP
		/// </summary>
	    public static bool IsPushERP
	    {
		    get
		    {
			    var isPushERP = ToString(System.Configuration.ConfigurationManager.AppSettings["IsPushERP"]);
				if (!string.IsNullOrWhiteSpace(isPushERP) && isPushERP=="false")
				{
					return false;
				}
			    return true;
		    }
	    }

        /// <summary>
        /// 获取图片服务器地址
        /// </summary>
        public static string ImgSiteUrl
        {
            get
            {
                return ToString(System.Configuration.ConfigurationManager.AppSettings["ImgSiteUrl"]);
            }
        }

        /// <summary>
        /// 读取appSettings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            return string.IsNullOrEmpty(key) ? "" : ToString(System.Configuration.ConfigurationManager.AppSettings[key]);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToString(object result)
        {
            return ToString(result, "");
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="result"></param>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        public static string ToString(object result, string strDefault)
        {
            return result == null ? strDefault : result.ToString();
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="result"></param>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        public static string ToNullOrEmptyString(object result, string strDefault)
        {
            return string.IsNullOrEmpty(ToString(result)) ? strDefault : result.ToString();
        }

        /// <summary>
        /// 转换为数字
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int ToInteger(object result)
        {
            return ToInteger(result, 0);
        }

        /// <summary>
        /// 转换为数字
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int ToInteger(object result, int intDefault)
        {
            return IsInteger(result) ? System.Convert.ToInt32(result) : intDefault;
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInteger(object obj)
        {
            Regex rex = new Regex(@"^\d+$");
            return rex.IsMatch(ToString(obj));
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string UnEscape(object result)
        {
            return GlobalObject.unescape(ToString(result));
        }

        /// <summary>
        /// 加码
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string Escape(object result)
        {
            return GlobalObject.escape(ToString(result));
        }

        /// <summary>
        /// 获取时间毫秒数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long UnixTicks(this DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1); DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return (long)ts.TotalMilliseconds;
        }
        
        /// <summary>
        /// 判断数组是否为空（字符串）
        /// </summary>
        /// <returns></returns>
        public static bool IsEmptyArray(string[] array)
        {
            return array == null || array.Length == 0 ? true : false;
        }

        /// <summary>
        /// 判断数组是否为空（字符）
        /// </summary>
        /// <returns></returns>
        public static bool IsEmptyArray(char[] array)
        {
            return array == null || array.Length == 0 ? true : false;
        }

        /// <summary>
        /// 获取数组值（字符串）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetArrayValue(string[] array, int index)
        {
            if (IsEmptyArray(array)) return "";
            if (array.Length <= index) return "";

            return array[index];
        }

        /// <summary>
        /// 获取数组值（字符）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static char GetArrayValue(char[] array, int index)
        {
            if (IsEmptyArray(array)) return '\0';
            if (array.Length <= index) return '\0';

            return array[index];
        }

        /// <summary>
        /// 获取数组值（字符）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetArrayValueString(char[] array, int index)
        {
            if (IsEmptyArray(array)) return "";
            if (array.Length <= index) return "";

            return array[index].ToString();
        }

        /// <summary>
        /// 判断数组中是否包含了该元素
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool InArray(string[] array, string value)
        {
            return Array.IndexOf(array, value) == -1 ? false : true;
        }

        /// <summary>
        /// 判断是否为数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsArray(string[] array)
        {
            return array == null ? false : array.GetType().IsArray;
        }

        /// <summary>
        /// 判断是否为数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsArray(int[] array)
        {
            return array == null ? false : array.GetType().IsArray;
        }

        /// <summary>
        /// 获取产品图片路径
        /// </summary>
        /// <param name="thumbnailPath"></param>
        /// <returns></returns>
        public static string GetAdvertiseImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            return ImgSiteUrl + path;
        }

        /// <summary>
        /// 获取产品图片路径
        /// </summary>
        /// <param name="thumbnailPath"></param>
        /// <returns></returns>
        public static string GetProductImage(string path)
        {
            return GetProductImage(path, null);
        }

        /// <summary>
        /// 获取产品图片路径
        /// </summary>
        /// <param name="thumbnailPath"></param>
        /// <returns></returns>
        public static string GetProductImage(string path, string num)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            return ImgSiteUrl + (!IsInteger(num) ? path : path.ToLower().Replace(".jpg", "_" + ToString(num) + ".jpg"));
        }

        /// <summary>
        /// 获取产品地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetProductUrl(int id)
        {
            return !IsInteger(id) ? "" : GetWebSiteUrl + "/Product/Item-id-" + ToString(id) + ".htm";
        }

        /// <summary>
        /// 替换添加
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Append(string target, string source, string value)
        {
            if (string.IsNullOrEmpty(target)) return "";
            if (string.IsNullOrEmpty(source)) return target + value;
            return target.Contains(source) ? target.Replace(source, value) : target + value;
        }

        /// <summary>
        /// 相等则返回C
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EqualReturn(string a, string b, string c)
        {
            return a == b ? c : "";
        }

        /// <summary>
        /// 添加软加载
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string AppendLazy(string html)
        {
            return AppendLazy(html, "img");
        }

        /// <summary>
        /// 添加软加载
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string AppendLazy(string html, string type)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<" + type + @"\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(html);

            int i = 0;
            string img = string.Empty, url = string.Empty, url_new = string.Empty;

            // 取得匹配项列表
            foreach (Match match in matches)
            {
                img = match.Value;
                url = match.Groups["imgUrl"].Value;
                if (!url.StartsWith("http"))
                {
                    url_new = url.StartsWith("/upload@@") ? url.Replace("/upload@@", "") : url;
                    img = img.Replace(url, Utils.ImgSiteUrl + url_new);
                }
                if (type == "input")
                {
                    img = img.Replace("<" + type + " ", "<img ");
                }
                html = html.Replace(match.Value, img);
            }

            return html;
        }

        /// <summary>
        /// 添加软加载
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string GetNewImageTag(Match match, string type)
        {
            string matchValue = match.Value;

            try
            {
                //从image 标签中匹配出URL <img src="http://img03.taobaocdn.com/imgextra/i3/692195348/T2yTGZXm0XXXXXXXXX_!!692195348.jpg">
                //标记
                var mark = "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm";
                //整个Img标签
                var img = match.Value;
                //检查是否有class属性
                Match matchImgClass = Regex.Match(img, @"class[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgClass>[^\s\t\r\n""'<>]*)");
                var imgClass = matchImgClass.Groups["imgClass"].Value;
                //Img标签的 src属性
                var url = match.Groups["imgUrl"].Value;
                string imgStr = string.Empty;
                imgStr += "<img ";
                //如果有class
                if (!string.IsNullOrWhiteSpace(imgClass))
                {
                    matchValue = matchValue.Replace(imgClass, imgClass + " lazy img-responsive");
                }
                else
                {
                    imgStr += " class=\"lazy\" ";
                }
                imgStr += " data-original=\"" + mark + "\" ";

                matchValue = matchValue.Replace("<" + type + " ", imgStr);
                matchValue = matchValue.Replace(url, "");
                if (!url.StartsWith("http"))
                {
                    url = url.StartsWith("/upload@@") ? url.Replace("/upload@@", "") : url;
                }
                url = url.StartsWith("http") ? url : Utils.ImgSiteUrl + url;
                matchValue = matchValue.Replace(mark, url);
                //matchValue += "<noscript>" + img + "</noscript>";
            }
            catch
            {

            }
            return matchValue;
        }

        /// <summary>
        /// 想客户端推送消息
        /// </summary>
        /// <returns></returns>
        public static void SendMessage(HttpResponseBase response,string message)
        {
            SendMessage(response, message, false);
        }

        /// <summary>
        /// 想客户端推送消息
        /// </summary>
        /// <returns></returns>
        public static void SendMessage(HttpResponseBase response, string message, bool close)
        {
            if (response == null) return;

            response.Write("【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + message + "<br/>");
            response.Flush();
            if (close)
            {
                response.Close();
            }
        }

        /// <summary>
        /// 创建目录.
        /// </summary>
        /// <param name="rootPath">
        /// 图片保存路径.
        /// </param>
        /// <param name="savePath">
        /// 数据库保存地址.
        /// </param>
        /// <returns>
        /// 图片路径.
        /// </returns>
        public static string CreateDirectory(string rootPath, out string savePath)
        {
            var path = rootPath + DateTime.Today.ToString("yyyy") + "\\" + DateTime.Today.ToString("MMdd") + "\\";
            savePath = DateTime.Today.ToString("yyyy") + "/" + DateTime.Today.ToString("MMdd") + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// 对象到XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetXmlFromObj<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(obj.GetType());
                xz.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <returns></returns>
        public static string ReadFile(string filename)
        {
            string file = HttpContext.Current.Server.MapPath("~") + filename;
            return File.Exists(file) ? File.ReadAllText(file) : "";
        }
    }
}
