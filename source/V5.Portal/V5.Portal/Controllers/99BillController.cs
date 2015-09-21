using System;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    using V5.Library.Logger;
    using V5.Portal.Attributes;
    using V5.Portal.Common;
    using V5.Service.Transact;
    public class _99BillController : BaseController
    {
        //
        // GET: /99Bill/

        public static string CerRSASignature(string OriginalString, string prikey_path, string CertificatePW, int SignType)
        {
            byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
            X509Certificate2 x509_Cer1 = new X509Certificate2(prikey_path, CertificatePW);
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)x509_Cer1.PrivateKey;
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
            byte[] result;
            switch (SignType)
            {
                case 1:
                    f.SetHashAlgorithm("MD5");//摘要算法MD5
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    result = md5.ComputeHash(OriginalByte);//摘要值
                    break;
                default:
                    f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    result = sha.ComputeHash(OriginalByte);//摘要值
                    break;
            }
            string SignData = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();

            return SignData;
        }

        //功能函数。将变量值不为空的参数组成字符串
        public string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + paramValue;
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }
            return returnStr;
        }
        //功能函数。将变量值不为空的参数组成字符串。结束

       
        /// <summary>
        /// 接收返回的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceivePki()
        {
            // payState & strPayMsg & strNumber 记录快钱支付结果
            bool payState = false;              // 快钱支付状态
            string strPayMsg = string.Empty;    // 支付结果
            string strNumber = string.Empty;    // 订单号
            try
            {
                //获取人民币网关账户号
                string merchantAcctId = Request["merchantAcctId"].ToString().Trim();

                //获取网关版本.固定值
                ///快钱会根据版本号来调用对应的接口处理程序。
                ///本代码版本号固定为v2.0
                string version = Request["version"].ToString().Trim();

                //获取语言种类.固定选择值。
                ///只能选择1、2、3
                ///1代表中文；2代表英文
                ///默认值为1
                string language = Request["language"].ToString().Trim();

                //签名类型.固定值
                ///1代表MD5签名
                ///当前版本固定为1
                string signType = Request["signType"].ToString().Trim();

                //获取支付方式
                ///值为：10、11、12、13、14
                ///00：组合支付（网关支付页面显示快钱支持的各种支付方式，推荐使用）10：银行卡支付（网关支付页面只显示银行卡支付）.11：电话银行支付（网关支付页面只显示电话支付）.12：快钱账户支付（网关支付页面只显示快钱账户支付）.13：线下支付（网关支付页面只显示线下支付方式）.14：B2B支付（网关支付页面只显示B2B支付，但需要向快钱申请开通才能使用）
                string payType = Request["payType"].ToString().Trim();

                //获取银行代码
                ///参见银行代码列表
                string bankId = Request["bankId"].ToString().Trim();

                //获取商户订单号
                string orderId = Request["orderId"].ToString().Trim();

                //获取订单提交时间
                ///获取商户提交订单时的时间.14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
                ///如：20080101010101
                string orderTime = Request["orderTime"].ToString().Trim();

                //获取原始订单金额
                ///订单提交到快钱时的金额，单位为分。
                ///比方2 ，代表0.02元
                string orderAmount = Request["orderAmount"].ToString().Trim();

                //获取快钱交易号
                ///获取该交易在快钱的交易号
                string dealId = Request["dealId"].ToString().Trim();

                //获取银行交易号
                ///如果使用银行卡支付时，在银行的交易号。如不是通过银行支付，则为空
                string bankDealId = Request["bankDealId"].ToString().Trim();

                //获取在快钱交易时间
                ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
                ///如；20080101010101
                string dealTime = Request["dealTime"].ToString().Trim();

                //获取实际支付金额
                ///单位为分
                ///比方 2 ，代表0.02元
                string payAmount = Request["payAmount"].ToString().Trim();

                //获取交易手续费
                ///单位为分
                ///比方 2 ，代表0.02元
                string fee = Request["fee"].ToString().Trim();

                //获取扩展字段1
                string ext1 = Request["ext1"].ToString().Trim();

                //获取扩展字段2
                string ext2 = Request["ext2"].ToString().Trim();

                //获取处理结果
                ///10代表 成功; 11代表 失败
                string payResult = Request["payResult"].ToString().Trim();

                //获取错误代码
                ///详细见文档错误代码列表
                string errCode = Request["errCode"].ToString().Trim();

                //获取加密签名串
                string signMsg = Request["signMsg"].ToString().Trim();


                //生成加密串。必须保持如下顺序。
                string merchantSignMsgVal = "";
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "merchantAcctId", merchantAcctId);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "version", version);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "language", language);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "signType", signType);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "payType", payType);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankId", bankId);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderId", orderId);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderTime", orderTime);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderAmount", orderAmount);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealId", dealId);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankDealId", bankDealId);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealTime", dealTime);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "payAmount", payAmount);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "fee", fee);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext1", ext1);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext2", ext2);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "payResult", payResult);
                merchantSignMsgVal = appendParam(merchantSignMsgVal, "errCode", errCode);


                strNumber = orderId;
                //商家进行数据处理，并跳转会商家显示支付结果的页面
                ///首先进行签名字符串验证

                string pubkey_path = this.Server.MapPath(@"/99bill/99bill.cert.rsa.20140728.cer");//快钱公钥证书路径
                string CertificatePW = "gou19jiu19wang19";//存放公钥的证书密码
                if (CerRSAVerifySignature(merchantSignMsgVal, signMsg, pubkey_path, CertificatePW, 2))
                {
                    switch (payResult)
                    {
                        case "10":
                            /*  
                             ' 商户网站逻辑处理，比方更新订单支付状态为成功
                            ' 特别注意：只有signMsg.ToUpper() == merchantSignMsg.ToUpper()，且payResult=10，才表示支付成功！
                             * 因为快钱会重复通知这个页面，首先判断订单是否已经更新，没有更新做更新有则不做更新，
                             * 同时将返回的付款金额payamount与提交订单前的订单金额进行对比校验,如果一致则更新订单。
                            */

                            //报告给快钱处理结果，并提供将要重定向的地址。


                            //Common.DataContext ctx = new Common.DataContext();
                            //Model.odr_Order_S os = new Model.odr_Order_S();
                            //os.Action = 14;
                            //os.Number = orderId;
                            //BLL.Order od = new BLL.Order();
                            //DataTable dt = od.GetOrderInfo(ctx, os);
                            //double totalFee = 0;
                            var orderSevice = new OrderService(this.UserSession.UserID, false);
                            var order = orderSevice.QueryByOrderCode(orderId);

                            //todo:此处应验证订单支付金额是否正确，测试使用1分
							//if (double.Parse(payAmount) == 1)
		                    double payMoney = 0.00;
		                    if (!double.TryParse(payAmount, out payMoney))
		                    {
			                    LogUtils.Log("快钱返回支付金额异常，异常金额为："+payAmount);
		                    }
							else if (payMoney == Math.Round(order.DeliveryCost + order.TotalMoney, 2) * 100)
                            {
                                if (order.PaymentStatus != 1) //需要更新订单状态信息
                                {
                                    ViewBag.Message = "快钱--支付成功，"
                                                      + string.Format(
                                                          "订单号：{0},快钱交易号:{1},支付金额:{2}",
                                                          order.OrderCode,
                                                          dealId,
                                                          payAmount);

                                    if (order.PaymentStatus != 1)
                                    {
                                        order.PaymentStatus = 1;
                                        order.Status = order.Status == 255 ? 0 : order.Status;
                                        if (orderSevice.OrderOnLinePayment(
                                            order,
                                            Math.Round(payMoney/100,2), //转为元单位
                                            7,
                                            dealId))
                                        {
                                            LogUtils.Log(
                                                string.Format(
                                                    "订单支付成功，订单状态更新成功，订单号：{0},快钱交易号:{1},支付金额:{2}",
                                                    order.OrderCode,
                                                    dealId,
                                                    payAmount),
                                                "快钱支付--支付通知",
                                                Category.Info,
                                                this.Session.SessionID,
                                                this.UserSession.UserID,
                                                "Tenpay/Return");
                                        }
                                        else
                                        {
                                            ViewBag.Message = "快钱通--支付成功，"
                                                              + string.Format(
                                                                  "订单状态更新失败，为了保护您的权益，请立即与客服人员联系。订单号：{0},快钱交易号:{1},支付金额:{2}",
                                                                  order.OrderCode,
                                                                  dealId,
                                                                  payAmount);
                                            LogUtils.Log(
                                                string.Format(
                                                    "订单支付成功，订单状态更新失败，订单号：{0},快钱交易号:{1},支付金额:{2}",
                                                    order.OrderCode,
                                                    dealId,
                                                    payAmount),
                                                "快钱支付--支付通知",
                                                Category.Info,
                                                this.Session.SessionID,
                                                this.UserSession.UserID,
                                                "Tenpay/Return");
                                        }
                                    }

                                    //判断是否以插入流程
                                    //bool isExists = Controllers.Logic.CartCircuit.IsExists(oc.Id, 1);
                                    //if (isExists)
                                    //{
                                    //    Controllers.Logic.Cart.SetPayWay(oc.Number);
                                    //}
                                }
                                payState = true;

                                //报告给快钱处理结果，并提供将要重定向的地址。
                                rtnOk = "1";
                                rtnUrl = ConstantParams.SiteUrl + "_99Bill/Success";
                            }
                            else
                            {
                                strPayMsg = "支付金额不匹配(" + double.Parse(payAmount).ToString() + "!=" + Math.Round((order.TotalMoney + order.DeliveryCost) * 100, 0).
                                            ToString() + "),订单号：" + orderId + ",快钱交易号：" + dealId + ",银行交易号:" + bankDealId;
                                LogUtils.Log(strPayMsg,"快钱支付通知",Category.Error,this.Session.SessionID,this.UserSession.UserID,"_99Bill/ReceivePki");
                                ViewBag.Message = "订单支付金额异常,为了保护您的权益，请立即与客服人员联系。"
                                                  + string.Format(
                                                      "订单号：{0},快钱交易号:{1},已支付金额:{2},应支付金额{3}",
                                                      orderId,
                                                      dealId,
                                                      Math.Round(Convert.ToDouble(payAmount) / (double)100, 2),
                                                      order.DeliveryCost + order.TotalMoney);

                                rtnUrl = ConstantParams.SiteUrl + "_99Bill/Fail";
                            }
                            break;
                        default:
                            strPayMsg = "支付结果：支付失败";
                            rtnOk = "1";
                            rtnUrl = ConstantParams.SiteUrl + "_99Bill/Fail"; //Common.Constant.SiteUrl + "/purchase/Success-Number-" + orderId + "-Msg-1.htm";
                            break;
                    }
                }
                else//验签失败
                {
                    strPayMsg = "验签失败";
                    rtnOk = "0";
					rtnUrl = ConstantParams.SiteUrl + "_99Bill/Fail";//Common.Constant.SiteUrl + "/purchase/Success-Number-" + orderId + "-Msg-1.htm";
                }
            }
            catch (Exception ex)
            {
                strPayMsg = strPayMsg + ex.Message;
                rtnOk = "0";
				rtnUrl = ConstantParams.SiteUrl + "_99Bill/Fail";
            }
            finally
            {
                if (!payState)
                {
                    if (!string.IsNullOrEmpty(strNumber))
                    {
                        strPayMsg = "(Number:" + strNumber + ")" + strPayMsg;
                    }
                    strPayMsg = "快钱支付异常," + strPayMsg;
                    if (strPayMsg.Length > 200)
                    {
                        strPayMsg = strPayMsg.Substring(0, 200);
                    }
                    //log.AddLog("odr_order", 0, strPayMsg);
                    LogUtils.Log(
                        strPayMsg,
                        "快钱支付--支付通知",
                        Category.Info,
                        this.Session.SessionID,
                        this.UserSession.UserID,
                        "Tenpay/Return");
                }
            }

            ViewBag.rtnOk = rtnOk;
            ViewBag.rtnUrl = rtnUrl;
            return this.View("ReceivePki");
        }

        public ActionResult Success()
        {
            //获取原始订单金额
            ///订单提交到快钱时的金额，单位为分。
            ///比方2 ，代表0.02元
            string orderAmount = Request["orderAmount"].ToString().Trim();
            string payResult = Request["payResult"].ToString().Trim();
            if (payResult == "10")
            {
                ViewBag.Money = double.Parse(orderAmount) / 100;
                return this.View();
            }
            else
            {
                return this.View("Fail");
            }
        }

        #region 功能函数
        
        /// <summary>
        /// 引用证书非对称加/解密RSA-公钥验签【OriginalString：原文；SignatureString：签名字符；pubkey_path：证书路径；CertificatePW：证书密码；SignType：签名摘要类型（1：MD5，2：SHA1）】
        /// </summary>
        public static bool CerRSAVerifySignature(string OriginalString, string SignatureString, string pubkey_path, string CertificatePW, int SignType)
        {
            byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
            byte[] SignatureByte = Convert.FromBase64String(SignatureString);
            X509Certificate2 x509_Cer1 = new X509Certificate2(pubkey_path, CertificatePW);
            RSACryptoServiceProvider rsapub = (RSACryptoServiceProvider)x509_Cer1.PublicKey.Key;
            rsapub.ImportCspBlob(rsapub.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapub);
            byte[] HashData;
            switch (SignType)
            {
                case 1:
                    f.SetHashAlgorithm("MD5");//摘要算法MD5
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    HashData = md5.ComputeHash(OriginalByte);
                    break;
                default:
                    f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    HashData = sha.ComputeHash(OriginalByte);
                    break;
            }
            if (f.VerifySignature(HashData, SignatureByte))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public string rtnOk = "";//定义是否重复通知标志
        public string rtnUrl = "";//定义显示结果show页面地址

        public ActionResult Index(string ono)
        {
             #region 参数定义
        //人民币网关账户号
        ///请登录快钱系统获取用户编号，用户编号后加01即为人民币网关账户号。
         string merchantAcctId = "";

        //字符集.固定选择值。可为空。
        ///只能选择1、2、3.
        ///1代表UTF-8; 2代表GBK; 3代表gb2312
        ///默认值为1
         string inputCharset = "";

        //接受支付结果的页面地址.与[bgUrl]不能同时为空。必须是绝对地址。
        ///如果[bgUrl]为空，快钱将支付结果Post到[pageUrl]对应的地址。
        ///如果[bgUrl]不为空，并且[bgUrl]页面指定的<redirecturl>地址不为空，则转向到<redirecturl>对应的地址
         string pageUrl = "";

        //服务器接受支付结果的后台地址.与[pageUrl]不能同时为空。必须是绝对地址。
        ///快钱通过服务器连接的方式将交易结果发送到[bgUrl]对应的页面地址，在商户处理完成后输出的<result>如果为1，页面会转向到<redirecturl>对应的地址。
        ///如果快钱未接收到<redirecturl>对应的地址，快钱将把支付结果post到[pageUrl]对应的页面。
         string bgUrl = "";

        //网关版本.固定值
        ///快钱会根据版本号来调用对应的接口处理程序。
        ///本代码版本号固定为v2.0
         string version = "";

        //语言种类.固定选择值。
        ///只能选择1、2、3
        ///1代表中文；2代表英文
        ///默认值为1
         string language = "";

        //签名类型.固定值
        ///1代表MD5签名
        ///当前版本固定为1
         string signType = "";

        //支付人姓名
        ///可为中文或英文字符
         string payerName = "";

        //支付人联系方式类型.固定选择值
        ///只能选择1
        ///1代表Email
         string payerContactType = "";

        //支付人联系方式
        ///只能选择Email或手机号
         string payerContact = "";

        //商户订单号
        ///由字母、数字、或[-][_]组成
         string orderId = "";

        //订单金额
        ///以分为单位，必须是整型数字
        ///比方2，代表0.02元
         string orderAmount = "";

        //订单提交时间
        ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
        ///如；20080101010101
         string orderTime = "";

        //订单提交时间
        ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
        ///如；20080101010101
         string stringorderTime = "";

        //商品名称
        ///可为中文或英文字符
         string productName = "";

        //商品数量
        ///可为空，非空时必须为数字
         string productNum = "";

        //商品代码
        ///可为字符或者数字
         string productId = "";

        //商品描述
         string productDesc = "";

        //扩展字段1
        ///在支付结束后原样返回给商户
         string ext1 = "";

        //扩展字段2
        ///在支付结束后原样返回给商户
         string ext2 = "";

        //支付方式.固定选择值
        ///只能选择00、10、11、12、13、14
        ///00：组合支付（网关支付页面显示快钱支持的各种支付方式，推荐使用）10：银行卡支付（网关支付页面只显示银行卡支付）.11：电话银行支付（网关支付页面只显示电话支付）.12：快钱账户支付（网关支付页面只显示快钱账户支付）.13：线下支付（网关支付页面只显示线下支付方式）.14：B2B支付（网关支付页面只显示B2B支付，但需要向快钱申请开通才能使用）
         string payType = "00";

        //银行代码
        ///实现直接跳转到银行页面去支付,只在payType=10时才需设置参数
        ///具体代码参见 接口文档银行代码列表
         string bankId = "";

        //同一订单禁止重复提交标志
        ///固定选择值： 1、0
        ///1代表同一订单号只允许提交1次；0表示同一订单号在没有支付成功的前提下可重复提交多次。默认为0建议实物购物车结算类商户采用0；虚拟产品类商户采用1
         string redoFlag = "0";

        //快钱的合作伙伴的账户号
        ///如未和快钱签订代理合作协议，不需要填写本参数
         string pid = "";

        //签名串
        /// 
         string signMsg = "";

        #endregion

            try
            {
                merchantAcctId = "1002135542801";


                inputCharset = "1";


                pageUrl = ConstantParams.SiteUrl + "_99Bill/ReceivePki";

                bgUrl = ConstantParams.SiteUrl + "_99Bill/ReceivePki";

                version = "v2.0";


                language = "1";


                signType = "4";


                payerName = "购酒网";


                payerContactType = "1";


                payerContact = "";

                //验证订单是否为空
                if (string.IsNullOrWhiteSpace(ono))
                {
                    LogUtils.Log("快钱支付错误：订单编号为空", "快钱支付--验证订单编号", Category.Warn, this.Session.SessionID, this.UserSession.UserID, "Tenpay/DoPay");
                    return this.Content("<script type='text/javascript'>alert('对不起，订单编号错误，无法完成支付！');window.location='" + ConstantParams.SiteUrl + "User/MyOrder';" + "</script>");
                }

                //验证订单信息
                var order = new OrderService(this.UserSession.UserID, false).QueryByOrderCode(ono);
                if (order == null || order.UserID != this.UserSession.UserID)
                {
                    LogUtils.Log("快钱支付错误：订单编号异常（" + ono + "）", "快钱支付--验证订单编号", Category.Warn, this.Session.SessionID, this.UserSession.UserID, "Tenpay/DoPay");
                    return this.Content("<script type='text/javascript'>alert('对不起，订单编号错误，无法完成支付！');window.location='" + ConstantParams.SiteUrl + "User/MyOrder';" + "</script>");
                }

                orderId = ono;

                //todo：测试环境使用1分作为支付金额
                orderAmount = (Math.Round(order.DeliveryCost + order.TotalMoney, 2) * 100).ToString();
                //orderAmount = "1";

                orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                productName = "购酒网酒水购买";

                productNum = "1";

                productId = "";

                productDesc = "购酒网在线下单（酒类或饮料）";

                ext1 = "";


                ext2 = "";


                payType = "00";


                bankId = "";
                if (!string.IsNullOrEmpty(Request.QueryString["bankId"]))
                {
                    payType = "10";
                    bankId = Request.QueryString["bankId"];
                }

                redoFlag = "0";

                pid = "";

                //生成加密签名串
                //请务必按照如下顺序和规则组成加密串！
                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
                signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl);
                signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
                signMsgVal = appendParam(signMsgVal, "version", version);
                signMsgVal = appendParam(signMsgVal, "language", language);
                signMsgVal = appendParam(signMsgVal, "signType", signType);
                signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
                signMsgVal = appendParam(signMsgVal, "payerName", payerName);
                signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType);
                signMsgVal = appendParam(signMsgVal, "payerContact", payerContact);
                signMsgVal = appendParam(signMsgVal, "orderId", orderId);
                signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
                signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
                signMsgVal = appendParam(signMsgVal, "productName", productName);
                signMsgVal = appendParam(signMsgVal, "productNum", productNum);
                signMsgVal = appendParam(signMsgVal, "productId", productId);
                signMsgVal = appendParam(signMsgVal, "productDesc", productDesc);
                signMsgVal = appendParam(signMsgVal, "ext1", ext1);
                signMsgVal = appendParam(signMsgVal, "ext2", ext2);
                signMsgVal = appendParam(signMsgVal, "payType", payType);
                signMsgVal = appendParam(signMsgVal, "bankId", bankId);
                signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
                signMsgVal = appendParam(signMsgVal, "pid", pid);

                //Response.Write(signMsgVal);

                string prikey_path = this.Server.MapPath(@"/99bill/goujiuwang-rsa.pfx");//商户私钥证书路径
                string CertificatePW = "gou19jiu19wang19";//商户私钥密钥
                signMsg = CerRSASignature(signMsgVal, prikey_path, CertificatePW, 2);

                ViewBag.signMsg = signMsg;
                ViewBag.inputCharset = inputCharset;
                ViewBag.pageUrl = pageUrl;
                ViewBag.bgUrl = bgUrl;
                ViewBag.version = version;
                ViewBag.language = language;
                ViewBag.signType = signType;
                ViewBag.merchantAcctId = merchantAcctId;
                ViewBag.payerName = payerName;
                ViewBag.payerContactType = payerContactType;
                ViewBag.payerContact = payerContact;
                ViewBag.orderId = orderId;
                ViewBag.orderAmount = orderAmount;
                ViewBag.orderTime = orderTime;
                ViewBag.productName = productName;
                ViewBag.productNum = productNum;
                ViewBag.productId = productId;
                ViewBag.productDesc = productDesc;
                ViewBag.ext1 = ext1;
                ViewBag.ext2 = ext2;
                ViewBag.payType = payType;
                ViewBag.bankId = bankId;
                ViewBag.redoFlag = redoFlag;
                ViewBag.pid = pid;
            }
            catch (Exception ex)
            {
                string reval = ex.ToString();
                //Response.Redirect(Common.Constant.SiteUrl + "/user/MyOrder.htm");
                return this.Redirect("/User/MyOrder");
            }

            return View("Index");
        }
    }
}
