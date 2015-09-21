// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRightsService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System;
    using global::System.Linq;

    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Logger;
    using V5.Library.Storage.DB.NoSql;

    /// <summary>
    /// The system rights service.
    /// </summary>
    public class SystemRightsService
    {
        /// <summary>
        /// The query user right.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="System_Rights"/>.
        /// </returns>
        public string QueryUserRight(int userID)
        {
            return new SystemRightsDA().SelectByUserID(userID);
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
        public string QueryRoleRight(int roleID)
        {
            return new SystemRightsDA().SelectByRoleID(roleID);
        }

        /// <summary>
        /// The validate right.
        /// </summary>
        /// <param name="resourceKey">
        /// The resource key.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ValidateRight(string resourceKey, string permissions)
        {
            if (string.IsNullOrWhiteSpace(permissions) || string.IsNullOrWhiteSpace(resourceKey))
            {
                return false;
            }

            try
            {
                var resources = new MongoDbStore<System_Resources>("Resources");
                var list = resources.List(i => i.ID > 0);
                var resource = list.FirstOrDefault(item => item.Key == resourceKey);

                if (resource == null || resource.Code == null)
                {
                    return true;
                }

                return resource.Position <= permissions.Length && permissions.Substring(resource.Position, 1).Equals("1");
            }
            catch (Exception exception)
            {
                LogUtils.SystemTrace(
                    string.Format("判断权限出错了，ResourceKey = {0}，错误信息:{1}", resourceKey, exception.Message),
                    "ValidateRight",
                    Category.Fatal,
                    "",
                    10,
                    "判断权限");
                throw;
            }
        }

        /// <summary>
        /// 获取资源描述
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public string GetResourceDescriptionByKey(string resourceKey)
        {
            var resources = new MongoDbStore<System_Resources>("Resources");
            var resource = resources.Single(r => r.Key.ToLower() == resourceKey);
            if (resource == null) return resourceKey;
            if (resource.ParentCode == null || resource.ParentCode == "0") return resource.Description == null ? resourceKey : resource.Description;
            return GetResourceDescriptionByCode(resource.ParentCode, resource.Description);
        }

        /// <summary>
        /// 获取资源描述
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public string GetResourceDescriptionByCode(string resourceCode, string resourceDescription)
        {
            var resources = new MongoDbStore<System_Resources>("Resources");
            var resource = resources.Single(r => r.Code.ToLower() == resourceCode);
            if (resource == null) return resourceDescription;
            if (resource.ParentCode == null || resource.ParentCode == "0") return resource.Description == null ? resourceDescription : resource.Description + "->" + resourceDescription;
            return GetResourceDescriptionByCode(resource.ParentCode, resource.Description + "->" + resourceDescription);
        }

        /// <summary>
        /// The build resource key.
        /// </summary>
        /// <param name="controllerName">
        /// The controller name.
        /// </param>
        /// <param name="actionName">
        /// The action name.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BuildResourceKey(string controllerName, string actionName, string method)
        {
            return (controllerName + "." + actionName + "." + method).ToLower();
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
        public int ModifyRights(int roleId, int userId, string permissions)
        {
            SystemRightsDA rightsDa = new SystemRightsDA();
            System_Rights rights = new System_Rights();
            rights.RoleID = roleId;
            rights.UserID = userId;
            rights.UserRights = permissions;

            int result = 0;
            if (rightsDa.Exists(rights))
            {
                result = rightsDa.Update(rights);
            }
            else
            {
                result = rightsDa.Insert(rights);
            }
            return result;
        }
    }
}