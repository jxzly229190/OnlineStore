// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICpsCommissionRatioDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   CPS佣金比例数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Transact
{
    using global::System.Collections.Generic;

    using V5.DataContract.Transact;

    /// <summary>
    /// CPS佣金比例数据访问接口.
    /// </summary>
    public interface ICpsCommissionRatioDA
    {
        /// <summary>
        /// 添加CPS佣金信息.
        /// </summary>
        /// <param name="cps_CommissionRatio">
        /// Cps_CommissionRatio的对象实例.
        /// </param>
        /// <returns>
        /// CPS_CommissionRatio的编号.
        /// </returns>
        int Insert(Cps_CommissionRatio cps_CommissionRatio);

        /// <summary>
        /// 更新CPS佣金信息.
        /// </summary>
        /// <param name="cpsCommissionRatio">
        /// Cps_CommissionRatio的对象实例..
        /// </param>
        void Updata(Cps_CommissionRatio cpsCommissionRatio);

        /// <summary>
        /// 查询指定的CPS佣金.
        /// </summary>
        /// <param name="codition">
        /// 查询条件.
        /// </param>
        /// <param name="totalCount">
        /// 查询结果的总条数.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        List<Cps_CommissionRatio> SelectCommissionRatioByCpsID(string codition, out int totalCount);

        /// <summary>
        /// 查询指定的CPS佣金
        /// </summary>
        /// <param name="cpsID">
        /// Cps的平台编号.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        List<Cps_CommissionRatio> SelectCommissionRatioByCpsID(int cpsID);

        /// <summary>
        /// 查询指定编号的佣金比例信息.
        /// </summary>
        /// <param name="ID">
        /// 编码.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio的对象实例.
        /// </returns>
        Cps_CommissionRatio SelectCommissionRatioByID(int ID);
    }
}