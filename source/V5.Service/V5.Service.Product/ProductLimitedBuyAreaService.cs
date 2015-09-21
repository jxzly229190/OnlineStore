using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataAccess;
using V5.DataAccess.Product;
using V5.DataContract.Product;

namespace V5.Service.Product
{
    public class ProductLimitedBuyAreaService
    {
        private IProductLimitedBuyAreaDA productLimited;

        /// <summary>
        /// 
        /// </summary>
        public ProductLimitedBuyAreaService()
        {
            productLimited = new DAFactoryProduct().CreateProductLimitedBuyAreaDA();
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Product_LimitedBuy_Area model)
        {
            return productLimited.Insert(model);
        }

        /// <summary>
        /// 根据productId查询记录
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Product_LimitedBuy_Area> SelectByProductId(int productId)
        {
            return productLimited.SelectByProductId(productId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int UpdateByProductId(string area, int productId)
        {
            return productLimited.UpdateByProductId(area, productId);
        }
    }
}
