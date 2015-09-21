// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetAmountService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满件优惠促销活动数据服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetAmount;
    using V5.DataContract.Promote.MeetAmount;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满件优惠促销活动数据服务类.
    /// </summary>
    public class PromoteMeetAmountService
    {
        #region Constants and Fields

        /// <summary>
        /// 满件优惠促销活动数据访问对象.
        /// </summary>
        private readonly IPromoteMeetAmountDA promoteMeetAmountDA;

        /// <summary>
        /// 满件优惠促销活动规则数据访问对象.
        /// </summary>
        private readonly IPromoteMeetAmountRuleDA promoteMeetAmountRuleDA;

        /// <summary>
        /// 满件优惠促销活动商品数据访问对象.
        /// </summary>
        private readonly IPromoteMeetAmountScopeDA promoteMeetAmountScopeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetAmountService"/> class. 
        /// </summary>
        public PromoteMeetAmountService()
        {
            this.promoteMeetAmountDA = new DAFactoryPromote().CreatePromoteMeetAmountDA();
            this.promoteMeetAmountRuleDA = new DAFactoryPromote().CreatePromoteMeetAmountRuleDA();
            this.promoteMeetAmountScopeDA = new DAFactoryPromote().CreatePromoteMeetAmountScopeDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 查询满件优惠列表
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
        /// 满件优惠列表
        /// </returns>
        public List<Promote_MeetAmount> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteMeetAmountDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 添加满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <returns>
        /// 满件优惠的编号.
        /// </returns>
        public int Add(Promote_MeetAmount promoteMeetAmount)
        {
            SqlTransaction transaction = null;
            try
            {
                // 添加促销活动主信息
                var meetAmountID = this.promoteMeetAmountDA.Insert(promoteMeetAmount, out transaction);

                promoteMeetAmount.MeetAmountScope.MeetAmountID = meetAmountID;

                // 添加促销活动活动商品信息
                this.promoteMeetAmountScopeDA.Insert(promoteMeetAmount.MeetAmountScope, transaction);
                
                // 添加促销活动规则 
                foreach (var promoteMeetAmountRule in promoteMeetAmount.MeetAmountRules)
                {
                    promoteMeetAmountRule.PromoteMeetAmountID = meetAmountID;
                    this.promoteMeetAmountRuleDA.Insert(promoteMeetAmountRule, transaction);
                }

                transaction.Commit();
                return meetAmountID;
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
        /// 查询指定编号的促销活动.
        /// </summary>
        /// <param name="id">
        /// 促销活动编号.
        /// </param>
        /// <returns>
        /// Promote_MeetAmount的对象实例.
        /// </returns>
        public Promote_MeetAmount QueryByID(int id)
        {
            return this.promoteMeetAmountDA.SelectByID(id);
        }

        /// <summary>
        /// 修改满件优惠促销活动.
        /// </summary>
        /// <param name="promoteMeetAmount">
        /// Promote_MeetAmount的对象.
        /// </param>
        /// <param name="removeRuleIdArry">
        /// 删除的规则编号集合.
        /// </param>
        public void Modify(Promote_MeetAmount promoteMeetAmount, string[] removeRuleIdArry)
        {
            SqlTransaction transaction = null;

            try
            {
                // 修改促销活动主信息
                this.promoteMeetAmountDA.Update(promoteMeetAmount, out transaction);

                // 修改促销活动活动商品信息
                this.promoteMeetAmountScopeDA.Update(promoteMeetAmount.MeetAmountScope, transaction);

                // 添加或修改促销活动规则 
                foreach (var promoteMeetAmountRule in promoteMeetAmount.MeetAmountRules)
                {
                    if (promoteMeetAmountRule.ID > 0)
                    {
                        this.promoteMeetAmountRuleDA.Update(promoteMeetAmountRule, transaction);
                    }
                    else
                    {
                        this.promoteMeetAmountRuleDA.Insert(promoteMeetAmountRule, transaction);
                    }
                }

                if (removeRuleIdArry != null)
                {
                    foreach (var ruleId in removeRuleIdArry)
                    {
                        if (!string.IsNullOrEmpty(ruleId))
                        {
                            this.promoteMeetAmountRuleDA.Delete(Convert.ToInt32(ruleId), transaction);
                        }
                    } // 删除无用的活动规则
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
        /// 修改活动状态
        /// </summary>
        /// <param name="meetAmountID">活动编号</param>
        /// <param name="status">状态（1：可用，2：暂停，3：停止）</param>
        public void ModifyStatus(int meetAmountID, int status)
        {
            this.promoteMeetAmountDA.UpdateStatus(meetAmountID, status);
        }

        /// <summary>
        /// 删除指定的活动.
        /// </summary>
        /// <param name="id">
        /// 满件优惠活动编号.
        /// </param>
        public void Remove(int id)
        {
            this.promoteMeetAmountDA.Delete(id);
        }

        #endregion
    }
}
