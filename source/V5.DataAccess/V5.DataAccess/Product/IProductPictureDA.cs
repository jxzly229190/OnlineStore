// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductPictureDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ProductPictureDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;

    /// <summary>
    /// The ProductPictureDA interface.
    /// </summary>
    public interface IProductPictureDA
    {
        void Insert(Product_Picture productPicture, SqlTransaction transaction);

        void Delete(int productID, SqlTransaction transaction);

        void Insert(Product_Picture productPicture);

        void Delete(int productID);

        List<Product_Picture> SelectByProductID(int productID);
    }
}
