// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员中心.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Mvc;

    using V5.DataContract.Transact.Order;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Security;
    using V5.Library.Storage.DB;
    using V5.Portal.Attributes;
    using V5.Portal.Filters;
    using V5.Service.Transact;
    using V5.Service.User;
    using V5.DataContract.Transact;

    /// <summary>
    /// 会员中心.
    /// </summary>
    [CustomAuthorize]
    public class UserController : BaseController
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var userID = this.GetUserID();
            var user = new UserService().QueryUserByID(userID);
            var paging = new Paging("[view_Orders]", null, "ID", null, 1, 3, "CreateTime", 1);
            int pageCount, rowCount;
            user.Orders = new OrderService(userID, false).Query(paging, out pageCount, out rowCount);
            return this.View(user);
        }

        /// <summary>
        /// 邮件 and 微信订阅
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Rss()
        {
            return this.View();
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [GzipFilter]
        public ActionResult MyOrder()
        {
            return this.View();
        }

        /// <summary>
        /// 获取订单数据
        /// </summary>
        /// <param name="pageIndex">
        /// 页码
        /// </param>
        /// <param name="pageSize">
        /// 页的大小
        /// </param>
        /// <param name="search">
        /// 查询条件
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [GzipFilter]
        public JsonResult GetOrderData(int pageIndex, int pageSize, string search)
        {
            var userID = this.GetUserID();
            var condition = "[UserID] = " + userID + " and " +
                            (string.IsNullOrEmpty(search) ? "1=1" : "productName like '%" + search + "%'");
            var paging = new Paging("[view_Orders]", null, "ID", condition, pageIndex, pageSize, "CreateTime", 1);
            int pageCount, rowCount, totalCount;
            var Orders = new OrderService(userID, false).Query(paging, out pageCount, out rowCount);
            if (Orders == null || Orders.Count < 1)
            {
                return this.Json(new { data = Orders, rowsCount = rowCount });
            }

            // 查找订单商品信息
            string orderIDs = "";
            foreach (var order in Orders)
            {
                orderIDs += order.ID + ",";
            }

            orderIDs = orderIDs.Remove(orderIDs.Length - 1, 1);
            paging = new Paging("[view_Order_Products]", null, "ID", "[OrderID] in (" + orderIDs + ")", 1, 1000);
            var orderProducts = new SqlServer().Paging<Order_Product>(paging, out pageCount, out totalCount, null);

            for (var i = 0; i < Orders.Count; i++)
            {
                Orders[i].Products = new List<Order_Product>();
                foreach (var orderProduct in orderProducts)
                {
                    if (orderProduct.OrderID == Orders[i].ID)
                    {
                        orderProduct.Path = Utils.GetProductImage(orderProduct.Path, "1");
                        Orders[i].Products.Add(orderProduct);
                    }
                }
            }

            return this.Json(new { data = Orders, rowsCount = rowCount });
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DeleteOrder(string id)
        {
            return id;
        }

        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Collect()
        {
            return this.View();
        }

        /// <summary>
        /// 获取收藏数据
        /// </summary>
        /// <param name="pageIndex">
        /// 页码
        /// </param>
        /// <param name="pageSize">
        /// 页的大小
        /// </param>
        /// <param name="search">
        /// 查询条件
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [GzipFilter]
        public JsonResult GetCollcetData(int pageIndex, int pageSize, string search)
        {
            var userID = this.GetUserID();
            var condition = "[UserID] = " + userID + " and " +
                            (string.IsNullOrEmpty(search) ? "1=1" : "productName like '%" + search + "%'");
            var paging = new Paging("[view_User_Collect]", null, "ID", condition, pageIndex, pageSize, "CreateTime", 1);
            int pageCount, rowCount;
            var list = new UserCollectRecordService().Paging(paging, out pageCount, out rowCount);
            if (list != null)
            {
                foreach (var userCollectRecord in list)
                {
                    userCollectRecord.Path = Utils.GetProductImage(userCollectRecord.Path, "1");
                }
            }

            return this.Json(new { data = list, rowsCount = rowCount });
        }

        /// <summary>
        /// 批量删除收藏.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult DeleteCollcet(string id)
        {
            try
            {
                var userCollect = new UserCollectRecordService();
                userCollect.RemoveBatch(id);
                return this.Json(new AjaxResponse(1, "删除成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "删除失败！" + exception.Message));
            }
        }

        /// <summary>
        /// 浏览历史
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult History()
        {
            return this.View();
        }

        /// <summary>
        /// 获取收藏数据
        /// </summary>
        /// <param name="pageIndex">
        /// 页码
        /// </param>
        /// <param name="pageSize">
        /// 页的大小
        /// </param>
        /// <param name="search">
        /// 查询条件
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [GzipFilter]
        public JsonResult GetHistoryData(int pageIndex, int pageSize, string search)
        {
            var userID = this.GetUserID();
            var condition = "[UserID] = " + userID + " and " +
                            (string.IsNullOrEmpty(search) ? "1=1" : "productName like '%" + search + "%'");
            var paging = new Paging("[view_User_BrowerHistory]", null, "ID", condition, pageIndex, pageSize, "CreateTime", 1);
            int pageCount, rowCount;

            var list = new UserBrowseHistoryService().Paging(paging, out pageCount, out rowCount);
            if (list != null)
            {
                foreach (var userBrowseHistory in list)
                {
                    userBrowseHistory.Path = Utils.GetProductImage(userBrowseHistory.Path, "1");
                }
            }

            return this.Json(new { data = list, rowsCount = rowCount });
        }

        /// <summary>
        /// 批量删除浏览历史.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult DeleteHistory(string id)
        {
            try
            {
                var userHistory = new UserBrowseHistoryService();
                userHistory.RemoveBatch(id);
                return this.Json(new AjaxResponse(1, "删除成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "删除失败！" + exception.Message));
            }
        }

        /// <summary>
        /// 我的咨询
        /// </summary>
        /// <param name="condition">
        /// 查询条件.
        /// </param>
        /// <param name="pageIndex">
        /// 页码.
        /// </param>
        /// <param name="pageSize">
        /// 页的大小.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [GzipFilter]
        public ActionResult Advisory(string condition, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.searchText = condition;
            var service = new ProductConsultService();
            int rowCount, pageCount;
            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "[UserID] = " + this.UserSession.UserID;
            }
            else
            {
                condition = "[UserID] = " + this.UserSession.UserID + " And (Content like '%" + condition
                            + "%' Or ConsultContent like '%" + condition + "%' Or ProductName like '%" + condition
                            + "%')";
            }

            var paging = new Paging("[view_Product_Consults_All]", null, "ID", condition, pageIndex, pageSize);
            var list = service.QueryConsult(paging, out pageCount, out rowCount);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndex = pageIndex;
            return this.View(list);
        }

        public ActionResult DoEvaluate(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return this.View();
            }

            var order = new OrderService(this.UserSession.UserID, false);

            return this.View();
        }

        /// <summary>
        /// 我的评价
        /// </summary>
        /// <param name="condition">
        /// 查询条件.
        /// </param>
        /// <param name="pageIndex">
        /// 页码.
        /// </param>
        /// <param name="pageSize">
        /// 页的大小.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [GzipFilter]
        public ActionResult Evaluate(string condition, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.searchText = condition;
            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "UserID=" + this.UserSession.UserID;
            }
            else
            {
                condition = "UserID=" + this.UserSession.UserID + " And (ProductName like '%" + condition + "%' Or Content like '%" + condition + "%')";
            }

            int pageCount, totalCount;
            var paging = new Paging("view_UserProductComment", null, "CommentID", condition, pageIndex, pageSize);
            var list = new SqlServer().Paging<UserCommentProduct>(paging, out pageCount, out totalCount, null);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndex = pageIndex;
            return this.View(list);
        }

        /// <summary>
        /// 消息精灵
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Message()
        {
            return this.View();
        }

        /// <summary>
        /// 我的积分
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Integral()
        {
            return this.View();
        }

        #region 用户个人资料

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Info()
        {
            var userModel = new UserService().QueryUserByID(this.GetUserID());
            return this.View(userModel);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfo()
        {
            var userModel = new UserService().QueryUserByID(this.GetUserID());
            var newUserModel = new User
            {
                Name = userModel.Name,
                NickName = userModel.NickName,
                Address = userModel.Address,
                CountyID = userModel.CountyID,
                Email = userModel.Email,
                Gender = userModel.Gender,
                Mobile = userModel.Mobile,
                Head = userModel.Head
            };
            return Json(newUserModel);
        }

        /// <summary>
        /// 修改个人资料.
        /// </summary>
        /// <param name="headerimg">
        /// The headerimg.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="gender">
        /// The gender.
        /// </param>
        /// <param name="nickname">
        /// The nickname.
        /// </param>
        /// <param name="country">
        /// The country.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult ModifiyUserMessage(
            string headerimg,
            string email,
            bool gender,
            string nickname,
            int country,
            string address)
        {
            var user = new User
                           {
                               ID = this.GetUserID(),
                               Head = headerimg,
                               Email = email,
                               Gender = gender,
                               CountyID = country,
                               Address = address,
                               NickName = nickname
                           };
            var recevId = new UserService().ModifyUserMessage(user);
            if (recevId > 0)
            {
                return this.Json(new AjaxResponse(1, "修改成功"));
            }

            return this.Json(string.Empty);
        }
        #endregion

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Password()
        {
            var rsa = new RSACryptoServiceProvider();
            this.Session["private_key"] = rsa.ToXmlString(true); // 将私钥存在Session
            RSAParameters parameter = rsa.ExportParameters(true); // 把公钥适当转换，准备发往客户端
            this.ViewBag.PublicKeyExponent = this.BytesToHexString(parameter.Exponent);
            this.ViewBag.PublicKeyModulus = this.BytesToHexString(parameter.Modulus);
            return this.View();
        }

        /// <summary>
        /// 修改密码.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult ModifyPassword()
        {
            try
            {
                var userID = this.GetUserID();
                if (userID == 0)
                {
                    return this.Json(new AjaxResponse(0, "未登录！"));
                }

                var oldpwd = Request.Form["op"];
                var newpwd = Request.Form["np"];

                if (string.IsNullOrEmpty(oldpwd))
                {
                    return this.Json(new AjaxResponse(0, "原密码不能为空"));
                }

                if (string.IsNullOrEmpty(newpwd))
                {
                    return this.Json(new AjaxResponse(0, "新密码不能为空"));
                }

                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString((string)Session["private_key"]);
                byte[] oldResult = rsa.Decrypt(HexStringToBytes(oldpwd), false); // 用私钥将密码解密出来
                byte[] newResult = rsa.Decrypt(HexStringToBytes(newpwd), false); // 用私钥将密码解密出来
                var enc = new ASCIIEncoding();

                oldpwd = enc.GetString(oldResult);
                newpwd = enc.GetString(newResult);

                var userService = new UserService();
                var user = userService.QueryUserByID(userID);
                if (user == null)
                {
                    return this.Json(new AjaxResponse(0, "用户名不存在"));
                }

                if (string.Compare(oldpwd, user.LoginPassword, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare(
                        Encrypt.HashByMD5(oldpwd),
                        user.LoginPassword,
                        StringComparison.OrdinalIgnoreCase) == 0)
                {
                    userService.ModifyPassword(userID, newpwd);
                    return this.Json(new AjaxResponse(1, "修改完成"));
                }

                return this.Json(new AjaxResponse(0, "密码不正确"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }
        
        /// <summary>
        /// 添加会员商品收藏.
        /// </summary>
        /// <param name="productId">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult AddCollect(string productId)
        {
            try
            {
                var userID = this.GetUserID();
                if (userID == 0)
                {
                    return this.Json(new AjaxResponse(3, "未登录"));
                }

                var userCollectRecordService = new UserCollectRecordService();
                var userCollectRecord = userCollectRecordService.QueryRow(userID, int.Parse(productId));
                if (userCollectRecord != null)
                {
                    return this.Json(new AjaxResponse(2, "已收藏过"));
                }

                userCollectRecord = new User_CollectRecord { UserID = userID, ProductID = int.Parse(productId) };
                userCollectRecordService.Add(userCollectRecord);
                return this.Json(new AjaxResponse(1, "收藏成功"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "收藏失败！:" + exception.Message));
            }
        }
        
        /// <summary>
        /// 添加商品咨询.
        /// </summary>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="content">
        /// 咨询内容.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddConsult(int productID, string content)
        {
            try
            {
                var userID = this.GetUserID();
                var productConsult = new Product_Consult
                {
                    UserID = userID,
                    ProductID = productID,
                    Content = content
                };
                productConsult.ID = new ProductConsultService().Add(productConsult);
                return this.Json(new AjaxResponse(1, "咨询成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "咨询失败！：" + exception.Message));
            }
        }
        
        /// <summary>
        /// 添加商品评论.
        /// </summary>
        /// <param name="score">
        /// The score.
        /// </param>
        /// <param name="productID">
        /// 商品编号.
        /// </param>
        /// <param name="content">
        /// 评论内容.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddComment(int score, int productID, string content)
        {
            try
            {
                var userID = this.GetUserID();
                if (userID <= 0)
                {
                    return this.Json(new AjaxResponse(2, "未登录！"));
                }

                // 商品ID、会员ID 查询订单
                var orderCount = new OrderProductService().QueryByProductIDAndUserID(productID, userID);
                if (orderCount <= 0)
                {
                    return this.Json(new AjaxResponse(3, "未购买过！"));
                }

                var commentReply = new Product_Comment
                {
                    UserID = userID,
                    ProductID = productID,
                    Content = content,
                    Status = 1,
                    Score = score
                };
                commentReply.ID = new ProductCommentService().Add(commentReply);
                return this.Json(new AjaxResponse(1, "评论成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "评论失败！：" + exception.Message));
            }
        }

        /// <summary>
        /// 添加商品评论的回复.
        /// </summary>
        /// <param name="commentID">
        /// The comment id.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddCommentReply(int commentID, string content)
        {
            try
            {
                var userID = this.GetUserID();
                if (userID <= 0)
                {
                    return this.Json(new AjaxResponse(2, "未登录！"));
                }

                var commentReply = new Product_Comment_Reply
                {
                    UserID = userID,
                    CommentID = commentID,
                    Content = content
                };
                commentReply.ID = new ProductCommentReplyService().Add(commentReply);
                return this.Json(new AjaxResponse(1, "回复成功！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, "回复失败！：" + exception.Message));
            }
        }

        #region 用户收货地址
        /// <summary>
        /// 收货地址簿
        /// </summary>
        /// <returns></returns>
        public ActionResult Address()
        {
            return this.View();
        }

        [GzipFilter]
        public ActionResult GetAddressByUserId()
        {
            List<User_RecieveAddress> addressList = new UserReceiveAddressService().QueryReceiveAddressByUserID(this.GetUserID());
            if (addressList != null && addressList.Any())
            {
                return this.Json(new AjaxResponse { State = 1, Data = addressList });
            }
            return Json(new AjaxResponse { State = 0, Data = 0 });
        }

        /// <summary>
        /// 根据ID查询Adress
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReceivedAddressById(int id)
        {
            var userAddress = new UserReceiveAddressService().QueryByID(id);
            if (userAddress != null)
            {
                return Json(userAddress);
            }
            return Json("");
        }

        /// <summary>
        /// 保存用户地址的修改
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <param name="consignee">用户名</param>
        /// <param name="address">地址</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="Isdefault">是否默认</param>
        /// <param name="email">邮箱</param>
        /// <param name="zipCode">区号</param>
        /// <param name="countryId">地区标识</param>
        /// <param name="tel"></param>
        /// <returns></returns>
        public ActionResult SaveModifyAddress(int id, string consignee, string address, string mobile, bool Isdefault, string email, string zipCode, int countryId, string tel)
        {
            try
            {
                if (id == -1)
                {
                    var receAddress = new User_RecieveAddress()
                    {
                        UserID = this.GetUserID(),
                        Consignee = consignee,
                        Address = address,
                        Mobile = mobile,
                        IsDefault = Isdefault,
                        Email = email,
                        ZipCode = zipCode,
                        CountyID = countryId,
                        Tel = tel
                    };
                    var recevid = new UserReceiveAddressService().Add(receAddress, null);
                    if (recevid > 0)
                    {
                        return Json(new AjaxResponse { State = 1, Message = "添加成功", Data = recevid });
                    }
                }
                else
                {
                    var receAddress = new User_RecieveAddress()
                    {
                        UserID = this.GetUserID(),
                        Consignee = consignee,
                        Address = address,
                        Mobile = mobile,
                        IsDefault = Isdefault,
                        Email = email,
                        ZipCode = zipCode,
                        CountyID = countryId,
                        ID = id,
                        Tel = tel
                    };
                    var recevid = new UserReceiveAddressService().UpdateAddresss(receAddress);
                    if (recevid > 0)
                    {
                        return Json(new AjaxResponse(1, "修改成功"));
                    }

                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }

            return Json("");
        }
        
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public ActionResult DeleteAddress(int addressId)
        {
            var recevid = new UserReceiveAddressService().RemoveByID(addressId);
            if (recevid > 0)
            {
                return Json(new AjaxResponse(1, "删除成功"));
            }
            return Json("");
        }
        
        /// <summary>
        /// 设为默认收货地址
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public ActionResult SetAddressDefault(int addressId)
        {
            var recevid = new UserReceiveAddressService().SetDefault(addressId, this.UserSession.UserID);
            if (recevid > 0)
            {
                return Json(new AjaxResponse(1, "已设为默认收货地址"));
            }
            return Json("");
        }
        #endregion

        #region 手机验证

        /// <summary>
        /// 发送短息.
        /// </summary>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult MobileVerify(string mobile)
        {
            try
            {
                var userID = this.GetUserID();
                var result = new UserService().QueryByMobileVerify(mobile, userID);
                if (result > 0)
                {
                    return this.Json(new AjaxResponse(0, "该手机号已经验证过！"));
                }

                var list = new[] { mobile };
                var code = SecurityCode.CreateSecurityCode();
                this.Session[mobile] = code;

                var sms = new SmsClient.SmsClient();
                var message = string.Format("您的验证码是：{0}。请不要把验证码泄露给其他人。", code);
                sms.SmsSend(list, message, "1");
                return this.Json(new AjaxResponse(1, "成功！"));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 保存手机号.
        /// </summary>
        /// <param name="mobile">
        /// The mobile.
        /// </param>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult SaveMobile(string mobile, string code)
        {
            try
            {
                var coderesult = this.Session[mobile];
                if (code != (string)coderesult)
                {
                    return this.Json(new AjaxResponse(0, "验证码错误！"));
                }

                var userID = this.GetUserID();
                if (userID <= 0)
                {
                    return this.Json(new AjaxResponse(0, "未登录！"));
                }

                var userService = new UserService();
                userService.ModifyMobile(mobile, userID);
                return this.Json(new AjaxResponse(1, "成功！"));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion

        #region

        /// <summary>
        /// The bytes to hex string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string BytesToHexString(byte[] input)
        {
            var hexString = new StringBuilder(64);

            foreach (byte t in input)
            {
                hexString.Append(string.Format("{0:X2}", t));
            }

            return hexString.ToString();
        }

        /// <summary>
        /// The hex string to bytes.
        /// </summary>
        /// <param name="hex">
        /// The hex.
        /// </param>
        /// <returns>
        /// byte数组.
        /// </returns>
        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            var result = new byte[hex.Length / 2];

            for (var i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }

        #endregion
    }
}
