using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using V5.DataContract.Product;
    using V5.DataContract.Transact.Order;
    using V5.DataContract.Transact.ShoppingCart;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Portal.Attributes;
    using V5.Portal.Models;
    using V5.Service.Product;
    using V5.Service.System;
    using V5.Service.Transact;
    using V5.Service.User;
    using V5.Service.Utility;
    using V5.Portal.Common;

    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return null;
        }

        [CustomAuthorize]
        public ActionResult RemoveAddress(int addressID)
        {
            if (addressID < 1)
            {
                return this.Json(new AjaxResponse(0, "没有操作"));
            }

            var service = new UserReceiveAddressService();
            var userID = service.QueryByID(addressID).UserID;

            if (this.UserSession.UserID != userID)
            {
                return this.Json(new AjaxResponse(-1, "无权修改本地址信息"));
            }

            service.RemoveByID(addressID);

            return this.Json(new AjaxResponse(1, "删除成功"));
        }

        [CustomAuthorize]
        public ActionResult SetAddressDefault(int addressID)
        {
            if (addressID < 1)
            {
                return this.Json(new AjaxResponse(0, "参数错误"));
            }

            var service = new UserReceiveAddressService();
            var userID = service.QueryByID(addressID).UserID;

            if (this.UserSession.UserID != userID)
            {
                return this.Json(new AjaxResponse(-1, "无权修改本地址信息"));
            }

            service.SetDefault(addressID, userID);
            
            return this.Json(new AjaxResponse(1, "操作成功"));
        }

        [CustomAuthorize]
        public ActionResult EditAddress(int addressID)
        {
            if (addressID < 1)
            {
                return this.Json(new AjaxResponse(0, "没错操作"));
            }
            return this.Json(new AjaxResponse(1, "编辑成功"));
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult AddOrEditUserReceiveAddress(int addressId, string consignee, string mobile, string tel, int countyID, string address, int isDefault)
        {
            if (string.IsNullOrWhiteSpace(consignee))
            {
                return this.Json(new AjaxResponse(0, "收货人地址不正确"));
            }

            if (!string.IsNullOrWhiteSpace(mobile) && !CustomValidator.IsMobile(mobile))
            {
                return this.Json(new AjaxResponse(0, "收货人手机号码不正确"));
            }

            if (!string.IsNullOrWhiteSpace(tel) && !CustomValidator.IsPhone(tel))
            {
                return this.Json( new AjaxResponse(0, "收货人电话号码不正确"));
            }

            if (string.IsNullOrWhiteSpace(mobile) && string.IsNullOrWhiteSpace(tel))
            {
                return this.Json(new AjaxResponse(0,"收货人电话号码和手机号码不能都为空"));
            }

            if (countyID < 1)
            {
                return this.Json(new AjaxResponse(0, "收货人省市区地址不正确"));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                return this.Json(new AjaxResponse(0, "收货人详细地址信息不能为空"));
            }

            var userReceiveAddressService = new UserReceiveAddressService();
            var model = new UserReceiveAddressModel
            {
                Consignee = consignee,
                Mobile = mobile,
                Tel = tel,
                CountyID = countyID,
                UserID = this.UserSession.UserID,
                IsDefault = false,
                Address = address
            };
			
			SqlTransaction transaction = null;
            try
            {
                if (addressId > 0)
                {
                    var userReceiveAddress = DataTransfer.Transfer<User_RecieveAddress>(model, typeof(UserReceiveAddressModel));
                    userReceiveAddress.ID = addressId;
					userReceiveAddress.IsDefault = isDefault == 1;
                    userReceiveAddressService.Modify(userReceiveAddress, out transaction);
					transaction.Commit();
                }
                else
                {
                    var userReceiveAddress = DataTransfer.Transfer<User_RecieveAddress>(model, typeof(UserReceiveAddressModel));
					userReceiveAddress.IsDefault = isDefault == 1;
                    addressId = userReceiveAddressService.Add(userReceiveAddress, null);
                }

                model.ID = addressId;
                return this.PartialView("Partial/NewAddress", model);
            }
            catch (Exception exception)
            {
	            if (transaction != null)
	            {
		            transaction.Rollback();
	            }

                LogUtils.Log(
                    "前台用户下单时添加或修改收货地址出错，错误原因:" + exception.Message,
                    "前台添加用户收货信息",
                    Category.Error,
                    this.Session.SessionID,
                    this.UserSession.UserID,
                    "Order/AddOrEditUserReceiveAddress");
                TextLogger.Instance.Log("前台用户下单时添加或修改收货地址出错", Category.Error, exception);
                return Json(new AjaxResponse(-1, "操作失败"));
            }
        }
        
        //提交订单
        [HttpGet]
        [CustomAuthorize]
        public ActionResult OrderInfo(int[] proIds, int[] quantity)
        {
	        try
	        {
				var orderInfo = new OrderInfoViewModel();
				var addresses = new UserReceiveAddressService().QueryReceiveAddressByUserID(this.UserSession.UserID);
				if (addresses != null)
				{
					orderInfo.UserReceiveAddressList = new List<UserReceiveAddressModel>();
					foreach (var userRecieveAddress in addresses)
					{
						orderInfo.UserReceiveAddressList.Add(
							DataTransfer.Transfer<UserReceiveAddressModel>(userRecieveAddress, typeof(User_RecieveAddress)));
					}
				}

				var userCart = MongoDBHelper.GetModel<UserCartModel>(m => m.VisitorKey == this.UserSession.VisitorKey);

	            CartController.WriteUserCartOperationLog(
	                new UserCartOperationLog()
	                    {
	                        OperateTime = DateTime.Now,
	                        SessionKey = this.Session.SessionID,
	                        VisitorKey = this.UserSession.VisitorKey,
	                        OperationType = "Get",
	                        Message =
	                            "订单确认获取当前User购物车（" + this.UserSession.VisitorKey
	                            + "）：Selector:m => m.UserId == this.UserSession.UserID || m.VisitorKey == this.UserSession.VisitorKey",
	                        UserCart =
	                            userCart
	                            ?? new UserCartModel()
	                                   {
	                                       BuyList = new List<CartProduct>(),
	                                       ProductItems = new List<CartProduct>()
	                                   },
	                        UserID = this.UserSession.UserID
	                    });

				var products = BuildCartProducts(proIds, quantity);
				if (userCart == null)
				{
					userCart = new UserCartModel
					{
						ProductItems = products,
						UserId = this.UserSession.UserID,
						VisitorKey = this.UserSession.VisitorKey
					};
				}

				if (userCart.ProductItems == null)
				{
					userCart.ProductItems = new List<CartProduct>();
				}

				foreach (var cartProduct in products)
				{
					if (userCart.ProductItems.FirstOrDefault(p => p.ProductID == cartProduct.ProductID) == null)
					{
						if (cartProduct != null)
						{
							userCart.ProductItems.Add(cartProduct);
						}
					}
				}

				userCart.BuyList = products;

	            MongoDBHelper.UpdateModel<UserCartModel>(userCart, u => u.VisitorKey == this.UserSession.VisitorKey);

	            CartController.WriteUserCartOperationLog(
	                new UserCartOperationLog()
	                    {
	                        OperateTime = DateTime.Now,
	                        SessionKey = this.Session.SessionID,
	                        VisitorKey = this.UserSession.VisitorKey,
	                        OperationType = "Get",
	                        Message =
	                            "订单确认已更新User购物车（" + this.UserSession.VisitorKey
	                            + "）：Selector:m => m.UserId == this.UserSession.UserID || m.VisitorKey == this.UserSession.VisitorKey",
	                        UserCart =
	                            MongoDBHelper.GetModel<UserCartModel>(
	                                m => m.VisitorKey == this.UserSession.VisitorKey)
	                            ?? new UserCartModel()
	                                   {
	                                       BuyList = new List<CartProduct>(),
	                                       ProductItems = new List<CartProduct>()
	                                   },
	                        UserID = this.UserSession.UserID
	                    });

				orderInfo.Products = userCart.BuyList;

				//获取促销相关信息
				var productDictionary = new Dictionary<int, int>();
				foreach (var cartProduct in orderInfo.Products)
				{
					productDictionary.Add(cartProduct.ProductID, cartProduct.Quantity);
				}

				var orderBill = this.GetOrderBill(productDictionary);


				foreach (var product in orderBill.Products)
				{
					if (product.ProductID == 4153)
					{
						orderBill.TotalPrice -= product.GoujiuPrice;
						product.PromotePrice = 0;
						product.Quantity = 1;
					}
				}

				orderInfo.BillDetail = orderBill;

				return this.View(orderInfo);
	        }
	        catch (Exception exception)
	        {
		        LogUtils.Log(
			        string.Format(
				        "订单核对出错，参数信息：proIds：{0},quantity:{1},错误消息：{2}，堆栈：{3}",
				        proIds,
				        quantity,
				        exception.Message,
				        exception.StackTrace),
			        "OrderInfo",
			        Category.Error);
		        throw;
	        }
        }

		/// <summary>
		/// 获取订单结算信息
		/// </summary>
		/// <param name="productDictionary"></param>
		/// <returns></returns>
	    private Order_Bill GetOrderBill(Dictionary<int, int> productDictionary)
	    {
		    Order_Bill orderBill = null;
		    try
		    {
			    orderBill = new OrderBillServices().QueryOrderBill(productDictionary, this.GetUserID());
		    }
		    catch (Exception exception)
		    {
			    LogUtils.Log("获取订单促销信息失败，出错原因：" + exception.Message + ";错误堆栈：" + exception.StackTrace, "OrderInfo", Category.Warn);
		    }

		    if (orderBill == null || orderBill.Products == null || orderBill.Products.Count < 1)
		    {
			    LogUtils.Log("没有获取订单促销信息", "OrderInfo", Category.Warn);
		    }
			return orderBill ?? new Order_Bill();
	    }

	    private List<CartProduct> BuildCartProducts(int[] proIds, int[] quantity)
        {
            var products = new List<CartProduct>();
	        var whereStr = new StringBuilder();
	        var messageSB = new StringBuilder();
	        whereStr.Append("ID in (");
	        for (int i = 0; i < proIds.Length; i++)
	        {
		        if (i < proIds.Length - 1)
		        {
			        whereStr.Append(proIds[i]).Append(",");
		        }
		        else
		        {
			        whereStr.Append(proIds[i]);
		        }
	        }
	        whereStr.Append(")");

			var list = new ProductService().Query(ProductType.Rand, 100, whereStr.ToString());

	        if (list == null)
	        {
		        LogUtils.Log("根据商品ID从数据批量获取商品信息为NULL;商品列表：" + proIds, "BuildCartProducts", Category.Warn);
		        return new List<CartProduct>();
	        }

	        for (int i = 0; i < proIds.Length; i++)
	        {
		        var product = list.FirstOrDefault(p => p.ID == proIds[i]);
		        if (product != null)
		        {
			        if (quantity[i] <= 0)
			        {
						messageSB.Append("商品：" + product.Name).Append(" 购买数量不能为0").Append("\r\n");
			        }
			        else if (product.Status!=2)
			        {
				        messageSB.Append("商品：" + product.Name).Append(" 已下架；").Append("\r\n");
			        }
					else if (product.InventoryNumber < 1)
					{
						messageSB.Append("商品：" + product.Name).Append(" 库存不足；").Append("\r\n");
					}
					else if (product.InventoryNumber < quantity[i])
					{
						messageSB.Append("商品：" + product.Name).Append(" 库存剩余").Append(product.InventoryNumber).Append("件；\r\n");
						products.Add(this.ConvertToCartProduct(product, product.InventoryNumber));
					}
					else
					{
						products.Add(this.ConvertToCartProduct(product, quantity[i]));
					}
		        }
	        }

	        ViewBag.ExtraMessage = messageSB.ToString();

            return products;
        }

		private CartProduct ConvertToCartProduct(ProductSearchResult product, int quantity)
        {
            if (product != null && product.InventoryNumber >0) //若库存不足，则不允许下单
            {
                var cartProduct = new CartProduct
                                      {
                                          ProductID = product.ID,
                                          ProductName = product.Name,
                                          GoujiuPrice = product.GoujiuPrice,
                                          Discount = 0,
                                          ProductPic = product.ThumbnailPath,
                                          Quantity =
                                              quantity > product.InventoryNumber
                                                  ? product.InventoryNumber
                                                  : quantity,
                                          UpdateTime = DateTime.Now
                                      };
                return cartProduct;
            }
            return null;
        }

        /// <summary>
        /// 检查余额是否足够
        /// </summary>
        /// <param name="balance">余额</param>
        /// <returns></returns>
        private bool CheckAccountBalance(double balance)
        {
            if (balance <= 0)
            {
                return true;
            }
            //todo:调用方法检查余额是否足够
            return false;
        }

	    private bool CheckBuyProducts(List<CartProduct> buyProducts,out string errorMessage)
	    {
			var products = new List<CartProduct>();
		    var messageSB = new StringBuilder();
			var whereStr = new StringBuilder();

			whereStr.Append("ID in (");
			for (int i = 0; i < buyProducts.Count; i++)
			{
				if (i < buyProducts.Count - 1)
				{
					whereStr.Append(buyProducts[i].ProductID).Append(",");
				}
				else
				{
					whereStr.Append(buyProducts[i].ProductID);
				}
			}
			whereStr.Append(")");

			var productList = new ProductService().Query(ProductType.Rand, 100, whereStr.ToString());

		    if (productList == null || productList.Count < 1)
		    {
			    errorMessage = "商品不存在或已下架。";
			    LogUtils.Log("根据商品ID从数据批量获取商品信息为NULL;商品查询条件：" + whereStr, "BuildCartProducts", Category.Warn);
			    return false;
		    }

		    foreach (var cartProduct in buyProducts)
		    {
				var quantity = 0;
				var response = this.CheckSpecialPromote(cartProduct.ProductID, ref quantity);

				if (response != null) //特殊全场活动赠品
				{
					if (response.State == 0) //不允许领取赠品
					{
						messageSB.Append("商品：").Append(cartProduct.ProductName).Append(" ").Append(response.Message).Append("\r\n");
						continue;
					}
					else
					{
						cartProduct.GoujiuPrice = 0;
						if (cartProduct.Quantity > quantity) //若数量大于可赠送数量
						{
							messageSB.Append("商品：")
								.Append(cartProduct.ProductName)
								.Append(" ")
								.Append("为活动赠品，最多可领取 " + quantity + "件。")
								.Append("\r\n");
							continue;
						}
					}
				}

			    var product = productList.FirstOrDefault(p => p.ID == cartProduct.ProductID);

				if (product.Status != 2)
				{
					messageSB.Append("商品：" + product.Name).Append(" 已下架；").Append("\r\n");
				}
				else if (product.InventoryNumber < 1)
				{
					messageSB.Append("商品：" + product.Name).Append(" 库存不足；").Append("\r\n");
				}
				else if (product.InventoryNumber < cartProduct.Quantity)
				{
					messageSB.Append("商品：" + product.Name).Append(" 库存剩余").Append(product.InventoryNumber).Append("件；\r\n");
					products.Add(this.ConvertToCartProduct(product, product.InventoryNumber));
				}
				else
				{
					products.Add(this.ConvertToCartProduct(product, cartProduct.Quantity));
				}
		    }

			errorMessage = messageSB.ToString();

		    if (!string.IsNullOrWhiteSpace(messageSB.ToString())) //检查不通过
		    {
			    return false;
		    }

			//检查通过
		    return true;
	    }

		/// <summary>
		/// 检查特殊全局促销信息
		/// </summary>
		/// <param name="product"></param>
		/// <param name="updateQuantity"></param>
		/// <returns></returns>
		private AjaxResponse CheckSpecialPromote(int productId, ref int updateQuantity)
		{
			//验证是否是特殊活动的商品
			if (this.UserSession.UserID > 0)
			{
				var specialPromotes = MongoDBHelper.GetModels<SpecialPromote>(p => p.GiftProductID == productId && p.ID > 0);

				if (specialPromotes != null && specialPromotes.Count > 0) //若不是，则直接跳过
				{
					var specialPromote = specialPromotes[0]; //一个赠品只能参加一个活动
					AjaxResponse ajaxResponse = null;
					if (specialPromote.IsValid == false)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，活动已失效，不能领取。");
						return ajaxResponse;
					}

					if (specialPromote.EndTime < DateTime.Now)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，活动已结束，不能领取。");
						return ajaxResponse;
					}

					if (specialPromote.StartTime > DateTime.Now)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，活动尚未开始，不能领取。");
						return ajaxResponse;
					}

					var userPromote = specialPromote.UserPromotes.FirstOrDefault(p => p.UserID == this.UserSession.UserID);

					//判断是否已参加活动，未参加，则直接跳过
					if (userPromote == null)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，您未参加此活动，不能领取。");
						return ajaxResponse;
					}

					//判断是否已领取活动，已领取，则直接跳过
					if (userPromote.HasGetGift)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，您已领取过来，不能多次领取。");
						return ajaxResponse;
					}

					//判断参加活动是否超过一天
					if ((DateTime.Now - userPromote.ParticipateTime).TotalDays > 1)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "为活动赠品，您今日未参加此活动，不能领取。");
						return ajaxResponse;
					}

					updateQuantity = 1;
					ajaxResponse = new AjaxResponse(1, "活动商品");
					return ajaxResponse;
				}
			}

			return null;
		}

		/// <summary>
		/// 获取CPS 编码 的Cookie信息
		/// </summary>
		/// <returns></returns>
		private int GetCpsID()
		{
			//检查CPS订单
			var cpsCookie = this.Request.Cookies["CPS_IN"];
			if (cpsCookie != null)
			{
				if (cpsCookie["SiteID"] == "5")//亿玛(亿起发)订单
				{
					return 5;
				}
			}

			return 0; //官网订单
		}

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Add(int addressID, int payMethod, string productIds, string intro, 
            int isRequireInvoice, string invoiceTitle, int invoiceContent, int ctype, int cId, double account)
        {
            /***************
             * 前台添加订单流程：
             * 1.检查是否用券，若用，则检查券是否存在，是否符合使用条件。
             * 2.检查是否用余额抵扣，若用，则检查用户是否有足够余额。
             * 3.检查用户是否开发票，若是，检查发票内容是否填写。
             * 4.检查用户收货地址ID是否填写正确
             * 5.检查商品ID列表是否正确
             * todo:下单时检查商品库存
             * ********************/

            #region 检查是否用券
            //todo:券检查
            #endregion

            #region 余额抵用检查

            if (account > 0)
            {
                if (!CheckAccountBalance(account))
                {
                    return this.Json(new AjaxResponse(-1, "账户余额不足"));
                }
            }

            #endregion

            #region 检查发票信息

            if (isRequireInvoice > 0)
            {
                if (invoiceContent < 0)
                {
                    return this.Json(new AjaxResponse(-1, "发票内容错误"));
                }
            }

            #endregion

            #region 检查收货地址ID是否填写正确

            if (addressID <= 0)
            {
                return this.Json(new AjaxResponse(-1, "收货地址错误"));
            }

            var address = new UserReceiveAddressService().QueryByID(addressID);
            
            //收货地址必须与用户对应
            if (address == null || address.UserID != this.UserSession.UserID)
            {
                return this.Json(new AjaxResponse(-1, "收货地址错误"));
            }

            var addressModel = DataTransfer.Transfer<UserReceiveAddressModel>(address, typeof(User_RecieveAddress));

            if (!new UtilityController().ValidSupportRegion(addressModel.ProvinceID))
            {
                return this.Json(new AjaxResponse(-1, "对不起，" + addressModel.CountyName("-") + "暂不支持。"));
            }

            #endregion
           
            var orderService = new OrderService(this.UserSession.UserID, false);
            try
            {
                var cart = MongoDBHelper.GetModel<UserCartModel>(u => u.VisitorKey == this.UserSession.VisitorKey);
	            if (cart == null || cart.BuyList == null || cart.BuyList.Count < 1)
	            {
		            return this.Json(new AjaxResponse(-1, "商品不存在或已下架。"));
				}

				string message;

				#region 检查商品列表信息

	            if (!CheckBuyProducts(cart.BuyList, out message))
	            {
		            LogUtils.Log(
			            message,
			            "前台提交订单:Add",
			            Category.Info,
			            this.UserSession.SessionId,
			            this.UserSession.UserID,
			            "Order/Add");
		            return this.Json(new AjaxResponse(0, message));
	            }

				#endregion

				//获取促销相关信息
				var productDictionary = new Dictionary<int, int>();

				foreach (var orderProduct in cart.BuyList)
				{
					productDictionary.Add(orderProduct.ProductID, orderProduct.Quantity);
				}

				var orderBill = this.GetOrderBill(productDictionary); //new OrderBillServices().QueryOrderBill(productDictionary, this.UserSession.UserID);

	            if (orderBill == null
	                || ((orderBill.Products == null || orderBill.Products.Count < 1)
	                    && (orderBill.SuitPromoteInfos == null || orderBill.SuitPromoteInfos.Count < 1)))
	            {
		            LogUtils.Log("没有获取订单促销信息", "Add", Category.Warn);
		            return this.Json(new AjaxResponse(-1, "商品不存在或者已下架。"));
	            }

	            #region 检查促销活动
                
				//限购验证
				foreach (var cartProduct in orderBill.Products)
				{
                    //验证是否有多选一情况
				    if (cartProduct.DenyFlag > 0)
				    {
				        switch (cartProduct.DenyFlag)
				        {
				            case 1:
				                return this.Json(new AjaxResponse(0, string.Format("对不起，{0} 只允许新会员购买！", cartProduct.ProductName)));
				            case 2:
				                return this.Json(new AjaxResponse(0, string.Format("对不起，{0} 只允许老会员购买!", cartProduct.ProductName)));
				            case 3:
				                return
				                    this.Json(new AjaxResponse(0, string.Format("对不起，{0} 只允许通过手机验证会员购买!", cartProduct.ProductName)));
				            case 4:
				                return
				                    this.Json(new AjaxResponse(0, string.Format("对不起，{0} 只允许通过邮箱验证会员购买。", cartProduct.ProductName)));
				            case 5:
				                return this.Json(new AjaxResponse(0, string.Format("对不起，{0} 请先登录。", cartProduct.ProductName)));
				            default:
				                return
				                    this.Json(new AjaxResponse(0, string.Format("对不起，{0} 您不满足此商品的购买条件。", cartProduct.ProductName)));
				        }
				    }

                    //验证多选一互斥促销
                    if (cartProduct.Promotes!=null&&cartProduct.Promotes.FirstOrDefault(p => p.PromoteType == 4) != null && !string.IsNullOrWhiteSpace(cartProduct.Exclude))
                    {
                        var userCart = MongoDBHelper.GetModel<UserCartModel>(u => u.VisitorKey == this.UserSession.VisitorKey);
                        if (userCart != null && userCart.ProductItems.Count > 0)
                        {
                            var excludes = cartProduct.Exclude.Split(',');
                            if (excludes.Length > 0)
                            {
                                foreach (var item in userCart.ProductItems)
                                {
                                    if (item.ProductID != cartProduct.ProductID && excludes.Contains(item.ProductID.ToString()))
                                    {
                                        return
                                            this.Json(
                                                new AjaxResponse(
                                                    0,
                                                    string.Format(
                                                        "对不起，【{0}】与【{1}】不能同时购买。",
                                                        item.ProductName,
                                                        cartProduct.ProductName)));
                                    }
                                }
                            }
                        }
                    }

				    //判断是否是限时抢购，且是否超出了限制量。
					if (cartProduct.LimitedBuyQuantity > 0 && cartProduct.MaxBuyQuantity < cartProduct.Quantity)
					{
						LogUtils.Log("用户订单商品数量超出了限时抢购数量", "Add", Category.Info);
						return
							this.Json(
								new AjaxResponse(
									0,
									string.Format(
										"对不起，{0} 每人限购{1}件，你还可以购买{2}件。",
										cartProduct.ProductName,
										cartProduct.LimitedBuyQuantity,
										cartProduct.MaxBuyQuantity)));
					}

					//若是限时抢购，判断是否还有活动库存
					if (cartProduct.LimitedBuyQuantity > 0 && cartProduct.PromoteResidueQuantity < cartProduct.Quantity)
					{
						LogUtils.Log(
							string.Format(
								"{0} 活动库存不满足，库存仅剩 {1},用户下单{2}",
								cartProduct.ProductName,
								cartProduct.PromoteResidueQuantity,
								cartProduct.Quantity),
							"Add",
							Category.Info);

						return
							this.Json(
								new AjaxResponse(
									0,
									string.Format(
										"对不起，{0} 活动库存不满足，库存仅剩 {1}件。",
										cartProduct.ProductName,
										cartProduct.PromoteResidueQuantity)));
					}
				}

				//检查组合促销商品限购情况
	            if (orderBill.SuitPromoteInfos != null)
	            {
		            foreach (var suitPromoteInfo in orderBill.SuitPromoteInfos)
		            {
			            if (suitPromoteInfo.Products != null)
			            {
				            foreach (var cartProduct in suitPromoteInfo.Products)
				            {
								//判断是否是限时抢购，且是否超出了限制量。
								if (cartProduct.LimitedBuyQuantity > 0 && cartProduct.MaxBuyQuantity < cartProduct.Quantity)
								{
									LogUtils.Log("用户订单商品数量超出了限时抢购数量", "Add", Category.Info);
									return
										this.Json(
											new AjaxResponse(
												0,
												string.Format(
													"对不起，{0} 每人限购{1}件，你还可以购买{2}件。",
													cartProduct.ProductName,
													cartProduct.LimitedBuyQuantity,
													cartProduct.MaxBuyQuantity)));
								}

								//若是限时抢购，判断是否还有活动库存
								if (cartProduct.LimitedBuyQuantity > 0 && cartProduct.PromoteResidueQuantity < cartProduct.Quantity)
								{
									LogUtils.Log(
										string.Format(
											"{0} 活动库存不满足，库存仅剩 {1},用户下单{2}",
											cartProduct.ProductName,
											cartProduct.PromoteResidueQuantity,
											cartProduct.Quantity),
										"Add",
										Category.Info);

									return
										this.Json(
											new AjaxResponse(
												0,
												string.Format(
													"对不起，{0} 活动库存不满足，库存仅剩 {1}件。",
													cartProduct.ProductName,
													cartProduct.PromoteResidueQuantity)));
								}
				            }
			            }
		            }
	            }
				#endregion

				DateTime createTime;
	            var orderCode = MadeCodeService.GetOrderCode(out createTime);
	            var order = new Order
		                        {
			                        UserID = this.UserSession.UserID,
			                        RecieveAddressID = addressID,
			                        CpsID = 0,
			                        PaymentMethodID = payMethod,
			                        OrderCode = orderCode,
			                        OrderNumber = MadeCodeService.ReverseOrderCode(orderCode, createTime),
			                        TotalMoney = 0,
			                        TotalIntegral = 0,
			                        PaymentStatus = 0,
			                        IsRequireInvoice = isRequireInvoice != 0,
			                        Status = payMethod == 0 ? 100 : 0, // 若是在线支付，则设置为等待付款，否则设置为等待确认
			                        Remark = intro,
			                        CreateTime = createTime
		                        };

				//获取CPS信息
				order.CpsID = this.GetCpsID();

                Order_Invoice invoice = null;

                if (isRequireInvoice!=0) //需要开发票
                {
	                invoice = new Order_Invoice
		                          {
			                          InvoiceContentID = invoiceContent,
			                          InvoiceTitle =
				                          string.IsNullOrWhiteSpace(invoiceTitle) ? "个人" : invoiceTitle,
			                          InvoiceTypeID = 0
		                          };
                }
				
				var promoteList = new List<Order_Product_Promote>();
				var buyProducts=new List<Order_Product>();
	            var giftCoupons = new List<Gift_Coupon>();

	            double totalAmount = 0, totalDiscount = 0, tempDiscount = 0;

				totalAmount += SetBuyList(order.ID, orderBill.Products, ref buyProducts, ref promoteList, out tempDiscount);
	            totalDiscount += tempDiscount;
				
				//组合促销商品
	            if (orderBill.SuitPromoteInfos != null)
	            {
		            foreach (var suitPromote in orderBill.SuitPromoteInfos)
		            {
			            totalDiscount += suitPromote.PromoteDiscount;

			            totalAmount += SetBuyList(
				            order.ID,
				            suitPromote.Products,
				            ref buyProducts,
				            ref promoteList,
				            out tempDiscount);

			            totalDiscount += tempDiscount;

						//成交价 = 单品成交价 - 组合优惠摊牌金额
			            if (suitPromote.PromoteDiscount > 0)
			            {
				            for (var i = 0; i < buyProducts.Count; i++)
				            {
					            if (suitPromote.Products.Exists(p => p.ProductID == buyProducts[i].ProductID))
					            {
									buyProducts[i].TransactPrice -= buyProducts[i].TransactPrice / suitPromote.TotalPrice
																	* suitPromote.PromoteDiscount;

									//组合促销信息 
									promoteList.Add(
										new Order_Product_Promote
										{
											ProductID = buyProducts[i].ProductID,
											PromoteID = suitPromote.PromoteID,
											PromoteDiscount =
												Math.Round(
													buyProducts[i].TransactPrice / suitPromote.TotalPrice
													* suitPromote.PromoteDiscount,
													2),
											PromoteType = suitPromote.PromoteType
										});
					            }
				            }
			            }

			            //赠品
			            if (suitPromote.GiftProducts != null)
			            {
				            foreach (var giftProduct in suitPromote.GiftProducts)
				            {
					            var product = new Order_Product
						                          {
							                          ProductID = giftProduct.ProductID,
							                          ProductName = "【赠品】" + giftProduct.ProductName,
							                          Quantity = giftProduct.Quantity,
							                          PromotionID = giftProduct.PromotID,
							                          PromotionType = giftProduct.PromotType,
							                          TransactPrice = 0,
							                          PromotionResult = 1,
							                          Integral = 0,
							                          RebateRate = 0,
							                          Commission = 0,
							                          CreateTime = DateTime.Now
						                          };
					            buyProducts.Add(product);

					            promoteList.Add(
						            new Order_Product_Promote
							            {
								            OrderID = order.ID,
											ProductID = giftProduct.ProductID,
								            PromoteID = giftProduct.PromotID,
								            PromoteType = giftProduct.PromotType,
											ExtField = "赠品"
							            });
				            }
			            }

			            if (suitPromote.GiftCoupons != null)
			            {
				            giftCoupons.AddRange(suitPromote.GiftCoupons);
			            }
		            }
	            }
				
	            order.TotalMoney = orderBill.TotalPrice - orderBill.TotalDiscount;

				//todo:特殊促销处理，此处为了快速开发，硬编码
	            foreach (var cartProduct in buyProducts)
	            {
		            if (cartProduct.ProductID == 4153)
		            {
			            order.TotalMoney = order.TotalMoney - cartProduct.TransactPrice;
			            cartProduct.TransactPrice = 0;
			            cartProduct.Quantity = 1;
		            }
	            }

	            order.DeliveryCost = order.TotalMoney >= 100 ? 0 : 10; //todo:需要改掉
	            order.Discount = totalDiscount;
				var orderId = orderService.Add(order, buyProducts, invoice, giftCoupons, promoteList);
	            this.ResetUserCart(cart, buyProducts);

				//更新特殊活动
	            foreach (var orderProduct in buyProducts)
				{
					var specialPromotes = MongoDBHelper.GetModels<SpecialPromote>(p => p.GiftProductID == orderProduct.ProductID && p.ID > 0);

					if (specialPromotes != null && specialPromotes.Count > 0) //若不是，则直接跳过
					{
						var specialPromote = specialPromotes[0]; //一个赠品只能参加一个活动

						var userPromote = specialPromote.UserPromotes.FirstOrDefault(u => u.UserID == this.UserSession.UserID);

						//即使找不到参加活动的记录，但领取了商品，我们依然认为此用户已经参加了活动
						if (userPromote == null)
						{
							userPromote = new UserPromote { UserID = this.UserSession.UserID };
						}

						userPromote.OrderID = order.ID;
						userPromote.HasGetGift=true;
						userPromote.GetGiftTime = DateTime.Now;
						userPromote.GiftID = orderProduct.ProductID;

						specialPromote.UserPromotes.Remove(u => u.UserID == userPromote.UserID);
						specialPromote.UserPromotes.Add(userPromote);

						MongoDBHelper.UpdateModel<SpecialPromote>(specialPromote, sp => sp.ID == specialPromote.ID);
					}
	            }

	            LogUtils.Log(
		            "前台添加订单成功，订单编码：" + orderId + "\n\r",
		            "Order.Add",
		            Category.Info,
		            this.UserSession.SessionId,
		            this.UserSession.UserID);

                return this.Json(new AjaxResponse(1, "订单提交成功", order.OrderCode));
            }
            catch (Exception ex)
            {
                LogUtils.Log(
                    "前台添加订单出错了，错误信息："+ex.Message+"\n\r"+ex.StackTrace,
                    "Order.Add",
                    Category.Error,
                    this.UserSession.SessionId,
                    this.UserSession.UserID);
                return this.Json(new AjaxResponse(-2, "对不起，提交订单出错了。"));
            }
        }

		private double SetBuyList(int orderID, IEnumerable<Cart_Product> products, ref List<Order_Product> buyProducts, ref List<Order_Product_Promote> promoteList, out double tempDiscount)
	    {
		    double totalAmount = 0;
			tempDiscount = 0;
		    foreach (var product in products)
		    {
			    buyProducts.Add(
				    new Order_Product
					    {
						    ProductID = product.ProductID,
						    Quantity = product.Quantity,
						    ProductName = product.ProductName,
						    GoujiuPrice = Math.Round(product.GoujiuPrice,2),
						    TransactPrice = Math.Round(product.PromotePrice,2),
						    PromotionResult = 1,
						    RebateRate = 0,
						    Remark = "",
						    Integral = (int)product.PromotePrice / 1,
						    Commission = 0
					    });

			    tempDiscount += product.FavorablePrice;

				//单品促销信息 
			    if (product.Promotes != null && product.Promotes.Count > 0)
			    {
				    foreach (var promote in product.Promotes)
				    {
					    promoteList.Add(
						    new Order_Product_Promote
							    {
								    OrderID = orderID,
								    ProductID = product.ProductID,
								    PromoteID = promote.PromoteID,
								    PromoteDiscount = Math.Round(product.GoujiuPrice - product.PromotePrice, 2),
								    PromoteType = promote.PromoteType
							    });
				    }
			    }

			    totalAmount += product.Subtotal;
		    }

		    return totalAmount;
	    }

	    /// <summary>
        /// 更新用户购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="removeItems"></param>
        private void ResetUserCart(UserCartModel cart, IEnumerable<Order_Product> removeItems)
        {
            foreach (var orderProduct in removeItems)
            {
                Order_Product product = orderProduct;
                cart.ProductItems.Remove(p => p.ProductID == product.ProductID);
	            cart.BuyList = new List<CartProduct>();
            }
            MongoDBHelper.UpdateModel<UserCartModel>(cart, c => c.VisitorKey == this.UserSession.VisitorKey);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Success(string ono, int payType)
        {
			var order = new OrderService().QueryByOrderCode(ono);
            if (order.UserID != this.UserSession.UserID)
            {
                return this.View(new Order());
            }

            this.ViewBag.PayType = payType;
            return this.View(order);
        }

        [CustomAuthorize]
        public ActionResult Payment(string ono)
        {
            var order = new OrderService().QueryByOrderCode(ono);
            if (order.UserID != this.UserSession.UserID || order.Status != 100)
            {
                order = null;
            }
            return this.View(order);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult OrderDetail(string ono)
        {
            var orderDetailModel = new OrderDetailModel();

            if (!string.IsNullOrWhiteSpace(ono))
            {
                var order = new OrderService().QueryByOrderCode(ono);

                if (order == null || order.UserID != this.UserSession.UserID)
                {
                    return this.View("OrderDetail", null);
                }

                var orderID = order.ID;

                orderDetailModel.OrderInfo = DataTransfer.Transfer<OrderModel>(order, typeof(Order));
                var orderStatusTrackings = new OrderStatusTrackingService().QueryByOrderID(orderID);

                #region 订单状态信息

                var orderState = new OrderState { PaymentMethod = order.PaymentMethodID };
                switch (order.Status)
                {
                    case 0:
                    case 1:
                        orderState.Status = order.PaymentMethodID == 0 ? 1 : 0;
                        break;
                    case 2:
                        orderState.Status = 2;
                        break;
                    case 3:
                        orderState.Status = 4;
                        break;
                    case 100:
                        if (order.PaymentMethodID == 0)
                        {
                            orderState.Status = 0;
                        }

                        break;
                }

                if (orderStatusTrackings != null)
                {
                    orderState.ProcessDatetimes = new List<string>();
                    foreach (var orderStatusTracking in orderStatusTrackings)
                    {
                        if (orderState.Status == 0 && orderStatusTracking.Status == 0)
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (order.PaymentMethodID == 0 && orderState.Status == 1
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (orderState.Status == 2
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1
                                || orderStatusTracking.Status == 2))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }

                        if (orderState.Status == 4
                            && (orderStatusTracking.Status == 0 || orderStatusTracking.Status == 1
                                || orderStatusTracking.Status == 2 || orderStatusTracking.Status == 3))
                        {
                            orderState.ProcessDatetimes.Add(
                                orderStatusTracking.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"));
                        }
                    }
                }

                orderDetailModel.CurrentOrderState = orderState;

                #endregion

                #region 订单发票信息

                if (order.IsRequireInvoice)
                {
                    orderDetailModel.InvoiceInfo =
                        DataTransfer.Transfer<OrderInvoiceModel>(
                            new OrderInvoiceService().SelectByOrderID(orderID),
                            typeof(Order_Invoice));
                }

                #endregion

                #region 订单变化追踪记录
                orderDetailModel.OrderTrackDetails = new List<OrderTrackDetailModel>();

                if (orderStatusTrackings != null)
                {
                    foreach (var orderStatusTracking in orderStatusTrackings)
                    {
                        orderDetailModel.OrderTrackDetails.Add(
                            new OrderTrackDetailModel()
                            {
                                Operator =
                                    orderStatusTracking.EmployeeID > 0
                                        ? new SystemEmployeeService().QueryByID(orderStatusTracking.EmployeeID).Name
                                        : (orderStatusTracking.UserID > 0) ? "客户" : "系统",
                                OperateSummary = orderStatusTracking.Remark,
                                OperateTime = orderStatusTracking.CreateTime
                            });

                        if (orderStatusTracking.Status == 2)
                        {
                            // 若产品已发货，则获取快递跟踪信息
                            var orderDeliverTrackDetail =
                                new OrderDeliveryTrackDetailService().QueryOrderDeliveryTrackDetailsByOrderID(orderID);
                            if (orderDeliverTrackDetail != null&&!string.IsNullOrWhiteSpace(orderDeliverTrackDetail.Steps))
                            {
	                            var details = orderDeliverTrackDetail.Steps.Split(';');

	                            foreach (var orderDeliveryTrackingDetail in details)
	                            {
									if (!string.IsNullOrWhiteSpace(orderDeliveryTrackingDetail))
									{
										var di = orderDeliveryTrackingDetail.Split('|');
										if (di.Length >= 2)
										{
											var tracking = new OrderTrackDetailModel { OperateSummary = di[1], Operator = "快递公司" };
											
											DateTime dt;
											if (DateTime.TryParse(di[0], out dt))
											{
												tracking.OperateTime = dt;
											}
											orderDetailModel.OrderTrackDetails.Add(tracking);
										}
									}
	                            }
                            }
                        }
                    }
                }

                #region 老的方法

                /*
                string summary = string.Empty;
                if (order.PaymentMethodID == 0)
                {
                    summary = order.PaymentStatus == 0 ? "您提交了订单，等待付款。" : "您提交了订单，商品准备出库。";
                }
                else if (order.PaymentMethodID == 1)
                {
                    summary = "您提交了订单，请等待确认。";
                }

                orderDetailModel.OrderDeliveryTrackDetails.Add(
                       new OrderDeliveryTrackDetailModel
                       {
                           OperateSummary = summary,
                           Operator = "客户",
                           OperateTime = order.CreateTime
                       });

                if (order.Status == 6)
                {
                    orderDetailModel.OrderDeliveryTrackDetails.Add(
                       new OrderDeliveryTrackDetailModel
                       {
                           OperateSummary = "订单已取消",
                           Operator = "客户",
                           OperateTime = order.CreateTime
                       });
                }
                else if (order.Status == 4 || order.Status == 8)
                {
                    orderDetailModel.OrderDeliveryTrackDetails.Add(
                        new OrderDeliveryTrackDetailModel
                            {
                                OperateSummary = "订单已作废",
                                Operator = "系统",
                                OperateTime = order.CreateTime
                            });
                }
                else
                {
                    if (orderStatusLogs != null)
                    {
                        foreach (var orderStatusLog in orderStatusLogs)
                        {
                            if (orderStatusLog.Status == 1)
                            {
                                summary = "您的订单已确认，准备出库";
                            }

                            if (orderStatusLog.Status == 2)
                            {
                                summary = "您的订单已出库"; // todo: 添加快递单号,快递公司名称
                            }

                            orderDetailModel.OrderDeliveryTrackDetails.Add(
                            new OrderDeliveryTrackDetailModel
                            {
                                OperateSummary = summary,
                                Operator = new SystemEmployeeService().QueryByID(orderStatusLog.EmployeeID).Name,  // todo: 添加操作员工名称
                                OperateTime = order.CreateTime
                            });
                        }
                    }
                }

                // 若订单已发货或已签收，则有物流流转信息
                if (order.Status == 3 || order.Status == 2)
                {
                    var orderDeliverTrackDetails =
                        new OrderDeliveryTrackDetailService().QueryOrderDeliveryTrackDetailsByOrderID(orderID);
                    foreach (var orderDeliveryTrackingDetail in orderDeliverTrackDetails)
                    {
                        orderDetailModel.OrderDeliveryTrackDetails.Add(
                            DataTransfer.Transfer<OrderDeliveryTrackDetailModel>(
                                orderDeliveryTrackingDetail,
                                typeof(Order_Delivery_Tracking_Details)));
                    }
                }
                */

                #endregion

                #endregion

                #region 订单收货信息

                orderDetailModel.ReceiverInfo =
                    DataTransfer.Transfer<UserReceiveAddressModel>(
                        new UserReceiveAddressService().QueryByID(order.RecieveAddressID),
                        typeof(User_RecieveAddress));

                #endregion

                #region 订单商品信息

                orderDetailModel.OrderProducts = new List<OrderProductModel>();

                var orderProducts = new OrderProductService().QueryByOrderId(orderID);
                if (orderProducts != null)
                {
                    foreach (var orderProduct in orderProducts)
                    {
                        orderDetailModel.OrderProducts.Add(
                            DataTransfer.Transfer<OrderProductModel>(orderProduct, typeof(Order_Product)));
                    }
                }

                #endregion

                #region 订单支付信息

                orderDetailModel.PaymentInfo = new PaymentInfoModel
                {
                    OrderID = orderID,
                    PaymentMethodID = order.PaymentMethodID,
                    TotalMoney = order.TotalMoney,
                    DeliveryCost = order.DeliveryCost,
                    ActualPaid =
                        order.PaymentStatus == 1
                            ? order.TotalMoney + order.DeliveryCost
                            : 0,
                    ActualMoney =
                        order.TotalMoney + order.DeliveryCost
                };

                #endregion
            }
            return this.View("OrderDetail", orderDetailModel);
        }

        [CustomAuthorize]
        public ActionResult MyOrder()
        {
            var orders = new OrderService(this.UserSession.UserID, false).QueryByUserID(this.UserSession.UserID);
            return this.View("MyOrder", orders);
        }

        [HttpGet]
        public PartialViewResult OrderProducts(int orderID)
        {
            var orderProducts = new OrderProductService().QueryByOrderId(orderID);
            return this.PartialView("Partial/OrderProduct", orderProducts);
        }

		/// <summary>
		/// 获取订单商品明细
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns></returns>
	    public List<Order_Product> GetOrderProducts(int orderId)
	    {
		    return new OrderProductService().QueryByOrderId(orderId);
	    }

		/// <summary>
		/// 取消订单
		/// </summary>
		/// <param name="ono"></param>
		/// <param name="orderCancelCauseID"></param>
		/// <returns></returns>
        public ActionResult Cancel(string ono, int orderCancelCauseID)
        {
            var response = new AjaxResponse();
            //if (orderID < 1 || orderCancelCauseID < 1)
            //{
            //    response.State = -1;
            //    response.Message = "参数错误";
            //    return this.Json(response, JsonRequestBehavior.AllowGet);
            //}

            var order = new OrderService(this.UserSession.UserID, false).QueryByOrderCode(ono);
            if (order == null || order.UserID != this.UserSession.UserID)
            {
                response.State = -1;
                response.Message = "订单编号错误";
                LogUtils.Log(
                    "用户取消订单时订单编号（" + ono + "）错误或者获取对应订单数据为空",
                    "前台取消订单",
                    Category.Error,
                    this.UserSession.SessionId,
                    this.UserSession.UserID,
                    "/Order/Cancel");
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }

            //可取消条件：
            // 1.只有在线支付且未支付
            // 2.订单货到付款且未确认
            if (order.Status != 100 && (order.Status == 0 && order.PaymentMethodID == 0))
            {
                LogUtils.Log(
                    "前台尝试取消不符合取消条件的订单（订单号：" + ono + "）",
                    "前台取消订单",
                    Category.Warn,
                    this.UserSession.SessionId,
                    this.UserSession.UserID,
                    "/Order/Cancel");
                TextLogger.Instance.Log("前台尝试取消不符合取消条件的订单（订单号：" + ono + ",用户名："+this.UserSession.UserID+"）", Category.Warn, null);

                response.State = -1;
                response.Message = "此订单不符合取消条件";
                return this.Json(response);
            }

            try
            {
                var orderCancel = new Order_Cancel
                                      {
                                          OrderID = order.ID,
                                          CreateTime = DateTime.Now,
                                          OrderCancelCauseID = orderCancelCauseID,
                                          UserID = this.UserSession.UserID
                                      };

                int state = new OrderService(this.UserSession.UserID,false).OrderCancelByBackstage(orderCancel);

                // 0-订单状态异常，1-操作成功，2-已发货，3-订单已取消、损失或者作废
                if (state == 0)
                {
                    response.State = 0;
                    response.Message = "订单状态异常";
                }
                else if (state == 1)
                {
                    response.State = 1;
                    response.Message = "订单取消成功";
                }
                else if (state == 2)
                {
                    response.State = 2;
                    response.Message = "已发货，取消订单操作取消";
                }
                else if (state == 3)
                {
                    response.State = 3;
                    response.Message = "订单已取消、已损失或者已作废";
                }
                else if (state == 4)
                {
                    response.State = 4;
                    response.Message = "订单已付款";
                }
                else
                {
                    response.State = 0;
                    response.Message = "订单状态异常";
                }

                LogUtils.Log(
                    "前台尝试取消订单（订单号：" + ono + "），结果：（状态：" + response.State + "；消息：" + response.Message + "）",
                    "前台取消订单",
                    Category.Info,
                    this.UserSession.SessionId,
                    this.UserSession.UserID,
                    "/Order/Cancel");

                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("订单取消操作发生错误", Category.Error, exception);

                response.State = -2;
                response.Message = exception.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
