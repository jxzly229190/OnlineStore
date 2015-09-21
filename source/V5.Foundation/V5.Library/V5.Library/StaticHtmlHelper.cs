namespace V5.Library
{
    using System.IO;
    using System.Net;
    using System;
    using System.Web;

    public static class StaticHtmlHelper
    {
        /// <summary>
        /// The get html.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public static void Refresh<T>(T[] ids, QueryType type)
        {
            Refresh<T>(ids, type, null);
        }

        /// <summary>
        /// The get html.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public static void Refresh<T>(T[] ids, QueryType type, HttpResponseBase response)
        {
            if (ids == null || ids.Length == 0) return;

            var wc = new WebClient();
            //string url = Utils.GetWebSiteUrl;
            string url = Utils.WebSiteLocalUrl;//获取内网访问地址 add by yaosy 2014-04-03 10：32
            string link = string.Empty;
            switch (type)
            {
                case QueryType.Product:
                    url += "/Product/item-id-";
                    break;
                case QueryType.Team:
                    url += "/Home/TuanItem/";
                    break;
                case QueryType.LP:
                    url += "/LandingPage/";
                    break;
                case QueryType.Acticle:
                    url += "/Acticle/";
                    break;
                case QueryType.Help:
                    url += "/Help/";
                    break;
                case QueryType.Home:
                    url += "/";
                    break;
                case QueryType.Brand:
                    url += "/";
                    break;
            }
            for (int i = 0; i < ids.Length; i++)
            {
                link = url + Utils.ToString(ids[i]) + ".htm";
                try
                {
                    Utils.SendMessage(response, "开始生成静态页," + link + "...");
                    wc.DownloadString(url + Utils.ToString(ids[i]) + ".htm");
                }
                catch (Exception ex)
                {
                    Utils.SendMessage(response, "生成文件【" + link + "】异常...,异常信息：" + ex.Message + "...");
                }
            }
        }

        public static void Remove(int id, QueryType type)
        {
            var path = Utils.GetDiskPath;
            var url = "";
            switch (type)
            {
                case QueryType.Product:
                    url += "/Product/item-id-" + id + ".htm";
                    break;
                case QueryType.Team:
                    url += "/Home/TuanItem/" + id + ".htm";
                    break;
                case QueryType.LP:
                    url += "/LandingPage/" + id + ".htm";
                    break;
                case QueryType.Acticle:
                    url += "/Acticle/" + id + ".htm";
                    break;
                case QueryType.Help:
                    url += "/Help/" + id + ".htm";
                    break;
            }

            if (File.Exists(path+url))
            {
                File.Delete(path + url);
            }
        }
        
        /// <summary>
        /// The get html.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public static void RefreshHome()
        {
            var url1 = Utils.GetWebSiteUrl + "/Home/RemoveIndex";
            var url2 = Utils.GetWebSiteUrl + "/Home/Index";
            var wc = new WebClient();

            try
            {
                wc.DownloadString(url1);
                wc.DownloadString(url2);
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static void RemoveHome()
        {
            var url = Utils.GetDiskPath + "index.htm";

            if (File.Exists(url))
            {
                File.Delete(url);
            }
        }
    }

    /// <summary>
    /// The result type.
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// 商品
        /// </summary>
        Product,

        /// <summary>
        /// 团购商品
        /// </summary>
        Team, 

        /// <summary>
        /// 文章
        /// </summary>
        Acticle,

        /// <summary>
        /// Lp 活动页
        /// </summary>
        LP,

        /// <summary>
        /// 帮助
        /// </summary>
        Help,

        /// <summary>
        /// 主页
        /// </summary>
        Home,

        /// <summary>
        /// 品牌
        /// </summary>
        Brand
    }
}
