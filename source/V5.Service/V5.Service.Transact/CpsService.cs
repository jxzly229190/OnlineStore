// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CpsService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   CPS平台服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Transact;
    using V5.DataContract.Transact;
    using V5.Library.Storage.DB;

    /// <summary>
    /// CPS平台服务类.
    /// </summary>
    public class CpsService
    {
        #region Constants and Fields

        /// <summary>
        /// Cps数据访问接口.
        /// </summary>
        private readonly ICpsDA cpsDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CpsService"/> class. 
        /// </summary>
        public CpsService()
        {
            this.cpsDA = new DAFactoryTransact().CreateCpsDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询Cps列表
        /// </summary>
        /// <param name="paging">
        /// 分页数据对象
        /// </param>
        /// <param name="pageCount">
        /// 总页数
        /// </param>
        /// <param name="totalCount">
        /// 总记录数
        /// </param>
        /// <returns>
        /// Cps列表
        /// </returns>
        public List<Cps> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.cpsDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 添加CPS信息.
        /// </summary>
        /// <param name="cps">
        /// Cps的对象实例.
        /// </param>
        /// <returns>
        /// 编号.
        /// </returns>
        public int AddCps(Cps cps)
        {
            return this.cpsDA.Insert(cps);
        }

        /// <summary>
        /// 修改CPS信息.
        /// </summary>
        /// <param name="cps">
        /// Cps对象实例.
        /// </param>
        public void Modify(Cps cps)
        {
            this.cpsDA.Update(cps);
        }

        /// <summary>
        /// 查询所有CPS
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Cps> QueryAll()
        {
            return this.cpsDA.SelectAll();
        }

        #endregion
    }
}
