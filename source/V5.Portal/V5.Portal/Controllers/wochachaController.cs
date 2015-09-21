using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using V5.DataContract.Product;
using V5.Library;
using System.Xml.Serialization;
using System.ComponentModel;

namespace V5.Portal.Controllers
{
    public class wochachaController : Controller
    {
        //
        // GET: /wochacha/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <returns></returns>
        public void GetProductInfo()
        {
            string result = string.Empty;
            try
            {
                List<Product_Cache> list = Refresh.GetCache<Product_Cache>(CacheType.Product, true);
                if (list == null || list.Count == 0)
                {
                    FormatXML("0", "", null, null);
                    return;
                }

                List<Product_Category_Brand> list2 = Refresh.GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
                if (list2 == null || list2.Count == 0)
                {
                    FormatXML("0", "", null, null);
                    return;
                }

                FormatXML("1", "", list, list2);
            }
            catch (Exception ex)
            {
                FormatXML("-1", "系统错误：" + ex.Message, null, null);
            }
        }

        /// <summary>
        /// 获取产品信息错误
        /// </summary>
        /// <returns></returns>
        public void FormatXML(string status, string message, List<Product_Cache> list, List<Product_Category_Brand> list2)
        {
            productList result = new productList();
            result.status = status;
            result.message = message;
            result.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (list != null && list.Count > 0 && list2 != null && list2.Count > 0)
            {
                result.products = (from p in list
                                   select new product
                                   {
                                       id = p.ProductID,
                                       name = "<![CDATA[" + p.ProductName + "]]>",
                                       barcode = p.Barcode,
                                       spec = "",
                                       price = p.PromotePrice.ToString("#0.00"),
                                       sale = string.IsNullOrEmpty(p.PromoteIDs) ? 0 : 1,
                                       stock = p.InventoryNumber > 0 ? 1 : 0,
                                       category = GetCategory(p.ProductCategoryID, list2),
                                       brand = GetBrand(p.ProductBrandID, list2),
                                       wapUrl = "",
                                       webUrl = "http://www.gjw.com/product/item-id-" + Utils.ToString(p.ProductID) + ".htm",
                                       updatetime = System.DateTime.Now
                                   }).ToList();
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Charset = "UTF-8";
            Response.Write(GetXml(result));
            Response.Flush();
            Response.End();
        }

        public string GetCategory(int id, List<Product_Category_Brand> list)
        {
            var c = list.Find(p => p.ProductCategory_ID == id);
            return c == null ? "" : c.ProductCategory_Name;
        }

        public string GetBrand(int id, List<Product_Category_Brand> list)
        {
            var c = list.Find(p => p.ProductBrand_ID == id);
            return c == null ? "" : c.ProductBrand_Name;
        }

        public string GetXml(productList list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
            sb.Append("<productList>\r\n");
            sb.Append("<timestamp>" + Utils.ToString(list.timestamp) + "</timestamp>\r\n");
            sb.Append("<status>" + Utils.ToString(list.status) + "</status>\r\n");
            sb.Append("<message>" + Utils.ToString(list.message) + "</message>\r\n");

            if (list.products != null && list.products.Count > 0)
            {
                foreach (var p in list.products)
                {
                    sb.Append("<product>\r\n");
                    var properties = TypeDescriptor.GetProperties(p);
                    foreach (PropertyDescriptor propertyDescriptror in properties)
                    {
                        sb.Append("\t<" + propertyDescriptror.Name + ">" + Utils.ToString(propertyDescriptror.GetValue(p)) + "</" + propertyDescriptror.Name + ">\r\n");
                    }
                    sb.Append("</product>\r\n");
                }
            }
            sb.Append("</productList>");
            return sb.ToString();
        }
    }
    
    /// <summary>
    /// 产品信息
    /// </summary>
    public class product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string barcode { get; set; }
        public string spec { get; set; }
        public string price { get; set; }
        public int sale { get; set; }
        public int stock { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string wapUrl { get; set; }
        public string webUrl { get; set; }
        public DateTime updatetime { get; set; }
    }

    /// <summary>
    /// 产品列表
    /// </summary>
    public class productList
    {
        public string timestamp { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public List<product> products { get; set; }
    }
}
