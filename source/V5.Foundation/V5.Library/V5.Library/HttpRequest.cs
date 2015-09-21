namespace V5.Library
{
    using System.IO;
    using System.Net;

    public static class HttpRequest
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
        public static void GetHtml(int id, ResultType type)
        {
            var wc = new WebClient();
            var url1 = Utils.GetWebSiteUrl;
            var url2 = url1;
            switch (type)
            {
                case ResultType.Product:
                    url1 += "/Product/item/" + id;
                    url2 += "/Product/item-id-" + id + ".htm";
                    break;
                case ResultType.Team:
                    url1 += "/Home/TuanItem/" + id;
                    url2 += "/Home/TuanItem/" + id + ".htm";
                    break;
                case ResultType.LP:
                    url1 += "/LandingPage/" + id;
                    url2 += "/LandingPage/" + id + ".htm";
                    break;
                case ResultType.Acticle:
                    url1 += "/Acticle/" + id;
                    url2 += "/Acticle/" + id + ".htm";
                    break;
                case ResultType.Help:
                    url1 += "/Help/" + id;
                    url2 += "/Help/" + id + ".htm";
                    break;
            }

            wc.DownloadString(url1);
            wc.DownloadString(url2);
        }

        public static void RemoveHtml(int id, ResultType type)
        {
            var path = Utils.GetDiskPath;
            var url = "";
            switch (type)
            {
                case ResultType.Product:
                    url += "/Product/item-id-" + id + ".htm";
                    break;
                case ResultType.Team:
                    url += "/Home/TuanItem/" + id + ".htm";
                    break;
                case ResultType.LP:
                    url += "/LandingPage/" + id + ".htm";
                    break;
                case ResultType.Acticle:
                    url += "/Acticle/" + id + ".htm";
                    break;
                case ResultType.Help:
                    url += "/Help/" + id + ".htm";
                    break;
            }

            if (File.Exists(path+url))
            {
                File.Delete(path + url);
            }
        }
    }

    /// <summary>
    /// The result type.
    /// </summary>
    public enum ResultType
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
    }
}
