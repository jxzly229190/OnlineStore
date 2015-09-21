// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserAccountDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员账户数据访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Data.SqlClient;

    using V5.DataContract.User;

    /// <summary>
    /// 会员账户数据访问接口.
    /// </summary>
    public interface IUserAccountDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 检验是否存在指定会员的会员账户.
        /// </summary>
        /// <param name="userID">
        /// 会员编号.
        /// </param>
        /// <returns>
        ///  存在：true，不存在：false .
        /// </returns>
        bool IsExistsUserID(int userID);

        /// <summary>
        /// 添加会员虚拟账户.
        /// </summary>
        /// <param name="userAccount">
        /// User_Account的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        /// <returns>
        /// 会员虚拟账户编号.
        /// </returns>
        int Insert(User_Account userAccount, SqlTransaction transaction);

        /// <summary>
        /// 修改会员虚拟账户.
        /// </summary>
        /// <param name="userAccount">
        /// User_Account的对象实例.
        /// </param>
        /// <param name="transaction">
        /// 数据事务.
        /// </param>
        void Update(User_Account userAccount, SqlTransaction transaction);

        #endregion
    }
}
