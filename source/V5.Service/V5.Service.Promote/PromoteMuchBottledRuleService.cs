// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteMuchBottledRuleService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销活动规则服务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Promote
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Promote;
    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 多瓶装促销活动规则服务类.
    /// </summary>
    public class PromoteMuchBottledRuleService
    {
        #region Constants and Fields

        /// <summary>
        /// 多瓶装促销数据访问对象.
        /// </summary>
        private readonly IPromoteMuchBottledRuleDA promoteMuchBottledRuleDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteMuchBottledRuleService"/> class.
        /// </summary>
        public PromoteMuchBottledRuleService()
        {
            this.promoteMuchBottledRuleDA = new DAFactoryPromote().CreatePromoteMuchBottledRuleDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加多瓶装活动规则.
        /// </summary>
        /// <param name="promoteMuchBottledRule">
        /// Promote_MuchBottled_Rule的对象实例.
        /// </param>
        /// <returns>
        /// 多瓶装活动规则的编号.
        /// </returns>
        public int Add(Promote_MuchBottled_Rule promoteMuchBottledRule)
        {
            return this.promoteMuchBottledRuleDA.Insert(promoteMuchBottledRule);
        }

        /// <summary>
        /// 查询多瓶装促销规则列表
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
        /// 多瓶装促销规则列表
        /// </returns>
        public List<Promote_MuchBottled_Rule> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.promoteMuchBottledRuleDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 更新多瓶装促销规则.
        /// </summary>
        /// <param name="promoteMuchBottledRule">
        /// Promote_MuchBottled_Rule的对象实例.
        /// </param>
        public void Modify(Promote_MuchBottled_Rule promoteMuchBottledRule)
        {
            this.promoteMuchBottledRuleDA.Update(promoteMuchBottledRule);
        }

        /// <summary>
        /// 查询指顶多瓶装促销的规则列表.
        /// </summary>
        /// <param name="muchBottledID">
        /// 多瓶装促销的编号.
        /// </param>
        /// <returns>
        /// 规则列表.
        /// </returns>
        public List<Promote_MuchBottled_Rule> QueryByMuchBottledID(int muchBottledID)
        {
            return this.promoteMuchBottledRuleDA.SelectByMuchBottledID(muchBottledID);
        }

        #endregion
    }
}
