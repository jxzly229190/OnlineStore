// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transact.Order.Add.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   添加订单部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Transact
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using V5.DataContract.Product;
    using V5.DataContract.Transact.Order;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Library.Security;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Product;
    using V5.Portal.Backstage.Models.Transact.Order;
    using V5.Portal.Backstage.Models.User;
    using V5.Service.Product;
    using V5.Service.Transact;
    using V5.Service.User;
    using V5.Service.Utility;

	/// <summary>
    /// The transact controller.
    /// </summary>
    public partial class TransactController
    {
        /// <summary>
        /// The add order.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult OrderAdd()
        {
            return this.PartialView("Order/Order.Add");
        }

        /// <summary>
        /// The add user.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult AddUser()
        {
            return this.PartialView("Order/AddUser", new UserModel());
        }

        #region Public Methods and Operators

        /// <summary>
        /// 后台客户添加会员信息
        /// </summary>
        /// <param name="userModel">
        /// 会员Model对象
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddUser(UserModel userModel)
        {
            try
            {
                var orderService = new OrderService(this.SystemUserSession.EmployeeID);
                var user = new User
                               {
                                   UserLevelID = 1,
                                   Email = userModel.Email ?? userModel.Mobile,  // 优先使用邮箱作为登录用户名，其次使用手机号。
                                   EmailValidate = false, 
                                   Name = userModel.Name,
                                   LoginName = userModel.Email ?? "user@gjw.com", // 后台客户添加用户信息时，若用户没有邮箱，则设置默认值“user@gjw.com”
                                   Mobile = userModel.Mobile, 
                                   MobileValidate = false, 
                                   Tel = userModel.Tel, 
                                   NickName = userModel.Name, 
                                   Address = userModel.Address, 
                                   LoginPassword = Encrypt.HashByMD5("123456"),  // 对会员密码进行加密
                                   Birthday = null,
                                   LastLoginTime = null,
                                   Integral = 0, 
                                   Status = 1, 
                                   CreateTime = DateTime.Now, 
                                   CountyID = userModel.CountyID
                               };
                orderService.AddUserInfo(user);
                var addressModel = new UserReceiveAddressModel { CountyID = user.CountyID };
	            return
		            this.Json(
			            new AjaxResponse(
				            1,
				            new { UserID = user.ID, CountyId = user.CountyID, CountyInfo = addressModel.CountyName }));
	            //this.Content(
	            //    "{State:'Ok',UserID:'" + user.ID + "',CountyInfo:'" + addressModel.CountyName + "'}");
            }
            catch
            {
                return this.Content("{State:'Error'}");
            }
        }

        /// <summary>
        /// The search user info.
        /// </summary>
        /// <param name="searchStr">
        /// The search str.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchUserInfo(string searchStr)
        {
            AjaxResponse response = null;
            try
            {
                var userService = new UserService();
                var user = userService.QueryUserByMobileOrEmail(searchStr);
                if (user == null)
                {
                    response = new AjaxResponse(2, "用户不存在");
                    return this.Json(response);
                }

                var userAddressService = new UserReceiveAddressService();
                var userDefaultAddress = userAddressService.QueryDefaultReceiveAddressByUserID(user.ID);

                if (userDefaultAddress != null)
                {
                    var addressModel = DataTransfer.Transfer<UserReceiveAddressModel>(
                        userDefaultAddress,
                        typeof(User_RecieveAddress));
                    addressModel.UserID = user.ID;
                    addressModel.UserName = user.Name;
                    response = new AjaxResponse(1, string.Empty, addressModel);
                    return this.Json(response);
                }
                else
                {
                    var userModel = DataTransfer.Transfer<UserModel>(user, typeof(User));
                    response = new AjaxResponse(3, "没有地址信息", userModel);
                    return this.Json(response);
                }
            }
            catch (Exception exception)
            {
                response = new AjaxResponse(-1, exception.Message);
                return this.Json(response);
            }
        }

        /// <summary>
        /// 根据会员编码查询送货地址信息
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <param name="currentUserID">
        /// The current User ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryUserReceiveAddress([DataSourceRequest] DataSourceRequest request, int currentUserID)
        {
            var service = new UserReceiveAddressService();
            var list = service.QueryReceiveAddressByUserID(currentUserID);
            if (list == null || list.Count == 0) return null;

            var modelList = new List<UserReceiveAddressModel>();
            foreach (var item in list)
            {
                modelList.Add(
                        DataTransfer.Transfer<UserReceiveAddressModel>(
                            item,
                            typeof(User_RecieveAddress)));
            }

            return Json(modelList.ToDataSourceResult(request));
        }

        /// <summary>
        /// 为会员添加收货地址
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <param name="consignee">
        /// The consignee.
        /// </param>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <param name="tel">
        /// The tel.
        /// </param>
        /// <param name="currentCountyID">
        /// The current County ID.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="currentUserID">
        /// The current User ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddUserReceiveAddress([DataSourceRequest] DataSourceRequest request, string consignee, string mobile, string tel, int currentCountyID, string address, int currentUserID)
        {
            var userReceiveAddressService = new UserReceiveAddressService();
            var model = new UserReceiveAddressModel
                            {
                                Consignee = consignee,
                                Mobile = mobile,
                                Tel = tel,
                                CountyID = currentCountyID,
                                UserID = currentUserID,
                                IsDefault = false,
                                Address = address
                            };
            var userReceiveAddress = DataTransfer.Transfer<User_RecieveAddress>(model, typeof(UserReceiveAddressModel));
            int addressId = userReceiveAddressService.Add(userReceiveAddress, null);
            model.ID = addressId;

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        /// <summary>
        /// 根据父品牌编码查询子品牌数据
        /// </summary>
        /// <param name="parentBrandId">
        /// 父品牌编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public ActionResult QuerySubProductBrandByParentId(int parentBrandId)
        {
            var items = new List<SelectListItem>();
            var service = new ProductConsultService();
            var listSubBrand = service.QuerySubProductBrandByParentId(parentBrandId);
            if (listSubBrand == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            foreach (var brand in listSubBrand)
            {
                var item = new SelectListItem()
                {
                    Text = brand.BrandName,
                    Value = brand.ID.ToString()
                };
                items.Add(item);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 订单后台-添加订单-查询商品
        /// </summary>
        /// <param name="request">
        /// 请求对象
        /// </param>
        /// <param name="searchModel">
        /// 查询对象
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public ActionResult QueryOrderProduct(
            [DataSourceRequest] DataSourceRequest request,
            OrderProductSearchModel searchModel)
        {
            int rowCount = 0;
            int pageCount = 0;
            var productService = new ProductService();
            var condition =
                productService.BuildProductQueryCondition(
                    searchModel.ProductCategoryID < 1 ? string.Empty : searchModel.ProductCategoryID.ToString(),
                    searchModel.SubProductCategoryID < 1 ? string.Empty : searchModel.SubProductCategoryID.ToString(),
                    searchModel.ProductBrandID < 1 ? string.Empty : searchModel.ProductBrandID.ToString(),
                    searchModel.SubProductBrandID < 1 ? string.Empty : searchModel.SubProductBrandID.ToString(),
                    searchModel.ProductName,
                    searchModel.Barcode,
                    string.Empty,
                    string.Empty);
            var paging = new Paging("view_Product_Paging", null, "ID", condition, request.Page, request.PageSize);
            var list = productService.Query(paging, out pageCount, out rowCount);
            var modelList = new List<ProductModel>();

            if (list == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            foreach (var item in list)
            {
                var model = DataTransfer.Transfer<ProductModel>(item, typeof(ProductSearchResult));
                modelList.Add(model);
            }

            var result = new DataSourceResult() { Data = modelList, Total = rowCount };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 查询选中的商品信息
        /// </summary>
        /// <param name="request">
        /// 请求
        /// </param>
        /// <param name="checkedProductIds">
        /// 选中的商品编码列表
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult QueryCartProduct([DataSourceRequest] DataSourceRequest request, string checkedProductIds)
        {
            if (string.IsNullOrWhiteSpace(checkedProductIds))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            int rowCount = 0;
            int pageCount;
            var productService = new ProductService();
            var paging = new Paging("view_Product_Paging", null, "ID", string.Format("[Status] = 2 And [ID] in ({0})", checkedProductIds), request.Page, request.PageSize);
            var list = productService.Query(paging, out pageCount, out rowCount);

            if (list == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var modelList = new List<OrderProductModel>();

            foreach (var item in list)
            {
                var model = new OrderProductModel
                                {
                                    ProductID = item.ID,
                                    ProductName = item.Name,
                                    Path = item.Path,
                                    Quantity = 1
                                };

                modelList.Add(model);
            }

            var result = new DataSourceResult() { Data = modelList, Total = rowCount };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 后台添加订单
        /// </summary>
        /// <param name="products">
        /// 添加的商品
        /// </param>
        /// <param name="userID">
        /// 会员编码
        /// </param>
        /// <param name="receiveAddressID">
        /// 会员收货地址
        /// </param>
        /// <param name="paymentMethodID">
        /// 支付方式
        /// </param>
        /// <param name="isRequireInvoice">
        /// </param>
        /// <param name="invoiceInfo">
        /// The invoice Model.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddOrder(
            List<ProductModel> products,
            int userID,
            int receiveAddressID,
            int paymentMethodID,
            bool isRequireInvoice,
            OrderInvoiceModel invoiceInfo,
            string description = null)
        {
            try
            {
                if (products == null || products.Count < 1)
                {
                    var data = new AjaxResponse(-1, "订单商品为空！");
                    return this.Json(data);
                }

                if (userID < 1)
                {
                    var data = new AjaxResponse(-1, "用户编码错误！");
                    return this.Json(data);
                }

                if (receiveAddressID < 1)
                {
                    var data = new AjaxResponse(-1, "收货信息地址编码错误！");
                    return this.Json(data);
                }

                if (paymentMethodID < 0)
                {
                    var data = new AjaxResponse(-1, "支付地址错误！");
                    return this.Json(data);
                }

                Order_Invoice invoice = null;
                if (isRequireInvoice)
                {
                    if (invoiceInfo.InvoiceContentID < 0)
                    {
                        var data = new AjaxResponse(-1, "发票消费类别错误！");
                        return this.Json(data);
                    }
                    else if (string.IsNullOrWhiteSpace(invoiceInfo.InvoiceTitle))
                    {
                        var data = new AjaxResponse(-1, "发票抬头为空！");
                        return this.Json(data);
                    }
                    else if (invoiceInfo.InvoiceCost < 0)
                    {
                        var data = new AjaxResponse(-1, "开票金额不能小于或等于0！");
                        return this.Json(data);
                    }

                    invoice = DataTransfer.Transfer<Order_Invoice>(invoiceInfo, typeof(OrderInvoiceModel));
                }

				DateTime createTime;
				var orderCode = MadeCodeService.GetOrderCode(out createTime);

                var orderService = new OrderService(this.SystemUserSession.EmployeeID);
	            var order = new Order
		                        {
			                        UserID = userID,
			                        RecieveAddressID = receiveAddressID,
			                        CpsID = 0,
			                        PaymentMethodID = paymentMethodID,
			                        OrderCode = orderCode,
			                        OrderNumber = MadeCodeService.ReverseOrderCode(orderCode, createTime),
			                        TotalMoney = 0,
			                        TotalIntegral = 0,
			                        PaymentStatus = 0,
			                        IsRequireInvoice = isRequireInvoice,
			                        Status = paymentMethodID == 0 ? 100 : 0,
			                        Description = string.IsNullOrWhiteSpace(description) ? "后台添加订单" : description,
			                        CreateTime = createTime
		                        };

                var orderProducts = new List<Order_Product>();

                foreach (var productModel in products)
                {
                    if (productModel.Quantity > 0)
                    {
                        orderProducts.Add(
                            new Order_Product
                                {
                                    ProductID = productModel.ID,
                                    Quantity = productModel.Quantity,
                                    ProductName = productModel.Name,
                                    TransactPrice = productModel.GoujiuPrice,
                                    CreateTime = DateTime.Now
                                });
                    }
                }

                // 判断是否能够由系统自动确认订单，若能，则自动确认。
                orderService.Add(order, orderProducts, invoice);

                if (orderService.ValidateConfirmOrderBySystem(order))
                {
                    orderService.ConfirmOrderBySystem(order);
                }

                return this.Json(new AjaxResponse(1, "执行成功"));
            }
            catch (Exception exception)
            {
                var data = new AjaxResponse(-1, exception.Message);
                TextLogger.Instance.Log("后台添加订单发生错误", Category.Error, exception);
                return this.Json(data);
            }
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// 创建订单商品搜索条件
        /// </summary>
        /// <param name="searchModel">
        /// 搜索对象
        /// </param>
        /// <returns>
        /// 搜索条件
        /// </returns>
        private string BuildOrderProductQueryCondition(OrderProductSearchModel searchModel)
        {
            /* **
             * select * from view_Product_SelectAllInfo where ProductBrandID in (
	         *           select ID from Product_Brand where ParentID = 1
             *      ) and ProductCategoryID in (select ID from Product_Category where ParentID=1)
             *       And Name like '%电%' And Barcode like '%电%'
             * **/

            if (searchModel == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            #region 类型级别转换

            if (searchModel.SubProductBrandID > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" And ProductBrandID =").Append(searchModel.SubProductBrandID);
                }
                else
                {
                    sb.Append(" ProductBrandID =").Append(searchModel.SubProductBrandID);
                }
            }
            else if (searchModel.ProductBrandID > 0)
            {
                // 若只选了一级品牌，需要获得二级品牌
                if (sb.Length > 0)
                {
                    sb.Append(" And ProductCategoryID in (select ID from Product_Category where ParentID=")
                        .Append(searchModel.ProductBrandID)
                        .Append(")");
                }
                else
                {
                    sb.Append(" ProductCategoryID in (select ID from Product_Category where ParentID=")
                        .Append(searchModel.ProductBrandID)
                        .Append(")");
                }
            }
            else if (searchModel.SubProductCategoryID > 0)
            {
                // 选了二级分类，则直接使用二级分类进行查询
                if (sb.Length > 0)
                {
                    sb.Append(" And ProductBrandID = ").Append(searchModel.SubProductCategoryID);
                }
                else
                {
                    sb.Append(" ProductBrandID = ").Append(searchModel.SubProductCategoryID);
                }
            }
            else if (searchModel.ProductCategoryID > 0)
            {
                // 若只选了一级分类，则转换为二级分类进行查询
                if (sb.Length > 0)
                {
                    sb.Append(
                        " And ProductCategoryID in (select ID from Product_Category where ParentID="
                        + searchModel.ProductCategoryID + ")");
                }
                else
                {
                    sb.Append(
                        " ProductCategoryID in (select ID from Product_Category where ParentID="
                        + searchModel.ProductCategoryID + ")");
                }
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(searchModel.ProductName))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" And Name like '%" + searchModel.ProductName + "%'");
                }
                else
                {
                    sb.Append(" Name like '%" + searchModel.ProductName + "%'");
                }
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Barcode))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" And Barcode like '%" + searchModel.Barcode + "%'");
                }
                else
                {
                    sb.Append(" Name Barcode '%" + searchModel.Barcode + "%'");
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}