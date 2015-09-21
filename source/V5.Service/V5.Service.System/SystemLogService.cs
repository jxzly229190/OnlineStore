// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemLogService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   系统日志类
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
    /// 系统用户服务类
    /// </summary>
    public class SystemLogService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户数据访问对象
        /// </summary>
        private readonly ISystemLogDA systemLogDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUserService"/> class.
        /// </summary>
        public SystemLogService()
        {
            this.systemLogDA = new DAFactorySystem().CreateSystemLogDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 删除指定编号的系统日志
        /// </summary>
        /// <param name="logID">
        /// 日志编号
        /// </param>
        public void RemoveByID(int logID)
        {
            if (logID <= 0)
            {
                return;
            }

            try
            {
                this.systemLogDA.DeleteByID(logID);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 查询指定编号的系统日志
        /// </summary>
        /// <param name="loginName">
        /// 登录名
        /// </param>
        /// <returns>
        /// 系统日志对象
        /// </returns>
        public System_Log QueryByID(int logID)
        {
            return this.systemLogDA.SelectByID(logID);
        }

        /// <summary>
        /// 查询系统日志列表
        /// </summary>
        /// <param name="paging">
        /// 分页信息对象
        /// </param>
        /// <param name="pageCount">
        /// The page Count.
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// 系统日志列表
        /// </returns>
        public List<System_Log> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.systemLogDA.Paging(paging, out pageCount, out totalCount);
        }

        #endregion
    }    
}