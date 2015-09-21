using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using V5.Library;
using System.Text;
using V5.DataContract.Product;
using V5.Service.Product;
using V5.Portal.Filters;
using V5.Library.Storage.DB;
using V5.Service.Transact;
using V5.DataContract.Transact.ShoppingCart;
using System.ComponentModel;

namespace V5.Portal.Controllers
{
    public class BrandController : Controller
    {
        int pageCount, totalCount;

        //
        // GET: /Brand/
        [OutputCache(Duration = 300)]
        public ActionResult Index(string brand)
        {
            if (!BrandValidate(brand))
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }

            Response.Cache.SetOmitVaryStar(true);
            NameValueCollection nv = new NameValueCollection();
            nv["brand"] = brand;
            nv["price"] = Utils.ToString(Request.Params["price"], "p0");
            nv["sort"] = Utils.ToString(Request.Params["sort"], "0");
            nv["desc"] = Utils.ToString(Request.Params["desc"], "0");
            nv["p"] = Utils.ToString(Request.Params["p"], "1");
            nv["s"] = "30";
            ViewBag.nv = nv;
            SetSEO(nv);
            return View();
        }

        public bool BrandValidate(string brand)
        {
            if (string.IsNullOrEmpty(brand)) return false;
            if (brand == "all") return true;

            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            if (list == null || list.Count == 0) return false;

            List<Product_Category_Brand> query = (from p in list where p.ProductCategory_NameSpell == brand select p).ToList(); //大类
            if (query == null || query.Count == 0)
            {
                query = (from p in list where p.ProductBrand_URL == brand select p).ToList();
                return query == null || query.Count == 0 ? false : true;
            }
            return true;
        }

        public string GetProductBrandSuggestTip(NameValueCollection nv)
        {
            string _condition = string.Empty;
            string _keyword = Utils.UnEscape(nv["w"]);
            string _brand = Utils.ToString(nv["brand"]);
            string _price = Utils.ToString(nv["price"]);
            string href = "/", category = "", productBrand = "", productSubBrand = "";

            //var paging = new Paging("view_Product_Brand", new List<string> { "ProductCategoryID", "ID", "ParentID", "BrandName", "BrandNameSpell", "Layer", "Sorting" }, "ID", condition, 1, 1000);
            //var list = new ProductBrandService().Query(paging, out pageCount, out totalCount);
            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            string[] arr = string.IsNullOrEmpty(_brand) ? null : _brand.Split('-');            
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0: //大类
                        category = arr[0];
                        if (category == "all")
                        {
                            //
                        }
                        else
                        {
                            list = (from p in list where p.ProductCategory_NameSpell == category select p).ToList();
                        }
                        break;
                    case 1: //品牌
                        productBrand = arr[1];
                        break;
                    case 2: //子品牌
                        productSubBrand = arr[2];
                        break;
                }
            }
            
            _price = string.IsNullOrEmpty(_price) ? "" : _price;
            _price = _price == "p0" ? "" : "?price=" + _price;
            StringBuilder productBrandList = null;
            List<Product_Category_Brand> query = null;
            int productBrandCount = 0;
            if (category == "all")
            {
                productBrandList = new StringBuilder();
                query = (from p in list where p.ProductBrand_Layer == 1 orderby p.ProductBrand_Sorting select p).ToList();
                productBrandCount = query.Count;
                foreach (var p in query)
                {
                    href = "/" + p.ProductCategory_NameSpell + "-";
                    productBrandList.Append("<li><a title=\"" + p.ProductBrand_Name + "\" href=\"" + (href + p.ProductBrand_NameSpell) + ".htm \">" + p.ProductBrand_Name + "</a></li>");                    
                }
            }
            else
            {
                //品牌
                href += category + "-";
                productBrandList = new StringBuilder();
                query = (from p in list where p.ProductBrand_Layer == 1 orderby p.ProductBrand_Sorting select p).ToList();
                productBrandCount = query.Count;
                foreach (var p in query)
                {
                    if (p.ProductBrand_NameSpell == productBrand)
                    {
                        productBrandList.Append("<li><a style=\"color:#fff; background-color:#cc0001;\"  title=\"" + p.ProductBrand_Name + "\" href=\"" + (href + p.ProductBrand_NameSpell) + ".htm \">" + p.ProductBrand_Name + "</a></li>");
                    }
                    else
                    {
                        productBrandList.Append("<li><a title=\"" + p.ProductBrand_Name + "\" href=\"" + (href + p.ProductBrand_NameSpell) + ".htm \">" + p.ProductBrand_Name + "</a></li>");
                    }
                }
            }

            href += string.IsNullOrEmpty(productBrand) ? "" : productBrand + "-";
            StringBuilder productSubBrandList = new StringBuilder();
            query = (from p in list where p.ProductBrand_NameSpell == productBrand select p).ToList();
            int id = query.Count == 0 ? 0 : query[0].ProductBrand_ID;
            int productSubBrandCount = 0;
            if (id > 0)
            {
                query = (from p in list where p.ProductBrand_ParentID == id orderby p.ProductBrand_Sorting select p).ToList();
                productSubBrandCount = query.Count;
                foreach (var p in query)
                {
                    if (p.ProductBrand_NameSpell == productSubBrand)
                    {
                        productSubBrandList.Append("<li><a style=\"color:#fff; background-color:#cc0001;\" title=\"" + p.ProductBrand_Name + "\" href=\"" + (href + p.ProductBrand_NameSpell) + ".htm \">" + p.ProductBrand_Name + "</a></li>");
                    }
                    else
                    {
                        productSubBrandList.Append("<li><a title=\"" + p.ProductBrand_Name + "\" href=\"" + (href + p.ProductBrand_NameSpell) + ".htm \">" + p.ProductBrand_Name + "</a></li>");
                    }
                }
            }
            href += string.IsNullOrEmpty(productSubBrand) ? "" : productSubBrand;
            href = href.EndsWith("-") ? href.Remove(href.Length - 1) : href;
            href += ".htm";
            StringBuilder price = new StringBuilder();
            string[] myprice = new string[] { "0-99", "100-199", "200-599", "600-999", "1000-1999", "2000-5999", "6000以上" };
            string priceno = string.Empty;
            for (int i = 0; i < myprice.Length; i++)
            {
                priceno = "?price=p" + (i + 1).ToString();
                if (priceno == _price)
                {
                    price.Append("<li><a style=\"color:#fff; background-color:#cc0001;\" title=\"" + myprice[i] + "\" href=\"" + href + priceno + " \">" + myprice[i] + "</a></li>");
                }
                else
                {
                    price.Append("<li><a title=\"" + myprice[i] + "\" href=\"" + href + priceno + " \">" + myprice[i] + "</a></li>");
                }
            }


            StringBuilder sb = new StringBuilder();
            //sb.Append(GetAttrValues("大类", category.ToString(), 0, href));
            sb.Append(GetAttrValues("品牌", productBrandList.ToString(), productBrandCount, "/" + category + _price, productBrand).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("子品牌", productSubBrandList.ToString(), productSubBrandCount, "/" + category+"-"+ productBrand + _price, productSubBrand).Replace("propAttrs", "brandAttr").Replace("j_Prop", "j_Brand"));
            sb.Append(GetAttrValues("价格", price.ToString(), 0, href, _price));
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
            sb.Append("<ul class=\"av-collapse\"><li><a " + arrStyle + " href=\"" + Utils.ToString(href) + ".htm \" >不限</a></li>" + attrValues + "</ul>");
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
        
        public string GetPageNavHtml(NameValueCollection nv)
        {
            int pageIndex = Utils.ToInteger(nv["p"]);
            int pageSize = Utils.ToInteger(nv["s"]);

            string brand = Utils.ToString(nv["brand"]);
            string sort = Utils.ToString(nv["sort"]);
            string desc = Utils.ToString(nv["desc"]);
            string price = Utils.ToString(nv["price"]);

            var url = "/" + brand + ".htm?price=" + price + "&sort=" + sort + "&desc=" + desc;

            StringBuilder sb = new StringBuilder();

            //首页
            if (pageIndex > 1)
            {
                sb.Append("<a class='ui-page-prev' href='" + url + "'>&lt;&lt;第一页</a>");
            }
            else
            {
                sb.Append("<b class='ui-page-prev'>&lt;&lt;第一页</b>");
            }

            //中间页
            int p = pageIndex - 5 > 0 ? pageIndex - 5 : 1;
            int c = pageCount - 5 > pageIndex ? pageIndex + 5 : pageCount;
            for (; p <= c; p++)
            {
                if (p == pageIndex)
                {
                    sb.Append("<b  class='ui-page-cur'>").Append(p).Append("</b>");
                }
                else
                {
                    sb.Append(GetPageNavUrl(url, p));
                }
            }

            if (pageCount - c > 0)
            {
                sb.Append(GetPageNavUrl(url, p, "..."));
                sb.Append(GetPageNavUrl(url, pageCount));
            }

            //尾页
            if (pageIndex < pageCount)
            {
                sb.Append("<a class='ui-page-next' href='" + (url + "&p=" + pageCount) + "'>最后一页&gt;&gt;</a>");
            }
            else
            {
                sb.Append("<b class='ui-page-next'>最后一页&gt;&gt;</b>");
            }

            return sb.ToString();
        }
        
        public string GetPageNavUrl(string url, int num)
        {
            return GetPageNavUrl(url, num, Utils.ToString(num));
        }

        public string GetPageNavUrl(string url, int num, string name)
        {
            string pageIndex = Utils.ToString(num);
            pageIndex = pageIndex == "1" ? "" : "&p=" + pageIndex;

            url = url + pageIndex;

            return "<a  href='" + url + "'>" + name + "</a>";
        }
                
        public string GetProductFilter(NameValueCollection nv)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"J_Filter\" class=\"filter clearfix\" >");
            sb.Append("  <!-- 排序 -->");
            sb.Append(GetOrder(nv, "0", "综合"));
            sb.Append(GetOrder(nv, "1", "人气"));
            sb.Append(GetOrder(nv, "2", "销量"));
            sb.Append(GetOrder(nv, "3", "价格"));
            sb.Append(GetOrder(nv, "4", "上架时间"));
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

        public string GetOrder(NameValueCollection nv, string _sort, string _name)
        {
            string brand = Utils.ToString(nv["brand"]);
            string price = Utils.ToString(nv["price"]);
            string sort = Utils.ToString(nv["sort"]);
            string desc = Utils.ToString(nv["desc"]);

            StringBuilder sb = new StringBuilder();
            if (_sort == sort)
            {
                if (desc == "1")
                {
                    desc = "0";
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从低到高") + "\" href=\"");
                    sb.Append("/" + brand + ".htm?price=" + price + "&sort=" + _sort + "&desc=" + desc);
                    sb.Append("\" class=\"fSort fSort-cur\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-u\"></i></a>");
                }
                else
                {
                    desc = "1";
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从高到低") + "\" href=\"");
                    sb.Append("/" + brand + ".htm?price=" + price + "&sort=" + _sort + "&desc=" + desc);
                    sb.Append("\" class=\"fSort fSort-cur\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-d\"></i></a>");
                }
            }
            else
            {
                if (_sort == "3")
                {
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从低到高") + "\" href=\"");
                    sb.Append("/" + brand + ".htm?price=" + price + "&sort=" + _sort + "&desc=1");
                    sb.Append("\" class=\"fSort\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-d\"></i></a>");
                }
                else
                {
                    sb.Append("<a title=\"" + Utils.ToString("点击后按" + _name + "从高到低") + "\" href=\"");
                    sb.Append("/" + brand + ".htm?price=" + price + "&sort=" + _sort + "&desc=0");
                    sb.Append("\" class=\"fSort\" >");
                    sb.Append(Utils.ToString(_name) + "<i class=\"f-ico-arrow-d\"></i></a>");
                }
            }

            return sb.ToString();
        }

        public string GetPageNavS(NameValueCollection nv)
        {
            int currentPage = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);

            string brand = Utils.ToString(nv["brand"]);
            string sort = Utils.ToString(nv["sort"]);
            string desc = Utils.ToString(nv["desc"]);
            string price = Utils.ToString(nv["price"]);
            var url = "/" + brand + ".htm?price=" + price + "&sort=" + sort + "&desc=" + desc + "&p=";

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
                    .Append(currentPage + 1)
                    .Append("'>");
                sb.Append("&gt;").Append("</a>");
            }
            else if (currentPage == pageCount)
            {
                sb.Append("<a class='ui-page-s-prev' title='上一页' href='")
                    .Append(url)
                    .Append(currentPage - 1)
                    .Append("'>");
                sb.Append("&lt").Append("</a>");
                sb.Append("<b title='下一页' class='ui-page-s-next'>&gt;</b>");
            }
            else
            {
                sb.Append("<a class='ui-page-s-prev' title='上一页' href='")
                    .Append(url)
                    .Append(currentPage - 1)
                    .Append("'>");
                sb.Append("&lt").Append("</a>");
                sb.Append("<a title='下一页' class='ui-page-s-next'  href='")
                    .Append(url)
                    .Append(currentPage + 1)
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
            foreach (var p in list)
            {
                sb.Append(GetHtmlByTemplete(htmlTemplete, p));
            }
            return sb.ToString();
        }

        public List<Product_Cache> GetProductSearchResultList(NameValueCollection nv)
        {
            Dictionary<string,int> result = GetSearchWhereString(nv);

            int pageIndex = Utils.ToInteger(nv["p"], 1);
            int pageSize = Utils.ToInteger(nv["s"], 30);
            int sort = Utils.ToInteger(nv["sort"]);
            int desc = Utils.ToInteger(nv["desc"]);

            List<Product_Cache> list = Refresh.GetCache<Product_Cache>(CacheType.Product);
            if (list == null)
            {
                return null;
            }

            //大类、品牌、子品牌
            List<Product_Cache> query = new List<Product_Cache>();
            if (result.ContainsKey("productBrandId") && result["productBrandId"] != 0)
            {
                query = (from p in list where p.ProductBrandID == result["productBrandId"] select p).ToList();
            }
            else if (result.ContainsKey("parentBrandId") && result["parentBrandId"] != 0)
            {
                query = (from p in list where p.ParentBrandID == result["parentBrandId"] select p).ToList();
            }
            else if (result.ContainsKey("categoryId") && result["categoryId"] != 0)
            {
                query = (from p in list where p.ProductCategoryID == result["categoryId"] select p).ToList();
            }
            else if (result.ContainsKey("all") && result["all"] == 1)
            {
                query = list;
            }

            list = query;

            //价格
            if (result.ContainsKey("PromotePrice_beg") && result["PromotePrice_beg"] >= 6000)
            {
                query = (from p in list where p.PromotePrice >= 6000 select p).ToList();
            }
            else
            {
                if (result.ContainsKey("PromotePrice_beg") && result.ContainsKey("PromotePrice_end"))
                {
                    query = (from p in list where p.PromotePrice >= result["PromotePrice_beg"] && p.PromotePrice < result["PromotePrice_end"] select p).ToList();
                }
            }
            list = query;

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

            if (list == null) return null;
            
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

        public Dictionary<string, int> GetSearchWhereString(NameValueCollection nv)
        {
            string _brand = Utils.ToString(nv["brand"]);
            string category = "", parentBrand = "", productBrand = "";
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
                            parentBrand = arr[1];
                            break;
                        case 2: //子品牌
                            productBrand = arr[2];
                            break;
                    }
                }
            }

            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            Dictionary<string,int> result = new Dictionary<string,int>();

            //类目
            List<Product_Category_Brand> query;
            if (category == "all")
            {
                result.Add("all", 1);
            }
            else
            {
                query = (from p in list where p.ProductCategory_NameSpell == category select p).ToList();
                int categoryId = query == null || query.Count == 0 ? 0 : query[0].ProductCategory_ID;
                result.Add("categoryId", categoryId);
            }

            //品牌
            query = (from p in list where p.ProductBrand_NameSpell == parentBrand select p).ToList();
            int parentBrandId = query == null || query.Count == 0 ? 0 : query[0].ProductBrand_ID;
            result.Add("parentBrandId", parentBrandId);

            //子品牌
            query = (from p in list where p.ProductBrand_NameSpell == productBrand select p).ToList();
            int productBrandId = query == null || query.Count == 0 ? 0 : query[0].ProductBrand_ID;
            result.Add("productBrandId", productBrandId);

            //价格
            string price = Utils.ToString(nv["price"]);
            if (!string.IsNullOrEmpty(price))
            {
                switch (price)
                {
                    case "p7": //6000以上
                        result.Add("PromotePrice_beg", 6000);
                        break;
                    case "p6": //2000-5999
                        result.Add("PromotePrice_beg", 2000);
                        result.Add("PromotePrice_end", 5999);
                        break;
                    case "p5": //1000-1999
                        result.Add("PromotePrice_beg", 1000);
                        result.Add("PromotePrice_end", 1999);
                        break;
                    case "p4": //600-999
                        result.Add("PromotePrice_beg", 600);
                        result.Add("PromotePrice_end", 999);
                        break;
                    case "p3": //200-599
                        result.Add("PromotePrice_beg", 200);
                        result.Add("PromotePrice_end", 599);
                        break;
                    case "p2": //100-199
                        result.Add("PromotePrice_beg", 100);
                        result.Add("PromotePrice_end", 199);
                        break;
                    case "p1": //0-99
                        result.Add("PromotePrice_beg", 0);
                        result.Add("PromotePrice_end", 99);
                        break;
                    case "p0": //其他
                        //
                        break;
                }
            }
            
            return result;
        }

        public void SetSEO(NameValueCollection nv)
        {
            string brand = Utils.ToString(nv["brand"]);
            string seoTitle = "", seoKeywords = "", seoDescription = "";
            string category = "", productBrand = "", productSubBrand = "";

            List<Product_Category_Brand> list = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
            List<Product_Category_Brand> query;

            if (brand.Equals("all"))
            {
                seoTitle = Utils.GetAppSettings("SEOTitleBrandAll");
                seoKeywords = Utils.GetAppSettings("SEOKeywordsBrandAll");
                seoDescription = Utils.GetAppSettings("SEODescriptionBrandAll");
            }
            else
            {
                query = (from p in list where p.ProductCategory_NameSpell == brand select p).ToList(); //大类
                if (query == null || query.Count == 0)
                {
                    query = (from p in list where p.ProductBrand_URL == brand select p).ToList();
                    if (query == null || query.Count == 0)
                    {
                        seoTitle = Utils.GetAppSettings("SEOTitleBrandAll");
                        seoKeywords = Utils.GetAppSettings("SEOKeywordsBrandAll");
                        seoDescription = Utils.GetAppSettings("SEODescriptionBrandAll");
                    }
                    else
                    {
                        seoTitle = Utils.ToNullOrEmptyString(query[0].ProductBrand_SEOTitle, Utils.GetAppSettings("SEOTitleBrand"));
                        seoKeywords = Utils.ToNullOrEmptyString(query[0].ProductBrand_SEOKeyWords, Utils.GetAppSettings("SEOKeywordsBrand"));
                        seoDescription = Utils.ToNullOrEmptyString(query[0].ProductBrand_SEODescription, Utils.GetAppSettings("SEODescriptionBrand"));
                    }
                }
                else
                {
                    seoTitle = Utils.ToNullOrEmptyString(query[0].ProductCategory_SEOTitle, Utils.GetAppSettings("SEOTitleBrand_" + brand));
                    seoKeywords = Utils.ToNullOrEmptyString(query[0].ProductCategory_SEOKeyWords, Utils.GetAppSettings("SEOKeywordsBrand"));
                    seoDescription = Utils.ToNullOrEmptyString(query[0].ProductCategory_SEODescription, Utils.GetAppSettings("SEODescriptionBrand_" + brand));
                }
            }

            string[] arr = string.IsNullOrEmpty(brand) ? null : brand.Split('-');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0: //大类
                        category = arr[0];
                        if (category == "all")
                        {
                            category = "新品";
                        }
                        else
                        {
                            query = (from p in list where p.ProductCategory_NameSpell == category select p).Distinct().ToList();
                            category = query == null || query.Count == 0 ? "" : query[0].ProductCategory_Name;
                        }
                        break;
                    case 1: //品牌
                        productBrand = arr[1];
                        query = (from p in list where p.ProductBrand_NameSpell == productBrand select p).Distinct().ToList();
                        productBrand = query == null || query.Count == 0 ? "" : query[0].ProductBrand_Name;
                        break;
                    case 2: //子品牌
                        productSubBrand = arr[2];
                        query = (from p in list where p.ProductBrand_NameSpell == productSubBrand select p).Distinct().ToList();
                        productSubBrand = query == null || query.Count == 0 ? "" : query[0].ProductBrand_Name;
                        break;
                }
            }

            seoTitle = seoTitle.Replace("$ProductCategory$", category);
            seoTitle = seoTitle.Replace("$ProductBrand$", productBrand);
            seoTitle = seoTitle.Replace("$ProductSubBrand$", productSubBrand);

            seoDescription = seoDescription.Replace("$ProductCategory$", category);
            seoDescription = seoDescription.Replace("$ProductBrand$", productBrand);
            seoDescription = seoDescription.Replace("$ProductSubBrand$", productSubBrand);

            seoKeywords = seoKeywords.Replace("$ProductCategory$", category);
            seoKeywords = seoKeywords.Replace("$ProductBrand$", productBrand);
            seoKeywords = seoKeywords.Replace("$ProductSubBrand$", productSubBrand);

            ViewBag.Title = seoTitle;
            ViewBag.Keywords = seoKeywords;
            ViewBag.Description = seoDescription;
        }
    }
}
