using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataContract.Utility;

namespace V5.DataAccess.Utility
{
    public interface ISystemVisitorDA
    {
        List<System_Visitor> Query(DateTime startTime, DateTime endTime, string condition);
        int QueryPV();

        int Insert(System_Visitor visitor);

        int Update(System_Visitor visitor);
    }
}
