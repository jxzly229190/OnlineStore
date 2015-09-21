// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataAccess.Promote.MeetMoney;
    using V5.DataContract.Product;
    using V5.DataContract.Transact;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Filters;
    using V5.Portal.Models;
    using V5.Service.Product;
    using V5.Service.Transact;

    /// <summary>
    /// 商品控制器.
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// 商品评论.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Comment(int id)
        {
            return this.View();
        }

        /// <summary>
        /// 商品咨询.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Consult(int id)
        {
            return this.View();
        }

        /// <summary> 商品评论. </summary>
        /// <param name="productID"> 商品编号. </param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">单页大小</param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        [GzipFilterAttribute]
        public JsonResult GetProductComment(int productID, int pageIndex, int pageSize)
        {
            try
            {
                var paging = new Paging(
                    "[view_product_Comment]",
                    null,
                    null,
                    "ProductID = " + productID,
                    pageIndex,
                    pageSize,
                    "[CreateTime]",
                    1);
                int pageCount, totalCount;
                var comment = new ProductCommentService().QueryWithPaging(
                    paging,
                    out pageCount,
                    out totalCount);
                if (comment == null)
                {
                    return this.Json(null);
                }

                var commentList = new List<ProductCommentModel>();
                foreach (var productComment in comment)
                {
                    commentList.Add(DataTransfer.Transfer<ProductCommentModel>(productComment, typeof(Product_Comment)));
                }

                foreach (var commentReplt in commentList)
                {
                    var commentReplyList = new ProductCommentReplyService().QueryByCommentID(commentReplt.ID);
                    var replys = new List<ProductCommentReplyModel>();
                    foreach (var commentReply in commentReplyList)
                    {
                        replys.Add(
                             DataTransfer.Transfer<ProductCommentReplyModel>(
                                 commentReply,
                                 typeof(Product_Comment_Reply)));
                    }

                    commentReplt.CommentReplys = replys;
                }

                return this.Json(new { data = commentList, rowsCount = totalCount });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        
        /// <summary> 商品咨询. </summary>
        /// <param name="productID"> 商品编号. </param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">单页大小</param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [GzipFilterAttribute]
        public JsonResult GetProductConsult(int productID, int pageIndex, int pageSize)
        {
            try
            {
                var paging = new Paging("ProductID = " + productID, pageIndex, pageSize);
                int pageCount, totalCount;
                var comment = new ProductConsultService().QueryConsultReplies(paging, out pageCount, out totalCount);

                return this.Json(new { data = comment, rowsCount = totalCount });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        
        /// <summary>
        /// 获取商品的促销信息.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [OutputCache(Duration = 60)]
        [GzipFilterAttribute]
        public JsonResult GetProductPromote(int productID)
        {
            try
            {
                Response.Cache.SetOmitVaryStar(true);
                //if (this.GetUserID() != 0)
                //{
                //    var userBrowseHistoryService = new UserBrowseHistoryService();
                //    var userBrowseHistory = new User_BrowseHistory { UserID = this.GetUserID(), ProductID = productID };
                //    userBrowseHistoryService.Add(userBrowseHistory);
                //}

                Product_Cache promote = new OrderBillServices().QueryCartProduct(productID);
                return this.Json(promote, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// 获取促销详情.
        /// </summary>
        /// <param name="promoteID">
        /// 促销编号.
        /// </param>
        /// <param name="type">
        /// 类型（1：满额，2：满件）.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [OutputCache(Duration = 60)]
        [GzipFilterAttribute]
        public JsonResult GetPromoteInfo(int promoteID, int type)
        {
            try
            {
                Response.Cache.SetOmitVaryStar(true);
                var promoteArray = new List<string>();
                if (type == 1)
                {
                    var promoteRules = new PromoteMeetMoneyRuleDA().SelectByMeetMoneyID(promoteID);
                    foreach (var promoteRule in promoteRules)
                    {
                        var rule = new StringBuilder();
                        rule.Append(string.Format("满{0}元", promoteRule.MeetMoney));
                        if (promoteRule.IsDecreaseCash)
                        {
                            rule.Append(string.Format(",优惠{0}元", promoteRule.DecreaseCash));
                        }

                        if (promoteRule.IsGiveGift)
                        {
                            rule.Append(string.Format(",赠送礼品"));
                        }

                        if (promoteRule.IsGiveCoupon)
                        {
                            rule.Append("赠送礼券");
                        }

                        if (promoteRule.IsGiveIntegral)
                        {
                            rule.Append(string.Format(",赠送{0}积分", promoteRule.Integral));
                        }

                        if (promoteRule.IsNoPostage)
                        {
                            rule.Append(",包邮");
                        }

                        promoteArray.Add(rule.ToString());
                    }
                }
                else if (type == 2)
                {
                    var promoteRules = new PromoteMeetAmountRuleDA().SelectByMeetAmountID(promoteID);
                    foreach (var promoteRule in promoteRules)
                    {
                        var rule = new StringBuilder();
                        rule.Append(string.Format("满{0}件", promoteRule.MeetAmount));
                        if (promoteRule.IsDiscount)
                        {
                            rule.Append(string.Format(",打{0}折", promoteRule.Discount));
                        }

                        if (promoteRule.IsGiveGift)
                        {
                            rule.Append(string.Format(",赠送礼品"));
                        }

                        if (promoteRule.IsNoPostage)
                        {
                            rule.Append(",包邮");
                        }

                        promoteArray.Add(rule.ToString());
                    }
                }

                return this.Json(promoteArray, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// 商品品牌信息
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult BrandInfo()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["brandId"]))
            {
                int brandId = 2;
                int.TryParse(Request.QueryString["brandId"], out brandId);
                ViewBag.brandId = brandId.ToString();
                var productParent = new ProductBrandService().QueryById(brandId);
                if (productParent != null)
                {
                    ViewBag.CategoryId = productParent.ProductCategoryID.ToString();
                }
                var brandInfo = new BrandInformationService().QueryByBrandID(brandId);
                if (brandInfo != null)
                {
                    return this.View(brandInfo);
                }
            }
            Response.StatusCode = 404;
            return this.Content(Utils.ReadFile("Error/404.htm"));
        }

        /// <summary>
        /// 品牌下的商品分类
        /// </summary>
        /// <param name="productIdString">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SimlarProduct(string productIdString)
        {
            if (!string.IsNullOrEmpty(productIdString))
            {
                string[] CharProduct = productIdString.Split(',');
                var sb = new StringBuilder();
                foreach (var s in CharProduct)
                {
                    sb.Append("" + s + " OR ID=");
                }
                var list = new ProductService().QueryProductFromInfo(sb.ToString());
                sb.Clear();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        sb.Append("<ul class=\"goods_list\">");
                        sb.Append(" <li class=\"goods_pic\"> <a href=\"##\" target=\"_blank\" title=\"" + item.SEOTitle + "\">");
                        sb.Append(" <img class=\"lazyload\" src=\"##\" alt='" + item.SEOKeywords + "' /></a></li>");
                        sb.Append(" <li class=\"goods_name\"><a href=\"http://www.gjw.com/product/item-id-3866.htm\" target=\"_blank\" title='53度 茅台 孝道酒 500ml'>" + item.Name + "<span class=\"red\"></span></a></li>");
                        sb.Append("<li class=\"goods_price\">¥" + item.GoujiuPrice + "</li>");
                        sb.Append(" <li class=\"goods_pinglun\"><span><a href=\"http://www.gjw.com/product/item-id-3866.htm#Comment\"  target=\"_blank\">已有33人评论</a></span></li></ul>");
                    }
                }
                return sb.ToString();
            }
            return "";
        }

        /// <summary>
        /// 商品评论
        /// </summary>
        /// <param name="productIdString">
        /// The product Id String.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SimliarProductComments(string productIdString)
        {
            if (!string.IsNullOrEmpty(productIdString))
            {
                var list = new ProductCommentService().SelectCommentsFromBrandInfo(productIdString);
                var sb = new StringBuilder();
                if (list != null && list.Any())
                {
                    foreach (var comment in list)
                    {
                        sb.Append("  <div class=\"bb1d pb10 pt10\"><ul>" + comment.ProductName + "</ul><ul class=\"pl_face fl\"><img src='../Content/images/wine.jpg' width=\"45\" height=\"45\" /></ul>");
                        sb.Append(" <ul class=\"pl_user_say_r fl\"><li>  <dd> <strong>" + comment.UserName + "</strong></dd>");
                        sb.Append("  <dd> <span>" + comment.CreateTime.ToString("yyyy-MM-dd hh:mm:ss") + "</span></dd>   </li>");
                        sb.Append(" <li>" + comment.Content + "</li>    </ul>");
                        sb.Append(" <ul class=\"pl_star_2\"> <li> <dd class=\"s_10 star db fl\">  </dd><div class=\"clear\"> </div>");
                        sb.Append(" </li> </ul> <div class=\"clear\">  </div></div>");
                    }
                    return sb.ToString();
                }

            }
            return "";
        }

        /// <summary>
        /// 商品详情页.
        /// </summary>
        /// <param name="id">
        /// 商品编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [StaticFileWriteFilter]
        public ActionResult Item(int id)
        {
            ProductSearchResult product = new ProductService().QueryByID(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return this.Content(Utils.ReadFile("Error/404.htm"));
            }
            
            Response.Cache.SetOmitVaryStar(true);
            var result = DataTransfer.Transfer<ProductModel>(product, typeof(ProductSearchResult));            
            result.Introduce = Utils.AppendLazy(result.Introduce);
            result.Introduce = Utils.AppendLazy(result.Introduce, "input");
            ViewBag.ID = id;
            //if (this.GetUserID() != 0)
            //{
            //    try
            //    {
            //        var userBrowseHistoryService = new UserBrowseHistoryService();
            //        var userBrowseHistory = new User_BrowseHistory { UserID = this.GetUserID(), ProductID = id };
            //        userBrowseHistoryService.Add(userBrowseHistory);
            //    }
            //    catch (Exception exception)
            //    {
            //        throw new Exception(exception.Message);
            //    }
            //}

            SetSEO(result.Name);

            return this.View(result);
        }

        /// <summary>
        ///  获取面包屑
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public string GetProductLevel(int brandID)
        {
            Product_Brand brand = new ProductBrandService().QueryById(brandID);
            if (brand == null) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("您当前的位置： <a href=\"" + Common.ConstantParams.IndexUrl + "\">首页</a>&nbsp;&gt;&nbsp;");
            sb.Append("<a href=\"/Home/Search?Brand=" + brand.BrandNameSpell + "\">" + brand.BrandName + "</a>");
            return sb.ToString();
        }

        #region

        public string GetHtml(string htmlTemplete, int count, ProductType productType, string brandCategory, string toString)
        {
            if (count == 0) return "";
            if (string.IsNullOrEmpty(htmlTemplete)) return "";

            var list = GetProductSearchResult(count, productType, brandCategory, toString);
            if (list == null || list.Count == 0) return "";

            var sb = new StringBuilder();
            foreach (var product in list)
            {
                sb.Append(GetHtmlByTemplete(htmlTemplete, product));
            }

            return sb.ToString();
        }

        public List<ProductSearchResult> GetProductSearchResult(int count, ProductType productType, string brandCategory, string search)
        {
            string whereString = " Path is not null ";
            string searchWhereString = GetSearchWhereString(search);
            return new ProductService().Query(productType, count, whereString + searchWhereString);
        }

        public string GetSearchWhereString(string searchString)
        {
            searchString = Utils.ToString(searchString);
            if (searchString == "") return "";
            string whereString = " AND ID in(" + searchString + ")";
            return whereString;
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
                htmlTemplete = htmlTemplete.Replace("$ThumbnailPath$", Utils.GetProductImage(product.Path, "2"));
            }

            foreach (PropertyDescriptor propertyDescriptror in properties)
            {
                htmlTemplete = htmlTemplete.Replace("$" + propertyDescriptror.Name + "$", Utils.ToString(propertyDescriptror.GetValue(product)));
            }
            return htmlTemplete;
        }

        #endregion

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CutStr(string str)
        {
            int firIndex = str.IndexOf("</b>", System.StringComparison.Ordinal);
            return str.Substring(3, firIndex);
        }
        
        /// <summary>
        /// 查询属性和属性值
        /// </summary>
        /// <param name="AttributeValues">
        /// The Attribute Values.
        /// </param>
        /// <param name="AttributeModels">
        /// The Attribute Models.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string QueryAttribute(ProductModel result)
        {
            var json = new ProductAttributeService().QueryByCategoryId(Utils.ToString(result.ProductCategoryID));
            if (string.IsNullOrEmpty(json)) return "";

            try
            {
                List<AttributeModel> AttributeModels = new JavaScriptSerializer().Deserialize<List<AttributeModel>>(json);
                if (AttributeModels == null || AttributeModels.Count == 0) return "";

                List<ProductAttributeModel> AttributeValues = new JavaScriptSerializer().Deserialize<List<ProductAttributeModel>>(Utils.ToString(result.Attributes));
                if (AttributeValues == null || AttributeValues.Count == 0) return "";

                StringBuilder sb = new StringBuilder();
                sb.Append("<li><img src=\"/Images/b1.gif\" alt=\"\">商品条码：<span>" + Utils.ToString(result.Barcode) + "</span></li>");
                foreach (var model in AttributeModels)
                {
                    sb.Append("<li><img src=\"/Images/b1.gif\" alt=\"\">");
                    sb.Append(model.AttributeName);
                    sb.Append("：<span id=\"form_custom_column_" + model.ID + "\">");
                    var val = AttributeValues.Find(v => v.ID == model.ID);
                    if (model.AttributeValues != null && val != null)
                    {
                        AttributeValueModel value = model.AttributeValues.Find(v => v.ID == val.ValueID);
                        if (value != null && value.Value != null)
                        {
                            sb.Append(value.Value);
                        }
                    }
                    sb.Append("</span></li>");
                }
                return "<ul>" + sb.ToString() + "</ul>";
            }
            catch (Exception ex)
            {
                return "";
            }
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
                var query = from p in list
                            select new
                            {
                                ID = p.ProductID,
                                S = p.SoldOfReality + p.SoldOfVirtual,
                                P = p.PromotePrice,
                                C = p.CommentNumber,
                                A = p.Advertisement
                            };
                return this.Json(query);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void SetSEO(string productName)
        {
            string seoTitle = Utils.GetAppSettings("SEOTitleItem");
            string seoKeywords = Utils.GetAppSettings("SEOKeywordsItem");
            string seoDescription = Utils.GetAppSettings("SEODescriptionItem");

            seoTitle = seoTitle.Replace("$ProductName$", productName);
            seoKeywords = seoKeywords.Replace("$ProductName$", productName);
            seoDescription = seoDescription.Replace("$ProductName$", productName);

            ViewBag.Title = seoTitle;
            ViewBag.Keywords = seoKeywords;
            ViewBag.Description = seoDescription;
        }
    }
}
