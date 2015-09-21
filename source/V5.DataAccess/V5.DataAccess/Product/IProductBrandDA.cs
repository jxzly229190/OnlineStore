using V5.DataContract.Transact;

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    public interface IProductBrandDA
    {
        /// <summary>
        /// 添加商品品牌
        /// </summary>
        /// <param name="category">
        /// 商品品牌实体
        /// </param>
        /// <returns>
        /// 新增数据主键值
        /// </returns>
        int Insert(Product_Brand brand);

        /// <summary>
        /// 根据类别获取品牌信息.
        /// </summary>
        /// <param name="categoryID">
        /// 品牌ID.
        /// </param>
        /// <returns>
        /// 品牌列表.
        /// </returns>
        List<Product_Brand> SelectProductBrandByCategoryID(int categoryID, int parentID);

        /// <summary>
        /// 根据父ID获取品牌信息.
        /// </summary>
        /// <param name="parentID">
        /// 父ID.
        /// </param>
        /// <returns>
        /// 品牌列表.
        /// </returns>
        List<Product_Brand> SelectProductBrandByParentID(int parentID);

        List<Product_Brand> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// 修改品牌信息.
        /// </summary>
        /// <param name="category">
        /// 品牌实体.
        /// </param>
        void Update(Product_Brand brand);

        /// <summary>
        /// 删除类别.
        /// </summary>
        /// <param name="categoryID">
        /// 商品品牌ID.
        /// </param>
        void DeleteByID(int brandID);

        /// <summary>
        /// 商品大类品牌树（大类、品牌（一级品牌））
        /// </summary>
        /// <returns></returns>
        List<Product_Tree> SelectBrandTree();

        Product_Brand SelectById(int id);
        
        List<Product_Category_Brand> SelectProductCategoryBrandAll();
    }
}