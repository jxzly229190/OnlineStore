using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Collections.Specialized;
using V5.DataContract.Product;
using V5.Library;
using V5.Service.Product;
using V5.Service.Transact;
using System.ComponentModel;

namespace V5.Portal.Controllers
{
    public class SearchController : Controller
    {
        int pageCount, totalCount;
        List<Product_Cache> ProductSearchList;

        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 搜索建议
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult Suggest(string search)
        {
            List<ProductSearch> list = Refresh.GetCache<ProductSearch>(CacheType.ProductSearch, true);
            if (list == null || list.Count == 0) return null;
            var query = (from p in list where p.ProductSearchText.Contains(search) select new { ID = p.ProductID, Name = p.ProductName }).ToList().Take(10);
            return this.Json(query, JsonRequestBehavior.AllowGet);
        }

        public string GetProductBrandSuggestTip(NameValueCollection nv)
        {
            string _condition = string.Empty;
            string _keyword = Utils.UnEscape(nv["w"]);
            string _brand = Utils.ToString(nv["brand"]);
            string _price = Utils.ToString(nv["price"]);
            string href = "/Search?w=" + Utils.Escape(_keyword) + "&brand=$brand$&price=$price$";
            string _url = string.Empty;
            string category = "", productBrand = "", productSubBrand = "";
            string[] arr = string.IsNullOrEmpty(_brand) ? null : _brand.Split('-');
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    switch (i)
                    {
                        case 0: //大类
                            category = arr[0];
                            break;
                        case 1: //品牌
                            productBrand = arr[1];
                            break;
                        case 2: //子品牌
                            productSubBrand = arr[2];
                            break;
                    }
                }
            }

            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            var list1 = (from p in list
                         join c in ProductSearchList
                         on p.ProductBrand_ID equals c.ProductBrandID
                         select p).ToList();
            if (list1 == null || list1.Count == 0) return null;
            
            _price = string.IsNullOrEmpty(_price) ? "" : _price;
            _price = _price == "p0" ? "" : _price;
            
            //品牌
            StringBuilder productBrandList = new StringBuilder();
            var query = (from p in list join c in list1 on p.ProductBrand_ID equals c.ProductBrand_ParentID orderby p.ProductBrand_Sorting select p).Distinct().ToList();
            int productBrandCount = query.Count;
            foreach (var p in query)
            {
                _url = GetUrl(href, p.ProductCategory_NameSpell + "-" + p.ProductBrand_NameSpell, _price);
                if (p.ProductBrand_NameSpell == productBrand)
                {
                    productBrandList.Append("<li><a style=\"color:#fff; background-color:#cc0001;\"  title=\"" + p.ProductBrand_Name + "\" href=\"" + _url+ " \">" + p.ProductBrand_Name + "</a></li>");
                }
                else
                {
                    productBrandList.Append("<li><a title=\"" + p.ProductBrand_Name + "\" href=\"" + _url + " \">" + p.ProductBrand_Name + "</a></li>");
                }
            }

            //子品牌
            StringBuilder productSubBrandList = new StringBuilder();
            query = (from p in list where p.ProductBrand_NameSpell == productBrand select p).ToList();
            int id = query.Count == 0 ? 0 : query[0].ProductBrand_ID;
            int productSubBrandCount = 0;
            if (id > 0)
            {
                query = (from p in list1 where p.ProductBrand_ParentID == id orderby p.ProductBrand_Sorting select p).Distinct().ToList();
                productSubBrandCount = query.Count;
                foreach (var p in query)
                {
                    _url = GetUrl(href, category + "-" + productBrand + "-" + p.ProductBrand_NameSpell, _price);
                    if (p.ProductBrand_NameSpell == productSubBrand)
                    {
                        productSubBrandList.Append("<li><a style=\"color:#fff; background-color:#cc0001;\" title=\"" + p.ProductBrand_Name + "\" href=\"" + _url + " \">" + p.ProductBrand_Name + "</a></li>");
                    }
                    else
                    {
                        productSubBrandList.Append("<li><a title=\"" + p.ProductBrand_Name + "\" href=\"" + _url + " \">" + p.ProductBrand_Name + "</a></li>");
                    }
                }
            }

            //价格
            StringBuilder price = new StringBuilder();
            string[] myprice = new string[] { "0-99", "100-199", "200-599", "600-999", "1000-1999", "2000-5999", "6000以上" };
            string priceno = string.Empty;
            for (int i = 0; i < myprice.Length; i++)
            {
                priceno = "p" + (i + 1).ToString();
                _url = GetUrl(href, _brand, priceno);
                if (priceno == _price)
                {
                    price.Append("<li><a style=\"color:#fff; background-color:#cc0001;\" title=\"" + myprice[i] + "\" href=\"" + _url + " \">" + myprice[i] + "</a></li>");
                }
                else
                {
                    price.Append("<li><a title=\"" + myprice[i] + "\" href=\"" + _url + " \">" + myprice[i] + "</a></li>");
                }
            }
            
            StringBuilder sb = new StringBuilder();
            //sb.Append(GetAttrValues("大类", category.ToString(), 0, href));
            sb.Append(GetAttrValues("品牌", productBrandList.ToString(), productBrandCount, GetUrl(href, category, _price), productBrand).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("子品牌", productSubBrandList.ToString(), productSubBrandCount, GetUrl(href, category + "-" + productBrand, _price), productSubBrand).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("价格", price.ToString(), 0, GetUrl(href, category + "-" + productBrand, ""), _price));
            return sb.ToString();
        }

        public string GetAttrValues(string attrKey, string attrValues, int recordCount, string href, string attrCurr)
        {
            if (Utils.ToString(attrValues) == "") return "";
            string arrStyle = string.IsNullOrEmpty(attrCurr) ? "style=\"color:#fff; background-color:#cc0001;\"" : "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"propAttrs\">");
            sb.Append("<div class=\"j_Prop attr\">");
            sb.Append("<div class=\"attrKey\">" + attrKey + "</div>");
            sb.Append("<div class=\"attrValues\">");
            sb.Append("<ul class=\"av-collapse\"><li><a " + arrStyle + " href=\"" + Utils.ToString(href) + " \" >不限</a></li>" + attrValues + "</ul>");
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

        public string GetUrl(string href, string brand, string price)
        {
            if (string.IsNullOrEmpty(href)) return "";
            return href.Replace("$brand$", brand).Replace("$price$", price);
        }

        public string GetProductFilter(NameValueCollection nv)
        {
            string _keyword = Utils.UnEscape(nv["w"]);
            string _brand = Utils.ToString(nv["brand"]);
            string _price = Utils.ToString(nv["price"]);
            _price = string.IsNullOrEmpty(_price) ? "" : _price;
            _price = _price == "p0" ? "" : _price;
            string search = "/Search?w=" + Utils.Escape(_keyword) + "&brand=" + _brand + "&price=" + _price;

            string sort = Utils.ToString(nv["sort"], "0");
            string desc = Utils.ToString(nv["desc"], "1");

            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"J_Filter\" class=\"filter clearfix\" >");
            sb.Append("  <!-- 排序 -->");
            sb.Append(GetOrder(search, sort, desc, "0", "综合"));
            sb.Append(GetOrder(search, sort, desc, "1", "人气"));
            sb.Append(GetOrder(search, sort, desc, "2", "销量"));
            sb.Append(GetOrder(search, sort, desc, "3", "价格"));
            sb.Append(GetOrder(search, sort, desc, "4", "上架时间"));
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
                
        public string GetPageNavHtml(NameValueCollection nv)
        {
            int pageIndex = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);

            string keyword = Utils.ToString(nv["w"]).Trim();
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);
            string sort = Utils.ToString(nv["sort"]);
            string desc = Utils.ToString(nv["desc"], "0");

            keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;
            brand = string.IsNullOrEmpty(brand) ? "" : "&brand=" + brand;
            price = string.IsNullOrEmpty(price) ? "" : "&price=" + price;
            sort = string.IsNullOrEmpty(sort) ? "" : "&sort=" + sort;
            desc = string.IsNullOrEmpty(desc) ? "" : "&desc=" + desc;

            var url = "/Search?w=" + Utils.Escape(keyword) + brand + price + sort + desc;

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
                    sb.Append("<b  class='ui-page-cur'>").Append(p).Append("</b>");
                }
                else
                {
                    sb.Append("<a  href='").Append(url).Append("&p=").Append(p).Append("&s=").Append(pageSize).Append("'>");
                    sb.Append(p).Append("</a>");
                }
            }
            if (pageCount - c > 0)
            {
                sb.Append("<a  href='").Append(url).Append("&p=").Append(p).Append("&s=").Append(pageSize).Append("'>");
                sb.Append("...").Append("</a>");
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

            string keyword = Utils.ToString(nv["w"]).Trim();
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);

            keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;
            brand = string.IsNullOrEmpty(brand) ? "" : "&brand=" + brand;
            price = string.IsNullOrEmpty(price) ? "" : "&price=" + price;

            var url = "/Search?w=" + Utils.Escape(keyword) + brand + price;

            //<b class="ui-page-s-count">相关商品7358件</b><b class="ui-page-s-len">1/50</b> <b title="上一页" class="ui-page-s-prev">&lt;</b> <a title="下一页"  class="ui-page-s-next" href="###" >&gt;</a>
            string bTag = "<b class='ui-page-s-count'>相关商品$totalCount$件</b><b class='ui-page-s-len'>$current$/$pageCount$</b>";

            bTag = bTag.Replace("$totalCount$", totalCount.ToString())
                .Replace("$current$", currentPage.ToString())
                .Replace("$pageCount$", pageCount.ToString());

            var sb = new StringBuilder();

            if (currentPage == 1 && currentPage < pageCount)
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
                sb.Append("<b class='ui-page-s-next' title='下一页' >&gt;</b>");
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
                sb.Append("<a class='ui-page-s-next' title='下一页' href='")
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

        public string GetHtml(string htmlTemplete, int count, ProductType productType, NameValueCollection nv)
        {
            if (htmlTemplete == null || htmlTemplete == "") return "";

            List<Product_Cache> list = GetProductSearchResultList(nv);
            if (list == null || list.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            foreach (var cartProduct in list)
            {
                sb.Append(GetHtmlByTemplete(htmlTemplete, cartProduct));
            }
            return sb.ToString();
        }

        public List<Product_Cache> GetProductSearchResultList(NameValueCollection nv)
        {
            List<Product_Cache> list = Refresh.GetCache<Product_Cache>(CacheType.Product, true);
            if (list == null || list.Count == 0) return null;

            List<ProductSearch> list2 = Refresh.GetCache<ProductSearch>(CacheType.ProductSearch, true);
            if (list2 == null || list2.Count == 0) return null;

            List<Product_Category_Brand> list3= Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand,true);
            
            //关键字
            string keyword = Utils.UnEscape(nv["w"]);
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);
            string category = "", productBrand = "", productSubBrand = "";
            int categoryId = -1, productBrandId = -1, productSubBrandId = -1;
            
            //模糊查询
            List<Product_Cache> query = (from p in list
                                         join c in list2
                                         on p.ProductID equals c.ProductID
                                         where c.ProductSearchText.Contains(keyword)
                                         select p).ToList();
            list = query;
            ProductSearchList = query;
            if (list == null || list.Count == 0) return null;

            //品牌
            List<Product_Category_Brand> query3;
            string[] arr = string.IsNullOrEmpty(brand) ? null : brand.Split('-');
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    switch (i)
                    {
                        case 0: //大类
                            category = arr[0];
                            query3 = (from p in list3 where p.ProductCategory_NameSpell == category select p).Distinct().ToList();
                            categoryId = query3 == null || query3.Count == 0 ? -1 : query3[0].ProductCategory_ID;
                            break;
                        case 1: //品牌
                            productBrand = arr[1];
                            query3 = (from p in list3 where p.ProductBrand_NameSpell == productBrand select p).Distinct().ToList();
                            productBrandId = query3 == null || query3.Count == 0 ? -1 : query3[0].ProductBrand_ID;
                            break;
                        case 2: //子品牌
                            productSubBrand = arr[2];
                            query3 = (from p in list3 where p.ProductBrand_NameSpell == productSubBrand select p).Distinct().ToList();
                            productSubBrandId = query3 == null || query3.Count == 0 ? -1 : query3[0].ProductBrand_ID;
                            break;
                    }
                }
            }
            if (productSubBrandId > 0)
            {
                query = (from p in list where p.ProductBrandID == productSubBrandId select p).ToList();
                list = query;
            }
            else if(productBrandId>0){
                query = (from p in list where p.ParentBrandID == productBrandId select p).ToList();
                list = query;
            }
            else if (categoryId > 0)
            {
                query = (from p in list where p.ProductCategoryID == categoryId select p).ToList();
                list = query;
            }

            if (list == null || list.Count == 0) return null;

            //价格
            switch (price)
            {
                case "p7": //6000以上
                    query = (from p in list where p.PromotePrice >= 6000 select p).ToList();
                    list = query;
                    break;
                case "p6": //2000-5999
                    query = (from p in list where p.PromotePrice >= 2000 && p.PromotePrice < 6000 select p).ToList();
                    list = query;
                    break;
                case "p5": //1000-1999
                    query = (from p in list where p.PromotePrice >= 1000 && p.PromotePrice < 2000 select p).ToList();
                    list = query;
                    break;
                case "p4": //600-999
                    query = (from p in list where p.PromotePrice >= 600 && p.PromotePrice < 1000 select p).ToList();
                    list = query;
                    break;
                case "p3": //200-599
                    query = (from p in list where p.PromotePrice >= 200 && p.PromotePrice < 600 select p).ToList();
                    list = query;
                    break;
                case "p2": //100-199
                    query = (from p in list where p.PromotePrice >= 100 && p.PromotePrice < 200 select p).ToList();
                    list = query;
                    break;
                case "p1": //0-99
                    query = (from p in list where p.PromotePrice >= 0 && p.PromotePrice < 100 select p).ToList();
                    list = query;
                    break;
                case "p0":
                    break;
            }
            if (list == null || list.Count == 0) return null;


            int sort = Utils.ToInteger(nv["sort"]);
            int desc = Utils.ToInteger(nv["desc"], 0);
            
            //排序
            switch (sort)
            {
                case 0: //pageView
                case 1: //pgaeView
                    if (desc == 1)
                    {
                        query = (from p in list orderby p.PageView select p).ToList();
                    }
                    else
                    {
                        query = (from p in list orderby p.PageView descending select p).ToList();
                    }
                    break;
                case 2: //SoldOfReality + SoldOfVirtual
                    if (desc == 1)
                    {
                        query = (from p in list orderby p.SoldOfReality + p.SoldOfVirtual select p).ToList();
                    }
                    else
                    {
                        query = (from p in list orderby p.SoldOfReality + p.SoldOfVirtual descending select p).ToList();
                    }
                    break;
                case 3: //PromotePrice
                    if (desc == 1)
                    {
                        query = (from p in list orderby p.PromotePrice select p).ToList();
                    }
                    else
                    {
                        query = (from p in list orderby p.PromotePrice descending select p).ToList();
                    }
                    break;
                case 4: //CreateTime
                    if (desc == 1)
                    {
                        query = (from p in list orderby p.CreateTime select p).ToList();
                    }
                    else
                    {
                        query = (from p in list orderby p.CreateTime descending select p).ToList();
                    }
                    break;
            }
            list = query;

            int pageIndex = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);

            totalCount = list.Count;
            pageCount = Convert.ToInt32(totalCount / pageSize) + 1;
            
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
    }
}
