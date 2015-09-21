using System;
using System.Collections.Generic;
using V5.DataContract.Utility;
using V5.DataAccess.Utility;

namespace V5.Service.Utility
{
    using V5.DataAccess;

    public class SystemVisitorService
    {
        /// <summary>
        /// 系统用户访问次数服务类
        /// </summary>
        public readonly ISystemVisitorDA systemVisitorDA;

        public SystemVisitorService()
        {
            this.systemVisitorDA = new DAFactoryUtility().CreateSystemVisitorDA();
        }

        public List<System_Visitor> Query(DateTime startTime, DateTime endTime, string condition)
        {
            return this.systemVisitorDA.Query(startTime, endTime, condition);
        }

        public int QueryPV()
        {
            return this.systemVisitorDA.QueryPV();
        }

        public int Insert(System_Visitor visitor)
        {
            return this.systemVisitorDA.Insert(visitor);
        }

        public int Update(System_Visitor visitor)
        {
            return this.systemVisitorDA.Update(visitor);
        }
    }
}
