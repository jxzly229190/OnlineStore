namespace V5.Portal.Backstage.Controllers.System
{
    using global::System.Web.Mvc;


    using V5.DataContract.System;
    using V5.Library;
    using V5.Service.System;

    public partial class SystemController
    {
        //
        // GET: /System.Rights/

        /// <summary>
        /// 系统资源对象
        /// </summary>
        private SystemRightsService systemRightsService;

        /// <summary>
        /// The query user right.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="System_Rights"/>.
        /// </returns>
        public string QueryUserRight(int userId)
        {
            this.systemRightsService = new SystemRightsService();
            return this.systemRightsService.QueryUserRight(userId);
        }

        /// <summary>
        /// The query role right.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="System_Rights"/>.
        /// </returns>
        public string QueryRoleRight(int roleId)
        {
            this.systemRightsService = new SystemRightsService();
            return this.systemRightsService.QueryRoleRight(roleId);
        }
        
        /// <summary>
        /// The modify rights.
        /// </summary>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>
        /// </returns>
        [AcceptVerbsAttribute("GET", "POST")]
        public int ModifyRights(int roleId, int userId, string permissions)
        {
            this.systemRightsService = new SystemRightsService();
            return this.systemRightsService.ModifyRights(roleId, userId, permissions);
        }

        /// <summary>
        /// 获取操作权限信息
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="requestMethod">
        /// 请求类型:Get或Post
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetRightInfo(string action, string controller, string requestMethod)
        {
            this.systemRightsService = new SystemRightsService();
            var key = this.systemRightsService.BuildResourceKey(controller, action, requestMethod);
            var permissions = this.SystemUserSession.Permissions;
            var response = new AjaxResponse(this.systemRightsService.ValidateRight(key, permissions) ? 0 : -403);
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The check right info.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="requestMethod">
        /// The request method.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CheckRightInfo(string action, string controller, string requestMethod,string sessionId)
        {
            this.systemRightsService = new SystemRightsService();
            var key = this.systemRightsService.BuildResourceKey(controller, action, requestMethod);
            var permissions = this.SystemUserSession.Permissions
                              ?? MongoDBHelper.GetSystemUserSession(sessionId).Permissions;
            return this.systemRightsService.ValidateRight(key, permissions);
        }
    }
}
