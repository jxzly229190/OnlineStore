// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPromoteMuchBottledRuleDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   多瓶装促销活动规则数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Promote
{
    using global::System.Collections.Generic;

    using V5.DataContract.Promote;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 多瓶装促销活动规则数据访问接口.
    /// </summary>
    public interface IPromoteMuchBottledRuleDA
    {
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
        int Insert(Promote_MuchBottled_Rule promoteMuchBottledRule);

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
        List<Promote_MuchBottled_Rule> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 更新多瓶装促销规则.
        /// </summary>
        /// <param name="promoteMuchBottledRule">
        /// Promote_MuchBottled_Rule的对象实例.
        /// </param>
        void Update(Promote_MuchBottled_Rule promoteMuchBottledRule);

        /// <summary>
        /// 查询指顶多瓶装促销的规则列表.
        /// </summary>
        /// <param name="muchBottledID">
        /// 多瓶装促销的编号.
        /// </param>
        /// <returns>
        /// 规则列表.
        /// </returns>
        List<Promote_MuchBottled_Rule> SelectByMuchBottledID(int muchBottledID);

        #endregion
    }
}
