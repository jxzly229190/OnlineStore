// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactorySystem.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.System;

    /// <summary>
    /// 系统模块数据访问工厂类
    /// </summary>
    public sealed class DAFactorySystem : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactorySystem"/> class.
        /// </summary>
        public DAFactorySystem()
        {
            this.AssemblyPath = this.AssemblyPath + ".System";
        }

        /// <summary>
        /// The create System department da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemDepartmentDA"/>.
        /// </returns>
        public ISystemDepartmentDA CreateSystemDepartmentDA()
        {
            string nameSpace = AssemblyPath + ".SystemDepartmentDA";
            object systemDepartmentDA = Create(AssemblyPath, nameSpace);
            return (ISystemDepartmentDA)systemDepartmentDA;
        }

        /// <summary>
        /// The create System employee da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemEmployeeDA"/>.
        /// </returns>
        public ISystemEmployeeDA CreateSystemEmployeeDA()
        {
            string nameSpace = AssemblyPath + ".SystemEmployeeDA";
            object systemEmployeeDA = Create(AssemblyPath, nameSpace);
            return (ISystemEmployeeDA)systemEmployeeDA;
        }

        /// <summary>
        /// The create System menu da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemMenuDA"/>.
        /// </returns>
        public ISystemMenuDA CreateSystemMenuDA()
        {
            string nameSpace = AssemblyPath + ".SystemMenuDA";
            object systemMenuDA = Create(AssemblyPath, nameSpace);
            return (ISystemMenuDA)systemMenuDA;
        }

        /// <summary>
        /// The create System permission da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemPermissionDA"/>.
        /// </returns>
        public ISystemPermissionDA CreateSystemPermissionDA()
        {
            string nameSpace = AssemblyPath + ".SystemPermissionDA";
            object systemPermissionDA = Create(AssemblyPath, nameSpace);
            return (ISystemPermissionDA)systemPermissionDA;
        }

        /// <summary>
        /// The create System role da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemRoleDA"/>.
        /// </returns>
        public ISystemRoleDA CreateSystemRoleDA()
        {
            string nameSpace = AssemblyPath + ".SystemRoleDA";
            object systemRoleDA = Create(AssemblyPath, nameSpace);
            return (ISystemRoleDA)systemRoleDA;
        }

        /// <summary>
        /// The create System role permission da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemRolePermissionDA"/>.
        /// </returns>
        public ISystemRolePermissionDA CreateSystemRolePermissionDA()
        {
            string nameSpace = AssemblyPath + ".SystemRolePermissionDA";
            object systemRolePermissionDA = Create(AssemblyPath, nameSpace);
            return (ISystemRolePermissionDA)systemRolePermissionDA;
        }

        /// <summary>
        /// The create System user da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemUserDA"/>.
        /// </returns>
        public ISystemUserDA CreateSystemUserDA()
        {
            string nameSpace = AssemblyPath + ".SystemUserDA";
            object systemUserDA = Create(AssemblyPath, nameSpace);
            return (ISystemUserDA)systemUserDA;
        }

        /// <summary>
        /// The create System home da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemHomeDA"/>.
        /// </returns>
        public ISystemHomeDA CreateSystemHomeDA()
        {
            string nameSpace = AssemblyPath + ".SystemHomeDA";
            object systemHomeDA = Create(AssemblyPath, nameSpace);
            return (ISystemHomeDA)systemHomeDA;
        }

        /// <summary>
        /// The create system resources da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemResourcesDA"/>.
        /// </returns>
        public ISystemResourcesDA CreateSystemResourcesDA()
        {
            string nameSpace = AssemblyPath + ".SystemResourcesDA";
            object systemResources = Create(AssemblyPath, nameSpace);
            return (ISystemResourcesDA)systemResources; 
        }

        /// <summary>
        /// The create system rights da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemRightsDA"/>.
        /// </returns>
        public ISystemRightsDA CreateSystemRightsDA()
        {
            string nameSpace = AssemblyPath + ".SystemRightsDA";
            object systemRightsDA = Create(AssemblyPath, nameSpace);
            return (ISystemRightsDA)systemRightsDA; 
        }

        /// <summary>
        /// The create system log da.
        /// </summary>
        /// <returns>
        /// The <see cref="ISystemLogDA"/>.
        /// </returns>
        public ISystemLogDA CreateSystemLogDA()
        {
            string nameSpace = AssemblyPath + ".SystemLogDA";
            object systemLogDA = Create(AssemblyPath, nameSpace);
            return (ISystemLogDA)systemLogDA;
        }
    }
}