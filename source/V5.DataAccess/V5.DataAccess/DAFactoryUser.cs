// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryUser.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   会员模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.User;

    /// <summary>
    /// 会员模块数据访问工厂类
    /// </summary>
    public class DAFactoryUser : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryUser"/> class.
        /// </summary>
        public DAFactoryUser()
        {
            this.AssemblyPath = this.AssemblyPath + ".User";
        }

        /// <summary>
        /// The create user da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserDA"/>.
        /// </returns>
        public IUserDA CreateUserDA()
        {
            string nameSpace = AssemblyPath + ".UserDA";
            object userDA = Create(AssemblyPath, nameSpace);
            return (IUserDA)userDA;
        }

        /// <summary>
        /// The create user leve da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserLevelDA"/>.
        /// </returns>
        public IUserLevelDA CreateUserLeveDA()
        {
            string nameSpace = AssemblyPath + ".UserLevelDA";
            object userLeveDA = Create(AssemblyPath, nameSpace);
            return (IUserLevelDA)userLeveDA;
        }

        /// <summary>
        /// The create user receive address da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserLevelDA"/>.
        /// </returns>
        public IUserReceiveAddressDA CreateUserReceiveAddressDA()
        {
            string nameSpace = AssemblyPath + ".UserReceiveAddressDA";
            object userReceiveAddressDA = Create(AssemblyPath, nameSpace);
            return (IUserReceiveAddressDA)userReceiveAddressDA;
        }

        /// <summary>
        /// The create user level price da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserLevelPriceDA"/>.
        /// </returns>
        public IUserLevelPriceDA CreateUserLevelPriceDA()
        {
            string nameSpace = AssemblyPath + ".UserLevelPriceDA";
            object userLevelPriceDA = Create(AssemblyPath, nameSpace);
            return (IUserLevelPriceDA)userLevelPriceDA;
        }

        /// <summary>
        /// The create user message email da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserMessageEmailDA"/>.
        /// </returns>
        public IUserMessageEmailDA CreateUserMessageEmailDA()
        {
            string nameSpace = AssemblyPath + ".UserMessageEmailDA";
            object userMessageEmailDA = Create(AssemblyPath, nameSpace);
            return (IUserMessageEmailDA)userMessageEmailDA;
        }

        /// <summary>
        /// The create user message send record da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserMessageSendRecordDA"/>.
        /// </returns>
        public IUserMessageSendRecordDA CreateUserMessageSendRecordDA()
        {
            string nameSpace = AssemblyPath + ".UserMessageSendRecordDA";
            object userMessageSendRecordDA = Create(AssemblyPath, nameSpace);
            return (IUserMessageSendRecordDA)userMessageSendRecordDA;
        }

        /// <summary>
        /// The create user message sms da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserMessageSmsDA"/>.
        /// </returns>
        public IUserMessageSmsDA CreateUserMessageSmsDA()
        {
            string nameSpace = AssemblyPath + ".UserMessageSmsDA";
            object userMessageSmsDA = Create(AssemblyPath, nameSpace);
            return (IUserMessageSmsDA)userMessageSmsDA;
        }

        /// <summary>
        /// The create user account da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserAccountDA"/>.
        /// </returns>
        public IUserAccountDA CreateUserAccountDA()
        {
            string nameSpace = AssemblyPath + ".UserAccountDA";
            object userAccountDA = Create(AssemblyPath, nameSpace);
            return (IUserAccountDA)userAccountDA;
        }

        /// <summary>
        /// The create user browse history da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserBrowseHistoryDA"/>.
        /// </returns>
        public IUserBrowseHistoryDA CreateUserBrowseHistoryDA()
        {
            string nameSpace = AssemblyPath + ".UserBrowseHistoryDA";
            object userBrowseHistoryDA = Create(AssemblyPath, nameSpace);
            return (IUserBrowseHistoryDA)userBrowseHistoryDA;
        }

        /// <summary>
        /// The create user collect record da.
        /// </summary>
        /// <returns>
        /// The <see cref="IUserCollectRecordDA"/>.
        /// </returns>
        public IUserCollectRecordDA CreateUserCollectRecordDA()
        {
            string nameSpace = AssemblyPath + ".UserCollectRecordDA";
            object userCollectRecordDA = Create(AssemblyPath, nameSpace);
            return (IUserCollectRecordDA)userCollectRecordDA;
        }

        public IFeedBackDA CreateUserFeedBackDA()
        {
            string nameSpace = AssemblyPath + ".FeedBackDA";
            object feedBackDA = Create(AssemblyPath, nameSpace);
            return (IFeedBackDA)feedBackDA;
        }

        /// <summary>
        /// 密码找回数据
        /// </summary>
        /// <returns></returns>
        public Iv4UsrFindMailPasswordDA CreateV4FindPassword()
        {
            string nameSpace = AssemblyPath + ".v4UsrFindMailPasswordDA";
            object v4UsrFindMailPasswordDA = Create(AssemblyPath, nameSpace);
            return (Iv4UsrFindMailPasswordDA)v4UsrFindMailPasswordDA;
        }
    }
}