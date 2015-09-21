// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteVipDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员促销数据访问接口.
    /// </summary>
    public interface IPromoteVipDA
    {
        #region Public Methods and Operators

        List<Promote_Vip> Paging(Paging paging, out int pageCount, out int totalCount);

        Promote_Vip SelectByID(int id);

        int Insert(Promote_Vip promoteVip, out SqlTransaction transaction);

        void Update(Promote_Vip promoteVip, out SqlTransaction transaction);

        void Delete(int id, out SqlTransaction transaction);

        void UpdateStatus(int id, int status);

        #endregion
    }
}
