// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsCommissionRatioService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   CPS平台的佣金服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Transact;
    using V5.DataContract.Transact;

    /// <summary>
    /// CPS平台的佣金服务类.
    /// </summary>
    public class CpsCommissionRatioService
    {
        #region Constants and Fields

        /// <summary>
        /// Cps数据访问接口.
        /// </summary>
        private readonly ICpsCommissionRatioDA cpsCommissionRatioDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CpsCommissionRatioService"/> class.
        /// </summary>
        public CpsCommissionRatioService()
        {
            this.cpsCommissionRatioDA = new DAFactoryTransact().CreateCpsCommissionRatioDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询指定CPS佣金比例信息.
        /// </summary>
        /// <param name="condition">
        /// 查询的条件.
        /// </param>
        /// <param name="totalCount">
        /// The total Count.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        public List<Cps_CommissionRatio> QueryCommissionRatioByCpsID(string condition, out int totalCount)
        {
            return this.cpsCommissionRatioDA.SelectCommissionRatioByCpsID(condition, out totalCount);
        }

        /// <summary>
        /// 查询指定平台编号的CPS佣金.
        /// </summary>
        /// <param name="cpsID">
        /// CPS的平台编号.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio对象实例的列表.
        /// </returns>
        public List<Cps_CommissionRatio> QueryCommissionRatioByCpsID(int cpsID)
        {
            return this.cpsCommissionRatioDA.SelectCommissionRatioByCpsID(cpsID);
        }

        /// <summary>
        /// 更新所有CPS佣金比例信息.
        /// </summary>
        /// <param name="cpsCommissionRatio">
        /// Cps_CommissionRatio的对象实例.
        /// </param>
        public void Modify(Cps_CommissionRatio cpsCommissionRatio)
        {
            this.cpsCommissionRatioDA.Updata(cpsCommissionRatio);
        }

        /// <summary>
        /// 添加CPS佣金信息.
        /// </summary>
        /// <param name="cpsCommissionRatio">
        /// Cps_CommissionRatio的对象实例.
        /// </param>
        /// <returns>
        /// CPS的平台编号.
        /// </returns>
        public int Add(Cps_CommissionRatio cpsCommissionRatio)
        {
            return this.cpsCommissionRatioDA.Insert(cpsCommissionRatio);
        }

        /// <summary>
        /// 查询指定编号的佣金比例信息.
        /// </summary>
        /// <param name="ID">
        /// 编码.
        /// </param>
        /// <returns>
        /// Cps_CommissionRatio的对象实例.
        /// </returns>
        public Cps_CommissionRatio SelectCommissionRatioByID(int ID)
        {
            return this.cpsCommissionRatioDA.SelectCommissionRatioByID(ID);
        }

        #endregion
    }
}
