using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
	using V5.DataContract.Transact;
	using V5.Library;
	using V5.Library.Logger;
	using V5.Service.Transact;

	public class CpsController : Controller
    {
        //
        // GET: /Cps/

        public ActionResult Index()
        {
            return View();
        }

	    public void CPS()
	    {
			string strJump = Request.QueryString["jump"];
			if (Request.QueryString["site"] == "5")
			{
				#region 购酒网接口
				int intSiteID = int.Parse(Request.QueryString["site"]);
				string strUid = Request.QueryString["uid"];
				 
				var cook = new HttpCookie("CPS_IN");
				cook.Values.Add("SiteID", intSiteID.ToString());
				cook.Values.Add("SiteUser", Server.UrlEncode(strUid));
			    cook.Expires = DateTime.Now.AddDays(30);
				if (!string.IsNullOrEmpty(Request.QueryString["ex"]))
				{
					if (Request.QueryString["ex"].Length > 200)
					{
						cook.Values.Add("SiteEx", Server.UrlEncode(Request.QueryString["ex"].Substring(0, 200).ToString()));
					}
					else
					{
						cook.Values.Add("SiteEx", Server.UrlEncode(Request.QueryString["ex"]));
					}
				}
				Response.AppendCookie(cook);

				//记录链入日志
				try
				{
					var lr = new Cps_LinkRecord();
					lr.CpsID = intSiteID;
					lr.URL = this.Request.Url.ToString();
					if (string.IsNullOrEmpty(strJump))
						strJump = "";
					lr.TargetURL = strJump;
					new CpsLinkRecordService().Add(lr);
				}
				catch (Exception exception)
				{
					LogUtils.Log(
						"[CPS]写入CPS调用日志发生错误。错误信息：" + exception.Message + "/" + exception.InnerException,
						"[CPS]",
						Category.Error,
						this.Session.SessionID,
						0,
						"CPS");
				}
				#endregion
			}

		    if (!string.IsNullOrWhiteSpace(strJump))
		    {
			    this.Response.Redirect(strJump);
		    }
		    else
			{
				this.Response.Redirect(Utils.GetWebSiteUrl);
		    }
	    }
    }
}
