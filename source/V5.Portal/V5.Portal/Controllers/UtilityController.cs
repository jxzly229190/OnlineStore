using System.Collections.Generic;
using System.Web.Mvc;

namespace V5.Portal.Controllers
{
    using System;
    using System.Web;

    using V5.DataContract.System;
	using V5.Library;
    using V5.Library.Logger;

    public class UtilityController : BaseController
	{
		//
		// GET: /Utility/
		#region
		public ActionResult Index()
		{
			return this.View();
		}

        /// <summary>
        /// 清除CPS 的Cookie信心
        /// </summary>
        public void ClearCookie(HttpContextBase context,string name)
        {
            //清除CPS订单
            var cpsCookie = context.Request.Cookies[name];
            if (cpsCookie != null)
            {
                cpsCookie.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Add(cpsCookie);
            }
        }

        public ActionResult GetProvinces()
		{
			var list = MongoDBHelper.List<Province>(p => p.ID > 0);
			return this.Json(new AjaxResponse(1, list), JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetCities(int provinceID)
		{
			IList<City> list = null;
			if (provinceID > 0)
			{
				list = MongoDBHelper.List<City>(c => c.ProvinceID == provinceID);
			}

			return this.Json(new AjaxResponse(list == null ? 0 : 1, list));
		}

		public ActionResult GetCounties(int cityID)
		{
			IList<County> list = null;
			if (cityID > 0)
			{
				list = MongoDBHelper.List<County>(c => c.CityID == cityID);
				foreach (var county in list)
				{
					if (county.CanLandPay)
					{
						county.Name = "*" + county.Name;
					}
				}
			}

			return this.Json(new AjaxResponse(list == null ? 0 : 1, list));
		}

		public ActionResult GetCountyById(int countyId)
		{
			IList<County> list = null;
			if (countyId > 0)
			{
				list = MongoDBHelper.List<County>(c => c.ID == countyId);
			}
			var data = list != null && list.Count > 0 ? list[0] : null;
			return this.Json(new AjaxResponse(data != null ? 1 : 0, data));
		}

        /// <summary>
        /// 检查是否支持区域
        /// </summary>
        /// <returns></returns>
	    public ActionResult CheckSupportRegion(int countyId)
        {
            try
            {
                var county = MongoDBHelper.GetModel<County>(c => c.ID == countyId);
                if (county == null)
                {
                    return null;
                }

                var city = MongoDBHelper.GetModel<City>(c => c.ID == county.CityID);

                if (city == null)
                {
                    return null;
                }

                if (Utils.UnsupportRegion.Contains("[" + city.ProvinceID + "]"))
                {
                    var province = MongoDBHelper.GetModel<Province>(p => p.ID == city.ProvinceID);
                    return this.Json(new AjaxResponse(0, "对不起，" + province.Name + "区域暂不支持。"));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                LogUtils.Log(
                    "[Order]检查是否支持下单地区出错,区县编号:" + countyId + "；错误详情:" + exception.Message + "/"
                    + exception.InnerException,
                    "前台订单处理");
                return null;
            }
        }

        /// <summary>
        /// 检查是否支持的省份
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public bool ValidSupportRegion(int provinceId)
        {
            if (Utils.UnsupportRegion.Contains("[" + provinceId + "]"))
            {
                return false;
            }

            return true;
        }

		#endregion

		/// <summary>
		/// 根据市ID获取省
		/// </summary>
		/// <param name="cityID"></param>
		/// <returns></returns>
		public ActionResult GetProvinceID(int cityID)
		{
			IList<City> list = null;
			if (cityID > 0)
			{
				list = MongoDBHelper.List<City>(c => c.ID == cityID);
			}

			return this.Json(new AjaxResponse(list == null ? 0 : 1, list));
		}

		/// <summary>
		/// 根据countryId获取市
		/// </summary>
		/// <param name="countryId"></param>
		/// <returns></returns>
		public ActionResult GetCityID(int countryId)
		{
			IList<County> list = null;
			if (countryId > 0)
			{
				list = MongoDBHelper.List<County>(c => c.ID == countryId);
			}

			return this.Json(new AjaxResponse(list == null ? 0 : 1, list));
		}

		public ActionResult GetProvince()
		{
			var list = MongoDBHelper.List<Province>(r => true);
			return this.Json(new AjaxResponse(1, list), JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetCity()
		{
			var list = MongoDBHelper.List<City>(r => true);
			return this.Json(new AjaxResponse(1, list), JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetCountry()
		{
			var list = MongoDBHelper.List<County>(r => true);
			return this.Json(new AjaxResponse(1, list), JsonRequestBehavior.AllowGet);
		}

		public ActionResult announcement()
		{
			return this.View();
		}
		
        #region 网站错误页面
		
        /// <summary>
		/// 网站错误页面
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public ActionResult Error(string error)
		{
            Response.StatusCode = 404;
			this.ViewData["Title"] = "WebSite 网站内部错误";
			this.ViewData["Description"] = error;            
			return this.View("Error");
		}
		
        /// <summary>
		/// 400错误页面
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public ActionResult HttpError400(string error)
		{
            Response.StatusCode = 400;
			this.ViewData["Title"] = "HTTP 400- 无法找到文件";
			this.ViewData["Description"] = error;
			return this.View("Error");
		}
		
        /// <summary>
		/// 404错误页面
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public ActionResult HttpError404(string error)
		{
            Response.StatusCode = 404;
			this.ViewData["Title"] = "HTTP 404- 无法找到文件";
			this.ViewData["Description"] = error;
			return this.View("Error");
		}
		
        /// <summary>
		/// 500错误页面
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public ActionResult HttpError500(string error)
		{
            Response.StatusCode = 500;
			this.ViewData["Title"] = "HTTP 500 - 内部服务器错误";
			this.ViewData["Description"] = error;
			return this.View("Error");
		}
		
        /// <summary>
		/// 默认错误页面
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		public ActionResult General(string error)
		{
            Response.StatusCode = 404;
			this.ViewData["Title"] = "HTTP 发生错误";
			this.ViewData["Description"] = error;
			return this.View("Error");
		}

		#endregion
	}
}
