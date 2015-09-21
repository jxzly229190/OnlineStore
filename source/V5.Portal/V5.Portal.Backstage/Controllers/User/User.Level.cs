// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.Level.cs" company="www.gjw.com">
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
    using global::System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using V5.DataContract.User;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Portal.Backstage.Models.User;
    using V5.Service.User;

    /// <summary>
    /// 会员控制器部分类
    /// </summary>
    public partial class UserController
    {
        #region Constants and Fields

        /// <summary>
        /// 会员等级服务.
        /// </summary>
        private UserLevelService userlevelService;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 会员等级局部视图.
        /// </summary>
        /// <returns>
        /// The <see cref="PartialViewResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult Level()
        {
            return this.PartialView("Level");
        }

        /// <summary>
        /// 添加新的会员级别.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelModel">
        /// UserLevelModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddLevel([DataSourceRequest] DataSourceRequest request, UserLevelModel userLevelModel)
        {
            try
            {
                if (userLevelModel != null)
                {
                    this.userlevelService = new UserLevelService();
                    var userLevel = DataTransfer.Transfer<User_Level>(userLevelModel, typeof(UserLevelModel));
                    userLevelModel.ID = this.userlevelService.AddUserLevel(userLevel);
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功添加会员等级", "AddLevel", Category.Info, Session.SessionID);
                    return this.Json(new[] { userLevelModel }.ToDataSourceResult(request, this.ModelState));
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 删除指定的会员等级.
        /// </summary>
        /// <param name="id">
        /// 会员等级编号.
        /// </param>
        [HttpPost]
        public void RemoveLevel(int id)
        {
            try
            {
                this.userService = new UserService();
                var list = this.userService.QueryUserByUserLevelID(id);
                if (list != null)
                {
                    Response.Write("请确认没有会员在此等级内！");
                    return;
                }

                this.userlevelService = new UserLevelService();
                this.userlevelService.RemoveByID(id);
                LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功删除会员等级", "RemoveLevel", Category.Info, Session.SessionID);
                Response.Write("成功删除！");
            }
            catch (Exception exception)
            {
                Response.Write("删除失败！");
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 修改会员等级信息.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="userLevelModel">
        /// UserLevelModel的对象实例.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ModifyLevel([DataSourceRequest] DataSourceRequest request, UserLevelModel userLevelModel)
        {
            try
            {
                if (userLevelModel != null)
                {
                    this.userlevelService = new UserLevelService();
                    var userLevel = DataTransfer.Transfer<User_Level>(userLevelModel, typeof(UserLevelModel));
                    this.userlevelService.ModifyLevel(userLevel);
                    LogUtils.Log("用户" + this.SystemUserSession.LoginName + "成功修改会员等级", "ModifyLevel", Category.Info, Session.SessionID);
                    return this.Json(new[] { userLevelModel }.ToDataSourceResult(request, this.ModelState));
                }

                return this.Json(string.Empty);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 会员等级列表数据源.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public JsonResult QueryLevel([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                this.userlevelService = new UserLevelService();
                var list = this.userlevelService.QueryAll();
                if (list != null)
                {
                    var modelList = new List<UserLevelModel>();
                    foreach (var user in list)
                    {
                        modelList.Add(DataTransfer.Transfer<UserLevelModel>(user, typeof(User_Level)));
                    }

                    return this.Json(modelList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
        public JsonResult QueryLevelSelectListItems()
        {
            try
            {
                this.userlevelService = new UserLevelService();
                var list = this.userlevelService.QueryAll();
                return this.Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        #endregion
    }
}
