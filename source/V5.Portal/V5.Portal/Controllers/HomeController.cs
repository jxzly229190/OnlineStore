namespace V5.Portal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using V5.DataContract.Advertise;
    using V5.DataContract.Configuration;
    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Library.Storage.DB;
    using V5.Portal.Filters;
    using V5.Portal.Models;
    using V5.Service.Advertise;
    using V5.Service.Configuration;
    using V5.Service.Product;
    using V5.Service.Promote;
    using V5.Service.Transact;
    using V5.Service.User;
    using V5.DataContract.Transact;

    public class HomeController: Controller
    {
        public int pageCount, totalCount;
        
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 120)]
        [StaticFileWriteFilter]
        public ActionResult Index()
        {
            Response.Cache.SetOmitVaryStar(true);
            return this.View();
        }

        /// <summary>
        /// 小酒版.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Quan()
        {
            return this.View();
        }

        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [OutputCache(Duration = 600)]
        [StaticFileWriteFilter]
        public ActionResult Help(int id)
        {
            Config_Page result = new ConfigPageService().QueryByID(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }

            Response.Cache.SetOmitVaryStar(true);
            ViewBag.ID = id;
            return this.View(result);
        }

        /// <summary>
        /// LP管理
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [OutputCache(Duration = 600)]
        [StaticFileWriteFilter]
        public ActionResult LandingPage(int id)
        {
            Promote_LandingPage result = new PromoteLandingPageService().Query(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }

            Response.Cache.SetOmitVaryStar(true);
            return this.View(result);
        }

        /// <summary>
        /// 文章管理
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 600)]
        [StaticFileWriteFilter]
        public ActionResult Article(int id)
        {
            Config_Page result = new ConfigPageService().QueryByID(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }

            Response.Cache.SetOmitVaryStar(true);
            return this.View(result);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            //string keyword = Utils.ToString(Request.Params["w"]);
            //string brand = Utils.ToString(Request.Params["brand"]);
            //if (StringHelper.CheckBadStr(new string[] { keyword, brand }))
            //{
            //    return this.RedirectToAction("HttpError404", "Utility");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }

        /// <summary>
        /// 团购
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 300)]
        public ActionResult Tuan()
        {
            Response.Cache.SetOmitVaryStar(true);
            return View();
        }

        /// <summary>
        /// 团购明细
        /// </summary>
        /// <param name="id">商品编号</param>
        /// <returns>视图</returns>
        [OutputCache(Duration = 300)]
        public ActionResult TuanItem(int id)
        {
            Response.Cache.SetOmitVaryStar(true);
            ProductSearchResult result = new ProductService().QueryByID(id);

            if (result == null)
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }

            result.Introduce = Utils.AppendLazy(result.Introduce);
            result.Introduce = Utils.AppendLazy(result.Introduce, "input");
            ViewBag.ID = id;

            var cart = new OrderBillServices().QueryCartProduct(result.ID);
            if (cart != null)
            {
                result.GoujiuPrice = cart.PromotePrice;
            }

            result.ThumbnailPath = Utils.GetProductImage(result.Path, "2");

            return this.View(result);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="type">
        /// 1：刷新商品，2：刷新城市.
        /// </param>
        /// <returns>
        /// 成功返回1，否者返回空字符
        /// </returns>
        public string RefreshCache(string type)
        {
            string result = string.Empty;

            switch (type)
            {
                case "1": //刷新产品信息
                    Refresh.Product();
                    result = "1";
                    break;
                case "2": //刷新城市信息
                    Refresh.Area();
                    result = "1";
                    break;
                case "3": //所有页面
                    Refresh.RefreshHtml(Response);
                    result = "1";
                    break;
                case "4": //商品品牌
                    Refresh.ProductBrand();
                    result = "1";
                    break;
                case "5": //商品搜索
                    Refresh.ProductSearch();
                    result = "1";
                    break;
                case "6": //商品评论
                    Refresh.Comment();
                    result = "1";
                    break;
                case "7": //商品评论回复
                    Refresh.CommentReply();
                    result = "1";
                    break;
                case "8": //商品咨询
                    Refresh.Consults();
                    result = "1";
                    break;
            }

            return result;
        }

        /// <summary>
        /// 刷新HTML
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RefreshHtml(string type, string id)
        {
            type = Utils.ToString(type);
            id = Utils.ToString(id);

            string path = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                switch (type)
                {
                    case "product":
                        path = "product\\item-id-" + id + ".htm";
                        break;
                    case "lp":
                        path = "home\\landingpage\\" + id + ".html";
                        break;
                    case "article":
                        path = "home\\article\\" + id + ".html";
                        break;
                    case "tuan":
                        path = "home\\tuanitem\\" + id + ".html";
                        break;
                    case "help":
                        path = "home\\help\\" + id + ".html";
                        break;
                    case "brand":
                        path = id.EndsWith(".htm") ? id : "";
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "product":
                        path = "product";
                        break;
                    case "article":
                        path = "home\\article";
                        break;
                    case "help":
                        path = "home\\help";
                        break;
                    case "lp":
                        path = "home\\landingpage";
                        break;
                    case "tuan":
                        path = "home\\tuanitem";
                        break;
                    case "index":
                        path = "index.htm";
                        break;
                }
            }

            string result = "删除失败";
            if (!string.IsNullOrEmpty(path))
            {
                Refresh.ClearHtml(path, Response);
                result = "删除成功";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public JsonResult ReadCache(string type)
        {
            string result = string.Empty;
            var obj = new object();
            switch (type)
            {
                case "1": //刷新产品信息
                    obj = Refresh.GetCache<Product_Cache>(CacheType.Product, true);
                    result = "1";
                    break;
                case "4": //商品品牌
                    obj = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
                    result = "1";
                    break;
                case "5": //商品搜索
                    obj = Refresh.GetCache<ProductSearch>(CacheType.ProductSearch, true);
                    result = "1";
                    break;
                case "6": //商品评论
                    obj = Refresh.GetCache<Product_Comment>(CacheType.ProductSearch, true);
                    result = "1";
                    break;
                case "7": //商品评论回复
                    obj = Refresh.GetCache<Product_Comment_Reply>(CacheType.ProductSearch, true);
                    result = "1";
                    break;
                case "8": //商品咨询
                    obj = Refresh.GetCache<Product_Consult>(CacheType.ProductSearch, true);
                    result = "1";
                    break;
            }

            return this.Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 刷新指定商品的缓存.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public JsonResult RefreshProductByID(int productID)
        {
            try
            {
                var products = Refresh.GetCache<Product_Cache>(CacheType.Product, true);
                if (products != null)
                {
                    var cartProdcut = products.Find(p => p.ProductID == productID);
                    products.Remove(cartProdcut);
                    cartProdcut = new OrderBillServices().QueryCartProductFromDB(productID);
                    products.Add(cartProdcut);
                    Refresh.WriteCache(products);
                }
                else
                {
                    Refresh.Product();
                }

                return this.Json(new AjaxResponse(1, "刷新成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult RefreshUserCart()
        {
            try
            {
                MongoDBHelper.RemoveModel<UserCartModel>(u => u.UserId >= 0);
                return this.Json("成功");
            }
            catch (Exception exception)
            {
                return this.Json(exception.Message);
            }
        }
        
        /// <summary>
        /// 搜索建议
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchSuggest(string search)
        {
            List<ProductSearchSuggest> list = new ProductService().SearchSuggest(search);
            if (list == null || list.Count == 0) return null;
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 移除首页静态页面
        /// </summary>
        public void RemoveIndex()
        {
            StaticHtmlHelper.RemoveHome();
        }

        /// <summary>
        /// 页面访问量
        /// </summary>
        /// <returns></returns>
        public void PageView()
        {
            return;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="count"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string GetArticleList(int count, string condition)
        {
            var paging = new Paging("[Config_Page]", new List<string> { "ID", "Name" }, "ID", condition, 1, count, "ID", 1);
            int pageCount, totalCount;
            List<Config_Page> list = new ConfigPageService().Paging(paging, out pageCount, out totalCount);
            if (list == null || list.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            foreach (Config_Page page in list)
            {
                sb.Append("<li><i></i><a href=\"/Home/Article/" + page.ID.ToString() + ".html\" target=\"_blank\">" + page.Name + "</a></li>");
            }
            return sb.ToString();
        }
        
        public string GetHtml(string htmlTemplete, int count, ProductType productType)
        {
            return this.GetHtml(htmlTemplete, count, productType, "");
        }

        public string GetHtml(string htmlTemplete, int count, ProductType productType, string brandCategory)
        {
            if (count == 0)
            {
                return "";
            }
            if (htmlTemplete == null || htmlTemplete == "")
            {
                return "";
            }

            List<int> list = GetProductSearchResult(count, productType, brandCategory, "");
            if (list == null || list.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            var cartParductList = new OrderBillServices().QueryCartProduct(list.ToArray());
            foreach (var cartProduct in cartParductList)
            {
                sb.Append(GetHtmlByTemplete(htmlTemplete, cartProduct));
            }
            return sb.ToString();
        }
        
        public string GetHtml(string htmlTemplete, int count, ProductType productType, NameValueCollection nv)
        {
            if (htmlTemplete == null || htmlTemplete == "") return "";

            List<int> list = GetProductSearchResultList(nv);
            if (list == null || list.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            var cartParductList = new OrderBillServices().QueryCartProduct(list.ToArray());
            foreach (var cartProduct in cartParductList)
            {
                sb.Append(GetHtmlByTemplete(htmlTemplete, cartProduct));
            }
            return sb.ToString();
        }

        public string GetPageNavHtml(NameValueCollection nv)
        {
            int pageIndex = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);

            string keyword = Utils.ToString(nv["w"]);
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);
            string sort = Utils.ToString(nv["sort"]);
            string desc = Utils.ToString(nv["desc"]);

            keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;
            brand = string.IsNullOrEmpty(brand) ? "" : "&brand=" + brand;
            price = string.IsNullOrEmpty(price) ? "" : "&price=" + price;
            sort = string.IsNullOrEmpty(sort) ? "" : "&sort=" + sort;
            desc = string.IsNullOrEmpty(desc) ? "" : "&desc=" + desc;

            var url = "/Home/Search?w=" + keyword + brand + price + sort + desc;

            StringBuilder sb = new StringBuilder();

            //首页
            if (pageIndex > 1)
            {
                sb.Append("<a class='ui-page-prev' href='$page_url_f$'>&lt;&lt;第一页</a>").Replace("$page_url_f$", url + "&p=1&s=" + pageSize);
            }
            else
            {
                sb.Append("<b class='ui-page-prev'>&lt;&lt;第一页</b>");
            }

            //中间页
            int p = pageIndex - 5 > 0 ? pageIndex - 5 : 1;
            int c = pageCount - pageIndex > 5 ? pageIndex + 5 : pageCount;
            for (; p <= c; p++)
            {
                if (p == pageIndex)
                {
                    sb.Append("<b  class='ui-page-cur'>");
                    sb.Append(p).Append("</b>");
                }
                else
                {
                    sb.Append("<a  href='").Append(url).Append("&p=").Append(p).Append("&s=").Append(pageSize).Append("'>");
                    sb.Append(p).Append("</a>");
                }
            }
            if (pageCount - pageIndex > 5)
            {
                sb.Append("<a  href='").Append(url).Append("&p=").Append(p).Append("&s=").Append(pageSize).Append("'>");
                sb.Append("...").Append("</a>");
                sb.Append("<a  href='").Append(url).Append("&p=").Append(pageCount - 2).Append("&s=").Append(pageSize).Append("'>");
                sb.Append(pageCount - 2).Append("</a>");
                sb.Append("<a  href='").Append(url).Append("&p=").Append(pageCount - 1).Append("&s=").Append(pageSize).Append("'>");
                sb.Append(pageCount - 1).Append("</a>");
                sb.Append("<a  href='").Append(url).Append("&p=").Append(pageCount).Append("&s=").Append(pageSize).Append("'>");
                sb.Append(pageCount).Append("</a>");
            }

            //尾页
            if (pageIndex < pageCount)
            {
                sb.Append("<a class='ui-page-next' href='$page_url_l$'>最后一页&gt;&gt;</a>").Replace("$page_url_l$", url + "&p=" + pageCount + "&s=" + pageSize);
            }
            else
            {
                sb.Append("<b class='ui-page-next'>&gt;&gt;最后一页</b>");
            }

            return sb.ToString();
        }

        public string GetPageNavS(NameValueCollection nv)
        {
            int currentPage = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);

            string keyword = Utils.ToString(nv["w"]);
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);

            keyword = string.IsNullOrEmpty(keyword) ? "" : "?w=" + keyword;
            brand = string.IsNullOrEmpty(brand) ? "" : "&brand=" + brand;
            price = string.IsNullOrEmpty(price) ? "" : "&price=" + price;

            var url = "/Home/Search?w=" + keyword + brand + price;

            //<b class="ui-page-s-count">相关商品7358件</b><b class="ui-page-s-len">1/50</b> <b title="上一页" class="ui-page-s-prev">&lt;</b> <a title="下一页"  class="ui-page-s-next" href="###" >&gt;</a>
            string bTag = "<b class='ui-page-s-count'>相关商品$totalCount$件</b><b class='ui-page-s-len'>$current$/$pageCount$</b>";

            bTag = bTag.Replace("$totalCount$", totalCount.ToString())
                .Replace("$current$", currentPage.ToString())
                .Replace("$pageCount$", pageCount.ToString());

            var sb = new StringBuilder();

            if (currentPage == 1)
            {
                sb.Append("<b title='上一页' class='ui-page-s-prev'>&lt;</b>")
                    .Append("<a title='下一页' class='ui-page-s-next'  href='")
                    .Append(url)
                    .Append("&p=")
                    .Append(currentPage + 1)
                    .Append("&s=")
                    .Append(pageSize)
                    .Append("'>");
                sb.Append("&gt;").Append("</a>");
            }
            else if (currentPage == pageCount)
            {
                sb.Append("<a class='ui-page-s-prev' title='上一页' href='")
                    .Append(url)
                    .Append("&p=")
                    .Append(currentPage - 1)
                    .Append("&s=")
                    .Append(pageSize)
                    .Append("'>");
                sb.Append("&lt").Append("</a>");
                sb.Append("<b title='上一页' class='ui-page-s-next'>&lt;</b>");
            }
            else
            {
                sb.Append("<a class='ui-page-s-prev' title='上一页' href='")
                    .Append(url)
                    .Append("&p=")
                    .Append(currentPage - 1)
                    .Append("&s=")
                    .Append(pageSize)
                    .Append("'>");
                sb.Append("&lt").Append("</a>");
                sb.Append("<a title='下一页' class='ui-page-s-next'  href='")
                    .Append(url)
                    .Append("&p=")
                    .Append(currentPage + 1)
                    .Append("&s=")
                    .Append(pageSize)
                    .Append("'>");
                sb.Append("&gt;").Append("</a>");
            }

            return bTag + sb.ToString();
        }
        
        public List<int> GetProductSearchResult(int count, ProductType productType, string brandCategory, string search)
        {
            string whereString = " [Status] = 2 " + (brandCategory == null || brandCategory == "" ? "" : " And ProductCategoryID = " + brandCategory);
            string searchWhereString = GetSearchWhereString(search);
            string order = productType == ProductType.Rand ? " newid() " : null;
            var paging = new Paging("view_Product_Info", new List<string> { "ID" }, "ID", whereString + searchWhereString, 1, count, order, 1);
            return new ProductService().QueryProductID(paging, out pageCount, out totalCount);
        }

        public List<int> GetProductSearchResultList(NameValueCollection nv)
        {
            string condition = GetSearchWhereString(nv);
            int pageIndex = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);
            string[] arrSort = { "PageView", "PageView", "SoldOfVirtual+SoldOfReality", "GoujiuPrice" };
            int sort = Utils.ToInteger(nv["sort"]);
            int desc = Utils.ToInteger(nv["desc"], 1);
            string order = sort >= arrSort.Length ? "" : arrSort[sort];
            var paging = new Paging("view_Product_Info", new List<string> { "ID" }, "ID", condition, pageIndex, pageSize, order, desc);

            //string condition = GetSearchWhereString(nv);
            //int pageIndex = Utils.ToInteger(nv["p"], 1);
            //int pageSize = Utils.ToInteger(nv["s"], 30);
            //var paging = new Paging("Product_Search", new List<string> { "ProductID" }, "ID", condition, pageIndex, pageSize);
            return new ProductService().QueryProductID(paging, out pageCount, out totalCount);
        }
        
        public string GetHtmlByTemplete(string htmlTemplete, ProductSearchResult product)
        {
            var properties = TypeDescriptor.GetProperties(product);

            if (htmlTemplete.Contains("$Path$"))
            {
                htmlTemplete = htmlTemplete.Replace("$Path$", Utils.GetProductImage(product.Path));
            }

            if (htmlTemplete.Contains("$ThumbnailPath$"))
            {
                htmlTemplete = htmlTemplete.Replace("$ThumbnailPath$", Utils.GetProductImage(product.Path, "1"));
            }

            if (htmlTemplete.Contains("$ThumbnailPath2$"))
            {
                htmlTemplete = htmlTemplete.Replace("$ThumbnailPath2$", Utils.GetProductImage(product.Path, "2"));
            }

            if (htmlTemplete.Contains("$Sold$"))
            {
                htmlTemplete = htmlTemplete.Replace("$Sold$", Utils.ToString(product.SoldOfReality + product.SoldOfVirtual));
            }
            
            var cart = new OrderBillServices().QueryCartProduct(product.ID);
            if (cart != null)
            {
                htmlTemplete = htmlTemplete.Replace("$GoujiuPrice$", Utils.ToString(cart.PromotePrice));
            }

            foreach (PropertyDescriptor propertyDescriptror in properties)
            {
                htmlTemplete = htmlTemplete.Replace("$" + propertyDescriptror.Name + "$", Utils.ToString(propertyDescriptror.GetValue(product)));
            }
            return htmlTemplete;
        }

        public string GetHtmlByTemplete(string htmlTemplete, Product_Cache cartProduct)
        {
            var properties = TypeDescriptor.GetProperties(cartProduct);
            htmlTemplete = htmlTemplete.Replace("$Path$", Utils.GetProductImage(cartProduct.Path));
            htmlTemplete = htmlTemplete.Replace("$ThumbnailPath$", Utils.GetProductImage(cartProduct.Path, "1"));
            htmlTemplete = htmlTemplete.Replace("$ThumbnailPath2$", Utils.GetProductImage(cartProduct.Path, "2"));
            htmlTemplete = htmlTemplete.Replace("$Sold$", Utils.ToString(cartProduct.SoldOfReality + cartProduct.SoldOfVirtual));
            htmlTemplete = htmlTemplete.Replace("$GoujiuPrice$", Utils.ToString(cartProduct.PromotePrice));

            foreach (PropertyDescriptor propertyDescriptror in properties)
            {
                htmlTemplete = htmlTemplete.Replace("$" + propertyDescriptror.Name + "$", Utils.ToString(propertyDescriptror.GetValue(cartProduct)));
            }
            return htmlTemplete;
        }

        public string GetSearchWhereString(NameValueCollection nv)
        {
            string keyword = Utils.UnEscape(nv["w"]);
            keyword = string.IsNullOrEmpty(keyword) ? "" : " And ProductSearchText Like '%" + keyword + "%'";

            string brand = Utils.ToString(nv["brand"]);
            brand = string.IsNullOrEmpty(brand) ? "" : " And ProductSearchText Like '" + brand + "%'";

            string price = Utils.ToString(nv["price"]);
            price = GetProductPrice(price);
            price = string.IsNullOrEmpty(price) ? "" : " And (" + price + ")";

            return " [ID] In( Select [ProductID] From [Product_Search] Where [Status] = 2 " + keyword + brand + price + " )";
        }

        public string GetSearchWhereString(string keyword)
        {
            keyword = string.IsNullOrEmpty(keyword) ? "" : " And ProductSearchText Like '%" + keyword + "%'";
            return " And [ID] In( Select [ProductID] From [Product_Search] Where 1=1 " + keyword + " )";
        }

        public string GetHtml_ProductPicture(string ID)
        {
            List<Product_Picture> list = new ProductPictureService().QueryByProductID(ID);
            if (list == null || list.Count == 0) return "";

            int i = 0;
            string src = Utils.GetProductImage(list[0].Path, "3");
            string bimg = Utils.GetProductImage(list[0].Path, "3");
            string limg = Utils.GetProductImage(list[0].Path, "4");

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"magnifier_bimg\">");
            sb.Append("<img alt=\"\" src=\"" + src + "\" bimg=\"" + bimg + "\" limg=\"" + limg + "\" id=\"mBigImg\">");
            sb.Append("<div id=\"divShowFlash\" style=\"display:none\">");
            sb.Append("<a id=\"aShowPic\" class=\"detailShow\" target=\"_blank\" href=\"#\"></a>");
            sb.Append("</div></div>");
            sb.Append("<a href=\"javascript:\" class=\"magnifier_larrow\"></a>");
            sb.Append("<div class=\"magnifier_simgbox\">");
            sb.Append("<ul style=\"width: 248px;\" class=\"magnifier_simglist fix\">");
            foreach (Product_Picture pic in list)
            {
                src = Utils.GetProductImage(pic.Path, "0");
                bimg = Utils.GetProductImage(pic.Path, "3");
                limg = Utils.GetProductImage(pic.Path, "4");

                sb.Append("<li class=\"magnifier_simg\"><a href=\"javascript:\"><img src=\"" + src + "\" bimg=\"" + bimg + "\" limg=\"" + limg + "\" id=\"mSmallImg" + i.ToString() + "\"></a></li>");
                i++;
            }
            sb.Append("</ul></div><a href=\"javascript:\" class=\"magnifier_rarrow\"></a>");
            return sb.ToString();
        }

        public string GetHtml_ProductPicture_List(string ID)
        {
            List<Product_Picture> list = new ProductPictureService().QueryByProductID(ID);
            if (list == null || list.Count == 0) return "";

            string src = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (Product_Picture pic in list)
            {
                src = Utils.GetProductImage(pic.Path);
                sb.Append("<img alt=\"\" src=\"" + src + "\">");
            }
            return sb.ToString();
        }

        public string GetProductPrice(string _price)
        {
            if (!string.IsNullOrEmpty(_price))
            {
                switch (_price)
                {
                    case "p7": //6000以上
                        _price = " GoujiuPrice >= 6000 ";
                        break;
                    case "p6": //2000-5999
                        _price = " GoujiuPrice >= 2000 And GoujiuPrice < 6000 ";
                        break;
                    case "p5": //1000-1999
                        _price = " GoujiuPrice >= 1000 And GoujiuPrice < 2000 ";
                        break;
                    case "p4": //600-999
                        _price = " GoujiuPrice >= 600 And GoujiuPrice < 1000 ";
                        break;
                    case "p3": //200-599
                        _price = " GoujiuPrice >= 200 And GoujiuPrice < 600  ";
                        break;
                    case "p2": //100-199
                        _price = " GoujiuPrice >= 100 And GoujiuPrice < 200  ";
                        break;
                    case "p1": //0-99
                        _price = " GoujiuPrice >= 1 And GoujiuPrice < 100  ";
                        break;
                    case "p0": //其他
                        _price = "";
                        break;
                }
            }

            return _price;
        }

        public string GetProductBrandSuggest(NameValueCollection nv)
        {
            string _keyword = Utils.UnEscape(nv["w"]);
            string _brand = Utils.ToString(nv["brand"]);

            List<ProductSearchSuggestTip> list = new ProductService().SearchSuggestTip(_keyword, _brand);
            if (list == null || list.Count == 0) return "";

            StringBuilder sb = new StringBuilder();

            string search = string.IsNullOrEmpty(_keyword) ? "" : "&w=" + Utils.Escape(_keyword);
            int count = 0;
            string item = string.Empty;
            foreach (var p in list)
            {
                item = "<a title=\"" + p.Name + "\" href=\"/Home/Search?brand=" + p.Search + search + "\">" + p.Name + "</a>";
                if (p.Type == "ProductBrand")
                {
                    count++;
                    if (count > 6) break;
                    sb.Append(item);
                }
            }

            return sb.ToString();
        }

        public string GetProductBrandSuggestTip(NameValueCollection nv)
        {
            string _condition = string.Empty;
            string _keyword = Utils.UnEscape(nv["w"]);
            string _brand = Utils.ToString(nv["brand"]);
            string _price = Utils.ToString(nv["price"]);

            _brand = string.IsNullOrEmpty(_brand) ? "" : "ProductSearchText Like '" + _brand + "%'";
            _price = GetProductPrice(_price);
            _condition += string.IsNullOrEmpty(_brand) ? " 1=1 " : _brand;
            _condition += string.IsNullOrEmpty(_price) ? "" : " and " + _price;

            List<ProductSearchSuggestTip> list = new ProductService().SearchSuggestTip(_keyword, _condition);
            if (list == null || list.Count == 0) return "";

            StringBuilder brand = new StringBuilder();
            StringBuilder category = new StringBuilder();
            StringBuilder price = new StringBuilder();
            StringBuilder parentbrand = new StringBuilder();

            string[] pricelist = new string[8];

            int brand_count = 0;
            int parent_brand_count = 0;

            _keyword = Utils.ToString(nv["w"]);
            _brand = Utils.ToString(nv["brand"]);
            _price = Utils.ToString(nv["price"]);

            string href = "/Home/Search?w=" + _keyword;
            string item = string.Empty;
            foreach (var p in list)
            {
                switch (p.Type)
                {
                    case "ProductCategory":
                        item = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + p.Search + "&price=" + _price) + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                        category.Append(item);
                        break;
                    case "ProductBrand":
                        item = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + p.Search + "&price=" + _price) + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                        brand_count++;
                        brand.Append(item);
                        break;
                    case "ParentBrand":
                        item = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + p.Search + "&price=" + _price) + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                        parent_brand_count++;
                        parentbrand.Append(item);
                        break;
                    case "GoujiuPrice":
                        switch (p.Name)
                        {
                            case "6000以上":
                                pricelist[7] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p7") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "2000-5999":
                                pricelist[6] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p6") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "1000-1999":
                                pricelist[5] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p5") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "600-999":
                                pricelist[4] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p4") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "200-599":
                                pricelist[3] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p3") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "100-199":
                                pricelist[2] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p2") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "0-99":
                                pricelist[1] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p1") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                            case "其他":
                                pricelist[0] = "<li><a title=\"" + p.Name + "\" href=\"" + (href + "&brand=" + _brand + "&price=p0") + "\">" + p.Name + "(" + Utils.ToString(p.Num) + ")</a></li>";
                                break;
                        }
                        break;
                }
            }

            for (int i = 0; i < pricelist.Length; i++)
            {
                price.Append(pricelist[i]);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(GetAttrValues("大类", category.ToString(), 0, href));
            sb.Append(GetAttrValues("品牌", parentbrand.ToString(), parent_brand_count, href + "&price=" + _price).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("子品牌", brand.ToString(), brand_count, href + "&price=" + _price).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("价格", price.ToString(), 0, href + "&brand=" + _brand));
            return sb.ToString();
        }

        public string GetProductFilter(NameValueCollection nv, string search)
        {
            search = Utils.ToString(search);
            string sort = Utils.ToString(nv["sort"], "0");
            string desc = Utils.ToString(nv["desc"], "1");
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"J_Filter\" class=\"filter clearfix\" >");
            sb.Append("  <!-- 排序 -->");
            sb.Append(GetOrder(search, sort, desc, "0", "综合"));
            sb.Append(GetOrder(search, sort, desc, "1", "人气"));
            sb.Append(GetOrder(search, sort, desc, "2", "销量"));
            sb.Append(GetOrder(search, sort, desc, "3", "价格"));
            /*
            sb.Append("  <!-- 价格区间 -->");
            sb.Append("  <div id=\"J_FPrice\" class=\"fPrice\">");
            sb.Append("    <div class=\"fP-box\">");
            sb.Append("        <b class=\"fPb-item\">");
            sb.Append("            <i class=\"ui-price-plain\">¥</i>");
            sb.Append("            <input type=\"text\" class=\"j_FPInput\" value=\"\" maxlength=\"6\" autocomplete=\"off\" name=\"start_price\">");
            sb.Append("        </b>");
            sb.Append("        <i class=\"fPb-split\"></i>");
            sb.Append("        <b class=\"fPb-item\">");
            sb.Append("            <i class=\"ui-price-plain\">¥</i>");
            sb.Append("            <input type=\"text\" class=\"j_FPInput\" maxlength=\"6\" value=\"\" autocomplete=\"off\" name=\"end_price\">");
            sb.Append("        </b>");
            sb.Append("    </div>");
            sb.Append("  </div>");
            sb.Append("  <!-- 包邮、折扣、满送、满减、货到付款 -->");
            sb.Append("  <div id=\"J_FMenu\" class=\"fMenu\">");
            sb.Append("      <div class=\"fM-con\">");
            sb.Append("          <label><input type=\"checkbox\" value=\"0\" name=\"post_fee\">包邮</label>");
            sb.Append("          <label><input type=\"checkbox\" value=\"0\" name=\"miaosha\">折扣</label>");
            sb.Append("          <label><input type=\"checkbox\" value=\"0\" name=\"pic_detail\">满送</label>");
            sb.Append("          <label><input type=\"checkbox\" value=\"0\" name=\"wwonline\">满减</label>");
            sb.Append("          <label><input type=\"checkbox\" value=\"0\" name=\"support_cod\">货到付款</label>");
            sb.Append("      </div>");
            sb.Append("  </div>");
            */
            sb.Append("  <!-- 分页按钮-->");
            sb.Append("  <p class=\"ui-page-s\">" + GetPageNavS(nv) + "</p>");
            sb.Append("  <div style=\"clear:both;\"></div>");
            sb.Append("</div>");

            return sb.ToString();
        }

        public string GetOrder(string search, string sort, string desc, string _sort, string _name)
        {
            StringBuilder sb = new StringBuilder();
            if (_sort == sort)
            {                
                if (desc == "1")
                {
                    search = Utils.Append(search, "&desc=1", "&desc=0");
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从低到高") + "\" href=\"");
                    sb.Append(Utils.Append(search, "&sort=" + sort, "&sort=" + _sort));
                    sb.Append("\" class=\"fSort fSort-cur\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-u\"></i></a>");
                }
                else
                {
                    search = Utils.Append(search, "&desc=0", "&desc=1");
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从高到低") + "\" href=\"");
                    sb.Append(Utils.Append(search, "&sort=" + sort, "&sort=" + _sort));
                    sb.Append("\" class=\"fSort fSort-cur\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-d\"></i></a>");
                }                
            }
            else
            {
                sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从高到低") + "\" href=\"");
                sb.Append(Utils.Append(search, "&sort=" + sort, "&sort=" + _sort));
                sb.Append("\" class=\"fSort\" >");
                sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-d\"></i></a>");
            }            

            return sb.ToString();
        }

        public string GetAttrValues(string attrKey, string attrValues, int recordCount, string href)
        {
            if (Utils.ToString(attrValues) == "") return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"propAttrs\">");
            sb.Append("<div class=\"j_Prop attr\">");
            sb.Append("<div class=\"attrKey\">" + attrKey + "</div>");
            sb.Append("<div class=\"attrValues\">");
            sb.Append("<ul class=\"av-collapse\"><li><a style=\"color:#fff; background-color:#cc0001;\" href=\"" + Utils.ToString(href) + "\" >不限</a></li>" + attrValues + "</ul>");
            sb.Append("<div class=\"av-options\">");
            sb.Append("<a href=\"###\" class=\"j_More avo-more ui-more-drop-l\" style=\" " + (recordCount > 10 ? "visibility: visible; display: inline;" : "display:hidden;") + "\" >更多</a>");
            sb.Append("</div>");
            sb.Append("<div class=\"av-btns\">");
            sb.Append("<a href=\"###\" class=\"j_SubmitBtn ui-btn-s-primary ui-btn-disable\">确定</a><a href=\"###\" class=\"j_CancelBtn ui-btn-s\">取消</a>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
            return sb.ToString();
        }
        
        public string GetGroupListHtml(int count, string condition, NameValueCollection nv)
        {
            List<Advertise_Config> list = GetAdvertiseResult(count, condition, AdvertiseSource.Product);
            if (list == null || list.Count == 0) return "";

            List<ProductSearchResult> products = new List<ProductSearchResult>();
            foreach (Advertise_Config adv in list)
            {
                if (adv.IndexID > 0)
                {
                    List<ProductSearchResult> product = new ProductService().Query(ProductType.Rand, count, "id =" + adv.IndexID.ToString());
                    if (product == null || product.Count == 0) continue;
                    product[0].ThumbnailPath = string.IsNullOrEmpty(adv.ImagePath) ? Utils.GetProductImage(product[0].Path, "3") : adv.ImagePath;
                    var cart = new OrderBillServices().QueryCartProduct(product[0].ID);
                    if (cart != null)
                    {
                        product[0].GoujiuPrice = cart.PromotePrice;
                    }
                    products.Add(product[0]);
                }
            }
            string sort = Utils.ToString(nv["sort"], "0");
            string desc = Utils.ToString(nv["desc"], "0");

            var query = new List<ProductSearchResult>();
            switch (sort)
            {
                case "0": //销量
                    if (desc == "1")
                    {
                        query = (from p in products orderby p.SoldOfReality + p.SoldOfVirtual descending select p).ToList<ProductSearchResult>();
                    }
                    else
                    {
                        query = (from p in products orderby p.SoldOfReality + p.SoldOfVirtual select p).ToList<ProductSearchResult>();
                    }
                    break;
                case "1": //价格
                    if (desc == "1")
                    {
                        query = (from p in products orderby p.GoujiuPrice descending select p).ToList<ProductSearchResult>();
                    }
                    else
                    {
                        query = (from p in products orderby p.GoujiuPrice select p).ToList<ProductSearchResult>();
                    }
                    break;
                case "2": //人气
                    if (desc == "1")
                    {
                        query = (from p in products orderby p.PageView descending select p).ToList<ProductSearchResult>();
                    }
                    else
                    {
                        query = (from p in products orderby p.PageView select p).ToList<ProductSearchResult>();
                    }
                    break;
            }

            int i = 1;
            StringBuilder sb = new StringBuilder();
            foreach (ProductSearchResult product in query)
            {
                if (i % 3 == 0)
                {
                    sb.Append(GetGroupItemHtml(product, true));
                }
                else
                {
                    sb.Append(GetGroupItemHtml(product, false));
                }
                i++;
            }
            return sb.ToString();
        }
        
        public string GetGroupItemHtml(ProductSearchResult product, bool end)
        {
            double marketprice = product.MarketPrice == null ? 0 : product.MarketPrice;
            double goujiuprice = product.GoujiuPrice == null ? 0 : product.GoujiuPrice;
            string discount = marketprice == 0 ? "" : (Math.Round(goujiuprice / marketprice, 2) * 10).ToString();

            return "<dl class=\"group_item " + (end ? "group_item_end" : "") + "\">" +
              "	<dt class=\"group_item_top image_group\"><em class=\"tp\">直降</em><em class=\"btm\">" + Utils.ToString(marketprice - goujiuprice) + "</em></dt>" +
              "	<dd class=\"group_item_list shadow\">" +
              "		<a href=\"/Home/TuanItem/" + product.ID.ToString() + ".html\" title=\"" + Utils.ToString(product.Name) + "\" target=\"_blank\">" +
              "		<dl>" +
              "			<div class=\"group_item_image\"><img class=\"lazy\" data-original=" + Utils.ToString(product.ThumbnailPath) + " alt=\"\" /></div>" +
              "			<div class=\"group_item_name\">" + Utils.ToString(product.Name) + "</div>" +
              "		</dl>" +
              "		<div class=\"group_item_price\">" +
              "			<div class=\"item_price\">&#65509;<em class=\"goujiu_price\">" + Utils.ToString(product.GoujiuPrice) + "</em></div>" +
              "			<div class=\"item_discount\">" +
              "				<em class=\"discount\">" + discount + "折</em>" +
              "				<em class=\"market_price\">&#65509;" + Utils.ToString(product.MarketPrice) + "</em>" +
              "			</div>" +
              "			<div class=\"item_buyers\">" +
              "				<i class=\"image_group\"></i><em class=\"buyers\"><em>" + Utils.ToString(product.SoldOfReality + product.SoldOfVirtual) + "</em>人已购买</em>" +
              "			</div>" +
              "			<div class=\"clear\"></div>                 " +
              "		</div></a>" +
              "	</dd>" +
              "</dl>";
        }

        public string GetGroupListNavHtml(NameValueCollection nv)
        {
            string sort = Utils.ToString(nv["sort"], "0");
            string desc = Utils.ToString(nv["desc"], "0");

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"list-paixu shadow\">");
            sb.Append("<b>排序：</b>");
            sb.Append("<div class=\"" + (sort == "0" ? "px1" : "px0") + "\"><a href=\"/Home/Tuan?sort=0&desc=" + (sort == "0" ? (desc == "1" ? "0" : "1") : "0") + "\">销量</a></div>");
            sb.Append("<div class=\"" + (sort == "1" ? "px1" : "px0") + "\"><a href=\"/Home/Tuan?sort=1&desc=" + (sort == "1" ? (desc == "1" ? "0" : "1") : "0") + "\">价格</a></div>");
            sb.Append("<div class=\"" + (sort == "2" ? "px1" : "px0") + "\"><a href=\"/Home/Tuan?sort=2&desc=" + (sort == "2" ? (desc == "1" ? "0" : "1") : "0") + "\">人气</a></div>");        
            sb.Append("</div>");

            return sb.ToString();
        }

        public string GetTuanSilderItemHtml(string condition, int count, AdvertiseSource source)
        {
            List<Advertise_Config> list = GetAdvertiseResult(count, condition, source);
            if (list == null || list.Count == 0) return "";

            string result = string.Empty;
            string id = string.Empty;

            List<ProductSearchResult> products = new List<ProductSearchResult>();
            foreach (Advertise_Config adv in list)
            {
                if (adv.IndexID > 0)
                {
                    switch (source)
                    {
                        case AdvertiseSource.LP:
                            break;
                        case AdvertiseSource.Product:
                            List<ProductSearchResult> product = new ProductService().Query(ProductType.Rand, count, "id =" + adv.IndexID.ToString());
                            if (product == null || product.Count == 0) continue;
                            product[0].ThumbnailPath = string.IsNullOrEmpty(adv.ImagePath) ? product[0].ThumbnailPath : adv.ImagePath;
                            products.Add(product[0]);
                            break;
                    }

                }
            }
            switch (source)
            {
                case AdvertiseSource.LP:
                    result = GetTuanSilderItemHtml(list);
                    break;
                case AdvertiseSource.Product:
                    result = GetTuanSilderItemHtml(products);
                    break;
            }

            return result;
        }

        public string GetTuanSilderItemHtml(List<ProductSearchResult> list)
        {
            if (list == null || list.Count == 0) return "";

            int i = 1;
            StringBuilder silderColumnItem = new StringBuilder();
            StringBuilder silderColumnFoot = new StringBuilder();
            silderColumnItem.Append("<ul class=\"silder_column_list\">");
            silderColumnFoot.Append("<ul class=\"silder_column_foot\">");
            foreach (ProductSearchResult product in list)
            {
                silderColumnItem.Append("<li class=\"silder_column_item\" data=\"silder_column_0" + i.ToString() + "\">");
                silderColumnItem.Append(GetTuanSilderColumnHtml(product));
                silderColumnItem.Append("</li>");

                silderColumnFoot.Append("<li data=\"silder_column_0" + i.ToString() + "\">" + Utils.ToString(product.Name) + "</li>");
                i++;
            }
            silderColumnFoot.Append("<div class=\"clear\"></div></ul>");
            silderColumnItem.Append("</ul>");

            silderColumnItem.Append(silderColumnFoot);
            return silderColumnItem.ToString();
        }

        public string GetTuanSilderItemHtml(List<Advertise_Config> list)
        {
            if (list == null || list.Count == 0) return "";

            int i = 1;
            StringBuilder silderColumnItem = new StringBuilder();
            StringBuilder silderColumnFoot = new StringBuilder();
            silderColumnItem.Append("<ul class=\"silder_column_list\">");
            silderColumnFoot.Append("<ul class=\"silder_column_foot\">");
            foreach (Advertise_Config item in list)
            {
                silderColumnItem.Append("<li class=\"silder_column_item\" data=\"silder_column_0" + i.ToString() + "\">");
                silderColumnItem.Append("<a href=\"/Home/LandingPage/" + item.IndexID.ToString() + ".html\" title=\"\" class=\"lp\"><img src=\"" + Utils.ToString(item.ImagePath) + "\" alt=\"\" /></a>");
                silderColumnItem.Append("</li>");

                silderColumnFoot.Append("<li data=\"silder_column_0" + i.ToString() + "\">" + Utils.ToString(item.Name) + "</li>");
                i++;
            }
            silderColumnFoot.Append("<div class=\"clear\"></div></ul>");
            silderColumnItem.Append("</ul>");

            silderColumnItem.Append(silderColumnFoot);
            return silderColumnItem.ToString();
        }
        
        public string GetTuanSilderColumnHtml(ProductSearchResult product)
        {
            var cart = new OrderBillServices().QueryCartProduct(product.ID);
            if (cart != null)
            {
                product.GoujiuPrice= cart.PromotePrice;
            }
            double marketprice = product.MarketPrice == null ? 0 : product.MarketPrice;
            double goujiuprice = product.GoujiuPrice == null ? 0 : product.GoujiuPrice;
            string discount = marketprice == 0 ? "" : (Math.Round(goujiuprice / marketprice, 2) * 10).ToString();
            long time = GetTime();

            System.Diagnostics.Trace.WriteLine(time.ToString());

            return "<div class=\"silder_column_image\" style=\"background-image:url(" + Utils.ToString(product.ThumbnailPath) + ");\" ></div>" +
            "<ul class=\"silder_column_intro\">" +
            "    <li class=\"silder_column_name\"><i class=\"image_group\"></i><span>" + Utils.ToString(product.Name) + "</span></li>" +
            "    <li class=\"silder_column_desc\">" + Utils.ToString(product.Advertisement, "没有任何描述信息，请添加描述信息") + "</li>" +
            "    <li class=\"silder_column_price\">" +
            "        <div class=\"goujiu_price\">&#65509;<span>" + Utils.ToString(product.GoujiuPrice) + "</span></div>" +
            "        <div class=\"goujiu_discount\">" +
            "            <div class=\"discount\"><span>" + discount + "</span>折</div>" +
            "            <div class=\"market_price\">&#65509;<span>" + Utils.ToString(product.MarketPrice) + "</span></div>" +
            "        </div>" +
            "        <div class=\"split\"></div>" +
            "        <a href=\"###\" class=\"image_group buy\"></a>" +
            "        <div class=\"clear\"></div>" +
            "    </li>" +
            "    <li class=\"silder_column_time\"><i class=\"image_group\"></i>" +
            "        <div class=\"time\" time=\"" + time.ToString() + "\"></div>" +
            "        <div class=\"clear\"></div>" +
            "    </li>" +
            "    <li class=\"silder_column_other\"><i class=\"image_group\"></i>" +
            "        <div class=\"attention\">共有<span class=\"attention_count\">" + Utils.ToString(product.PageView) + "</span>人关注</div>" +
            "        <div class=\"buyer\"><span class=\"buyer_count\">" + Utils.ToString(product.SoldOfVirtual) + "</span>人购买</div>" +
            "        <div class=\"clear\"></div>" +
            "    </li>" +
            "</ul>" +
            "<div class=\"clear\"></div>";
        }

        public string GetTuanImagePath(string productId, string thumbnailpath)
        {
            List<Advertise_Config> config = this.GetAdvertiseResult(
                1,
                " pid = 348 and indexid = " + productId,
                AdvertiseSource.Product);
            if (config == null || config.Count == 0)
            {
                return thumbnailpath;
            }

            return config[0].ImagePath ?? thumbnailpath;
        }

        public string GetDiscount(ProductSearchResult product)
        {
            if (product.MarketPrice == 0) return "0";
            return Math.Floor(product.GoujiuPrice * 10.00 / product.MarketPrice).ToString();
        }

        public long GetTime()
        {
            Random vrand = new Random(Guid.NewGuid().GetHashCode());
            DateTime dt = DateTime.Now.AddHours(vrand.Next(10)).AddMinutes(vrand.Next(60)).AddSeconds(vrand.Next(60));
            return Utils.UnixTicks(dt);
        }

        /// <summary>
        /// 获取帮助列表
        /// </summary>
        /// <returns></returns>
        public string GetHelpNavHtml()
        {
            var list = new ConfigPageService().Query(1);
            var append = new StringBuilder();

            foreach (var configPage in list)
            {
                if (configPage.PID == 0)
                {
                    append.Append("<dd class=\"uc_left_item\"><dl>");
                    append.Append("<dt><em></em>" + configPage.Name + "</dt>");
                }
                var pageList = list.Where(l => l.PID == configPage.ID);
                foreach (var page in pageList)
                {
                    append.Append("<dd> <em></em><a href=\"/Home/Help/" + page.ID + ".html\">" + page.Name + "</a></dd>");
                }
                append.Append("</dd>");
            }

            return append.ToString();
        }

        /// <summary>
        /// 获取帮助正文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetHelpContent(Config_Page content)
        {
            if (content == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("<dt class=\"uc_help_top\">" + content.Name + "</dt>");
            sb.Append("<dd class=\"uc_help_conent\">" + content.Content + "");
            sb.Append("</dd>");
            return sb.ToString();
        }
        
        /// <summary>
        /// 猜你喜欢.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult GuessLike()
        {
            return this.PartialView("GuessLike");
        }

        /// <summary>
        ///  获取面包屑
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public string GetProductLevel(int productCategoryID, int parentBrandID, string productName)
        {            
            StringBuilder sb = new StringBuilder();
            sb.Append("您当前的位置： <a href=\"" + Common.ConstantParams.IndexUrl + "\">首页</a>&nbsp;&gt;&nbsp;");
            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            var query = (from p in list where p.ProductCategory_ID == productCategoryID && p.ProductBrand_ID == parentBrandID select p).ToList();
            if (query == null || query.Count == 0)
            {
                //
            }
            else
            {
                var product = query[0];

                //产品目录
                string level = string.Empty;
                if (!string.IsNullOrEmpty(product.ProductCategory_Name))
                {
                    level = product.ProductCategory_NameSpell;
                    sb.Append("<a href=\"/" + level + ".htm\">" + product.ProductCategory_Name + "</a>&nbsp;&gt;&nbsp;");
                    if (!string.IsNullOrEmpty(product.ProductBrand_Name))
                    {
                        level += "-" + product.ProductBrand_NameSpell;
                        sb.Append("<a href=\"/" + level + ".htm\">" + product.ProductBrand_Name + "</a>&nbsp;&gt;&nbsp;");

                        //if (!string.IsNullOrEmpty(product.ProductBrand))
                        //{
                        //    level += "-" + product.ProductBrandPinYin;
                        //    sb.Append("<a href=\"/Home/Search?Brand=" + level + "\">" + product.ProductBrand + "</a>&nbsp;&gt;&nbsp;");
                        //}
                    }
                }
            }
            sb.Append("<span class=\"red\">" + Utils.ToString(productName) + "</span>");
            return sb.ToString();
        }

        /// <summary>
        ///  获取品牌连接
        /// </summary>
        /// <returns></returns>
        public string GetBrandLink(int productCategoryID, int parentBrandID)
        {
            var sb = new StringBuilder();
            var list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            var query =
                (from p in list
                 where p.ProductCategory_ID == productCategoryID && p.ProductBrand_ID == parentBrandID
                 select p).ToList();
            if (query.Count == 0)
            {
                return string.Empty;
            }

            var product = query[0];

            // 产品目录
            if (!string.IsNullOrEmpty(product.ProductCategory_Name))
            {
                var level = product.ProductCategory_NameSpell;
                if (!string.IsNullOrEmpty(product.ProductBrand_Name))
                {
                    level += "-" + product.ProductBrand_NameSpell;
                    sb.Append("<a href=\"/" + level + ".htm\">" + product.ProductBrand_Name + "</a>&nbsp;&nbsp;");
                }
            }


            return sb.ToString();
        }

        [OutputCache(Duration = 60)]
        [GzipFilterAttribute]
        public JsonResult GetProductListInfo(List<int> id)
        {
            Response.Cache.SetOmitVaryStar(true);
            if (id == null) return null;

            try
            {
                var list = new OrderBillServices().QueryCartProduct(id.ToArray());
                if (list == null) return null;

                var query = from p in list
                            select new
                            {
                                ID = p.ProductID,
                                S = p.SoldOfReality + p.SoldOfVirtual,
                                P = p.PromotePrice,
                                C = p.CommentNumber,
                                A = p.Advertisement,
                                M = p.MarketPrice
                            };
                return this.Json(query);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        
        #region 图片上传处理

        /// <summary>
        /// 图片预览
        /// </summary>
        public void ImagePreview()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (file.ContentLength > 0 && file.ContentType.IndexOf("image/") >= 0)
                {
                    int width = Convert.ToInt32(Request.Form["width"]);
                    int height = Convert.ToInt32(Request.Form["height"]);
                    string path = "data:image/jpeg;base64," + Convert.ToBase64String(this.ResizeImg(file.InputStream, width, height).GetBuffer());

                    Response.Write(path);
                }
            }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public void ImagePreviewPath()
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (file.ContentLength > 0 && file.ContentType.IndexOf("image/") >= 0)
                {
                    //int width = Convert.ToInt32(Request.Form["width"]);
                    //int height = Convert.ToInt32(Request.Form["height"]);
                    //string path = "data:image/jpeg;base64," + Convert.ToBase64String(this.ResizeImg(file.InputStream, width, height).GetBuffer());
                    string path = "Images/FeedBack/" + file.FileName;
                    Response.Write(path);
                }
            }
        }
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="ImgFile"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public MemoryStream ResizeImg(Stream ImgFile, int maxWidth, int maxHeight)
        {
            Image imgPhoto = Image.FromStream(ImgFile);

            decimal desiredRatio = 0.25M; //Math.Min((decimal)maxWidth / imgPhoto.Width, (decimal)maxHeight / imgPhoto.Height);
            int iWidth = (int)(imgPhoto.Width * desiredRatio);
            int iHeight = (int)(imgPhoto.Height * desiredRatio);

            Bitmap bmPhoto = new Bitmap(maxWidth, maxHeight);

            Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
            gbmPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, maxWidth, maxHeight), new Rectangle(0, 0, imgPhoto.Width, imgPhoto.Height), GraphicsUnit.Pixel);

            MemoryStream ms = new MemoryStream();
            bmPhoto.Save(ms, ImageFormat.Jpeg);

            imgPhoto.Dispose();
            gbmPhoto.Dispose();
            bmPhoto.Dispose();

            return ms;
        }

        #endregion

        #region 获取广告设置

        /// <summary>
        /// 获取头部广告
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 300)]
        public string GetTopAdvertise()
        {
            Response.Cache.SetOmitVaryStar(true);
            return GetAdvertiseHtml("<a href=\"$URL$\" style=\"background-image:url($ImagePath$);\"></a>", "id=1050");
        }

        public string GetAdvertiseHtml(string htmlTemplete, string condition)
        {
            return GetAdvertiseHtml(htmlTemplete, condition, 1);
        }

        public string GetAdvertiseHtml(string htmlTemplete, string condition, int count)
        {
            return GetAdvertiseHtml(htmlTemplete, condition, count, AdvertiseSource.Custom);
        }

        public string GetAdvertiseHtml(string htmlTemplete, string condition, int count, AdvertiseSource source)
        {
            return this.GetAdvertiseHtml(htmlTemplete, condition, count, source, "1");
        }

        public string GetAdvertiseHtml(string htmlTemplete, string condition, int count, AdvertiseSource source, string imgSize)
        {
            if (count == 0)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(htmlTemplete))
            {
                return string.Empty;
            }

            List<Advertise_Config> list = this.GetAdvertiseResult(count, condition, source);
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (Advertise_Config adv in list)
            {
                sb.Append(this.GetAdvertiseHtmlByTemplete(htmlTemplete, adv, imgSize));
            }

            return sb.ToString();
        }

        public string GetAdvertiseHtml(string htmlTemplete, string htmlTemplete2, string condition, string condition2, int count, AdvertiseSource source)
        {
            if (count == 0) return "";
            if (htmlTemplete == null || htmlTemplete == "") return "";

            List<Advertise_Config> list = GetAdvertiseResult(count, condition, source);
            if (list == null || list.Count == 0) return "";

            List<Advertise_Config> list2 = GetAdvertiseResult(count, condition2, source);
            if (list2 == null || list2.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(GetAdvertiseHtmlByTemplete(htmlTemplete, list[i]));
                if (i < list2.Count)
                {
                    sb.Append(GetAdvertiseHtmlByTemplete(htmlTemplete2, list2[i]));
                }
            }

            return sb.ToString();
        }

        public string GetAdvertiseHtml(string htmlTemplete, string htmlTemplete2, string condition, int count, AdvertiseSource source)
        {
            return this.GetAdvertiseHtml(htmlTemplete, htmlTemplete2, condition, count, source, "1");
        }

        public string GetAdvertiseHtml(string htmlTemplete, string htmlTemplete2, string condition, int count, int exception, AdvertiseSource source)
        {
            if (count == 0)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(htmlTemplete))
            {
                return string.Empty;
            }

            List<Advertise_Config> list = this.GetAdvertiseResult(count, condition, source);
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(
                    i < exception
                        ? this.GetAdvertiseHtmlByTemplete(htmlTemplete, list[i])
                        : this.GetAdvertiseHtmlByTemplete(htmlTemplete2, list[i]));
            }

            return sb.ToString();
        }

        public string GetAdvertiseHtml(string htmlTemplete, string htmlTemplete2, string condition, int count, AdvertiseSource source,string imgSize)
        {
            if (count == 0)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(htmlTemplete))
            {
                return string.Empty;
            }

            List<Advertise_Config> list = this.GetAdvertiseResult(count, condition, source);
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(
                    i < count / 2
                        ? this.GetAdvertiseHtmlByTemplete(htmlTemplete, list[i], imgSize)
                        : this.GetAdvertiseHtmlByTemplete(htmlTemplete2, list[i], imgSize));
            }

            return sb.ToString();
        }

        public List<Advertise_Config> GetAdvertiseResult(int count, string search, AdvertiseSource source)
        {
            search = string.IsNullOrEmpty(search) ? "1=1" : search;
            switch (source)
            {
                case AdvertiseSource.Product:
                    search += " and source = '1'";
                    break;
                case AdvertiseSource.LP:
                    search += " and source = '2'";
                    break;
                case AdvertiseSource.Other:
                    search += " and source = '3'";
                    break;
                case AdvertiseSource.Rand:
                    search += " order by newid()";
                    break;
                case AdvertiseSource.Custom:
                    search += " order by IsOrder";
                    break;
            }
            return new AdvertiseConfigService().Query(count, search);
        }

        public string GetAdvertiseHtmlByTemplete(string htmlTemplete, Advertise_Config adv )
        {
            return this.GetAdvertiseHtmlByTemplete(htmlTemplete, adv, "1");
        }

        public string GetAdvertiseHtmlByTemplete(string htmlTemplete, Advertise_Config adv, string imgsize)
        {
            var obj = new object();
            adv.ImagePath = Utils.GetAdvertiseImage(adv.ImagePath);
            switch (adv.Source)
            {
                case "1": // 产品
                    if (adv.IndexID > 0)
                    {
                        var cart = new OrderBillServices().QueryCartProduct(adv.IndexID);
                        if (cart == null)
                        {
                            obj = adv;
                        }
                        else
                        {
                            adv.ImagePath = string.IsNullOrEmpty(adv.ImagePath) ? Utils.GetProductImage(cart.Path, imgsize) : adv.ImagePath;
                            htmlTemplete = htmlTemplete.Replace("$Name$", Utils.ToString(cart.ProductName));
                            htmlTemplete = htmlTemplete.Replace("$Sold$", Utils.ToString(cart.SoldOfReality + cart.SoldOfVirtual));
                            htmlTemplete = htmlTemplete.Replace("$GoujiuPrice$", Utils.ToString(cart.PromotePrice));
                            obj = cart;
                        }
                    }
                    else
                    {
                        obj = adv;
                    }

                    break;
                case "2": // LP
                    if (adv.IndexID > 0)
                    {
                        Promote_LandingPage lp = new PromoteLandingPageService().Query(adv.IndexID);
                        if (lp == null)
                        {
                            obj = adv;
                        }
                        else
                        {
                            lp.Name = adv.Name;
                            obj = lp;
                        }
                    }
                    else
                    {
                        obj = adv;
                    }

                    break;
                case "3": // 其他
                    obj = adv;
                    break;
                default: // 默认
                    obj = adv;
                    break;
            }

            if (htmlTemplete.Contains("$Description$"))
            {
                htmlTemplete = htmlTemplete.Replace("$Description$", Utils.ToString(adv.Description));
            }

            if (htmlTemplete.Contains("$ImagePath$"))
            {
                htmlTemplete = htmlTemplete.Replace("$ImagePath$", Utils.ToString(adv.ImagePath));
            }

            if (htmlTemplete.Contains("$BackgroundColor$"))
            {
                htmlTemplete = htmlTemplete.Replace("$BackgroundColor$", Utils.ToString(adv.BackgroundColor));
            }

            if (htmlTemplete.Contains("$URL$"))
            {
                htmlTemplete = htmlTemplete.Replace("$URL$", Utils.ToString(adv.URL));
            }

            if (htmlTemplete.Contains("$IndexID$"))
            {
                htmlTemplete = htmlTemplete.Replace("$IndexID$", Utils.ToString(adv.IndexID));
            }

            var properties = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor propertyDescriptror in properties)
            {
                htmlTemplete = htmlTemplete.Replace("$" + propertyDescriptror.Name + "$", Utils.ToString(propertyDescriptror.GetValue(obj)));
            }
            return htmlTemplete;
        }

        /// <summary>
        /// 返回品牌信息
        /// </summary>
        /// <returns></returns>
        public string GetAdvertiseHtml_Brand()
        {
            return GetAdvertiseHtml("<li><a href=\"$URL$\" title=\"$Name$\" target=\"_blank\"><img src=\"$ImagePath$\" alt=\"$Name$\" /></a></li>", "pid=21", 6, AdvertiseSource.Rand);
        }

        #endregion

        #region 用户反馈信息

        /// <summary>
        /// 用户反馈信息
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBack()
        {
            return View();
        }

        /// <summary>
        /// 保存用户反馈的信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="imgUrl"></param>
        /// <param name="gender"></param>
        /// <param name="gjwNumber"></param>
        /// <param name="email"></param>
        /// <param name="checkCode"></param>
        /// <param name="telpPhone"></param>
        /// <returns></returns>
        public ActionResult SaveFeedBack(int type, string name, string content, string imgUrl, bool gender, string gjwNumber,
           string email, string checkCode, string telPhone)
        {
            if (string.IsNullOrEmpty(checkCode))
            {
                return this.Json(new AjaxResponse(0, "验证码不能为空"));
            }
            if (this.TempData[this.Session.SessionID] == null || checkCode != this.TempData[this.Session.SessionID].ToString())
            {
                return this.Json(new AjaxResponse(0, "验证码输入有误"));
            }
            var feedBack = new FeedBack()
            {
                Type = type,
                Name = name,
                Content = content,
                ImgUrl = imgUrl,
                Gender = gender,
                GjwNumber = gjwNumber,
                Email = email,
                TelPhone = telPhone
            };
            var returnValue = new FeedBackService().Inert(feedBack);
            if (returnValue > 0)
            {
                return Json(new AjaxResponse(1, "提交成功"));
            }
            return this.Json(string.Empty);
        }
        #endregion
    }
}
