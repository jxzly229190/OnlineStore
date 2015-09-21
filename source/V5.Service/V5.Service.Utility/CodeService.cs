// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeService.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ChannelGroupBuyDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using V5.DataContract.Utility;
using V5.DataAccess.Utility;
using V5.DataAccess;

namespace V5.Service.Utility
{
    public class CodeService
    {
        #region Constants and Fields
        /// <summary>
        /// 编码服务类
        /// </summary>
        private readonly ICodeDA codeDA;

        #endregion

        #region Constructors and Destructors

        public CodeService()
        {
            this.codeDA = new DAFactoryUtility().CreateCodeDA();
        }

        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 编码添加
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Insert(Code code)
        {
            return this.codeDA.Insert(code);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Update(Code code)
        {
            return this.codeDA.Update(code);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<Code> FindByUserCode(string userCode)
        {
            return this.codeDA.FindByUserCode(userCode);
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Code FindById(int id)
        {
            return this.codeDA.FindById(id).FirstOrDefault();
        }

        /// <summary>
        /// 修改自增字段
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateIterator(int iterator, int id)
        {
            return this.codeDA.UpdateIterator(iterator, id);
        }

        /// <summary>
        /// 修改开始时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="id"></param>
        /// <param name="iterator">自增字段赋值</param>
        /// <returns></returns>
        public int UpdateStartTime(DateTime time, int id, int iterator)
        {
            return this.codeDA.UpdateStartTime(time, id, iterator);
        }

        /// <summary>
        /// 修改用户自增字段
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateUserIterator(int iterator, int id)
        {
            return this.codeDA.UpdateUserIterator(iterator, id);
        }

        public Order_Code GetOrderCount()
        {
            return this.codeDA.GetOrderCount();
        }

        #endregion
    }
}
