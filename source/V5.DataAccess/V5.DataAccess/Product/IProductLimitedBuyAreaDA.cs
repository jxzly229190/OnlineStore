using V5.DataContract.Product;
using System.Collections.Generic;

namespace V5.DataAccess.Product
{
    public interface IProductLimitedBuyAreaDA
    {
        int Insert(Product_LimitedBuy_Area model);

        List<Product_LimitedBuy_Area> SelectByProductId(int productId);

        int UpdateByProductId(string area, int productId);

        int BatchInsert(int[] id, string content);
    }
}