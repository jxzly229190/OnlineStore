// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryProduct.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.Product;

    /// <summary>
    /// 商品模块数据访问工厂类
    /// </summary>
    public class DAFactoryProduct : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryProduct"/> class.
        /// </summary>
        public DAFactoryProduct()
        {
            this.AssemblyPath = this.AssemblyPath + ".Product";
        }

        /// <summary>
        /// The create product da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductDA"/>.
        /// </returns>
        public IProductDA CreateProductDA()
        {
            string nameSpace = AssemblyPath + ".ProductDA";
            object productDA = Create(AssemblyPath, nameSpace);
            return (IProductDA)productDA;
        }

        /// <summary>
        /// The create product category da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductCategoryDA"/>.
        /// </returns>
        public IProductCategoryDA CreateProductCategoryDA()
        {
            string nameSpace = AssemblyPath + ".ProductCategoryDA";
            object productCategoryDA = Create(AssemblyPath, nameSpace);
            return (IProductCategoryDA)productCategoryDA;
        }

        /// <summary>
        /// The create product brand da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductBrandDA"/>.
        /// </returns>
        public IProductBrandDA CreateProductBrandDA()
        {
            string nameSpace = AssemblyPath + ".ProductBrandDA";
            object productBrandDA = Create(AssemblyPath, nameSpace);
            return (IProductBrandDA)productBrandDA;
        }

        /// <summary>
        /// The create picture da.
        /// </summary>
        /// <returns>
        /// The <see cref="IPictureDA"/>.
        /// </returns>
        public IPictureDA CreatePictureDA()
        {
            string nameSpace = AssemblyPath + ".PictureDA";
            object pictureDA = Create(AssemblyPath, nameSpace);
            return (IPictureDA)pictureDA;
        }

        /// <summary>
        /// The create product attribute da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductAttributeDA"/>.
        /// </returns>
        public IProductAttributeDA CreateProductAttributeDA()
        {
            string nameSpace = AssemblyPath + ".ProductAttributeDA";
            object productAttributeDA = Create(AssemblyPath, nameSpace);
            return (IProductAttributeDA)productAttributeDA;
        }

        /// <summary>
        /// The create product attribute value da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductAttributeValueDA"/>.
        /// </returns>
        public IProductAttributeValueDA CreateProductAttributeValueDA()
        {
            string nameSpace = AssemblyPath + ".ProductAttributeValueDA";
            object productAttributeValueDA = Create(AssemblyPath, nameSpace);
            return (IProductAttributeValueDA)productAttributeValueDA;
        }

        /// <summary>
        /// The create product attribute value set da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductAttributeValueSetDA"/>.
        /// </returns>
        public IProductAttributeValueSetDA CreateProductAttributeValueSetDA()
        {
            string nameSpace = AssemblyPath + ".ProductAttributeValueSetDA";
            object productAttributeValueSetDA = Create(AssemblyPath, nameSpace);
            return (IProductAttributeValueSetDA)productAttributeValueSetDA;
        }

        /// <summary>
        /// The create product picture da.
        /// </summary>
        /// <returns>
        /// The <see cref="IProductPictureDA"/>.
        /// </returns>
        public IProductPictureDA CreateProductPictureDA()
        {
            string nameSpace = AssemblyPath + ".ProductPictureDA";
            object productPictureDA = Create(AssemblyPath, nameSpace);
            return (IProductPictureDA)productPictureDA;
        }
        /// <summary>
        /// 创建品牌信息数据层对象
        /// </summary>
        /// <returns></returns>
        public IBrandInformationDA CreateBrandDescriptionDA()
        {
            string nameSpace = AssemblyPath + ".BrandInformationDA";
            object brandDescriptionDA = Create(AssemblyPath, nameSpace);
            return (IBrandInformationDA)brandDescriptionDA;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IProductLimitedBuyAreaDA CreateProductLimitedBuyAreaDA()
        {
            string nameSpace = AssemblyPath + ".ProductLimitedBuyAreaDA";
            object productLimitedBuyAreaDA = Create(AssemblyPath, nameSpace);
            return (IProductLimitedBuyAreaDA)productLimitedBuyAreaDA;
        }
    }
}