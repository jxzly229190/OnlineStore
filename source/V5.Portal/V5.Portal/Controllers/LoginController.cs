// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员登录控制器.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace V5.Portal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.OAuth2.OAuths;
    using V5.Library.Security;
    using V5.Library.Security.Regular;
    using V5.Portal.Common;
    using V5.Portal.EmailClient;
    using V5.Portal.Models;
    using V5.Portal.SmsClient;
    using V5.Service.User;

    /// <summary>
    /// 会员登录控制器.
    /// </summary>
    public class LoginController : BaseController
    {
        #region Constants and Fields

        /// <summary>
        /// 会员数据访问服务类.
        /// </summary>
        private UserService userService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 获取登录安全码
        /// </summary>
        /// <returns>
        /// 执行方法结果
        /// </returns>
        //[OutputCache(Duration = 0)]
        public ActionResult GetSecurityCode()
        {
            string securityCode;
            var security = SecurityCode.CreateSecurityCode(out securityCode);
            //this.Session["securityCode"] = securityCode;
            UserSessionManager.SecurityCode = securityCode;
            return this.File(security.ToArray(), @"image/jpeg");
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [OutputCache(Duration = 3600)]
        public ActionResult Index()
        {
            Response.Cache.SetOmitVaryStar(true);
            return this.View();
        }

        /// <summary>
        /// 获取密钥对.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult GetPrivateKey()
        {
            var rsa = new RSACryptoServiceProvider();
            //this.UserSession.ExtObject = rsa.ToXmlString(true); // 将私钥存在Session
            UserSessionManager.ExtObject = rsa.ToXmlString(true); // 将私钥存在Session
            RSAParameters parameter = rsa.ExportParameters(true); // 把公钥适当转换，准备发往客户端
            var exponent = this.BytesToHexString(parameter.Exponent);
            var modulus = this.BytesToHexString(parameter.Modulus);
            //MongoDBHelper.UpdateModel(this.UserSession, u => u.SessionId == this.UserSession.SessionId);
            //HttpRuntime.Cache[this.UserSession.VisitorKey] = this.UserSession;
            return this.Json(new { exponent, modulus });
        }

        /// <summary>
        /// 注册页面.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [OutputCache(Duration = 3600)]
        public ActionResult Register()
        {
            Response.Cache.SetOmitVaryStar(true);
            return this.View("Register");
        }

        /// <summary>
        /// 注册成功
        /// </summary>
        /// <param name="LoginName">
        /// The Login Name.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult RegisterSuccess(string LoginName)
        {
            ViewBag.LoginName = LoginName;
            return this.View();
        }

        /// <summary>
        /// 注册页面.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult RegisterResult()
        {
            try
            {                
                //用户名、密码、验证码检测
                string message = "";
                if (!Check(out message))
                {
                    return this.Json(new AjaxResponse(0, message));
                }

                //用户信息检测
                var userName = Request.Form["n"];
                this.userService = new UserService();
                var user = this.userService.QueryLogin(userName);
                if (user != null)
                {
                    return this.Json(new AjaxResponse(0, "用户名已存在"));
                }

                user = new User();
                if (userName.Contains("@"))
                {
                    if (RegexValidate.IsEmailAddress(userName))
                    {
                        user.Email = userName;
                    }
                    else
                    {
                        return this.Json(new AjaxResponse(0, "邮箱格式有误"));
                    }
                }
                else
                {
                    if (RegexValidate.IsMobilePhone(userName))
                    {
                        user.Mobile = userName;
                    }
                    else
                    {
                        return this.Json(new AjaxResponse(0, "手机格式有误"));
                    }
                }

                //密码
                var pwdToDecrypt = Request.Form["p"];
                var rsa = new RSACryptoServiceProvider();
	            //rsa.FromXmlString(this.UserSession.ExtObject);
                rsa.FromXmlString(UserSessionManager.ExtObject);
                byte[] result = rsa.Decrypt(HexStringToBytes(pwdToDecrypt), false); // 用私钥将密码解密出来
                var enc = new ASCIIEncoding();
                user.LoginPassword = enc.GetString(result);
                user.NickName = Request.Form["u"];
                user.Status = 1;
                user.CreateTime = DateTime.Now;
                user.LastLoginTime = DateTime.Now;
                user.ID = this.userService.AddUser(user);
                if (user.ID > 0)
                {
                    this.LoginRegisterSeucced(user);
                    return this.Json(new AjaxResponse(1, "注册成功"));
                }

                return this.Json(new AjaxResponse(0, "注册失败！"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 会员登录.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult Login()
        {
            try
            {
                //用户名、密码、验证码检测
                string message = "";
                if (!Check(out message))
                {
                    return this.Json(new AjaxResponse(0, message));
                }                
                
                //登录次数检测
                if (UserSessionManager.LoginErrCount >= 5)
                {
                    return this.Json(new AjaxResponse(0, "密码错误5次,5分钟后再试"));
                }
                
                //用户信息检测
                var userName = Request.Form["n"];
                this.userService = new UserService();
                var user = this.userService.QueryLogin(userName);
                if (user == null)
                {
                    return this.Json(new AjaxResponse(0, "用户名不存在"));
                }

                //密码检测
                var pwdToDecrypt = Request.Form["p"];
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(UserSessionManager.ExtObject);
                byte[] result = rsa.Decrypt(HexStringToBytes(pwdToDecrypt), false); // 用私钥将密码解密出来
                var enc = new ASCIIEncoding();
                var strPwdMD5 = enc.GetString(result);
                
                if (string.Compare(strPwdMD5, user.LoginPassword, StringComparison.OrdinalIgnoreCase) == 0
                    || string.Compare(
                        Encrypt.HashByMD5(strPwdMD5),
                        user.LoginPassword,
                        StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.LoginRegisterSeucced(user);
                    return this.Json(new AjaxResponse(1, "登录成功"));
                }

                UserSessionManager.UserID = user.ID;
                UserSessionManager.LoginTime = DateTime.Now;
                UserSessionManager.LoginErrCount += 1;
                return this.Json(new AjaxResponse(0, "密码不正确"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        /// <summary>
        /// 登录注册成功
        /// </summary>
        /// <param name="user"></param>
        private void LoginRegisterSeucced(User user)
        {
            //设置UserSessionManager
            UserSessionManager.UserID = user.ID;
            UserSessionManager.Name = user.Name;
            UserSessionManager.LoginName = user.LoginName;
            UserSessionManager.ShowName = user.NickName ?? (user.Name ?? (user.LoginName ?? (user.Email ?? user.Mobile)));
            UserSessionManager.LoginTime = DateTime.Now;

            //移除登录次数和私钥
            UserSessionManager.Remove("ExtObject");
            UserSessionManager.Remove("LoginErrCount");

            //合并购物车
            CartController.CartMerge(user.ID);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool Check(out string message)
        {
            var userName = Request.Form["n"];
            var pwdToDecrypt = Request.Form["p"];
            var verify = Request.Form["v"];
            if (string.IsNullOrEmpty(userName))
            {
                message = "用户名不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(pwdToDecrypt))
            {
                message = "密码不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(verify))
            {
                message = "验证码不能为空";
                return false;
            }

            if (!(UserSessionManager.SecurityCode.Trim() == verify.Trim()))
            {
                message = "验证码输入有误";
                return false;
            }

            message = "";
            return true;
        }

        /// <summary>
        /// 注册登录成功后保存登录数据.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        //private void LoginRegisterSeucced(UserSession session, User user)
        //{
        //    session.UserID = user.ID;
        //    session.Name = user.Name;
        //    session.LoginName = user.LoginName;
        //    session.ShowName = user.NickName ?? (user.Name ?? (user.LoginName ?? (user.Email ?? user.Mobile)));
        //    session.LastVisitTime = DateTime.Now;
        //    session.LoginTime = DateTime.Now;

        //    // 更新用户的会话Session
        //    MongoDBHelper.RemoveModel<UserSession>(u => u.UserID == user.ID);
        //    MongoDBHelper.UpdateModel(session, u => u.VisitorKey == session.VisitorKey);

        //    // 更新购物车
        //    var anonyCart = MongoDBHelper.GetModel<UserCartModel>(u => u.VisitorKey == session.VisitorKey);
        //    CartController.WriteUserCartOperationLog(
        //        new UserCartOperationLog()
        //            {
        //                OperateTime = DateTime.Now,
        //                SessionKey = this.Session.SessionID,
        //                VisitorKey = this.UserSession.VisitorKey,
        //                OperationType = "Get",
        //                Message =
        //                    "登录获取当前匿名购物车（" + this.UserSession.VisitorKey
        //                    + "）：Selector:c => u => u.VisitorKey == session.VisitorKey",
        //                UserCart =
        //                    anonyCart
        //                    ?? new UserCartModel()
        //                           {
        //                               BuyList = new List<CartProduct>(),
        //                               ProductItems = new List<CartProduct>()
        //                           },
        //                UserID = this.UserSession.UserID
        //            });

        //    var userCart = MongoDBHelper.GetModel<UserCartModel>(u => u.UserId == user.ID);

        //    CartController.WriteUserCartOperationLog(
        //        new UserCartOperationLog()
        //            {
        //                OperateTime = DateTime.Now,
        //                SessionKey = this.Session.SessionID,
        //                VisitorKey = this.UserSession.VisitorKey,
        //                OperationType = "Get",
        //                Message =
        //                    "登录获取当前User购物车（" + this.UserSession.VisitorKey
        //                    + "）：Selector:c => u => u.VisitorKey == session.VisitorKey",
        //                UserCart =
        //                    userCart
        //                    ?? new UserCartModel()
        //                           {
        //                               BuyList = new List<CartProduct>(),
        //                               ProductItems = new List<CartProduct>()
        //                           },
        //                UserID = this.UserSession.UserID
        //            });

        //    var cart = userCart != null && userCart.ProductItems.Count > 0 ? userCart : anonyCart;

        //    if (cart != null)
        //    {
        //        // 此用户已经有购物车数据
        //        if (cart.UserId > 0)
        //        {
        //            if (anonyCart != null && anonyCart.ProductItems.Count > 0)
        //            {
        //                // 购物车商品合并
        //                foreach (var productItem in anonyCart.ProductItems)
        //                {
        //                    var product = cart.ProductItems.FirstOrDefault(p => p.ProductID == productItem.ProductID);
        //                    if (product != null)
        //                    {
        //                        // 若原先购物车中已有此商品，则将新数据替代老数据 
        //                        product.GoujiuPrice = productItem.GoujiuPrice;
        //                        product.Quantity = productItem.Quantity;
        //                        product.Discount = productItem.Discount;
        //                    }
        //                    else
        //                    {
        //                        cart.ProductItems.Add(productItem);
        //                    }
        //                }
        //            }

        //            cart.VisitorKey = session.VisitorKey; // 更新购物车的访问标示
        //        }
        //        else
        //        {
        //            cart.UserId = user.ID;
        //        }

        //        CartController.WriteUserCartOperationLog(
        //            new UserCartOperationLog()
        //                {
        //                    OperateTime = DateTime.Now,
        //                    SessionKey = this.Session.SessionID,
        //                    VisitorKey = this.UserSession.VisitorKey,
        //                    OperationType = "Edit",
        //                    Message = "登录开始更新当前User购物车（" + this.UserSession.VisitorKey + "）",
        //                    UserCart = cart,
        //                    UserID = this.UserSession.UserID
        //                });

        //        MongoDBHelper.UpdateModel<UserCartModel>(cart, uc => uc.VisitorKey == session.VisitorKey);

        //        CartController.WriteUserCartOperationLog(
        //            new UserCartOperationLog()
        //                {
        //                    OperateTime = DateTime.Now,
        //                    SessionKey = this.Session.SessionID,
        //                    VisitorKey = this.UserSession.VisitorKey,
        //                    OperationType = "Get",
        //                    Message =
        //                        "登录更新完毕当前User购物车（" + this.UserSession.VisitorKey
        //                        + "）：Selector:u => u.UserId == user.ID || u.VisitorKey == session.VisitorKey",
        //                    UserCart =
        //                        MongoDBHelper.GetModel<UserCartModel>(
        //                            u => u.UserId == user.ID || u.VisitorKey == session.VisitorKey)
        //                        ?? new UserCartModel()
        //                               {
        //                                   BuyList = new List<CartProduct>(),
        //                                   ProductItems = new List<CartProduct>()
        //                               },
        //                    UserID = this.UserSession.UserID
        //                });
        //    }

        //    this.userService.ModifyUserLastLoginTime(user.ID); // 更新会员的最后登录时间
        //    HttpRuntime.Cache[session.VisitorKey] = session;
        //}

        /// <summary>
        /// 互联登录.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult OAuthLogin(string type)
        {
            var url = string.Empty;
            switch (type)
            {
                case "sina":
                    var sina = new Sina();
                    url = sina.OAuthUrl;
                    break;
                case "qq":
                    var qq = new QQ();
                    url = qq.OAuthUrl;
                    break;
                case "renren":
                    var rr = new RenRen();
                    url = rr.OAuthUrl;
                    break;
                case "163":
                    var wangyi = new Wangyi();
                    url = wangyi.OAuthUrl;
                    break;
                case "kaixin":
                    var kaixin = new KaiXin();
                    url = kaixin.OAuthUrl;
                    break;
                case "sohu":
                    var sohu = new Sohu();
                    url = sohu.OAuthUrl;
                    break;
                case "douban":
                    var douban = new DouBan();
                    url = douban.OAuthUrl;
                    break;
            }
            return this.Redirect(url);
        }
        
        /// <summary>
        /// 互联登陆结果.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult OAuthReturn(string type)
        {
            if (Request.QueryString["code"] != null)
            {
                string code = Request.QueryString["code"];
                bool accredit;
                switch (type)
                {
                    case "sina":
                        var sina = new Sina();
                        accredit = sina.Authorize(code);
                        if (accredit)
                        {
                            this.SaveUserInfo(sina.openID, sina.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "qq":
                        var qq = new QQ();
                        accredit = qq.Authorize(code);
                        if (accredit)
                        {
                            this.SaveUserInfo(qq.openID, qq.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "renren":
                        var rr = new RenRen();
                        accredit = rr.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(rr.openID, rr.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "163":
                        var wy = new Wangyi();
                        accredit = wy.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(wy.openID, wy.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "kaixin":
                        var kx = new KaiXin();
                        accredit = kx.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(kx.openID, kx.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "sohu":
                        var sh = new Sohu();
                        accredit = sh.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(sh.openID, sh.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "douban":
                        var douban = new DouBan();
                        accredit = douban.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(douban.openID, douban.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                    case "taobao":
                        var taobao = new TaoBao();
                        accredit = taobao.Authorize(code);
                        if (accredit)
                        {
                            // 保存登录信息
                            this.SaveUserInfo(taobao.openID, taobao.nickName);
                        }
                        else
                        {
                            return this.View("Index");
                        }
                        break;
                }
            }

            return this.RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// 退出登录.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult Exit()
        {
            try
            {
                UserSessionManager.Remove("UserID");
                return this.Json(new AjaxResponse(1, "成功退出"));
            }
            catch (Exception exception)
            {
                return this.Json(new AjaxResponse(0, exception.Message));
            }
        }

        #region 找回密码

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPassword()
        {
            return this.View();
        }

        /// <summary>
        /// 校验用户是否正确
        /// </summary>
        /// <param name="emailOrSms"></param>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public ActionResult SendNewEncyCode(string emailOrSms, string validateCode)
        {
            if (string.IsNullOrWhiteSpace(validateCode)) //验证码
            {
                return this.Json(new AjaxResponse { State = 0, Message = "验证码不能为空" });
            }
            string code = UserSessionManager.SecurityCode;
            if (string.IsNullOrWhiteSpace(code) ||
                validateCode != code)
            {
                return Json(new AjaxResponse { State = 0, Message = "验证码输入有误" });
            }

            var userModel = new UserService().QueryUserByMobileOrEmail(emailOrSms);    //验证用户是否存在
            if (userModel == null)
            {
                return Json(new AjaxResponse { State = 0, Message = "用户名不存在或用户名错误，请重新输入" });
            }
            var ran = new Random();
            var ranCode = ran.Next(100, 999).ToString() + ran.Next(100, 999);
            var pwd = Encrypt.HashByMD5(ranCode);
            var findPassword = new v4UsrFindPasswordService().GetByUserId(userModel.ID);
            int validateCount = -1;
            if (findPassword != null)
            {
                validateCount = findPassword.ValidateCount;
                if (DateTime.Now.Day - findPassword.ExtField.Day > 1)
                {
                    new v4UsrFindPasswordService().UpdateValidateCount(userModel.ID, 1);
                }
            }
            else
            {
                AddUserFindPassword(userModel, ranCode);//用户信息插入数据库
            }
            if (validateCount == 5)
            {
                return Json(new AjaxResponse { State = 0, Message = "您已超过系统最大受理次数，系统不予受理，请明天再申请" });
            }

            if (emailOrSms.Contains("@"))   //检验用户输入的是邮箱还是手机号码
            {
                string[] address = { userModel.Email };
                if (SendEmail(ranCode, address) == 1)
                {
                    new UserService().UpdateUserPassword(pwd, userModel.ID);//修改用户密码
                    RecodValidateCount(userModel.ID, validateCount);//记录用操作次数
                    return Json(new AjaxResponse
                    {
                        State = 1,
                        Message = "邮箱"
                    });
                }
            }
            try
            {
                string[] mobile = { userModel.Mobile };
                SendSms(mobile, ranCode); //发短信
                RecodValidateCount(userModel.ID, validateCount);
                new UserService().UpdateUserPassword(pwd, userModel.ID);//修改用户密码
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return Json(new AjaxResponse { State = 1, Message = "手机" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="validateCount"></param>
        private void RecodValidateCount(int userId, int validateCount)
        {
            validateCount += 1;
            new v4UsrFindPasswordService().UpdateValidateCount(userId, validateCount);
        }

        /// <summary>
        /// 插入用户信息到FindPassWord表，并设置状态为0
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="securityCode"></param>
        private int AddUserFindPassword(User userModel, string securityCode)
        {
            var findPassModel = new v4_Usr_FindMailPassword()
            {
                usr_UserID = userModel.ID,
                VirificationCode = securityCode,
                Mail = userModel.Email ?? "@Email",
                State = 0,
                ExtField = DateTime.Now,
                Tel = userModel.Mobile,
                ValidateCount = 1,
                FailTime = DateTime.Now.AddHours(1),
                CreateTime = DateTime.Now
            };
            //用户信息插入数据库
            var recevied = new v4UsrFindPasswordService().Insert(findPassModel);
            if (recevied > 0)
            {
                return recevied;
            }
            return -1;
        }

        /// <summary>
        /// 发邮件 返回1则发送成功
        /// </summary>
        /// <param name="securityCode"></param>
        /// <param name="EtoAddress"></param>
        private int SendEmail(string securityCode, string[] EtoAddress)
        {
            var emailModel = new UserMessageEmailService().QueryByID(1);//查询出来要发送的邮件内容
            string content = emailModel.Content.Replace("{code}", securityCode);
            var emailClient = new EmailClient();
            if (emailClient.EmailSend(EtoAddress, emailModel.Title, content, "1") == "1")
            {
                return 1;
            }
            return -1;
        }

        /// <summary>
        /// 发短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="newCode"></param>
        private void SendSms(string[] mobile, string newCode)
        {
            var sms = new SmsClient();
            //string message = "您的验证码是：{0}。请不要把验证码泄露给其他人。";
            string message = new UserMessageSmsService().QueryByID(1).Content;
            message = message.Replace("{0}", newCode);
            var smsModel = new UserMessageSmsService().QueryByID(1);
            sms.SmsSend(mobile, message, "1");
        }

        #endregion

        #region Method

        /// <summary>
        /// 互联登录保存会员信息.
        /// </summary>
        /// <param name="openId">
        /// The open id.
        /// </param>
        /// <param name="nickName">
        /// The nick name.
        /// </param>
        private void SaveUserInfo(string openId, string nickName)
        {
            this.userService = new UserService();
            var user = this.userService.QueryByOpenID(openId);

            // 查询是否是第一次登录购酒网
            if (user == null)
            {
                // 保存会员信息到数据库
                user = new User
                           {
                               OpenID = openId,
                               Email = openId + "@gjw.com",
                               NickName = nickName,
                               Status = 1,
                               CreateTime = DateTime.Now,
                               LoginPassword = Encrypt.HashByMD5("123456")
                           };
                user.ID = this.userService.AddUser(user);
            }

            // 保存会员信息到Sesion
            //var session = new UserSession { UserID = user.ID, ShowName = nickName };

            // 先获取VisitorKey
            //var visitorKey = this.Request.Cookies.Get(ConstantParams.VisitorKeyName);
            //session.VisitorKey = visitorKey != null ? visitorKey.Value : Guid.NewGuid().ToString();

            this.LoginRegisterSeucced(user);
            this.Response.Cookies.Remove("L");
            var httpCookie = this.Response.Cookies["L"];
            if (httpCookie != null)
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

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

        #endregion
    }
}