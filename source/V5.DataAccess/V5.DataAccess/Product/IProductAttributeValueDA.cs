namespace V5.DataAccess.Product
{
    using global::System.Collections.Generic;

    using V5.DataContract.Product;
    using V5.Library.Storage.DB;

    public interface IProductAttributeValueDA
    {
        int Insert(Product_AttributeValue productAttributeValue);

        List<Product_AttributeValue> Paging(int pageIndex, int pageSize, List<string> columns, string condition, string orderBy, out int totalCount);

        List<Product_AttributeValue> Paging(Paging paging, out int pageCount, out int totalCount);

        void Update(Product_AttributeValue productAttributeValue);

        void DeleteByID(int productAttributeValueID);
    }
}