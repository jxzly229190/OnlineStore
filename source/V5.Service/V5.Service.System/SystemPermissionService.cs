// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemPermissionService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统权限服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System;
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统权限服务类
    /// </summary>
    public class SystemPermissionService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统权限数据访问对象
        /// </summary>
        private readonly ISystemPermissionDA systemPermissionDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemPermissionService"/> class.
        /// </summary>
        public SystemPermissionService()
        {
            this.systemPermissionDA = new DAFactorySystem().CreateSystemPermissionDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加系统权限
        /// </summary>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        /// <returns>
        /// 权限编号
        /// </returns>
        public int AddPermission(System_Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            return this.systemPermissionDA.Insert(permission);
        }

        /// <summary>
        /// 移除指定编号的系统权限
        /// </summary>
        /// <param name="permissionID">
        /// 权限编号
        /// </param>
        public void RemovePermissionByID(int permissionID)
        {
            if (permissionID <= 0)
            {
                return;
            }

            this.systemPermissionDA.DeleteByID(permissionID);
        }

        /// <summary>
        /// 修改系统权限
        /// </summary>
        /// <param name="permission">
        /// 权限对象
        /// </param>
        public void ModifyPermission(System_Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            this.systemPermissionDA.Update(permission);
        }

        /// <summary>
        /// 查询系统权限列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 系统权限列表
        /// </returns>
        public List<System_Permission> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemPermissionDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询所有系统权限
        /// </summary>
        /// <returns>
        /// 系统权限列表
        /// </returns>
        public List<System_Permission> QueryAll()
        {
            return this.systemPermissionDA.SelectAll();
        }
        
        /// <summary>
        /// 指定系统权限编号是否拥有子权限
        /// </summary>
        /// <param name="permissionID">
        /// 权限编号
        /// </param>
        /// <returns>
        /// 是否拥有子权限
        /// </returns>
        public bool HasChildren(int permissionID)
        {
            return this.systemPermissionDA.HasChildren(permissionID);
        }

        #endregion
    }
}