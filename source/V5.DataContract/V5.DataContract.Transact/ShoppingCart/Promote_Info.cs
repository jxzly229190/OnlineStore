namespace V5.DataContract.Transact.ShoppingCart
{
    /// <summary>
    /// 购物车中的促销信息.
    /// </summary>
    public class Promote_Info
    {
        /// <summary>
        /// 获取或设置促销编号.
        /// </summary>
        public int PromoteID { get; set; }

        /// <summary>
        /// （1:MeetMoney,2:MeetAmount）
        /// </summary>
        public int PromoteType { get; set; }
        
        public double TotalPrice { get; set; }

        public int TotalQuantity { get; set; }

    }
}
