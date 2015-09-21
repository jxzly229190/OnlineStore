// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromoteLandingPageService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   LP（Landing Page）服务类.
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
    /// LP（Landing Page）服务类.
    /// </summary>
    public class PromoteLandingPageService
    {
        #region Constants and Fields

        /// <summary>
        /// LP（Landing Page）数据库访问对象.
        /// </summary>
        private readonly IPromoteLandingPageDA landingPageDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PromoteLandingPageService"/> class.
        /// </summary>
        public PromoteLandingPageService()
        {
            this.landingPageDA = new DAFactoryPromote().CreatePromoteLandingPageDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加LP.
        /// </summary>
        /// <param name="landingPage">
        /// PromoteLandingPage对象实例.
        /// </param>
        /// <returns>
        /// PromoteLandingPage编号.
        /// </returns>
        public int Add(Promote_LandingPage landingPage)
        {
            return this.landingPageDA.Insert(landingPage);
        }

        /// <summary>
        /// 修改LP
        /// </summary>
        /// <param name="landingPage">
        /// PromoteLandingPage对象实例.
        /// </param>
        public void Modify(Promote_LandingPage landingPage)
        {
            this.landingPageDA.Update(landingPage);
        }

        /// <summary>
        /// 查询所有LP信息（用于构建LP树）.
        /// </summary>
        /// <returns>
        /// LP列表.
        /// </returns>
        public List<Promote_LandingPage> QueryAll()
        {
            return this.landingPageDA.SelectAll();
        }
        
        /// <summary>
        /// 查询指定编号的LP信息.
        /// </summary>
        /// <param name="ID">
        /// LP编号.
        /// </param>
        /// <returns>
        /// PromoteLandingPage对象实例.
        /// </returns>
        public Promote_LandingPage Query(int ID)
        {
            return this.landingPageDA.SelectRowByID(ID);
        }

        /// <summary>
        /// 查询LP列表（编码、父级编码、名称、制作人）.
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
        /// LP列表
        /// </returns>
        public List<Promote_LandingPage> QueryList(Paging paging, out int pageCount, out int totalCount)
        {
            return this.landingPageDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 删除指定编号的LP信息.
        /// </summary>
        /// <param name="ID">
        /// LP编号.
        /// </param>
        public void Remove(int ID)
        {
            this.landingPageDA.Delete(ID);
        }

        #endregion
    }
}
