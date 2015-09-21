// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserReceiveAddressDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员收货地址的数据库访问接口.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.User
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.User;

    /// <summary>
    /// 会员收货地址的数据库访问接口.
    /// </summary>
    public interface IUserReceiveAddressDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// 根据会员编号查询相应的默认收货地址.
        /// </summary>
        /// <param name="userID">
        /// 会员的编号.
        /// </param>
        /// <returns>
        /// User_RecieveAddress 的对象实例.
        /// </returns>
        User_RecieveAddress SelectDefaultReceiveAddressByUserID(int userID);

        /// <summary>
        /// 根据会员编号查询相应的收货地址.
        /// </summary>
        /// <param name="userID">
        /// 会员的编号.
        /// </param>
        /// <returns>
        /// User_RecieveAddress 的对象实例的列表.
        /// </returns>
        List<User_RecieveAddress> SelectReceiveAddressByUserID(int userID);

        /// <summary>
        /// 根据区县ID获取相应的邮政编码.
        /// </summary>
        /// <param name="countyID">
        /// 区县ID.
        /// </param>
        /// <returns>
        /// 邮政编码.
        /// </returns>
        string SelectPostCodeByCountyID(int countyID);

        /// <summary>
        /// 事务新增一条会员收货信息
        /// </summary>
        /// <param name="userRecieveAddress">
        /// 收货对象
        /// </param>
        /// <param name="transaction">
        /// 事务对象
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(User_RecieveAddress userRecieveAddress, global::System.Data.SqlClient.SqlTransaction transaction);

        /// <summary>
        /// 根据用户收货地址编码查询收货地址
        /// </summary>
        /// <param name="id">
        /// 收货地址编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        User_RecieveAddress SelectByID(int id);

        /// <summary>
        /// 更新用户收货地址信息
        /// </summary>
        /// <param name="userRecieveAddress">
        /// 用户收货地址
        /// </param>
        /// <param name="transaction">事务</param>
        void Update(User_RecieveAddress userRecieveAddress, out SqlTransaction transaction);

        /// <summary>
        /// 根据ID删除收货地址
        /// </summary>
        /// <param name="addressID">地址编码</param>
        int DeleteByID(int addressID);

        /// <summary>
        /// 根据ID和用户名设置用户的收货地址为默认地址
        /// </summary>
        /// <param name="addressID"></param>
        /// <param name="userID"></param>
        int SetDefault(int addressID, int userID);
        /// <summary>
        /// 修改用户地址信息
        /// </summary>
        /// <param name="userRecieveAddress"></param>
        int UpdateAddresss(User_RecieveAddress userRecieveAddress);

        #endregion

    }
}
