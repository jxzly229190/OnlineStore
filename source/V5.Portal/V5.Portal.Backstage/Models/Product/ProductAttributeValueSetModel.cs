// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAttributeValueSetModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The product attribute value set model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    /// <summary>
    /// The product attribute value set model.
    /// </summary>
    public class ProductAttributeValueSetModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the attribute id.
        /// </summary>
        public int AttributeID { get; set; }

        /// <summary>
        /// Gets or sets the attribute value id.
        /// </summary>
        public int AttributeValueID { get; set; }

        #endregion
    }
}