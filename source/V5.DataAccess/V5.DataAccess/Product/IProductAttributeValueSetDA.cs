// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductAttributeValueSetDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ProductAttributeValueSetDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;
    using global::System.Data.SqlClient;

    using V5.DataContract.Product;

    /// <summary>
    /// The ProductAttributeValueSetDA interface.
    /// </summary>
    public interface IProductAttributeValueSetDA
    {
        void Insert(Product_AttributeValueSet productAttributeValueSet, SqlTransaction transaction);

        void Delete(int productID, SqlTransaction transaction);

        List<Product_AttributeValueSet> SelectByProductID(int productID);
    }
}