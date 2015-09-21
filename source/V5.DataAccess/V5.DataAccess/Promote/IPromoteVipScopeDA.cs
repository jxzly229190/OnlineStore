// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteVipScope.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员促销商品数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.DataContract.Promote;

    /// <summary>
    /// 会员促销商品数据访问接口.
    /// </summary>
    public interface IPromoteVipScopeDA
    {
        #region Public Methods and Operators

        List<Promote_Vip_Scope> SelectByPromoteVipID(int id);

        int Insert(Promote_Vip_Scope promoteVipScope, SqlTransaction transaction);
        
        void Update(Promote_Vip_Scope promoteVipScope, SqlTransaction transaction);
        
        void Delete(int id,SqlTransaction transaction);

        List<ProductSearchResult> SelectByPromoteProduct(string productIDs, int promoteVipID);

        #endregion
    }
}
