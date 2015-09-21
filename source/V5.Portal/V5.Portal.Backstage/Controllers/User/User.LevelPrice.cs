// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.LevelPrice.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员控制器部分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Controllers.User
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Text;
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.User;
    using V5.Service.User;

    /// <summary>
    /// 会员控制器部分类
    /// </summary>
    public partial class UserController
    {
        #region Constants and Fields

        /// <summary>
        /// 会员等级价格服务对象.
        /// </summary>
        private UserLevelPriceService userLevelPriceService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 会员等级价格执行方法
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public PartialViewResult LevelPrice()
        {
            return this.PartialView("LevelPrice");
        }

        /// <summary>
        /// 添加会员等级价格.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelPriceModel">
        /// UserLevelPriceModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public JsonResult AddUserLevelPrice([DataSourceRequest] DataSourceRequest request, UserLevelPriceModel userLevelPriceModel)
        {
            try
            {
                if (userLevelPriceModel != null)
                {
                    userLevelPriceModel.EmployeeID = this.SystemUserSession.SystemUserID;
                    this.userLevelPriceService = new UserLevelPriceService();

                    var userLevelPrice = DataTransfer.Transfer<User_Level_Price>(
                        userLevelPriceModel,
                        typeof(UserLevelPriceModel));

                    userLevelPriceModel.ID = this.userLevelPriceService.Add(userLevelPrice);

                    userLevelPrice = this.userLevelPriceService.QueryByID(userLevelPriceModel.ID);
                    userLevelPriceModel = DataTransfer.Transfer<UserLevelPriceModel>(
                        userLevelPrice,
                        typeof(User_Level_Price));
                    userLevelPriceModel.StatusName = userLevelPriceModel.Status == 0 ? "正常" : "停止";

                    return this.Json(new[] { userLevelPriceModel }.ToDataSourceResult(request, this.ModelState));
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 删除指定编号的会员等级价格.
        /// </summary>
        /// <param name="id">
        /// 会员等级价格编号.
        /// </param>
        [HttpPost]
        public void RemoveUserLevelPrice(int id)
        {
            try
            {
                this.userLevelPriceService = new UserLevelPriceService();
                this.userLevelPriceService.RemoveByID(id);
                Response.Write("成功删除！");
            }
            catch (Exception exception)
            {
                Response.Write("删除失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询会员等级价格列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelID">
        /// 会员等级编号.
        /// </param>
        /// <param name="productName">
        /// 商品名称.
        /// </param>
        /// <param name="employeeName">
        /// 员工名称.
        /// </param>
        /// <param name="status">
        /// 会员等级价格启用状态.
        /// </param>
        /// <param name="startTime">
        /// 创建时间开始时间.
        /// </param>
        /// <param name="endTime">
        /// 创建时间结束时间.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryUserLevelPrice(
            [DataSourceRequest] DataSourceRequest request,
            string userLevelID,
            string productName,
            string employeeName,
            string status,
            string startTime,
            string endTime)
        {
            this.userLevelPriceService = new UserLevelPriceService();

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            var stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(userLevelID))
            {
                stringBuilder.Append(" [UserLevelID] = " + int.Parse(userLevelID));
            }

            if (!string.IsNullOrEmpty(productName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[ProductName] like '%" + productName + "%'");
            }

            if (!string.IsNullOrEmpty(employeeName))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[EmployeeName] like '%" + employeeName + "%'");
            }

            switch (status)
            {
                case "1":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[status] = " + status);
                    break;
                case "2":
                    CheckCondition(stringBuilder);
                    stringBuilder.Append("[status] = 0");
                    break;
            }

            if (!string.IsNullOrEmpty(startTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[CreateTime] >= '" + startTime + "'");
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                CheckCondition(stringBuilder);
                stringBuilder.Append("[CreateTime] <= '" + endTime + "'");
            }

            var condition = stringBuilder.ToString();
            try
            {
                var paging = new Paging(
                    "[view_User_Level_Price_SelectAll]", //// Todo:
                    null,
                    "ID",
                    condition,
                    request.Page,
                    request.PageSize);

                int pageCount;
                int totalCount;
                var list = this.userLevelPriceService.Query(paging, out pageCount, out totalCount);
                if (list == null)
                {
                    return this.Json(null);
                }

                var modelList = new List<UserLevelPriceModel>();
                foreach (var userLevelPrice in list)
                {
                    var model = DataTransfer.Transfer<UserLevelPriceModel>(userLevelPrice, typeof(User_Level_Price));
                    model.StatusName = model.Status == 0 ? "正常" : "停止";
                    modelList.Add(model);
                }

                return this.Json(modelList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改会员等级价格列表.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelPriceModel">
        /// userLevelPriceModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult ModifyUserLevelPrice([DataSourceRequest] DataSourceRequest request, UserLevelPriceModel userLevelPriceModel)
        {
            try
            {
                if (userLevelPriceModel != null)
                {
                    this.userLevelPriceService = new UserLevelPriceService();

                    var userLevelPrice = DataTransfer.Transfer<User_Level_Price>(
                        userLevelPriceModel,
                        typeof(UserLevelPriceModel));

                    this.userLevelPriceService.Modify(userLevelPrice);
                    userLevelPriceModel.StatusName = userLevelPriceModel.Status == 0 ? "正常" : "停止";

                    return this.Json(new[] { userLevelPriceModel }.ToDataSourceResult(request, this.ModelState));
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询会员列表.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult QueryStatusSelectListItems()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem { Text = "正常", Value = "0" },
                    new SelectListItem { Text = "停止", Value = "1" }
                };

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
