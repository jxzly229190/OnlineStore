// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemResourcesService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统资源服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.System
{
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataContract.System;

    /// <summary>
    /// The system resources service.
    /// </summary>
    public class SystemResourcesService
    {
        /// <summary>
        /// The query.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<System_Resources> QueryAll()
        {
            return new DAFactorySystem().CreateSystemResourcesDA().SelectAll();
        } 
    }
}