using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
	using System.Data;
	using System.Data.SqlClient;

	using Sms;

	using V5.DataContract.Configuration;
	using V5.DataContract.Transact;
	using V5.DataContract.Transact.Order;
	using V5.Library.Logger;
	using V5.Library.Security;
	using V5.Library.Storage.DB;
	using V5.Service.Configuration;
	using V5.Service.Transact;
	using V5.Service.User;

	public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            return View();
        }
		
	    public ActionResult API()
	    {

			string orderCode = Server.UrlDecode(Request.QueryString["orderid"]);//订单编号
			string strMsg = "";
			SqlTransaction transaction = null;

		    try
		    {
				string username = Server.UrlDecode(Request.QueryString["username"]);//ERP昵称
				string password = Server.UrlDecode(Request.QueryString["password"]);//ERP密码
				string key = Server.UrlDecode(Request.QueryString["key"]);//公钥
				string sign = Server.UrlDecode(Request.QueryString["sign"]);//检验码
				string method = Server.UrlDecode(Request.QueryString["method"]);//调用接口

				#region 访问日志

				try
				{
					new OrderERPLogService().AddHwLog(new Hw_Log { Content = Request.Url.ToString(), Number = orderCode }, null);
					LogUtils.Log(string.Format("成功写入ERP系统访问日志，订单编号：{0}，日志信息：{1}", orderCode, Request.Url.ToString()), "ERP订单回写", Category.Error);
				}
				catch (Exception exception)
				{
					LogUtils.Log(string.Format("写入ERP系统访问日志失败，订单编号：{0}，日志信息：{1},错误信息：{2}", orderCode, Request.Url.ToString(), exception.Message + "/" + exception.InnerException), "ERP订单回写", Category.Error);
				}

				#endregion

				#region 基本参数验证
				if (string.IsNullOrEmpty(username))
				{
					strMsg = SetMsg("0", "昵称不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(orderCode))
				{
					strMsg = SetMsg("0", "订单不正确", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (username != "hongware")
				{
					strMsg = SetMsg("0", "昵称不正确", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(password))
				{
					strMsg = SetMsg("0", "密码不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (password != "bir19ming19ham")
				{
					strMsg = SetMsg("0", "密码不正确", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(key))
				{
					strMsg = SetMsg("0", "公钥不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (key != "g1w9j1r9w")
				{
					strMsg = SetMsg("0", "公钥不正确", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(sign))
				{
					strMsg = SetMsg("0", "检验码不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(orderCode))
				{
					strMsg = SetMsg("0", "订单编号不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				string sign_Md5 = Encrypt.HwErpMd5(username + password + orderCode + key);
				if (sign != sign_Md5)
				{
					strMsg = SetMsg("0", "检验码不正确", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				if (string.IsNullOrEmpty(method))
				{
					strMsg = SetMsg("0", "调用方法名不能为空", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				method = method.ToLower();
				#endregion

				#region 订单当前状态

				var orderService = new OrderService();
				var orderTracking = new OrderStatusTrackingService();

				var order = orderService.QueryByOrderCode(orderCode);

				if (order == null || order.ID < 1)
				{
					strMsg = SetMsg("0", "未查到此订单号,请核实", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);
				}
				int OrderID = order.ID;
				int State = order.Status;
				#endregion

				if (method == "api.order.send")
				{
					#region 订单发货
					if (State == 0)
					{
						strMsg = SetMsg("0", "订单还未确认", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 2)
					{
						strMsg = SetMsg("0", "订单已发货", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 3)
					{
						strMsg = SetMsg("0", "订单已签收", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 4 || State == 5 || State == 8)
					{
						strMsg = SetMsg("0", "订单已取消", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}

					string expressno = string.Empty;
					expressno = Server.UrlDecode(Request.QueryString["expressno"]);//快递代码
					string expressnum = Server.UrlDecode(Request.QueryString["expressnum"]);//快递单号
					string deliverydate = Server.UrlDecode(Request.QueryString["deliverydate"]);//发货日期

					#region 参数验证
					if (string.IsNullOrEmpty(expressno))
					{
						strMsg = SetMsg("0", "快递代码不能为空", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					if (string.IsNullOrEmpty(expressnum))
					{
						strMsg = SetMsg("0", "快递单号不能为空", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					if (string.IsNullOrEmpty(deliverydate))
					{
						strMsg = SetMsg("0", "发货日期不能为空", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					#endregion

					Config_Delivery_Corporation deliveryCorporation = null;

					#region 更新订单发货状态
					/*
					 订单发货：
					 * 1.更改订单状态，
					 * 2.更改订单跟踪状态
					 */
					try
					{
						var deliveryCorporations = new ConfigDeliveryCorporationService().QueryAllConfigDeliveryCorporations();

						if (deliveryCorporations != null && deliveryCorporations.Count > 1)
						{
							deliveryCorporation =
								deliveryCorporations.Find(dc => expressno.Equals(dc.Number, StringComparison.OrdinalIgnoreCase));

						}

						if (deliveryCorporation == null)
						{
							deliveryCorporation = new Config_Delivery_Corporation();
							LogUtils.Log("没有获取到代号为:" + expressno.ToUpper() + "的配送公司", "API", Category.Warn);
						}

						var tracking = new Order_Status_Tracking();

						tracking.MailNo = expressnum;
						tracking.ExpressNumber = expressno.ToUpper();
						tracking.OrderID = OrderID;
						tracking.Status = 2;
						tracking.EmployeeID = 0;
						tracking.UserID = 0;
						tracking.Remark = string.Format(
							"订单已发货，配送单位：{0} {1}； 快递单号：{2}",
							deliveryCorporation.Name,
							deliveryCorporation.URL,
							expressnum);

						tracking.CreateTime = Convert.ToDateTime(deliverydate);

						orderService.SqlServer.BeginTransaction();
						transaction = orderService.SqlServer.Transaction;

						orderTracking.Add(tracking, transaction);

						order.Status = 2;
						order.DeliveryCorporationID = deliveryCorporation.ID;
						orderService.Edit(order, transaction);

						transaction.Commit();
					}
					catch (Exception exception)
					{
						if (transaction != null)
						{
							transaction.Rollback();
						}

						LogUtils.Log(
							string.Format(
								"[Order_ERP]ERP回写发货订单时发生错误，订单编号:{0}，错误消息:{1}",
								orderCode,
								exception.Message + "/" + exception.InnerException),
							"[Order_ERP]ERP订单发货回写官网",
							Category.Error);

						strMsg = SetMsg("0", "更新订单发货状态发生异常", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					finally
					{
						if (transaction != null && transaction.Connection != null
							&& transaction.Connection.State != ConnectionState.Closed)
						{
							transaction.Connection.Close();
						}
					}
					#endregion

					#region 短信发送

					try
					{
						var orderReceiver = new UserReceiveAddressService().QueryByID(order.UserID);

						if (orderReceiver != null && orderReceiver.Mobile != null)
						{
							var moList = new List<string>();
							moList.Add(orderReceiver.Mobile);

							var sm = new ShortMessage
							{
								ReceiveMobiles = moList,
								Content =
									string.Format(
										"亲爱的购酒网会员，您的订单（订单号：{0}）支付方式为：{1}，已经发货，配送公司:{2}， 单号：{3}。请注意保持手机畅通。",
										orderCode,
										order.PaymentMethodName,
										deliveryCorporation.Name,
										deliveryCorporation.Number)
							};
							sm.Send();
							LogUtils.Log(
								"用户:" + orderReceiver.Consignee + "电话：" + orderReceiver.Mobile + "成功发送短信",
								"SendSms",
								Category.Info,
								Session.SessionID);

						}
						else
						{
							LogUtils.Log("[Order_ERP]由于没有获取到订单收货人或收货人的手机号不存在，因此订单（" + order.OrderCode + "）发货，未发送通知短信", "[Order_ERP]订单发货回写发送短信SendSms", Category.Error);
						}
					}
					catch (Exception ex)
					{
						LogUtils.Log("[Order_ERP]ERP订单发货发送短信发生错误，错误消息:" + ex.Message + "/" + ex.InnerException, "[Order_ERP]订单发货回写发送短信SendSms", Category.Error);
					}


					#endregion

					strMsg = SetMsg("1", "订单状态更新成功", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);

					#endregion
				}
				else if (method == "api.order.cancel")
				{
					#region 订单取
					if (State == 0)
					{
						strMsg = SetMsg("0", "订单还未确认", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 2)
					{
						strMsg = SetMsg("0", "订单已发货", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 3)
					{
						strMsg = SetMsg("0", "订单已确认收货", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}
					else if (State == 4 || State == 5 || State == 8)
					{
						strMsg = SetMsg("0", "订单已取消", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}

					try
					{
						orderService.SetInvalidByERP(order.ID, "订单作废");
					}
					catch (Exception exception)
					{

						LogUtils.Log(
							string.Format(
								"[Order_ERP]ERP订单取消回写发生错误，订单号:{0},错误消息:{1}",
								orderCode,
								exception.Message + "/" + exception.InnerException),
							"[Order_ERP]ERP订单取消回写",
							Category.Error);

						strMsg = SetMsg("0", "订单作废发生错误", orderCode);
						//Response.Write(strMsg);
						return this.Content(strMsg);
					}

					strMsg = SetMsg("1", "订单作废成功", orderCode);
					//Response.Write(strMsg);
					return this.Content(strMsg);

					#endregion
				}

				strMsg = SetMsg("0", "调用方法名不正确", orderCode);
				//Response.Write(strMsg);
				return this.Content(strMsg);
		    }
		    catch (Exception)
		    {
				strMsg = SetMsg("0", "订单回写发生异常", orderCode);
				//Response.Write(strMsg);
				return this.Content(strMsg);
		    }
			
	    }

		public string SetMsg(string is_success, string result, string number)
		{
			return "{\"is_success\":\"" + is_success + "\", \"result\":\"" + result + "\", \"number\":\"" + number + "\"}";
		}
    }
}
