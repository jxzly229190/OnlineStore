// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureDA.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   图片数据库访问类.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    /// <summary>
    /// 图片数据库访问类.
    /// </summary>
    public class PictureDA : IPictureDA
    {
        #region Constants and Fields

        /// <summary>
        /// 数据库访问对象.
        /// </summary>
        private SqlServer sqlServer;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取数据库操作对象
        /// </summary>
        public SqlServer SqlServer
        {
            get
            {
                return this.sqlServer ?? (this.sqlServer = new SqlServer());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 插入图片.
        /// </summary>
        /// <param name="picture">
        /// 图片实体.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Insert(Picture picture)
        {
            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }
            
            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentCategoryID",
                                         SqlDbType.Int,
                                         picture.ParentCategoryID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         picture.ProductCategoryID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         picture.ParentBrandID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         picture.ProductBrandID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Name",
                                         SqlDbType.NVarChar,
                                         picture.Name,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Type",
                                         SqlDbType.NVarChar,
                                         picture.Type,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Path",
                                         SqlDbType.NVarChar,
                                         picture.Path,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ThumbnailPath",
                                         SqlDbType.NVarChar,
                                         picture.ThumbnailPath,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "FileName",
                                         SqlDbType.NVarChar,
                                         picture.FileName,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Size",
                                         SqlDbType.Int,
                                         picture.Size,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Height",
                                         SqlDbType.Int,
                                         picture.Height,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Width",
                                         SqlDbType.Int,
                                         picture.Width,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "Status",
                                         SqlDbType.Int,
                                         picture.Status,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UploadTime",
                                         SqlDbType.DateTime,
                                         picture.UploadTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "CreateTime",
                                         SqlDbType.DateTime,
                                         picture.CreateTime,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Picture_Insert", parameters, null);
                id = (int)parameters[11].Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - PictureDA - Insert", exception);
            }

            return id;
        }

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
        public List<Picture> Paging(Paging paging, out int pageCount, out int totalCount)
        {
            return this.SqlServer.Paging<Picture>(paging, out pageCount, out totalCount, null);
        }

        /// <summary>
        /// 根据查看预览图片信息.
        /// </summary>
        /// <param name="pictureID">
        /// 图片 id.
        /// </param>
        /// <returns>
        /// The .
        /// </returns>
        public List<Picture> SelectPreviewByPictureID(int pictureID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "pictureID",
                                         SqlDbType.Int,
                                         pictureID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Picture_SelectPreview", parameters, null);
                var list = dataReader.ToList<Picture>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - PictureDA - SelectPreview", exception);
            }

            return null;
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
        public List<Picture> SelectPictureByProductID(int productID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "productID",
                                         SqlDbType.Int,
                                         productID,
                                         ParameterDirection.Input)
                                 };
            try
            {
                var dataReader = this.SqlServer.ExecuteDataReader(CommandType.StoredProcedure, "sp_Picture_SelectByProductID", parameters, null);
                var list = dataReader.ToList<Picture>();
                if (list.Count > 0)
                {
                    return list;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - PictureDA - SelectPictureByProductID", exception);
            }

            return null;
        }

        /// <summary>
        /// 修改图片信息.
        /// </summary>
        /// <param name="picture">
        /// 图片实体.
        /// </param>
        public void Update(Picture picture)
        {
            throw new NotImplementedException();
        }

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
        public void UpdatePictureCategory(
            int pictureID,
            string parentCategoryID,
            string productCategoryID,
            string parentBrandID,
            string productBrandID)
        {
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.NVarChar,
                                         pictureID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ParentCategoryID",
                                         SqlDbType.Int,
                                         parentCategoryID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ProductCategoryID",
                                         SqlDbType.Int,
                                         productCategoryID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ParentBrandID",
                                         SqlDbType.Int,
                                         parentBrandID,
                                         ParameterDirection.Input),
                                         this.SqlServer.CreateSqlParameter(
                                         "ProductBrandID",
                                         SqlDbType.Int,
                                         productBrandID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_Picture_UpdatePictureCategory]", parameters, null);
        }

        /// <summary>
        /// 根据图片ID做删除.
        /// </summary>
        /// <param name="pictureID">
        /// 图片ID.
        /// </param>
        public void DeleteByID(int pictureID)
        {
            if (pictureID <= 0)
            {
                throw new ArgumentNullException("pictureID");
            }

            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "ID",
                                         SqlDbType.Int,
                                         pictureID,
                                         ParameterDirection.Input)
                                 };

            this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Picture_DeleteRow", parameters, null);
        }

        #endregion
    }
}