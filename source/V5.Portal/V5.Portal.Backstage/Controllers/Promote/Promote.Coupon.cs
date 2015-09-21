// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Promote.Coupon.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   促销管理控制类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.Promote
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Security.Cryptography;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Promote;
    using V5.Service.Product;
    using V5.Service.Promote;
    using V5.Service.User;

    /// <summary>
    /// 促销管理控制类.
    /// </summary>
    public partial class PromoteController
    {
        #region Constants and Fields

        /// <summary>
        /// 现金券管理服务.
        /// </summary>
        private CouponCashService couponCashService;

        /// <summary>
        /// 现金券绑定服务.
        /// </summary>
        private CouponCashBindingService couponCashBindingService;

        /// <summary>
        /// 满减券管理服务.
        /// </summary>
        private CouponDecreaseService couponDecreaseService;

        /// <summary>
        /// 满减券绑定服务
        /// </summary>
        private CouponDecreaseBindingService couponDecreaseBindingService;

        /// <summary>
        /// 电子券使用范围服务
        /// </summary>
        private CouponScopeService couponScopeService;

        /// <summary>
        /// 会员服务
        /// </summary>
        private UserService userService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 电子券管理局部试图
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult Coupon()
        {
            return this.PartialView("Coupon");
        }

        /// <summary>
        /// The decrease list.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        public PartialViewResult DecreaseList()
        {
            return this.PartialView("DecreaseList");
        }

        /// <summary>
        /// 现金券. 
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cashName">
        /// The cash Name.
        /// </param>
        /// <param name="cashStatus">
        /// The cash Status.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryCouponCash([DataSourceRequest] DataSourceRequest request, string cashName, string cashStatus)
        {
            this.couponCashService = new CouponCashService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            int totalCount;
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(cashName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[Name] like '%" + cashName + "%'");
            }

            switch (cashStatus)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[EndTime] > '" + DateTime.Now + "'");
                    break;
                // case "2":
                //    CheckCondition(stringBuilder);
                //    stringBuilder.Append("[Status] = 0");
                //    break;
                case "3":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[EndTime] < '" + DateTime.Now + "'");
                    break;
            }

            var condition = stringBuilder.ToString();
            List<Coupon_Cash> list;
            try
            {
                var paging = new Paging("[view_Coupon_Cash_SelectAllInfo]", null, "[ID]", condition, request.Page, request.PageSize);
                int pageCount;
                list = this.couponCashService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            
            if (list != null)
            {
                var modelList = new List<CouponCashModel>();
                foreach (var coupon in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<CouponCashModel>(coupon, typeof(Coupon_Cash)));
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary>
        /// 满减券. 
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="DecreaseName">
        /// The Decrease Name.
        /// </param>
        /// <param name="DecreaseStatus">
        /// The Decrease Status.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryCouponDecrease([DataSourceRequest] DataSourceRequest request, string DecreaseName, string DecreaseStatus)
        {
            this.couponDecreaseService = new CouponDecreaseService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            int totalCount;
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(DecreaseName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[Name] like '%" + DecreaseName + "%'");
            }

            switch (DecreaseStatus)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[EndTime] < '" + DateTime.Now + "'");
                    break;
                // case "2":
                //    CheckCondition(stringBuilder);
                //    stringBuilder.Append("[Status] = 0");
                //    break;
                case "3":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[EndTime] < '" + DateTime.Now + "'");
                    break;
            }

            var condition = stringBuilder.ToString();
            List<Coupon_Decrease> list;
            try
            {
                var paging = new Paging("[view_Coupon_Decrease_SelectAllInfo]", null, "[ID]", condition, request.Page, request.PageSize);
                int pageCount;
                list = this.couponDecreaseService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            
            if (list != null)
            {
                var modelList = new List<CouponDecreaseModel>();
                foreach (var coupon in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<CouponDecreaseModel>(coupon, typeof(Coupon_Decrease)));
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary> 添加现金券. </summary>
        /// <param name="objtype">
        /// 适用范围对象类型（全场、类型、类别、品牌、商品）.
        /// </param>
        /// <param name="objdata">
        /// 使用范围数据.
        /// </param>
        /// <param name="couponName">
        /// 优惠券名称.
        /// </param>
        /// <param name="quantity">
        /// 优惠券初始数量.
        /// </param>
        /// <param name="faceValue">
        /// 优惠券面额.
        /// </param>
        /// <param name="startTime">
        /// 优惠券的生效日期.
        /// </param>
        /// <param name="endTime">
        /// 优惠券的作废日期.
        /// </param>
        /// <param name="remarks">
        /// 活动备注.
        /// </param>
        [HttpPost]
        public void AddCouponCash(
            string objtype,
            string objdata,
            string couponName,
            string quantity,
            string faceValue,
            string startTime,
            string endTime,
            string remarks)
        {
            if (string.IsNullOrEmpty(objtype) || string.IsNullOrEmpty(objdata) || string.IsNullOrEmpty(couponName)
                || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(faceValue) || string.IsNullOrEmpty(startTime)
                || string.IsNullOrEmpty(endTime))
            {
                Response.Write("添加失败！");
                return;
            }

            try
            {
                this.couponCashService = new CouponCashService();
                var isNameEcists = this.couponCashService.IsNameExists(couponName);
                if (isNameEcists != 0)
                {
                    Response.Write("已存在此名称的券！");
                    return;
                }

                var couponCashModel = new CouponCashModel
                                          {
                                              EmployeeID = this.SystemUserSession.EmployeeID,
                                              Name = couponName,
                                              InitialNumber = int.Parse(quantity),
                                              FaceValue = double.Parse(faceValue),
                                              StartTime = DateTime.Parse(startTime),
                                              EndTime = DateTime.Parse(endTime),
                                              Description = remarks,
                                              CreateTime = DateTime.Now
                                          };

                var couponCash = DataTransfer.Transfer<Coupon_Cash>(couponCashModel, typeof(CouponCashModel));
                couponCashModel.ID = this.couponCashService.Add(couponCash);
                objdata = objdata.Substring(0, objdata.Length - 1);
                var result = objdata.Split(',');
                foreach (var targetType in result)
                {
                    var couponScopeModel = new CouponScopeModel
                                               {
                                                   CouponID = couponCashModel.ID,
                                                   CouponTypeID = 0,
                                                   ScopeType = int.Parse(objtype),
                                                   TargetTypeID = int.Parse(targetType),
                                                   CreateTime = DateTime.Now
                                               };
                    this.couponScopeService = new CouponScopeService();
                    var couponScope = DataTransfer.Transfer<Coupon_Scope>(couponScopeModel, typeof(CouponScopeModel));
                    couponScopeModel.ID = this.couponScopeService.Add(couponScope);
                }
            }
            catch (Exception exception)
            {
                Response.Write("添加失败！");
                throw new Exception(exception.Message, exception);
            }

            Response.Write("添加成功！");
        }

        /// <summary> 添加满减券. </summary>
        /// <param name="objtype">
        /// 适用范围对象类型（全场、类型、类别、品牌、商品）.
        /// </param>
        /// <param name="objdata">
        /// 使用范围数据.
        /// </param>
        /// <param name="couponName">
        /// 优惠券名称.
        /// </param>
        /// <param name="quantity">
        /// 优惠券初始数量.
        /// </param>
        /// <param name="faceValue">
        /// 优惠券面额.
        /// </param>
        /// <param name="meetMoney">
        /// 满足的消费金额.
        /// </param>
        /// <param name="startTime">
        /// 优惠券的生效日期.
        /// </param>
        /// <param name="endTime">
        /// 优惠券的作废日期.
        /// </param>
        /// <param name="remarks">
        /// 活动备注.
        /// </param>
        [HttpPost]
        public void AddCouponDecrease(
            string objtype,
            string objdata,
            string couponName,
            string quantity,
            string faceValue,
            string meetMoney,
            string startTime,
            string endTime,
            string remarks)
        {
            if (string.IsNullOrEmpty(objtype) || string.IsNullOrEmpty(objdata) || string.IsNullOrEmpty(couponName)
                || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(faceValue) || string.IsNullOrEmpty(meetMoney)
                || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime))
            {
                Response.Write("添加失败！");
                return;
            }

            try
            {
                this.couponDecreaseService = new CouponDecreaseService();
                var isNameEcists = this.couponDecreaseService.IsNameExists(couponName);
                if (isNameEcists != 0)
                {
                    Response.Write("已存在此名称的券！");
                    return;
                }

                var couponDecreaseModel = new CouponDecreaseModel
                                              {
                                                  EmployeeID = this.SystemUserSession.EmployeeID,
                                                  Name = couponName,
                                                  InitialNumber = int.Parse(quantity),
                                                  FaceValue = double.Parse(faceValue),
                                                  MeetAmount = double.Parse(meetMoney),
                                                  StartTime = DateTime.Parse(startTime),
                                                  EndTime = DateTime.Parse(endTime),
                                                  Description = remarks,
                                                  CreateTime = DateTime.Now
                                              };

                var couponDecrease = DataTransfer.Transfer<Coupon_Decrease>(
                    couponDecreaseModel,
                    typeof(CouponDecreaseModel));
                couponDecreaseModel.ID = this.couponDecreaseService.Add(couponDecrease);
                objdata = objdata.Substring(0, objdata.Length - 1);
                var result = objdata.Split(',');
                foreach (var targetType in result)
                {
                    var couponScopeModel = new CouponScopeModel
                                               {
                                                   CouponID = couponDecreaseModel.ID,
                                                   CouponTypeID = 1,
                                                   ScopeType = int.Parse(objtype),
                                                   TargetTypeID = int.Parse(targetType),
                                                   CreateTime = DateTime.Now
                                               };
                    this.couponScopeService = new CouponScopeService();
                    var couponScope = DataTransfer.Transfer<Coupon_Scope>(couponScopeModel, typeof(CouponScopeModel));
                    couponScopeModel.ID = this.couponScopeService.Add(couponScope);
                }
            }
            catch (Exception exception)
            {
                Response.Write("添加失败！");
                throw new Exception(exception.Message, exception);
            }

            Response.Write("添加成功！");
        }

        /// <summary> 现金券绑定. </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="couponCashID">
        /// 现金券编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryCouponCashBinding([DataSourceRequest] DataSourceRequest request, int couponCashID)
        {
            this.couponCashBindingService = new CouponCashBindingService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            string condition = "[CouponCashID]=" + couponCashID;

            int totalCount;

            List<Coupon_Cash_Binding> list;
            try
            {
                int pageCount;
                var paging = new Paging("[Coupon_Cash_Binding]", null, "[ID]", condition, request.Page, request.PageSize);
                list = this.couponCashBindingService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            
            if (list != null)
            {
                var modelList = new List<CouponCashBindingModel>();
                foreach (var counpon in list)
                {
                    modelList.Add(DataTransfer.Transfer<CouponCashBindingModel>(counpon, typeof(Coupon_Cash_Binding)));
                }

                foreach (var cashBindingModel in modelList)
                {
                    switch (cashBindingModel.Status)
                    {
                        case 0:
                            cashBindingModel.StatusName = "已绑定";
                            break;
                        case 1:
                            cashBindingModel.StatusName = "已使用";
                            break;
                        case 2:
                            cashBindingModel.StatusName = "已过期";
                            break;
                    }
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary> 满减券绑定. </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="couponDecreaseID">
        /// 满减券编号.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryCouponDecreaseBinding(
            [DataSourceRequest] DataSourceRequest request,
            int couponDecreaseID)
        {
            this.couponDecreaseBindingService = new CouponDecreaseBindingService();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }

            string condition = string.Format("[CouponDecreaseID] = {0}", couponDecreaseID);

            int totalCount;
            List<Coupon_Decrease_Binding> list;
            try
            {
                int pageCount;
                var paging = new Paging(
                    "[Coupon_Decrease_Binding]",
                    null,
                    "[ID]",
                    condition,
                    request.Page,
                    request.PageSize);
                list = this.couponDecreaseBindingService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var modelList = new List<CouponDecreaseBindingModel>();
                foreach (var counpon in list)
                {
                    modelList.Add(
                        DataTransfer.Transfer<CouponDecreaseBindingModel>(counpon, typeof(Coupon_Decrease_Binding)));
                }

                foreach (var decreaseBindingModel in modelList)
                {
                    switch (decreaseBindingModel.Status)
                    {
                        case 0:
                            decreaseBindingModel.StatusName = "已绑定";
                            break;
                        case 1:
                            decreaseBindingModel.StatusName = "已使用";
                            break;
                        case 2:
                            decreaseBindingModel.StatusName = "已过期";
                            break;
                    }
                }

                var data = new DataSource { Data = modelList, Total = totalCount };
                return this.Json(data, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty);
        }

        /// <summary> 赠券 </summary>
        /// <param name="objType"> 赠券范围对象（0：所有会员，1：等级会员，2：制定会员）</param>
        /// <param name="objData"> 赠送范围对象编号 </param>
        /// <param name="couponTpye"> 赠送券的类型（0：现金券，1：满减券）</param>
        /// <param name="couponID">赠送券的编号</param>
        /// <param name="couponCount">赠券的数量</param>
        /// <param name="cause">赠券的原因</param>
        public void GiveCoupon(
            string objType,
            string objData,
            string couponTpye,
            string couponID,
            string couponCount,
            string cause)
        {
            if (string.IsNullOrEmpty(objType) || string.IsNullOrEmpty(objData) || string.IsNullOrEmpty(couponTpye)
                || string.IsNullOrEmpty(couponID) || string.IsNullOrEmpty(couponCount) || string.IsNullOrEmpty(cause))
            {
                Response.Write("添加失败！");
                return;
            }

            try
            {
                var restCount = this.GetRest(couponID, couponTpye); // 获取电子券可用数量
                if (objType == "0")
                {
                    this.userService = new UserService();
                    var list = this.userService.QueryAll();
                    if (restCount < (list.Count * int.Parse(couponCount)))
                    {
                        Response.Write("优惠券的数量不足！");
                        return;
                    }

                    foreach (var user in list)
                    {
                        this.ChoiceCouponType(couponTpye, user.ID, couponID, couponCount, cause);
                    }
                }
                else if (objType == "1")
                {
                    objData = objData.Substring(0, objData.Length - 1);
                    var result = objData.Split(',');

                    var userList = new ArrayList(); // 会员编号的动态数组
                    foreach (var s in result)
                    {
                        this.userService = new UserService();
                        var list = this.userService.QueryUserByUserLevelID(int.Parse(s));
                        foreach (var user in list)
                        {
                            userList.Add(user.ID);
                        } // 遍历当前等级下的所有会员
                    } // 遍历所有等级

                    if (restCount < userList.Count)
                    {
                        Response.Write("优惠券的数量不足！");
                        return;
                    }

                    foreach (int userID in userList)
                    {
                        this.ChoiceCouponType(couponTpye, userID, couponID, couponCount, cause);
                    }  
                }
                else if (objType == "2")
                {
                    objData = objData.Substring(0, objData.Length - 1);
                    var result = objData.Split(',');
                    if (restCount < (result.Length * int.Parse(couponCount)))
                    {
                        Response.Write("优惠券的数量不足！");
                        return;
                    }

                    foreach (var user in result)
                    {
                        this.ChoiceCouponType(couponTpye, int.Parse(user), couponID, couponCount, cause);
                    }
                }

                Response.Write("添加成功！");
            }
            catch (Exception exception)
            {
                Response.Write("添加失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary> 获取电子券可用数量 </summary>
        /// <param name="couponID">
        /// 电子券编号
        /// </param>
        /// <param name="couponTypeID">
        /// 电子券类型编号（0：现金券，1：满减券）
        /// </param>
        /// <returns>
        /// 可用数量
        /// </returns>
        public int GetRest(string couponID, string couponTypeID)
        {
            int restCount = 0;
            switch (couponTypeID)
            {
                case "0":
                    this.couponCashService = new CouponCashService();
                    var couponCashlist = this.couponCashService.QueryCouponCashByID(int.Parse(couponID));
                    restCount = couponCashlist.Remain;
                    break;
                case "1":
                    this.couponDecreaseService = new CouponDecreaseService();
                    var couponDecreaselist = this.couponDecreaseService.QueryCouponDecreaseByID(int.Parse(couponID));
                    restCount = couponDecreaselist.Remain;
                    break;
            }

            return restCount;
        }

        /// <summary> 选择赠券类型. </summary>
        /// <param name="couponTpye">
        /// 赠送券的类型（0：现金券，1：满减券）
        /// </param>
        /// <param name="userID">
        /// 会员编号.
        /// </param>
        /// <param name="couponID">
        /// 赠送券的编号
        /// </param>
        /// <param name="couponCount">
        /// 赠券的数量
        /// </param>
        /// <param name="cause">
        /// 赠券的原因
        /// </param>
        public void ChoiceCouponType(string couponTpye, int userID, string couponID, string couponCount, string cause)
        {
            switch (couponTpye)
            {
                case "0":
                    var couponCashBindingModel = new CouponCashBindingModel
                                                     {
                                                         CouponCashID = int.Parse(couponID),
                                                         UserID = userID,
                                                         Cause = cause
                                                     };
                    for (int i = 0; i < int.Parse(couponCount); i++)
                    {
                        this.AddCouponCashBinding(couponCashBindingModel);
                    }

                    break;
                case "1":
                    var couponDecreaseBindingModel = new CouponDecreaseBindingModel
                                                     {
                                                         CouponDecreaseID = int.Parse(couponID),
                                                         UserID = userID,
                                                         Cause = cause
                                                     };
                    for (int i = 0; i < int.Parse(couponCount); i++)
                    {
                        this.AddCouponDecreaseBinding(couponDecreaseBindingModel);
                    }

                    break;
            }
        }

        /// <summary> 赠现金券. </summary>
        /// <param name="couponCashBindingModel">
        /// The coupon cash binding model.
        /// </param>
        public void AddCouponCashBinding(CouponCashBindingModel couponCashBindingModel)
        {
            try
            {
                if (couponCashBindingModel != null)
                {
					this.couponCashBindingService = new CouponCashBindingService();

                    couponCashBindingModel.Number = "M" + this.couponCashBindingService.CreateRandomCode(8);
                    couponCashBindingModel.Password = this.couponCashBindingService.CreateRandomCode(6);
                    couponCashBindingModel.BindingTime = DateTime.Now;
                    couponCashBindingModel.UseTime = null;

                    var couponCashBing = DataTransfer.Transfer<Coupon_Cash_Binding>(
                        couponCashBindingModel,
                        typeof(CouponCashBindingModel));

                    couponCashBindingModel.ID = this.couponCashBindingService.Add(couponCashBing);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("赠现金券时发生错误", exception);
            }
        }

        /// <summary> 赠送满减券. </summary>
        /// <param name="couponDecreaseBindingModel">
        /// The coupon Decrease Binding Model.
        /// </param>
        public void AddCouponDecreaseBinding(CouponDecreaseBindingModel couponDecreaseBindingModel)
        {
            try
            {
                if (couponDecreaseBindingModel != null)
				{
					this.couponDecreaseBindingService = new CouponDecreaseBindingService();

					couponDecreaseBindingModel.Number = "L" + couponDecreaseBindingService.CreateRandomCode(8);
					couponDecreaseBindingModel.Password = couponDecreaseBindingService.CreateRandomCode(6);
                    couponDecreaseBindingModel.BindingTime = DateTime.Now;
                    couponDecreaseBindingModel.UseTime = null;


                    var couponDecreaseBinding = DataTransfer.Transfer<Coupon_Decrease_Binding>(
                        couponDecreaseBindingModel,
                        typeof(CouponDecreaseBindingModel));

                    couponDecreaseBindingModel.ID = this.couponDecreaseBindingService.Add(couponDecreaseBinding);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("赠满减券时发生错误", exception);
            }
        }

        /// <summary> 现金券详情. </summary>
        /// <param name="couponCashID">
        /// The coupon cach id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult CouponCashDetail(int couponCashID)
        {
            try
            {
                this.couponCashService = new CouponCashService();
                var couponCash = this.couponCashService.QueryCouponCashByID(couponCashID);
                var couponCashInfo = DataTransfer.Transfer<CouponCashModel>(couponCash, typeof(Coupon_Cash));
                this.couponScopeService = new CouponScopeService();
                var scopeList = this.couponScopeService.QueryByCouponID(couponCashInfo.ID, 0);
                var sb = new StringBuilder();
                foreach (var scopeName in scopeList)
                {
                    sb.Append(scopeName.TargetTypeName + "、");
                }

                couponCashInfo.UseObject = sb.ToString();
                return this.PartialView("CouponCashDetail", couponCashInfo);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary> 满减券详情 </summary>
        /// <param name="couponDecreaseID">满减券的编号</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public PartialViewResult CouponDecreaseDetail(int couponDecreaseID)
        {
            try
            {
                this.couponDecreaseService = new CouponDecreaseService();
                var couponDecrease = this.couponDecreaseService.QueryCouponDecreaseByID(couponDecreaseID);
                var couponDecreaseInfo = DataTransfer.Transfer<CouponDecreaseModel>(
                    couponDecrease,
                    typeof(Coupon_Decrease));

                this.couponScopeService = new CouponScopeService();
                var scopeList = this.couponScopeService.QueryByCouponID(couponDecreaseInfo.ID, 1);
                var sb = new StringBuilder();
                foreach (var scopeName in scopeList)
                {
                    sb.Append(scopeName.TargetTypeName + "、");
                }

                couponDecreaseInfo.UseObject = sb.ToString();
                return this.PartialView("CouponDecreaseDetail", couponDecreaseInfo);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 追加现金券初始数量.
        /// </summary>
        /// <param name="couponCashID">
        /// 现金券编号.
        /// </param>
        /// <param name="initialNumber">
        /// 追加的数量.
        /// </param>
        public void AddCouponCashInitialNumber(int couponCashID, int initialNumber)
        {
            try
            {
                this.couponCashService = new CouponCashService();
                this.couponCashService.Modify(couponCashID, initialNumber);
                Response.Write("添加成功！");
            }
            catch (Exception exception)
            {
                Response.Write("添加失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 追加满减券初始数量.
        /// </summary>
        /// <param name="couponDecreaseID">
        /// 满减券编号.
        /// </param>
        /// <param name="initialNumber">
        /// 追加的数量.
        /// </param>
        public void AddCouponDecreaseInitialNumber(int couponDecreaseID, int initialNumber)
        {
            try
            {
                this.couponDecreaseService = new CouponDecreaseService();
                this.couponDecreaseService.ModifyInitialNumber(couponDecreaseID, initialNumber);
                Response.Write("添加成功！");
            }
            catch (Exception exception)
            {
                Response.Write("添加失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary> 商品类型列表. </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult QuerySelectTypeListItems()
        {
            var productCategoryService = new ProductCategoryService();

            List<Product_Category> list;
            try
            {
                var paging = new Paging("[Product_Category]", null, "ID", "ParentID = 0 and layer = 1 ", 1, 10);
                int pageCount;
                int totalCount;
                list = productCategoryService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var items = new List<SelectListItem>();
                foreach (var category in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value = category.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = category.CategoryName,
                                             };
                    items.Add(selectListItem);
                }

                return PartialView("EditorTemplates/QuerySelectTypeListItems", items);
            }

            return null;
        }

        /// <summary> 商品类别列表.
        /// </summary>
        /// <param name="parentID">
        /// The parent ID.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public string QuerySelectCategoryListItems(string parentID)
        {
            if (string.IsNullOrEmpty(parentID))
            {
                return null;
            }

            var productCategoryService = new ProductCategoryService();

            var condition = string.Format("ParentID = {0}", parentID);
            List<Product_Category> list;
            try
            {
                var paging = new Paging("[Product_Category]", null, "ID", condition, 1, 100);
                int pageCount;
                int totalCount;
                list = productCategoryService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var sb = new StringBuilder();
                foreach (var category in list)
                {
                    sb.Append(
                        "<input type='checkbox' name='ck_category' value='" + category.ID + "'/>"
                        + category.CategoryName);
                    sb.Append("&nbsp;&nbsp;");
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 商品品牌列表.
        /// </summary>
        /// <param name="categoryID">
        /// categoryID.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [HttpPost]
        public string QuerySelectCategoryListItemsByParentID(string categoryID)
        {
            if (string.IsNullOrEmpty(categoryID))
            {
                return null;
            }

            var productBrandService = new ProductBrandService();

            List<Product_Brand> list;
            try
            {
                int totalCount;
                int pageCount;
                string condition = string.Format("ProductCategoryID = {0} and layer = 1 ", categoryID);

                var paging = new Paging("[Product_Brand]", null, "ID", condition, 1, 30);
                list = productBrandService.Query(paging, out pageCount, out totalCount);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            
            if (list != null)
            {
                var sb = new StringBuilder();
                foreach (var category in list)
                {
                    sb.Append(
                        "<input type='checkbox' name='ck_Brand' value='" + category.ID + "'/>" + category.BrandName);
                    sb.Append("&nbsp;&nbsp;");
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 会员等级列表.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult QuerySelectUserLevelListItems()
        {
            List<User_Level> list;
            try
            {
                var userLevelService = new UserLevelService();
                list = userLevelService.QueryAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var items = new List<SelectListItem>();
                foreach (var userlevel in list)
                {
                    var selectListItem = new SelectListItem
                                             {
                                                 Value =
                                                     userlevel.ID.ToString(CultureInfo.InvariantCulture),
                                                 Text = userlevel.Name,
                                             };
                    items.Add(selectListItem);
                }

                return this.PartialView("EditorTemplates/QuerySelectUserLevelListItems", items);
            }

            return null;
        }

        /// <summary>
        /// 现金券列表.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCounponCashListItems()
        {
            List<Coupon_Cash> list;
            try
            {
                this.couponCashService = new CouponCashService();
                list = this.couponCashService.QueryAllValid();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var selectItems = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "请选择" } };
                foreach (var couponCash in list)
                {
                    var selectItem = new SelectListItem
                                         {
                                             Value = couponCash.ID.ToString(CultureInfo.InvariantCulture),
                                             Text = couponCash.Name
                                         };
                    selectItems.Add(selectItem);
                }

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        /// <summary>
        /// 满减券列表.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCounponDecreaseListItems()
        {
            List<Coupon_Decrease> list;
            try
            {
                this.couponDecreaseService = new CouponDecreaseService();
                list = this.couponDecreaseService.QueryAllValidCouponDecreases();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            if (list != null)
            {
                var selectItems = new List<SelectListItem> { new SelectListItem { Value = "0", Text = "请选择" } };
                foreach (var couponDecrease in list)
                {
                    var selectItem = new SelectListItem
                                         {
                                             Value =
                                                 couponDecrease.ID.ToString(CultureInfo.InvariantCulture),
                                             Text = couponDecrease.Name
                                         };
                    selectItems.Add(selectItem);
                }

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        /// <summary>
        /// 电子券的类别.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryCounponTypeItems()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem { Text = "现金券", Value = "0" },
                    new SelectListItem { Text = "满减券", Value = "1" }
                };

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult QueryCoupon(int pageIndex, int pageSize, string sortField, string sortOrder, string Name)
        {
            int totalCount, pageCount;

            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                stringBuilder.Append(string.Format(" [Name] like '%{0}%' ", Name));
            }

            var condition = stringBuilder.ToString();
            var paging = new Paging("view_Coupon_Paging", null, "ID", condition, pageIndex, pageSize, "CreateTime", 1);
            var searchResult =  new CouponCashService().CouponPaging(paging, out pageCount, out totalCount);
            return this.Json(new { data = searchResult, total = totalCount });
        }

        #endregion
    }
}