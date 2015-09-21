// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPictureDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   图片数据库访问接口
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 图片数据库访问接口
    /// </summary>
    public interface IPictureDA
    {
        #region Public Methods and Operators

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="picture">
        /// The picture.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Insert(Picture picture);

        /// <summary>
        /// The paging.
        /// </summary>
        /// <param name="paging">
        /// The paging.
        /// </param>
        /// <param name="pageCount">
        /// The page count.
        /// </param>
        /// <param name="totalCount">
        /// The total count.
        /// </param>
        /// <returns>
        /// The.
        /// </returns>
        List<Picture> Paging(Paging paging, out int pageCount, out int totalCount);

        /// <summary>
        /// The update picture category.
        /// </summary>
        /// <param name="pictureID">
        /// The picture id.
        /// </param>
        /// <param name="parentCategoryID">
        /// The parent category id.
        /// </param>
        /// <param name="productCategoryID">
        /// The product category id.
        /// </param>
        /// <param name="parentBrandID">
        /// The parent brand id.
        /// </param>
        /// <param name="productBrandID">
        /// The product brand id.
        /// </param>
        void UpdatePictureCategory(int pictureID, string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID);

        /// <summary>
        /// The select preview by picture id.
        /// </summary>
        /// <param name="pictureId">
        /// The picture id.
        /// </param>
        /// <returns>
        /// The .
        /// </returns>
        List<Picture> SelectPreviewByPictureID(int pictureId);

        /// <summary>
        /// The select picture by product id.
        /// </summary>
        /// <param name="productID">
        /// The product id.
        /// </param>
        /// <returns>
        /// The .
        /// </returns>
        List<Picture> SelectPictureByProductID(int productID);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="brand">
        /// The brand.
        /// </param>
        void Update(Picture brand);

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="brandID">
        /// The brand id.
        /// </param>
        void DeleteByID(int brandID);

        #endregion
    }
}