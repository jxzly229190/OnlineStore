using System;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using tenpay;

    using V5.Library.Logger;
    using V5.Portal.Attributes;
    using V5.Portal.Common;
    using V5.Service.Transact;

    public class TenpayController : BaseController
    {
        //
        // GET: /Tenpay/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Return()
        {
            LogUtils.Log("接收财付通支付通知消息，订单","财付通支付--支付通知", Category.Info, this.Session.SessionID, this.UserSession.UserID, "Tenpay/Return");
            
            //密钥
            string key = "f24967df0075851d22411c36b04899ac";

            //创建PayResponseHandler实例
            PayResponseHandler resHandler = new PayResponseHandler(System.Web.HttpContext.Current);

            resHandler.setKey(key);

            string order_code = resHandler.getParameter("sp_billno");

            //判断签名
            if (resHandler.isTenpaySign())
            {
                LogUtils.Log("判断签名合法", "财付通支付--支付通知", Category.Info, this.Session.SessionID, this.UserSession.UserID, "Tenpay/Return");

                //交易单号
                string transaction_id = resHandler.getParameter("transaction_id");

                //支付金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");

                //支付结果
                string pay_result = resHandler.getParameter("pay_result");

                if (pay_result == "0") //0-已支付，1-未支付，其他为支付错误
                {
                    LogUtils.Log("订单支付成功"+string.Format("订单号：{0},财付通交易号:{1},支付金额:{2}",order_code,transaction_id,total_fee), "财付通支付--支付通知", Category.Info, this.Session.SessionID, this.UserSession.UserID, "Tenpay/Return");
                    //------------------------------
                    //处理业务开始
                    //------------------------------ 

                    //注意交易单不要重复处理
                    //注意判断返回金额

                    //Common.DataContext ctx = new Common.DataContext();
                    //Model.odr_Order_S os = new Model.odr_Order_S();
                    //os.Action = 14;
                    //os.Number = strNumber;
                    //BLL.Order od = new BLL.Order();
                    //DataTable dt = od.GetOrderInfo(ctx, os);

                    //double totalFee = 0;
                    var orderSevice = new OrderService(this.UserSession.UserID, false);
                    var order = orderSevice.QueryByOrderCode(order_code);

                    //todo:此处应验证订单支付金额是否正确，测试使用1分
					//if (double.Parse(total_fee) == 1)
                    if (double.Parse(total_fee) == (Math.Round(order.DeliveryCost + order.TotalMoney, 2) * 100))
                    {
                        ViewBag.Message = "财富通--支付成功，"
                                          + string.Format(
                                              "订单号：{0},财付通交易号:{1},支付金额:{2}",
                                              order.OrderCode,
                                              transaction_id,
                                              total_fee);

                        if (order.PaymentStatus != 1)
                        {
                            order.PaymentStatus = 1;
                            order.Status = order.Status == 255 ? 0 : order.Status;
							//默认5为财富通
                            if (orderSevice.OrderOnLinePayment(order, Math.Round(double.Parse(total_fee)/100,2), 5, transaction_id))
                            {
                                LogUtils.Log(
                                    string.Format(
                                        "订单支付成功，订单状态更新成功，订单号：{0},财付通交易号:{1},支付金额:{2}",
                                        order.OrderCode,
                                        transaction_id,
                                        total_fee),
                                    "财付通支付--支付通知",
                                    Category.Info,
                                    this.Session.SessionID,
                                    this.UserSession.UserID,
                                    "Tenpay/Return");
                            }
                            else
                            {
                                ViewBag.Message = "财富通--支付成功，"
                                                  + string.Format(
                                                      "订单状态更新失败，为了保护您的权益，请立即与客服人员联系。订单号：{0},财付通交易号:{1},支付金额:{2}",
                                                      order.OrderCode,
                                                      transaction_id,
                                                      total_fee);
                                LogUtils.Log(
                                    string.Format(
                                        "订单支付成功，订单状态更新失败，订单号：{0},财付通交易号:{1},支付金额:{2}",
                                        order.OrderCode,
                                        transaction_id,
                                        total_fee),
                                    "财付通支付--支付通知",
                                    Category.Info,
                                    this.Session.SessionID,
                                    this.UserSession.UserID,
                                    "Tenpay/Return");
                            }
                        }
                        ViewBag.Money = total_fee;
                        ViewBag.Url = ConstantParams.SiteUrl + "Tenpay/Success";
                    }
                }
                else
                {
                    LogUtils.Log("订单支付失败(参数指示未支付),"+string.Format("订单号：{0},财付通交易号:{1},支付金额:{2}",order_code,transaction_id,total_fee), "财付通支付--支付通知", Category.Info, this.Session.SessionID, this.UserSession.UserID, "Tenpay/Return");
                    
                    //当做不成功处理
                    ViewBag.Message = "订单支付失败，"
                                      + string.Format(
                                          "订单号：{0},财付通交易号:{1},支付金额:{2}",
                                          order_code,
                                          transaction_id,
                                          total_fee);

                    //Response.Redirect(CONST.Url + "/purchase/PayFinish-Number-" + strNumber + "-Msg-1.htm");
                }

                if (ViewBag.Url == null)
                {
                    ViewBag.Url = ConstantParams.SiteUrl + "Tenpay/Fail";
                }

                return this.View("Return");
            }
            else
            {
                LogUtils.Log("认证签名失败，订单编号："+order_code, "财付通支付--支付通知", Category.Info, this.Session.SessionID, this.UserSession.UserID, "Tenpay/Return");
                    
                //当做不成功处理
                ViewBag.Message = "订单支付失败，" + string.Format("订单号：{0}", order_code);

                return null; //不处理，等待下一次通知
                //Response.Redirect(CONST.Url + "/purchase/PayFinish-Number-" + strNumber + "-Msg-1.htm");
                //string debugInfo = resHandler.getDebugInfo();
                //Response.Write("<br/>debugInfo:" + debugInfo);
            }
        }

        public ActionResult Fail()
        {
            return this.View();
        }

        public ActionResult Success()
        {
            //密钥
            string key = "f24967df0075851d22411c36b04899ac";

            //创建PayResponseHandler实例
            PayResponseHandler resHandler = new PayResponseHandler(System.Web.HttpContext.Current);

            resHandler.setKey(key);

            ViewBag.Money = resHandler.getParameter("total_fee");

            return this.View();
        }
        
        public ActionResult DoPay(string ono)
        {
            //商户号
            string bargainor_id = "1211288901";

            //密钥
            string key = "f24967df0075851d22411c36b04899ac";

            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrWhiteSpace(ono))
            {
                LogUtils.Log("财付通支付错误：订单编号为空", "财付通支付--验证订单编号", Category.Warn, this.Session.SessionID, this.UserSession.UserID, "Tenpay/DoPay");
                return this.Content("<script type='text/javascript'>alert('对不起，订单编号错误，无法完成支付！');window.location='" + ConstantParams.SiteUrl + "User/MyOrder';" + "</script>");
            }

            //验证订单信息
            var order = new OrderService(this.UserSession.UserID, false).QueryByOrderCode(ono);
            if (order == null || order.UserID != this.UserSession.UserID)
            {
                LogUtils.Log("财付通支付错误：订单编号异常（" + ono + "）", "财付通支付--验证订单编号", Category.Warn, this.Session.SessionID, this.UserSession.UserID, "Tenpay/DoPay");
                return this.Content("<script type='text/javascript'>alert('对不起，订单编号错误，无法完成支付！');window.location='" + ConstantParams.SiteUrl + "User/MyOrder';" + "</script>");
            }

            //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            string strReq = ono;
            //if (strReq.Length > 9)
            //    strReq = Session["Number"].ToString().Substring(3);// "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);
            //else
            //    strReq = "0" + Session["Number"].ToString();

            //商户订单号，不超过32位，财付通只做记录，不保证唯一性
            string sp_billno = strReq;

            //财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
            string transaction_id = bargainor_id + date + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4); //todo：生成唯一的10位系列号，目前用时间和随机数生成
            string return_url = ConstantParams.SiteUrl + "Tenpay/Return";

            //创建PayRequestHandler实例
            PayRequestHandler reqHandler = new PayRequestHandler(System.Web.HttpContext.Current);

            //设置密钥
            reqHandler.setKey(key);

            //初始化
            reqHandler.init();

            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("bargainor_id", bargainor_id);			//商户号
            reqHandler.setParameter("sp_billno", sp_billno);				//商家订单号
            reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
            reqHandler.setParameter("return_url", return_url);				//支付通知url
            reqHandler.setParameter("desc", "购酒网酒水购买 ");	//商品名称

            double total_fee = Math.Round(order.TotalMoney + order.DeliveryCost, 2) * 100;
            string totalfee = total_fee.ToString();
            reqHandler.setParameter("total_fee", totalfee);			//商品金额,以分为单位


            //用户ip,测试环境时不要加这个ip参数，正式环境再加此参数
            reqHandler.setParameter("spbill_create_ip", Request.UserHostAddress);

            //获取请求带参数的url
            string requestUrl = reqHandler.getRequestURL();

            return this.Redirect(requestUrl);

            //string a_link = "<a target=\"_blank\" href=\"" + requestUrl + "\">" + "财付通支付" + "</a>";
            //Response.Write(a_link);


            //post实现方式

            //reqHandler.getRequestURL();
            //Response.Write("<form method=\"post\" action=\""+ reqHandler.getGateUrl() + "\" >\n");
            //Hashtable ht = reqHandler.getAllParameters();
            //foreach(DictionaryEntry de in ht) 
            //{
            //    Response.Write("<input type=\"hidden\" name=\"" + de.Key + "\" value=\"" + de.Value + "\" >\n");
            //}
            //Response.Write("<input type=\"submit\" value=\"财付通支付\" >\n</form>\n");


            //获取debug信息
            //string debuginfo = reqHandler.getDebugInfo();
            //Response.Write("<br/>" + debuginfo + "<br/>");

            //Session.Remove("Number");
            //Session.Remove("SumMoney");
            //Session.Remove("PaywayChildID");
            //Session.Remove("BankName");

            //重定向到财付通支付
            //reqHandler.doSend();
        }
    }
}
