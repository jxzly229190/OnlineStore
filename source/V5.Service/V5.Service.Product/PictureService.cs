// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   图片业务类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Product
{
    using System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.Product;
    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 图片业务类.
    /// </summary>
    public class PictureService
    {
        #region Constants and Fields

        /// <summary>
        /// The product category da.
        /// </summary>
        private readonly IPictureDA pictureDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureService"/> class.
        /// </summary>
        public PictureService()
        {
            this.pictureDA = new DAFactoryProduct().CreatePictureDA();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 添加图片.
        /// </summary>
        /// <param name="picture">
        /// 图片实体
        /// </param>
        /// <returns>
        /// 添加记录的主键值.
        /// </returns>
        public int AddPicture(Picture picture)
        {
            return this.pictureDA.Insert(picture);
        }

        /// <summary>
        /// The query.
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
        /// The <see cref="List"/>.
        /// </returns>
        public List<Picture> Query(Paging paging, out int pageCount, out int totalCount)
        {
            return this.pictureDA.Paging(paging, out pageCount, out totalCount);
        }

        /// <summary>
        /// The modify picture category.
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
        public void ModifyPictureCategory(int pictureID, string parentCategoryID, string productCategoryID, string parentBrandID, string productBrandID)
        {
            this.pictureDA.UpdatePictureCategory(pictureID, parentCategoryID, productCategoryID, parentBrandID, productBrandID);
        }

        /// <summary>
        /// 根据查看预览图片信息.
        /// </summary>
        /// <param name="pictureID">
        /// The picture id.
        /// </param>
        /// <returns>
        /// The .
        /// </returns>
        public List<Picture> QueryPreviewByPictureID(int pictureID)
        {
            return this.pictureDA.SelectPreviewByPictureID(pictureID);
        }

        /// <summary>
        /// 得到修改商品时商品图片的信息.
        /// </summary>
        /// <param name="productID">
        /// 商品ID.
        /// </param>
        /// <returns>
        /// 图片列表.
        /// </returns>
        public List<Picture> QueryPictureByProductID(int productID)
        {
            return this.pictureDA.SelectPictureByProductID(productID);
        }

        /// <summary>
        /// 根据图片ID做删除.
        /// </summary>
        /// <param name="pictureID">
        /// 图片ID.
        /// </param>
        public void RemovePictureByID(int pictureID)
        {
            this.pictureDA.DeleteByID(pictureID);
        }

        /// <summary>
        /// 修改图片信息.
        /// </summary>
        /// <param name="picture">
        /// 图片实体.
        /// </param>
        public void ModifyPicture(Picture picture)
        {
            this.pictureDA.Update(picture);
        }

        #endregion
    }
}
