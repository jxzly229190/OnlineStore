// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserLevelPriceDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员等级价格数据访问接口类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;

    using V5.DataContract.User;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 会员等级价格数据访问接口类.
    /// </summary>
    public interface IUserLevelPriceDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 添加会员等级价格.
        /// </summary>
        /// <param name="userLevelPrice">
        /// user_ level_ price的对象实例.
        /// </param>
        /// <returns>
        /// 会员等级价格编号.
        /// </returns>
        int Insert(User_Level_Price userLevelPrice);

        /// <summary>
        /// 删除指定编号的会员等级价格.
        /// </summary>
        /// <param name="id">
        /// 会员等级价格编号.
        /// </param>
        void DeleteByID(int id);

        /// <summary>
        /// 修改会员等级价格.
        /// </summary>
        /// <param name="userLevelPrice">
        /// user_ level_ price的对象实例.
        /// </param>
        void Update(User_Level_Price userLevelPrice);

        /// <summary>
        /// 查询会员等级价格列表
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
        /// 会员等级价格列表
        /// </returns>
        List<User_Level_Price> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 查询指定编号的会员等级价格.
        /// </summary>
        /// <param name="id">
        /// 会员等级价格编号.
        /// </param>
        /// <returns>
        /// User_Level_Price的对象实例.
        /// </returns>
        User_Level_Price SelectByID(int id);

        #endregion
    }
}