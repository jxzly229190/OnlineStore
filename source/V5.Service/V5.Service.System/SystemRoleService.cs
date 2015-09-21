// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemRoleService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统角色服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;
    using global::System.Data;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统角色服务类
    /// </summary>
    public class SystemRoleService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统角色数据访问对象
        /// </summary>
        private readonly ISystemRoleDA systemRoleDA;

        /// <summary>
        /// 系统角色权限数据访问对象
        /// </summary>
        private readonly ISystemRolePermissionDA systemRolePermissionDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRoleService"/> class.
        /// </summary>
        public SystemRoleService()
        {
            this.systemRoleDA = new DAFactorySystem().CreateSystemRoleDA();
            this.systemRolePermissionDA = new DAFactorySystem().CreateSystemRolePermissionDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加系统角色
        /// </summary>
        /// <param name="role">
        /// 角色对象
        /// </param>
        /// <param name="rolePermissions">
        /// 角色权限关系对象列表
        /// </param>
        /// <returns>
        /// 角色编号
        /// </returns>
        /// <exception cref="Exception">
        /// 执行添加系统角色操作异常
        /// </exception>
        public int AddRole(System_Role role, List<System_Role_Permission> rolePermissions)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (rolePermissions == null)
            {
                throw new ArgumentNullException("rolePermissions");
            }

            if (rolePermissions.Count <= 0)
            {
                return -1;
            }

            try
            {
                SqlTransaction transaction;
                var roleID = this.systemRoleDA.Insert(role, out transaction);

                foreach (var systemRolePermission in rolePermissions)
                {
                    systemRolePermission.RoleID = roleID;
                    this.systemRolePermissionDA.Insert(systemRolePermission, transaction);
                }

                transaction.Commit();
                return roleID;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 删除指定编号的系统角色
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <exception cref="Exception">
        /// 执行移除系统角色操作异常
        /// </exception>
        public void RemoveRoleByID(int roleID)
        {
            if (roleID <= 0)
            {
                return;
            }

            try
            {
                SqlTransaction transaction;
                this.systemRolePermissionDA.DeleteByID(roleID, out transaction);
                this.systemRoleDA.DeleteByID(roleID, transaction);
                transaction.Commit();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询系统角色列表
        /// </summary>
        /// <param name="paging">
        /// 分数数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 系统角色列表
        /// </returns>
        public List<System_Role> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemRoleDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询指定系统角色编号的角色权限关系列表
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <returns>
        /// 角色权限关系列表
        /// </returns>
        public List<System_Role_Permission> QueryByRoleID(int roleID)
        {
            return this.systemRolePermissionDA.SelectByRoleID(roleID);
        }

        /// <summary>
        /// 查询所有系统权限
        /// </summary>
        /// <returns>
        /// 系统权限列表
        /// </returns>
        public List<System_Role> QueryAll()
        {
            return this.systemRoleDA.SelectAll();
        }

        /// <summary>
        /// 查询所有系统权限
        /// </summary>
        /// <returns>
        /// 系统权限列表
        /// </returns>
        public List<System_Role_User> QueryAllWithUser()
        {
            return this.systemRoleDA.SelectAllWithUser();
        }

        #endregion 
    }
}