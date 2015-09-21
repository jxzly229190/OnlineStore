using V5.Library;
using System.Collections.Specialized;

namespace V5.Portal.Models
{
    using System.Collections.Generic;
    using System.Text;

    public class SearchFilterModel
    {
        private NameValueCollection _filter;
        private string[] _sort;
        private Dictionary<string, string> _key; //注册键值

        public SearchFilterModel(NameValueCollection filter)
        {
            _key = new Dictionary<string, string>();
            _filter = filter;
        }

        /// <summary>
        /// 过滤关键词（注意关键词范围：a0-z9）
        /// </summary>
        public Dictionary<string,string> Key
        {
            get
            {
                if (_key == null)
                {
                    _key = new Dictionary<string, string>(); 

                    _key.Add("sort", "sort");        //排序方式（综合、人气、销量、价格）

                }

                return _key;
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return GetValue(key, null);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public string GetValue(string key, string[] arr)
        {
            if (string.IsNullOrEmpty(key)) return "";

            string fvalue = _filter.Get(Key[key]);
            if (string.IsNullOrEmpty(fvalue)) return "";

            if (Utils.IsArray(arr))
            {
                return Utils.InArray(arr, fvalue) ? fvalue : "";
            }

            return fvalue;
        }

        /// <summary>
        /// 生成SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="fvalue"></param>
        /// <returns></returns>
        public string Sql(string sql, string fvalue)
        {
            return string.IsNullOrEmpty(sql) ? "" : Utils.ToString(sql) + " and " + Utils.ToString(fvalue);
        }

        /// <summary>
        /// 排序方式（综合、人气、销量、价格）
        /// </summary>
        public string Sort
        {
            get
            {
                string fvalue = GetValue("sort", new string[] { "zh", "rq", "xl", "jp1", "jg2" });
                string forder = string.Empty;
                switch (fvalue)
                {
                    case "zh":
                        forder = "order by PageView";
                        break;
                    case "rq":
                        forder = "order by PageView";
                        break;
                    case "xl":
                        forder = "order by SoldOfVirtual";
                        break;
                    case "jg1":
                        forder = "order by GoujiuPrice desc";
                        break;
                    case "jg2":
                        forder = "order by GoujiuPrice asc";
                        break;
                }

                return string.IsNullOrEmpty(forder) ? "" : forder;
            }
        }
        
    }
}