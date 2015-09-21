// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductConsultService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   产品咨询服务类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Transact
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using V5.DataAccess;
    using V5.DataAccess.Transact;
    using V5.DataContract.Product;
    using V5.DataContract.Transact;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 产品咨询服务类
    /// </summary>
    public class ProductConsultService
    {
        #region  Constants and Fields

        /// <summary>
        /// 私有对象
        /// </summary>
        private readonly IProductConsultDA productConsultDA;

        #endregion

        #region  Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductConsultService"/> class. 
        /// 实例化服务对象
        /// </summary>
        public ProductConsultService()
        {
            this.productConsultDA = new DAFactoryTransact().CreateProductConsultDA();
        }

        #endregion

        #region Public Methods and Operators
        /// <summary>
        /// 回复商品咨询
        /// </summary>
        /// <param name="reply">
        /// 回复咨询
        /// </param>
        public void ReplyConsult(Product_Consult reply)
        {
            this.productConsultDA.ReplyConsult(reply);
        }

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="id">
        /// id
        /// </param>
        /// <returns>
        /// 删除的ID
        /// </returns>
        public int RemoveConsult(int id)
        {
            return this.productConsultDA.DeleteConsult(id);
        }

        /// <summary>
        /// 删除咨询回复信息
        /// </summary>
        /// <param name="id">
        /// 删除的Id
        /// </param>
        /// <returns>
        /// 成功删除对象的ID
        /// </returns>
        public int RemoveConsultReply(int id)
        {
            return this.productConsultDA.DeleteConsultReply(id);
        }

        /// <summary>
        /// 修改咨询的回复信息
        /// </summary>
        /// <param name="reply">
        /// 咨询对象
        /// </param>
        public void ModifyConsultReply(Product_Consult reply)
        {
            this.productConsultDA.UpdateConsultReply(reply);
        }

        /// <summary>
        /// 查询一级商品分类信息
        /// </summary>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Category> QueryParentProductCategories()
        {
            var productCategoryDa = new DAFactoryProduct().CreateProductCategoryDA();
            return productCategoryDa.SelectCategoryByParentID(0);
        }

        /// <summary>
        /// 根据一级类别ID查询子类别信息
        /// </summary>
        /// <param name="parentId">
        /// 一级类别Id
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Category> QuerySubCategoriesByParentId(int parentId)
        {
            var productCategoryDa = new DAFactoryProduct().CreateProductCategoryDA();
            return productCategoryDa.SelectCategoryByParentID(parentId);
        }

        /// <summary>
        /// 根据商品类别查询品牌信息
        /// </summary>
        /// <param name="categoryId">
        /// 类别ID
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Brand> QueryProductBrandByCategoryId(int categoryId)
        {
            var productBrandDa = new DAFactoryProduct().CreateProductBrandDA();
            return productBrandDa.SelectProductBrandByCategoryID(categoryId, 0);
        }

        /// <summary>
        /// 根据父品牌编码查询子品牌
        /// </summary>
        /// <param name="parentId">
        /// 父品牌编码
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Brand> QuerySubProductBrandByParentId(int parentId)
        {
            var productBrandDa = new DAFactoryProduct().CreateProductBrandDA();
            return productBrandDa.SelectProductBrandByParentID(parentId);
        }

        /// <summary>
        /// 分页查询商品咨询回复信息
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Consult> QueryConsultReplies(Paging paging, out int pageCount, out int totalCount)
        {
            var list = HttpRuntime.Cache["ProductConsult"] as List<Product_Consult>;
            if (list == null)
            {
                var productConsultDa = new DAFactoryTransact().CreateProductConsultDA();
                return productConsultDa.PagingReplies(paging, out pageCount, out totalCount);
            }

            var productID = int.Parse(paging.Condition.Substring(11));
            list = (from consult in list where consult.ProductID == productID orderby consult.CreateTime descending select consult).ToList();
            totalCount = list.Count;
            pageCount = totalCount / paging.PageSize;
            return list.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
        }

        /// <summary>
        /// 分页未回复商品咨询
        /// </summary>
        /// <param name="paging">
        /// 分页对象
        /// </param>
        /// <param name="pageCount">
        /// 页数
        /// </param>
        /// <param name="totalCount">
        /// 行数
        /// </param>
        /// <returns>
        /// 查询结果
        /// </returns>
        public List<Product_Consult> QueryConsult(Paging paging, out int pageCount, out int totalCount)
        {
            var productConsultDa = new DAFactoryTransact().CreateProductConsultDA();
            return productConsultDa.QueryConsult(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// 添加商品咨询.
        /// </summary>
        /// <param name="productConsult">
        /// Product_Consult.
        /// </param>
        /// <returns>
        /// 咨询的编号.
        /// </returns>
        public int Add(Product_Consult productConsult)
        {
            return this.productConsultDA.Insert(productConsult);
        }
        
        /// <summary>
        /// 查询所有商品咨询.
        /// </summary>
        /// <returns>
        /// 商品咨询列表.
        /// </returns>
        public List<Product_Consult> QueryAll()
        {
            return this.productConsultDA.SelectAll();
        }

        #endregion
    }
}