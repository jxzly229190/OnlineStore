// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteVipService.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   会员促销数据服务.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Product;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员促销数据服务.
    /// </summary>
    public class PromoteVipService
    {
        #region Constants and Fields

        /// <summary>
        /// 会员促销数据访问类.
        /// </summary>
        private readonly IPromoteVipDA promoteVipDA;

        /// <summary>
        /// 会员促销商品数据访问类.
        /// </summary>
        private readonly IPromoteVipScopeDA promoteVipScopeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteVipService"/> class.
        /// </summary>
        public PromoteVipService()
        {
            this.promoteVipDA = new DAFactoryPromote().CreatePromoteVipDA();
            this.promoteVipScopeDA = new DAFactoryPromote().CreatePromoteVipScopeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询会员促销列表
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
        /// 会员促销列表
        /// </returns>
        public List<Promote_Vip> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteVipDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询指定的会员促销.
        /// </summary>
        /// <param name="id">
        /// 会员促销编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_Vip"/>.
        /// </returns>
        public Promote_Vip QueryByID(int id)
        {
            return this.promoteVipDA.SelectByID(id);
        }

        /// <summary>
        /// 查询指定的会员促销活动商品.
        /// </summary>
        /// <param name="id">
        /// 会员促销编号.
        /// </param>
        /// <returns>
        /// The <see cref="Promote_Vip"/>.
        /// </returns>
        public List<Promote_Vip_Scope> QueryScopeByID(int id)
        {
            return this.promoteVipScopeDA.SelectByPromoteVipID(id);
        }

        /// <summary>
        /// 添加会员促销活动.
        /// </summary>
        /// <param name="promoteVip">
        /// Promote_Vip的对象实例.
        /// </param>
        /// <returns>
        /// 会员促销活动的编号.
        /// </returns>
        public int Add(Promote_Vip promoteVip)
        {
            SqlTransaction transaction = null;
            try
            {
                var promoteVipID = this.promoteVipDA.Insert(promoteVip, out transaction);
                if (promoteVip.Scopes != null && promoteVip.Scopes.Count > 0)
                {
                    foreach (var scope in promoteVip.Scopes)
                    {
                        scope.PromoteVipID = promoteVipID;
                        this.promoteVipScopeDA.Insert(scope, transaction);
                    }
                }

                transaction.Commit();
                return promoteVipID;
            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw new Exception(exception.Message, exception);
            }
        }
        
        /// <summary>
        /// 添加会员促销活动.
        /// </summary>
        /// <param name="promoteVip">
        /// Promote_Vip的对象实例.
        /// </param>
        public void Modify(Promote_Vip promoteVip)
        {
            SqlTransaction transaction = null;
            try
            {
                this.promoteVipDA.Update(promoteVip, out transaction);
                if (promoteVip.Scopes != null && promoteVip.Scopes.Count > 0)
                {
                    this.promoteVipScopeDA.Delete(promoteVip.ID, transaction);
                    foreach (var scope in promoteVip.Scopes)
                    {
                        scope.PromoteVipID = promoteVip.ID;
                        this.promoteVipScopeDA.Insert(scope, transaction);
                    }
                }

                transaction.Commit();
            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 更新活动状态.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        public void ModifyStatus(int id, int status)
        {
            this.promoteVipDA.UpdateStatus(id, status);
        }

        /// <summary>
        /// 删除指定的会员促销.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Remove(int id)
        {
            SqlTransaction transaction = null;
            try
            {
                this.promoteVipDA.Delete(id, out transaction);
                this.promoteVipScopeDA.Delete(id, transaction);
                transaction.Commit();
            }
            catch (Exception exception)
            {
                  if (transaction != null)
                {
                    transaction.Rollback();
                }

                throw new Exception(exception.Message, exception);
            }
        }

        /// <summary>
        /// 当前商品中已参加会员促销的商品.
        /// </summary>
        /// <param name="productIDs">
        /// The product i ds.
        /// </param>
        /// <returns>
        /// 当前商品中已参加限时抢购的商品信息.
        /// </returns>
        public List<ProductSearchResult> QueryByPromoteProduct(string productIDs, int promoteVipID)
        {
            return this.promoteVipScopeDA.SelectByPromoteProduct(productIDs, promoteVipID);
        }

        #endregion
    }
}
