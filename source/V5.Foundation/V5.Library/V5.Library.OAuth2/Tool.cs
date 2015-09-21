// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tool.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   DynamicJsonConverter 类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library.OAuth2
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using System.Web.Script.Serialization;
    
    #region JSON序列化

    /// <summary>
    /// DynamicJsonConverter  类 
    /// </summary>
    public class DynamicJsonConverter : JavaScriptConverter
    {
        #region

        public override object Deserialize(
            IDictionary<string, object> dictionary,
            Type type,
            JavaScriptSerializer serializer)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new List<Type>(new[] { typeof(object) }));
            }
        }

        #endregion
    }



    /// <summary>
    /// DynamicJsonObject 类
    /// </summary>
    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
            {
                result =
                    new List<DynamicJsonObject>(
                        (result as ArrayList).ToArray()
                            .Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }
    }

    #endregion

    class OperateString
    {
        /// <summary>
        /// 截取参数,取不到值时返回""
        /// </summary>
        /// <param name="s">
        /// 不带?号的url参数
        /// </param>
        /// <param name="para">
        /// 要取的参数
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string QueryString(string s, string para)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            s = s.Trim('?').Replace("%26", "&").Replace('?', '&');
            int num = s.Length;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = s[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }

                    i++;
                }

                if (num4 >= 0)
                {
                    string str = s.Substring(startIndex, num4 - startIndex);
                    string str2 = s.Substring(num4 + 1, (i - num4) - 1);
                    if (str == para)
                    {
                        return System.Web.HttpUtility.UrlDecode(str2);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取Json string某节点的值。
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="key">参数项</param>
        /// <returns>该参数项的值</returns>
        public static string GetJosnValue(string json, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(json))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = json.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    // 先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = json.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = json.IndexOf('}', index);
                    }

                    // index = json.IndexOf('"', index + key.Length + 1) + 1;
                    result = json.Substring(index, end - index);

                    // 过滤引号或空格
                    result = result.Trim(new char[] { '"', ' ', '\'' });
                }
            }

            return result;
        }
    }
}
