// --------------------------------------------------------------------------------------------------------------------
// <copyright file="System_Menu.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   后台菜单类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    ///     后台菜单类
    /// </summary>
    public class System_Menu
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     获取或设置后台菜单权限编号．
        /// </summary>
        public int PermissionID { get; set; }

        /// <summary>
        ///     获取或设置父菜单编号．
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        ///     获取或设置后台菜单层级．
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        ///     获取或设置后台菜单名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置后台菜单网址．
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        ///     获取或设置后台菜单排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        ///     获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}