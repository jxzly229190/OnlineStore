using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V5.Service.Utility;
using V5.DataContract.Utility;

namespace V5.Portal.Backstage.Controllers.Utility
{
    public partial class UtilityController
    {
        //
        // GET: /Utility.Code/

        private SystemVisitorService systemVisitorService;
        //
        // GET: /Utility/
        public PartialViewResult BussinessCode()
        {
            return PartialView();
        }
        /// <summary>
        /// 获取订单编号
        /// </summary>
        /// <param name="userCode">用户纺号，系统返回</param>
        /// <returns></returns>
        public ActionResult GetCode(string userCode)
        {
            DateTime create;
            string _orderCode = MadeCodeService.GetOrderCode(out create);
            return Json(new { createTime = create, orderCode = _orderCode });
        }
        /// <summary>
        /// 反推编号
        /// </summary>
        /// <param name="orderCode">订单号</param>
        /// <param name="createOrderTime">订单号生成时间</param>
        /// <returns></returns>
        public ActionResult ReverseOrderCode(string orderCode, DateTime createOrderTime)
        {
            string newOrder = MadeCodeService.ReverseOrderCode(orderCode, createOrderTime);
            return Json(new { newCode = newOrder });
        }
        /// <summary>
        /// 获取自定义编号
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public ActionResult GetCustomeCode(string usercode)
        {
            string resultCode = MadeCodeService.GetCodeByClientCode(usercode);
            return Json(new { customCode = resultCode });
        }
    }
}
