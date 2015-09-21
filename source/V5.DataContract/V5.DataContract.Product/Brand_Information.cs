namespace V5.DataContract.Product
{
    /// <summary>
    /// 品牌描述
    /// </summary>
    public class Brand_Information
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// 品牌描述
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 品牌介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductID { get; set; }
    }
}
