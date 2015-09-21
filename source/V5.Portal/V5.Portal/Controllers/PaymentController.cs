using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.Data;

    using V5.Library.Logger;
    using V5.Portal.Common;
    using V5.Service.Transact;

    public class PaymentController : BaseController
    {
        //
        // GET: /Alipay/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Alipay(string ono)
        {
            LogUtils.Log(
                string.Format("支付宝支付流程开始。用户UserID：{0}，支付订单编号：{1}", this.UserSession.UserID, ono),
                "进入支付宝支付",
                Category.Info,
                this.Session.SessionID,
                this.UserSession.UserID,
                "Alipay");
            
            string partner = "2088301856479212";		//合作身份者ID
            string key = "sji2sos0koz072vl07sg5xvtctvq6hfp";			//安全检验码
            string seller_email = "george@goujiuwang.com";	//签约支付宝账号或卖家支付宝帐户
            string input_charset = "utf-8";						//字符编码格式 目前支持 gb2312 或 utf-8
            string show_url = "";				//网站商品的展示地址，不允许加?id=123这类自定义参数
            string sign_type = "MD5";							//加密方式 不需修改
            string out_trade_no = ono; 	//订单号
            string subject = "开发测试";			//订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string body = "购酒网在线下单（酒类或饮料）";			//订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里

            if (true)//即时到账
            {
                #region 即时到账

                string notify_url = "http://fybc90.oicp.net/Payment/Notify";	//交易过程中服务器通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                string return_url = "http://fybc90.oicp.net/Payment/Success";	//付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数

                string antiphishing = "0";							//防钓鱼功能开关，'0'表示该功能关闭，'1'表示该功能开启。默认为关闭
                
                //string total_fee = (Convert.ToDouble(dt.Rows[0]["SumMoney"]) + Convert.ToDouble(dt.Rows[0]["Freight"])).ToString();	//需要支付的金额

                //扩展功能参数——网银提前           
                string paymethod = "directPay";						//默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)
                string defaultbank = "";							//默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379

                if (!string.IsNullOrEmpty(Request.QueryString["paymethod"]))
                {
                    paymethod = "bankPay"; 	//默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)
                    defaultbank = Request.QueryString["defaultbank"];		//默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379
                }
                //扩展功能参数——防钓鱼
                string encrypt_key = "";							//防钓鱼时间戳，初始值
                string exter_invoke_ip = "";						//客户端的IP地址，初始值
                if (antiphishing == "1")
                {
                    encrypt_key = AlipayClass.AlipayFunction.Query_timestamp(partner);
                    exter_invoke_ip = "";							//获取客户端的IP地址，建议：编写获取客户端IP地址的程序
                }

                //扩展功能参数——其他
                string extra_common_param = "";						//自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
                string buyer_email = "";							//默认买家支付宝账号       

                //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)
                string royalty_type = "";							//提成类型，该值为固定值：10，不需要修改
                string royalty_parameters = "";
                //提成信息集，与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
                //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
                //如：
                //royalty_type = "10";
                //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";

                //扩展功能参数——自定义超时(若要使用，请按照注释要求的格式赋值)
                //该功能默认不开通，需联系客户经理咨询
                string it_b_pay = "";								//超时时间，不填默认是15天。八个值可选：1h(1小时),2h(2小时),3h(3小时),1d(1天),3d(3天),7d(7天),15d(15天),1c(当天)

                //构造请求函数
                AlipayClass.AlipayService aliService;
                if (Session["token"] == null)
                {
                    aliService = new AlipayClass.AlipayService
                        (partner,
                        seller_email,
                        return_url,
                        notify_url,
                        show_url,
                        out_trade_no,
                        subject,
                        body,
                        "0.01",
                        paymethod,
                        defaultbank,
                        encrypt_key,
                        exter_invoke_ip,
                        extra_common_param,
                        buyer_email,
                        royalty_type,
                        royalty_parameters,
                        it_b_pay,
                        key,
                        input_charset,
                        sign_type
                        );
                }
                else
                {
                    string token = Session["token"].ToString();
                    aliService = new AlipayClass.AlipayService
                        (partner,
                        seller_email,
                        return_url,
                        notify_url,
                        show_url,
                        out_trade_no,
                        subject,
                        body,
                        "0.01",
                        paymethod,
                        defaultbank,
                        encrypt_key,
                        exter_invoke_ip,
                        extra_common_param,
                        buyer_email,
                        royalty_type,
                        royalty_parameters,
                        it_b_pay,
                        key,
                        input_charset,
                        sign_type,
                        token
                        );
                    Session.Remove("token");
                }
                //GET方式传递
                LogUtils.Log(
                    string.Format("支付宝支付--创建支付链接。"),
                    "进入支付宝支付",
                    Category.Info,
                    this.Session.SessionID,
                    this.UserSession.UserID,
                    "Alipay");

                string url = aliService.Create_url();

                LogUtils.Log(
                    string.Format("支付宝支付--创建支付链接完成。链接：{0}",url),
                    "进入支付宝支付",
                    Category.Info,
                    this.Session.SessionID,
                    this.UserSession.UserID,
                    "Alipay");

                //Session.Remove("OrderID");
                //Session.Remove("Number");
                //Session.Remove("SumMoney");
                //Session.Remove("SendDays");
                //Response.Redirect(url);
                return this.Redirect(url);

                #endregion
            }
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Notify()
        {
            LogUtils.Log(
                    string.Format("支付宝支付--接收到支付宝异步通知。准备处理订单支付信息"),
                    "支付宝通知",
                    Category.Info,
                    this.Session.SessionID,
                    this.UserSession.UserID,
                    "Alipay");

            ArrayList sArrary = GetRequestPost();
            string partner = "2088301856479212";		//合作身份者ID
            string key = "sji2sos0koz072vl07sg5xvtctvq6hfp";			//安全检验码
            string input_charset = "utf-8";                     //字符编码格式 目前支持 gb2312 或 utf-8
            string sign_type = "MD5";                           //加密方式 不需修改
            string transport = "http";                         //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http

            if (sArrary.Count > 0)//判断是否有带返回参数
            {
                AlipayClass.AlipayNotify aliNotify = new AlipayClass.AlipayNotify(sArrary, Request.Form["notify_id"], partner, key, input_charset, sign_type, transport);
                string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                string sign = Request.Form["sign"];         //获取支付宝反馈回来的sign结果
                string mysign = aliNotify.Mysign;           //获取通知返回后计算后（验证）的加密结果

                //写日志记录（若要调试，请取消下面两行注释）
                //string sWord = "responseTxt=" + responseTxt + "\n notify_url_log:sign=" + Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + AlipayFunction.Create_linkstring(sArrary);
                //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
                string order_code = Request.Form["out_trade_no"];     //获取订单号
                if (responseTxt == "true" && sign == mysign)//验证成功
                {
                    //获取支付宝的通知返回参数
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号                
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态
                    //int sOld_trade_status = 0;						//获取商户数据库中查询得到该笔交易当前的交易状态

                    //检查此交易在我系统是否已处理，若是，则不进行其他处理
                    var paymentList = new OrderPaymentService().QueryByTradeNo(trade_no);

                    if (paymentList != null && paymentList.Count > 0)
                    {
                        this.ViewBag.Message = "支付成功,订单号：" + trade_no;
                        return this.Content("Success");
                    }

                    if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //放入订单交易完成后的数据库更新程序代码，请务必保证response.Write出来的信息只有success
                        //为了保证不被重复调用，或重复执行数据库更新程序，请判断该笔交易状态是否是订单未处理状态
                        double totalFee = 0;
                        var orderSevice = new OrderService(this.UserSession.UserID, false);
                        var order = orderSevice.QueryByOrderCode(order_code);
                        if (!double.TryParse(total_fee, out totalFee))
                        {
                            LogUtils.Log(
                                string.Format(
                                    "支付宝支付返回支付金额异常，支付宝交易号{0}，购酒网订单号{1},支付金额{2}，买家支付宝账号{3}，交易状态{4}",
                                    trade_no,
                                    order_code,
                                    total_fee,
                                    buyer_email,
                                    trade_status),
                                "支付宝交易通知",
                                Category.Error,
                                this.UserSession.SessionId);
                            return this.Content("failed");
                        }

                        if (order == null)
                        {
                            LogUtils.Log(
                                string.Format(
                                    "支付宝支付返回时获取订单信息异常，支付宝交易号{0}，购酒网订单号{1},支付金额{2}，买家支付宝账号{3}，交易状态{4}",
                                    trade_no,
                                    order_code,
                                    total_fee,
                                    buyer_email,
                                    trade_status),
                                "支付宝交易通知",
                                Category.Error,
                                this.UserSession.SessionId);
                            return this.Content("Success"); //作为成功信息进行返回，防止支付宝服务器反复请求
                        }

                        //测试使用0.01作为支付金额 
                        //if (totalFee != (order.TotalMoney + order.DeliveryCost))
                        if (totalFee != 0.01)
                        {
                            LogUtils.Log(
                                string.Format(
                                    "支付宝支付返回支付金额异常，支付宝交易号{0}，购酒网订单号{1},支付金额{2}，买家支付宝账号{3}，交易状态{4}",
                                    trade_no,
                                    order_code,
                                    total_fee,
                                    buyer_email,
                                    trade_status),
                                "支付宝交易通知",
                                Category.Error,
                                this.UserSession.SessionId);
                            return this.Content("failed");
                        }

                        //支付成功，改写数据库订单信息
                        //添加支付记录信息
                        //添加订单状态跟踪信息
                        order.PaymentStatus = 1;
                        order.Status = order.Status == 255 ? 0 : order.Status;
                        if (orderSevice.OrderOnLinePayment(order, totalFee, 1, trade_no))
                        {
                            return this.Content("Success");
                        }
                        else
                        {
                            return this.Content("Fail");
                        }
                        
                        //Response.Redirect(Common.Constant.SiteUrl + "/purchase/Success-Number-" + order_no + "-Msg-1.htm");
                    }
                    else
                    {
                        return this.Content("Success");  //其他状态判断。普通即时到帐中，其他状态不用判断，直接打印success。
                        //logpay = new BLL.AdminLog();
                        //logpay.AddLog("odr_order", 0, "（Page:notify支付宝支付异常）交易状态：" + Request.Form["trade_status"].ToString());
                        //Response.Redirect(Common.Constant.SiteUrl + "/purchase/Success-Number-" + order_no + "-Msg-1.htm");
                    }
                }
                else//验证失败
                {
                    return this.Content("fail");
                    //logpay = new BLL.AdminLog();
                    //string strMsg = string.Format("（Page:notify支付宝支付异常）验证失败(responseTxt:{0},sign:{1},mysign:{2},number:{3})", responseTxt, sign, mysign, order_no);
                    //logpay.AddLog("odr_order", 0, strMsg);
                    //Response.Redirect(Common.Constant.SiteUrl + "/purchase/Success-Number-" + Request.Form["out_trade_no"] + "-Msg-1.htm");
                }
            }
            else
            {
                LogUtils.Log("支付宝支付异步通知无返回参数");
                return this.Content("无通知参数");
                //logpay = new BLL.AdminLog();
                //logpay.AddLog("odr_order", 0, "（Page:notify支付宝支付异常）无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public ArrayList GetRequestPost()
        {
            int i = 0;
            ArrayList sArray = new ArrayList();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i] + "=" + Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        [HttpGet]
        public ActionResult Success()
        {
            ArrayList sArrary = GetRequestGet();
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的//////////////////////
            string partner = "2088301856479212"; //合作身份者ID
            string key = "sji2sos0koz072vl07sg5xvtctvq6hfp"; //安全检验码
            string input_charset = "utf-8"; //字符编码格式 目前支持 gb2312 或 utf-8
            string sign_type = "MD5"; //加密方式 不需修改
            string transport = "http"; //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
            //////////////////////////////////////////////////////////////////////////////////////////////

            if (sArrary.Count > 0) //判断是否有带返回参数
            {
                AlipayClass.AlipayNotify aliNotify = new AlipayClass.AlipayNotify(
                    sArrary,
                    Request.QueryString["notify_id"],
                    partner,
                    key,
                    input_charset,
                    sign_type,
                    transport);
                string responseTxt = aliNotify.ResponseTxt; //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
                string sign = Request.QueryString["sign"]; //获取支付宝反馈回来的sign结果
                string mysign = aliNotify.Mysign; //获取通知返回后计算后（验证）的加密结果

                //写日志记录（若要调试，请取消下面两行注释）
                //string sWord = "responseTxt=" + responseTxt + "\n return_url_log:sign=" + Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + AlipayFunction.Create_linkstring(sArrary);
                //AlipayFunction.log_result(Server.MapPath("log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", sWord);

                //判断responsetTxt是否为ture，生成的签名结果mysign与获得的签名结果sign是否一致
                //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
                //mysign与sign不等，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
                string order_code = Request.QueryString["out_trade_no"]; //获取订单号
                if (responseTxt == "true" && sign == mysign) //验证成功
                {
                    //获取支付宝的通知返回参数
                    string trade_no = Request.QueryString["trade_no"]; //支付宝交易号
                    string total_fee = Request.QueryString["total_fee"]; //获取总金额
                    string subject = Request.QueryString["subject"]; //商品名称、订单名称
                    string body = Request.QueryString["body"]; //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"]; //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"]; //交易状态
                    int sOld_trade_status = 0; //获取商户数据库中查询得到该笔交易当前的交易状态

                    //验证此第三方交易是否已经处理，若是，则不进行重复处理，否则，更新订单信息

                    var paymentList = new OrderPaymentService().QueryByTradeNo(trade_no);
                    if (paymentList != null && paymentList.Count > 0)
                    {
                        this.ViewBag.Message = "支付成功,订单号：" + order_code;
                        return this.View("Success");
                    }

                    double totalFee = 0;
                    var orderSevice = new OrderService(this.UserSession.UserID, false);
                    var order = orderSevice.QueryByOrderCode(order_code);
                    if (!double.TryParse(total_fee, out totalFee))
                    {
                        LogUtils.Log(
                            string.Format(
                                "支付宝支付返回支付金额异常，支付宝交易号{0}，购酒网订单号{1},支付金额{2}，买家支付宝账号{3}，交易状态{4}",
                                trade_no,
                                order_code,
                                total_fee,
                                buyer_email,
                                trade_status),
                            "支付宝交易通知",
                            Category.Error,
                            this.UserSession.SessionId);
                        this.ViewBag.Message = "支付异常，请速与我司客服联系处理。订单号：" + order_code;
                        return this.View("Success");
                    }

                    if (order == null)
                    {
                        LogUtils.Log(
                            string.Format(
                                "支付宝支付返回时获取订单信息异常，支付宝交易号{0}，购酒网订单号{1},支付金额{2}，买家支付宝账号{3}，交易状态{4}",
                                trade_no,
                                order_code,
                                total_fee,
                                buyer_email,
                                trade_status),
                            "支付宝交易通知",
                            Category.Error,
                            this.UserSession.SessionId);
                        this.ViewBag.Message = "支付异常，请速与我司客服联系处理。订单号：" + order_code;
                        return this.View("Success");
                    }

                    if (totalFee != 0.01)
                    {
                        LogUtils.Log(
                            string.Format(
                                "支付宝支付返回支付金额异常，支付宝交易号{0}，购酒网订单号{1},已支付金额{2}，订单应支付金额{3}，买家支付宝账号{4}，交易状态{5}",
                                trade_no,
                                order_code,
                                total_fee,
                                order.TotalMoney + order.DeliveryCost,
                                buyer_email,
                                trade_status),
                            "支付宝交易通知",
                            Category.Error,
                            this.UserSession.SessionId);

                        this.ViewBag.Message = "支付异常，请速与我司客服联系，确认订单状态。订单号：" + order_code;
                        return this.View("Success");
                    }

                    //支付成功，改写数据库订单信息
                    //添加支付记录信息
                    //添加订单状态跟踪信息
                    order.PaymentStatus = 1;
                    order.Status = order.Status == 255 ? 0 : order.Status;
                    orderSevice.OrderOnLinePayment(order, totalFee, 1, trade_no);
                    this.ViewBag.Message = "订单支付成功，订单号：" + order_code;
                    return this.View("Success");
                    
                }
                else //验证失败
                {
                    //lbVerify.Text = "验证失败";
                    //logpay = new BLL.AdminLog();
                    string strMsg =
                        string.Format(
                            "（Page:Return支付宝支付异常）验证失败(responseTxt:{0},sign:{1},mysign:{2},number:{3})",
                            responseTxt,
                            sign,
                            mysign,
                            order_code);
                    //logpay.AddLog("odr_order", 0, strMsg);
                    //Response.Redirect(
                    //    Common.Constant.SiteUrl + "/purchase/Success-Number-" + Request.QueryString["out_trade_no"]
                    //    + "-Msg-1.htm");
                    this.ViewBag.Message = strMsg;
                    return this.View("Success");
                }
            }
            else
            {
                //lbVerify.Text = "无返回参数";
                //logpay = new BLL.AdminLog();
                //logpay.AddLog("odr_order", 0, "（Page:Return支付宝支付异常）" + lbVerify.Text);
                //Response.Redirect(
                //    Common.Constant.SiteUrl + "/purchase/Success-Number-" + Request.QueryString["out_trade_no"]
                //    + "-Msg-1.htm");
                this.ViewBag.Message = "支付失败，无返回参数";
                return this.View("Success");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public ArrayList GetRequestGet()
        {
            int i = 0;
            ArrayList sArray = new ArrayList();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i] + "=" + Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

    }
}
