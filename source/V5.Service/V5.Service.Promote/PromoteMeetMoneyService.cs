// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMeetMoneyService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   满就送促销服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using V5.DataAccess;
    using V5.DataAccess.Promote.MeetMoney;
    using V5.DataContract.Promote.MeetMoney;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 满就送促销服务类.
    /// </summary>
    public class PromoteMeetMoneyService
    {
        #region Constants and Fields

        /// <summary>
        /// 满就送促销数据访问对象.
        /// </summary>
        private readonly IPromoteMeetMoneyDA promoteMeetMoneyDA;

        /// <summary>
        /// 满额优惠活动范围数据访问类.
        /// </summary>
        private readonly IPromoteMeetMoneyScopeDA promoteMeetMoneyScopeDA;

        /// <summary>
        /// 满就送促销规则数据访问对象.
        /// </summary>
        private readonly IPromoteMeetMoneyRuleDA promoteMeetMoneyRuleDA;
        
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMeetMoneyService"/> class.
        /// </summary>
        public PromoteMeetMoneyService()
        {
            this.promoteMeetMoneyDA = new DAFactoryPromote().CreatePromoteMeetMoneyDA();
            this.promoteMeetMoneyScopeDA = new DAFactoryPromote().CreatePromoteMeetMoneyScopeDA();
            this.promoteMeetMoneyRuleDA = new DAFactoryPromote().CreatePromoteMeetMoneyRuleDA();
        }

        #endregion

        #region Public Methods and Operators
        
        /// <summary>
        /// 添加满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <returns>
        /// 满就送促销活动的编号.
        /// </returns>
        public int Add(Promote_MeetMoney promoteMeetMoney)
        {
            SqlTransaction transaction = null;

            try
            {
                var meetMoneyID = this.promoteMeetMoneyDA.Insert(promoteMeetMoney, out transaction); // 添加满额优惠主信息

                promoteMeetMoney.MeetMoneyScope.MeetMoneyID = meetMoneyID;
                this.promoteMeetMoneyScopeDA.Insert(promoteMeetMoney.MeetMoneyScope, transaction);  // 添加满额优惠活动范围

                // 添加满额优惠规则
                foreach (var promoteMeetMoneyRule in promoteMeetMoney.MeetMoneyRules)
                {
                    promoteMeetMoneyRule.PromoteMeetMoneyID = meetMoneyID;
                    this.promoteMeetMoneyRuleDA.Insert(promoteMeetMoneyRule, transaction);
                }

                transaction.Commit();
                return meetMoneyID;
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
        /// 修改满就送促销活动.
        /// </summary>
        /// <param name="promoteMeetMoney">
        /// Promote_MeetMoney的对象实例.
        /// </param>
        /// <param name="removeRuleIdArry">
        /// The remove Rule Id Items.
        /// </param>
        public void Modify(Promote_MeetMoney promoteMeetMoney, string[] removeRuleIdArry)
        {
            SqlTransaction transaction = null;

            try
            {
                // 修改促销活动主信息
                this.promoteMeetMoneyDA.Update(promoteMeetMoney, out transaction);

                // 修改促销活动活动商品信息
                this.promoteMeetMoneyScopeDA.Update(promoteMeetMoney.MeetMoneyScope, transaction);

                foreach (var promoteMeetMoneyRule in promoteMeetMoney.MeetMoneyRules)
                {
                    if (promoteMeetMoneyRule.ID > 0)
                    {
                        this.promoteMeetMoneyRuleDA.Update(promoteMeetMoneyRule, transaction);
                    }
                    else
                    {
                        promoteMeetMoneyRule.PromoteMeetMoneyID = promoteMeetMoney.ID;
                        this.promoteMeetMoneyRuleDA.Insert(promoteMeetMoneyRule, transaction);
                    } // 有该规则就修改，没有就添加
                }

                if (removeRuleIdArry != null)
                {
                    foreach (var ruleId in removeRuleIdArry)
                    {
                        if (!string.IsNullOrEmpty(ruleId))
                        {
                            this.promoteMeetMoneyRuleDA.Delete(Convert.ToInt32(ruleId), transaction);
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
        /// 查询满就送促销列表
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
        /// 满就送列表
        /// </returns>
        public List<Promote_MeetMoney> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteMeetMoneyDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 查询指定的满就送促销.
        /// </summary>
        /// <param name="id">
        /// 满额优惠的编号.
        /// </param>
        /// <returns>
        /// Promote_MeetMoney的对象实例.
        /// </returns>
        public Promote_MeetMoney QueryByID(int id)
        {
            return this.promoteMeetMoneyDA.SelectByID(id);
        }

        /// <summary>
        /// 修改促销活动状态.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号
        /// </param>
        /// <param name="status">
        /// 促销活动状态（1：正常,2:暂停,3：停止）.
        /// </param>
        public void ModifyStatus(int id, int status)
        {
            this.promoteMeetMoneyDA.UpdateStatus(id, status);
        }

        /// <summary>
        /// 删除促销活动.
        /// </summary>
        /// <param name="id">
        /// 满就送促销活动的编号.
        /// </param>
        public void Remove(int id)
        {
            this.promoteMeetMoneyDA.Delete(id);
        }

        #endregion
    }
}
