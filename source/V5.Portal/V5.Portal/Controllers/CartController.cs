using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using System.Linq;

    using Newtonsoft.Json;

    using V5.DataContract.Product;
    using V5.DataContract.Transact.ShoppingCart;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Portal.Filters;
    using V5.Portal.Models;
    using V5.Service.Product;
    using V5.Service.Transact;

    public class CartController : BaseController
    {
        //
        // GET: /Cart/
        [GzipFilter]
        public ActionResult Index()
        {
			var orderBill = this.GetCartProductBill();

	        return this.View("Detail", orderBill);
        }

        /// <summary>
        /// 添加商品到购物车中
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public ActionResult Add(int productId, int quantity)
        {
            return this.Json(this.Edit(productId, quantity, true),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前用户购物车中商品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCartInfo()
        {
            var count = 0;
            var userCart = this.GetUserCart();
            if (userCart != null&&userCart.ProductItems != null)
            {
                foreach (var product in userCart.ProductItems)
                {
                    count += product.Quantity;
                }
            }

            return this.Content(count.ToString());
        }

        [HttpGet]
        [GzipFilter]
        public ActionResult ShoppingCart()
        {
            var userCart = this.GetUserCart();

            return this.View("ShoppingCart", userCart);
        }

        public ActionResult Delete(int proId)
        {
            if (this.RemoveCartProducts(proId))
            {
                return this.Json(new AjaxResponse(1, "删除成功"));
            }

            return this.Json(new AjaxResponse(0, "没有操作"));
        }

        private bool RemoveCartProducts(params int[] proIds)
        {
            string strIds = "";
            if (proIds != null && proIds.Length>0)
            {
                var userCart = this.GetUserCart();
                if (userCart != null && userCart.ProductItems != null)
                {
                    foreach (var proId in proIds)
                    {
                        var item = userCart.ProductItems.Find(p => p.ProductID == proId);
                        userCart.ProductItems.Remove(item);
                        strIds += proId.ToString() + ",";
                    }
	                MongoDBHelper.UpdateModel<UserCartModel>(
		                userCart,
		                uc => uc.VisitorKey == this.UserSession.VisitorKey);

                    WriteUserCartOperationLog(
                        new UserCartOperationLog()
                            {
                                VisitorKey = this.UserSession.VisitorKey,
                                OperateTime = DateTime.Now,
                                SessionKey = this.Session.SessionID,
                                OperationType = "Remove",
                                Message =
                                    "proIds=" + strIds
                                    + ";Selector:uc => uc.VisitorKey == this.UserSession.VisitorKey",
                                UserCart = userCart,
                                UserID = this.UserSession.UserID
                            });
                    
                    return true;
                }
            }
            return false;
        }

        public ActionResult BatchDelete(int[] proIds)
        {
            if (this.RemoveCartProducts(proIds))
            {
                return this.Json(new AjaxResponse(1, "删除成功"));
            }

            return this.Json(new AjaxResponse(-1, "删除成功"));
        }

        public ActionResult Edit(int productId, int quantity)
        {
            return this.Json(this.Edit(productId, quantity, false));
        }

        /// <summary>
        /// 获取用户购物车对象
        /// </summary>
        /// <returns></returns>
        private UserCartModel GetUserCart()
        {
            UserCartModel cartModel = null;
            cartModel = MongoDBHelper.GetModel<UserCartModel>(c => c.VisitorKey == this.UserSession.VisitorKey);
            

            if (cartModel == null)
            {
                cartModel = new UserCartModel()
                                {
                                    VisitorKey = this.UserSession.VisitorKey,
                                    UserId = this.UserSession.UserID,
                                    ProductItems = new List<CartProduct>(),
                                    BuyList = new List<CartProduct>()
                                };
            }

            WriteUserCartOperationLog(
                new UserCartOperationLog()
                    {
                        OperateTime = DateTime.Now,
                        SessionKey = this.Session.SessionID,
                        VisitorKey = this.UserSession.VisitorKey,
                        OperationType = "Get",
                        Message =
                            "Seletor:c => c.VisitorKey == this.UserSession.VisitorKey",
                        UserCart = cartModel,
                        UserID = this.UserSession.UserID
                    });

            return cartModel;
        }

        private AjaxResponse Edit(int productId, int quantity, bool isAdd)
        {
            AjaxResponse ajaxResponse = null;
            if (productId < 1 || quantity < 1)
            {
	            LogUtils.Log(
		            string.Format("加入或者修改购物车时商品/数量 参数错误，错误参数为:productId:{0},quantity:{1}", productId, quantity),
		            "Cart/Edit",
		            Category.Error,
		            this.UserSession.VisitorKey,
					this.GetUserID(), "Cart/Edit");

                return new AjaxResponse(-1, "商品错误~~");
            }
            var userId = this.UserSession.UserID;
            if (userId < 0)
            {
	            LogUtils.Log(
		            string.Format("加入或者修改购物车时用户编码错误，错误参数为:userId：{0}", userId),
		            "Cart/Edit",
		            Category.Error,
		            this.UserSession.VisitorKey,
		            this.GetUserID(),
		            "Cart/Edit");

                return new AjaxResponse(-2, "用户编码错误！");
            }

            var billProduct = new ProductService().QueryByID(productId);

	        if (billProduct == null || billProduct.Status!=2 || billProduct.InventoryNumber < 1)
	        {
		        this.RemoveCartProducts(billProduct.ID);

				LogUtils.Log(
					string.Format("加入或者修改购物车时发现 商品不存在、库存小于1或已下架，商品编码:{0}", productId),
					"Cart/Edit",
					Category.Info,
					this.UserSession.VisitorKey,
					this.GetUserID(),
					"Cart/Edit");

		        return new AjaxResponse(-2,"对不起，此商品库存不足或已下架，不能购买。");
	        }
			
            var cart = this.GetUserCart();

            if (cart == null) //用户购物车不存在
            {
                cart = new UserCartModel
                           {
                               UserId = this.UserSession.UserID,
                               VisitorKey = this.UserSession.VisitorKey,
                               ProductItems = new List<CartProduct>()
                           };
            }

            if (cart.ProductItems == null)
            {
                cart.ProductItems=new List<CartProduct>();
            }

            var cartProduct = cart.ProductItems.Find(p => p.ProductID == productId);

            if (cartProduct == null)
            {
                ajaxResponse = this.AddCartProducts(quantity, billProduct, ref cart);
            }
            else  //更新购物车中的商品
            {
                int updateQuantity = 0;
                if (isAdd)
                {
                    updateQuantity = quantity + cartProduct.Quantity;
                }
                else
                {
                    updateQuantity = quantity;
                }

	            ajaxResponse = this.SetUpdateQuantity(billProduct, ref updateQuantity);

				cartProduct.Quantity = updateQuantity;
            }
			
			cart.BuyList=new List<CartProduct>(); //由于MongoDB不能存储为Null的List对象

            this.UpdateUserCart(cart);

            return ajaxResponse ?? new AjaxResponse(1, "执行成功"); 
        }

        /// <summary>
        /// 写入购物车操作日志
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="log"></param>
        public static void WriteUserCartOperationLog(UserCartOperationLog log)
        {
            var config = Utils.GetAppSettings("UserCartLog");
            if (config != null && config == "true")
            {
                MongoDBHelper.UpdateModel<UserCartOperationLog>(log, u => u.UserID == -1);
            }
        }

        /// <summary>
        /// 写入购物车操作日志
        /// </summary>
        /// <param name="UserCart"></param>
        /// <param name="Message"></param>
        public static void WriteUserCartOperationLog(UserCartModel UserCart, string Message)
        {
            WriteUserCartOperationLog(UserCart, Message, "GET");
        }

        /// <summary>
        /// 写入购物车操作日志
        /// </summary>
        /// <param name="UserCart"></param>
        /// <param name="Message"></param>
        /// <param name="OperationType"></param>
        public static void WriteUserCartOperationLog(UserCartModel UserCart, string Message, string OperationType)
        {
            CartController.WriteUserCartOperationLog(
                   new UserCartOperationLog()
                   {
                       OperateTime = DateTime.Now,
                       SessionKey = UserSessionManager.SessionID,
                       VisitorKey = UserSessionManager.SessionID,
                       OperationType = OperationType,
                       Message = Message,
                       UserCart = UserCart
                            ?? new UserCartModel()
                            {
                                BuyList = new List<CartProduct>(),
                                ProductItems = new List<CartProduct>()
                            },
                       UserID = UserSessionManager.UserID
                   });
        }

        /// <summary>
        /// 合并用户购物车和匿名购物车(此处可优化处理)
        /// </summary>
        public static void CartMerge(int UserId)
        {
            // 匿名购物车
            var anonyCart = MongoDBHelper.GetModel<UserCartModel>(u => u.VisitorKey == UserSessionManager.SessionID);
            CartController.WriteUserCartOperationLog(anonyCart, "获取当前匿名购物车（" + UserSessionManager.SessionID + "）");
            if (anonyCart == null) return;

            // 用户购物车
            var userCart = MongoDBHelper.GetModel<UserCartModel>(u => u.UserId == UserId);
            CartController.WriteUserCartOperationLog(userCart, "获取当前用户购物车（" + UserSessionManager.UserID + "）");


            //移除匿名购物车
            CartRemove(anonyCart);
            CartRemove(userCart.UserId);

            if (userCart != null)
            {
                // 购物车商品合并
                foreach (var productItem in anonyCart.ProductItems)
                {
                    var product = userCart.ProductItems.FirstOrDefault(p => p.ProductID == productItem.ProductID);
                    if (product != null)
                    {
                        // 若原先购物车中已有此商品，则将新数据替代老数据 
                        product.GoujiuPrice = productItem.GoujiuPrice;
                        product.Quantity = productItem.Quantity;
                        product.Discount = productItem.Discount;
                    }
                    else
                    {
                        userCart.ProductItems.Add(productItem);
                    }
                }
                userCart.VisitorKey = UserSessionManager.SessionID; // 更新购物车的访问标示
            }
            else
            {
                //说明用户购物车中没有数据，则直接使用匿名购物车的数据，并更新它成为用户购物车
                userCart = anonyCart;
                userCart.UserId = UserId;
            }

            MongoDBHelper.UpdateModel<UserCartModel>(userCart, uc => uc.VisitorKey == UserSessionManager.SessionID);
        }

        /// <summary>
        /// 移除购物车
        /// </summary>
        /// <param name="cart"></param>
        public static void CartRemove(int UserId)
        {
            // 用户购物车
            var userCart = MongoDBHelper.GetModel<UserCartModel>(u => u.UserId == UserId);
            CartRemove(userCart);
        }

        /// <summary>
        /// 移除购物车
        /// </summary>
        /// <param name="cart"></param>
        public static void CartRemove(UserCartModel cart)
        {
            cart.VisitorKey = Guid.NewGuid().ToString();
            MongoDBHelper.UpdateModel<UserCartModel>(cart, uc => uc.VisitorKey == cart.VisitorKey); //此处为了防止删除异常，故现将对象visitorkey更新为新的key，然后再移除这个key
            MongoDBHelper.RemoveModel<UserCartModel>(uc => uc.VisitorKey == cart.VisitorKey);
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="updateQuantity"></param>
        /// <param name="product"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        private AjaxResponse AddCartProducts(int updateQuantity, ProductSearchResult product, ref UserCartModel cart)
	    {
		    if (cart.ProductItems == null)
		    {
			    cart.ProductItems = new List<CartProduct>();
		    }

		    AjaxResponse ajaxResponse = SetUpdateQuantity(product, ref updateQuantity);

		    if (updateQuantity > 0)
		    {
				cart.ProductItems.Add(this.ConvertToCartProduct(product, updateQuantity)); //设置购买数量
		    }

			return ajaxResponse;
	    }

		/// <summary>
		/// 检查库存、限购信息并设置购买数量
		/// </summary>
		/// <param name="product"></param>
		/// <param name="updateQuantity"></param>
		/// <returns></returns>
		private AjaxResponse SetUpdateQuantity(ProductSearchResult product, ref int updateQuantity)
		{
			AjaxResponse ajaxResponse = null;

			ajaxResponse = CheckSpecialPromote(product.ID, ref updateQuantity);

			//此商品参加了特殊全局促销活动
			if (ajaxResponse != null)
			{
				return ajaxResponse;
			}

			var billProduct = new OrderBillServices().QueryCartProduct(product.ID, updateQuantity, this.UserSession.UserID);
 
            //不允许购买情况
		    if (billProduct.DenyFlag > 0)
		    {
		        switch (billProduct.DenyFlag)
		        {
		            case 1:
		                ajaxResponse = new AjaxResponse(
		                    11,
		                    "对不起，此商品只允许新会员购买!");
		                updateQuantity = 0;
		                return ajaxResponse;
		            case 2:
		                ajaxResponse = new AjaxResponse(
		                    12,
		                    "对不起，此商品只允许老会员购买!");
		                updateQuantity = 0;
		                return ajaxResponse;
		            case 3:
		                ajaxResponse = new AjaxResponse(
		                    13,
		                    "对不起，此商品只允许通过手机验证会员购买。");
		                updateQuantity = 0;
		                return ajaxResponse;
		            case 4:
		                ajaxResponse = new AjaxResponse(
		                    14,
		                    "对不起，此商品只允许通过邮箱验证会员购买。");
		                updateQuantity = 0;
		                return ajaxResponse;
		            case 5:
		                ajaxResponse = new AjaxResponse(
		                    15,
		                    "对不起，此商品仅限新会员购买，请先注册或登录。");
		                updateQuantity = 0;
                        return ajaxResponse;
                    case 6:
                        ajaxResponse = new AjaxResponse(
                            16,
                            "对不起，您已参加过此活动。");
                        updateQuantity = 0;
                        return ajaxResponse;
		            default:
		                ajaxResponse = new AjaxResponse(
		                    0,
		                    "对不起，您不满足此商品的购买条件。");
		                updateQuantity = 0;
		                return ajaxResponse;
		        }
		    }

            var promnoteItems = billProduct.PromoteTypes.Split(',');

            //验证多选一互斥促销
            if (promnoteItems.Contains("4")&&!string.IsNullOrWhiteSpace(billProduct.Exclude))
            {
                var userCart = MongoDBHelper.GetModel<UserCartModel>(u => u.VisitorKey == this.UserSession.VisitorKey);
                if (userCart != null && userCart.ProductItems.Count > 0)
                {
                    var excludes = billProduct.Exclude.Split(',');
                    if (excludes.Length > 0)
                    {
                        foreach (var item in userCart.ProductItems)
                        {
                            if (item.ProductID != billProduct.ProductID && excludes.Contains(item.ProductID.ToString()))
                            {
                                ajaxResponse = new AjaxResponse(
                                    0,
                                    string.Format("对不起，【{0}】与【{1}】不能同时购买。", item.ProductName, billProduct.ProductName));
                                updateQuantity = 0;
                                return ajaxResponse;
                            }
                        }
                    }
                }
            }

            var islimited = promnoteItems.Contains("1"); // 是否参加限时抢购
		    if (islimited)
		    {
		        if (product.InventoryNumber <= 0)
		        {
                    ajaxResponse = new AjaxResponse(
                        0,
                        "对不起，商品已售完!",
                        new { quantity = updateQuantity, totalDiscount = billProduct.FavorablePrice });
		            updateQuantity = 0;
		        }
		        else if (billProduct.PromoteResidueQuantity <= 0)
		        {
		            ajaxResponse = new AjaxResponse(
		                0,
		                "对不起，活动商品库存不足!",
		                new { quantity = updateQuantity, totalDiscount = billProduct.FavorablePrice });
                    updateQuantity = 0;
		        }
		        else
		        {
		            // 检查此商品是否限购件数（不限购）
		            if (billProduct.LimitedBuyQuantity <= 0)
		            {
		                if (billProduct.PromoteResidueQuantity < updateQuantity)
		                {
		                    // 购物车数量必须小于活动库存
		                    ajaxResponse = new AjaxResponse(
		                        0,
		                        "对不起，此商品库存不足!你最多可购买" + billProduct.PromoteResidueQuantity + "件",
		                        new { quantity = updateQuantity, totalDiscount = billProduct.FavorablePrice });
                            updateQuantity = billProduct.PromoteResidueQuantity;
		                }
		            }
		            else
		            {
		                // 每人限购
		                if (billProduct.MaxBuyQuantity < updateQuantity)
		                {

		                    ajaxResponse = new AjaxResponse(
		                        0,
		                        string.Format("对不起，此商品每人限购{0}件", billProduct.LimitedBuyQuantity),
		                        new { quantity = updateQuantity, totalDiscount = billProduct.FavorablePrice });
		                    updateQuantity = billProduct.MaxBuyQuantity;
		                }
		            }
		        }
		    }
		    else if (product.InventoryNumber < updateQuantity) // 检查库存
		    {
		        if (product.InventoryNumber > 0)
		        {
		            ajaxResponse = new AjaxResponse(
		                0,
		                "对不起，此商品的库存不足，您最多可购买" + product.InventoryNumber + "件",
		                new { quantity = product.InventoryNumber, totalDiscount = billProduct.FavorablePrice });
		            //cart.ProductItems.Add(this.ConvertToCartProduct(product, product.InventoryNumber)); //库存不够，则设置库存值
		            updateQuantity = product.InventoryNumber;
		        }
		        else
		        {
                    ajaxResponse = new AjaxResponse(
                        0,
                        "对不起，此商品已售完",
                        new { quantity = product.InventoryNumber, totalDiscount = billProduct.FavorablePrice });
                    //cart.ProductItems.Add(this.ConvertToCartProduct(product, product.InventoryNumber)); //库存不够，则设置库存值
		            updateQuantity = 0;
		        }
		    }

		    return ajaxResponse;
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
					    ajaxResponse = new AjaxResponse(0, "此为活动商品，且活动已失效，不能领取。");
						return ajaxResponse;
				    }

				    if (specialPromote.EndTime < DateTime.Now)
				    {
					    updateQuantity = 0;
					    ajaxResponse = new AjaxResponse(0, "活动已结束，不能领取。");
						return ajaxResponse;
				    }

				    if (specialPromote.StartTime > DateTime.Now)
				    {
					    updateQuantity = 0;
					    ajaxResponse = new AjaxResponse(0, "活动尚未开始，不能领取。");
						return ajaxResponse;
				    }

				    var userPromote = specialPromote.UserPromotes.FirstOrDefault(p => p.UserID == this.UserSession.UserID);

				    //判断是否已参加活动，未参加，则直接跳过
				    if (userPromote == null)
				    {
					    updateQuantity = 0;
					    ajaxResponse = new AjaxResponse(0, "此为活动赠品，您未参加此活动，不能领取。");
						return ajaxResponse;
				    }

				    //判断是否已领取活动，已领取，则直接跳过
				    if (userPromote.HasGetGift)
				    {
					    updateQuantity = 0;
					    ajaxResponse = new AjaxResponse(0, "此为活动赠品，您已领取过来，不能多次领取。");
						return ajaxResponse;
				    }

					//判断参加活动是否超过一天
					if ((DateTime.Now - userPromote.ParticipateTime).TotalDays > 1)
					{
						updateQuantity = 0;
						ajaxResponse = new AjaxResponse(0, "此为活动赠品，您今日未参加此活动，不能领取。");
						return ajaxResponse;
					}

				    updateQuantity = 1;
				    ajaxResponse = new AjaxResponse(1, "活动商品");
					return ajaxResponse;
			    }
		    }

			return null;
		}

	    private CartProduct ConvertToCartProduct(ProductSearchResult product,int quantity)
        {
            var cartProduct = new CartProduct();
            cartProduct.GoujiuPrice = product.GoujiuPrice;
            cartProduct.ProductName = product.Name;
            cartProduct.Quantity = quantity;
            cartProduct.ProductID = product.ID;
            cartProduct.Discount = 0;
            cartProduct.UpdateTime = DateTime.Now;
            cartProduct.ProductPic = product.ThumbnailPath;
            return cartProduct;
        }

        [GzipFilter]
        public ActionResult Trinket(int count)
        {
            var list = new ProductService().Query(
                ProductType.Rand,
                count,
                "Status=2 and InventoryNumber>0 and IsDelete= 0 and ProductCategoryID <> 9");
            return this.View("Trinket", list);
        }

        [GzipFilter]
		public ActionResult TrinketAddProCartPro(int productId)
		{
			var response = this.Edit(productId, 1, true);
			if (response.State == 1)
			{
				//cartPro = this.GetUserCart().ProductItems.FirstOrDefault(i => i.ProductID == productId);
				return this.GetCartProductsView();
			}
			return this.Json(null);
		}

		/// <summary>
		/// 获取购物车信息
		/// </summary>
		/// <returns></returns>
        [GzipFilter]
	    public PartialViewResult GetCartProductsView()
		{
			var orderBill = this.GetCartProductBill();

			return this.PartialView("CartProduct", orderBill);
		}

	    private Order_Bill GetCartProductBill()
	    {
		    int count = 0;
		    var userCart = this.GetUserCart();

		    var productDictionary = new Dictionary<int, int>();

		    List<Cart_Product> specialPromoteProducts = new List<Cart_Product>();

		    if (userCart != null && userCart.ProductItems != null)
		    {
			    foreach (var productItem in userCart.ProductItems)
			    {
				    int quantity = 0;
				    if (this.CheckSpecialPromote(productItem.ProductID, ref quantity) != null) //参加全场特殊活动
				    {
					    if (quantity > 0)
					    {
							specialPromoteProducts.Add(
							new Cart_Product { GoujiuPrice = 0, ProductName = "[赠品]" + productItem.ProductName, ProductID = productItem.ProductID, PromotePrice = 0, Quantity = quantity });
					    }

						continue;
				    }

					//只需要加入数量大于0的商品
				    if (productItem.Quantity > 0)
				    {
					    productDictionary.Add(productItem.ProductID, productItem.Quantity);
					    count += productItem.Quantity;
				    }
			    }
		    }

		    var orderBill = new OrderBillServices().QueryOrderBill(productDictionary, this.UserSession.UserID);

		    orderBill.Products.AddRange(specialPromoteProducts);

		    orderBill.ProductCount = count;
		    return orderBill;
	    }

	    private void UpdateUserCart(UserCartModel cartModel)
	    {
	        MongoDBHelper.UpdateModel<UserCartModel>(
	            cartModel,
	            c => c.VisitorKey == cartModel.VisitorKey);
	    }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCarProductInfo()
        {
            var cartModel = this.GetUserCart();

            var productDictionary = new Dictionary<int, int>();
            foreach (var items in cartModel.ProductItems)
            {
                productDictionary.Add(items.ProductID, items.Quantity);
            }

            var cart = new OrderBillServices();
            var orderBill = cart.QueryOrderBill(productDictionary, this.GetUserID());
            return this.Json(orderBill);
        }
    }
}
