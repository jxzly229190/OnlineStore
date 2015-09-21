// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemMenuService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统菜单服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System.Collections.Generic;
    using global::System.Linq;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 系统菜单服务类
    /// </summary>
    public class SystemMenuService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户数据访问对象
        /// </summary>
        private readonly ISystemMenuDA systemMenuDA;

        /// <summary>
        /// Gets or sets the system menus.
        /// </summary>
        private List<System_Menu> systemMenus;

        #endregion

        /// <summary>
        /// Gets the system menus.
        /// </summary>
        private List<System_Menu> SystemMenus
        {
            get
            {
                return this.systemMenus ?? (this.systemMenus = this.systemMenuDA.SelectAll());
            }
        } 

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMenuService"/> class.
        /// </summary>
        public SystemMenuService()
        {
            this.systemMenuDA = new DAFactorySystem().CreateSystemMenuDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加系统菜单
        /// </summary>
        /// <param name="menu">
        /// 菜单对象
        /// </param>
        /// <returns>
        /// 菜单编号
        /// </returns>
        public int AddMenu(System_Menu menu)
        {
            return this.systemMenuDA.Insert(menu);
        }

        /// <summary>
        /// 移除指定编号的系统菜单
        /// </summary>
        /// <param name="menuID">
        /// 菜单编号
        /// </param>
        public void RemoveMenuByID(int menuID)
        {
            this.systemMenuDA.DeleteByID(menuID);
        }

        /// <summary>
        /// 修改系统菜单
        /// </summary>
        /// <param name="menu">
        /// 菜单对象
        /// </param>
        public void ModifyMenu(System_Menu menu)
        {
            this.systemMenuDA.Update(menu);
        }

        /// <summary>
        /// 查询系统菜单列表
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
        /// 系统菜单列表
        /// </returns>
        public List<System_Menu> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemMenuDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询所有系统菜单
        /// </summary>
        /// <returns>
        /// 系统菜单列表
        /// </returns>
        public List<System_Menu> QueryAll()
        {
            return this.systemMenuDA.SelectAll();
        }

        /// <summary>
        /// 查询指定角色编号的菜单列表
        /// </summary>
        /// <param name="roleID">
        /// 角色编号
        /// </param>
        /// <param name="isParent">
        /// 是否为父级菜单
        /// </param>
        /// <returns>
        /// 菜单列表
        /// </returns>
        public List<System_Menu> QueryByRoleID(int roleID, bool isParent)
        {
            var systemMenus = this.systemMenuDA.SelectByRoleID(roleID);
            if (systemMenus != null)
            {
                return isParent
                           ? systemMenus.Where(item => item.ParentID == 0).ToList()
                           : systemMenus.Where(item => item.ParentID != 0).ToList();
            }

            return null;
        }

        /// <summary>
        /// 获取用户顶层菜单
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// 返回顶层菜单
        /// </returns>
        public List<System_Menu> GetUserTopMenus(string userRights)
        {
            var systemRightService = new SystemRightsService();
            if (this.SystemMenus == null)
            {
                return new List<System_Menu>();
            }

            var topMenus = new List<System_Menu>();
            foreach (var systemMenu in this.SystemMenus)
            {
                if (systemMenu.ParentID == 0)
                {
                    var resourceKey =
                        systemRightService.BuildResourceKey(
                            systemMenu.URL.ToLower(),
                            "index",
                            "get");
                    if (systemRightService.ValidateRight(resourceKey, userRights))
                    {
                        topMenus.Add(systemMenu);
                    }
                }
            }

            return topMenus;
        }

        /// <summary>
        /// 获取左边菜单列表
        /// </summary>
        /// <param name="userID">
        /// The user ID.
        /// </param>
        /// <returns>
        /// 左边菜单列表
        /// </returns>
        public List<System_Menu> GetUserLeftMenus(string userRights)
        {
            var systemRightService = new SystemRightsService();
            if (this.SystemMenus == null)
            {
                return new List<System_Menu>();
            }

            var leftMenus = new List<System_Menu>();
            foreach (var systemMenu in this.SystemMenus)
            {
                if (systemMenu.ParentID != 0)
                {
                    var resourceKey =
                        systemRightService.BuildResourceKey(
                            this.systemMenus.Find(m => m.ID == systemMenu.ParentID).URL.ToLower(),
                            systemMenu.URL.ToLower(),
                            "get");
                    if (systemRightService.ValidateRight(resourceKey, userRights))
                    {
                        leftMenus.Add(systemMenu);
                    }
                }
            }

            return leftMenus;
        }
        #endregion
    }
}