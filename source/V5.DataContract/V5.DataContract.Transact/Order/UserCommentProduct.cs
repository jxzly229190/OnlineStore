namespace V5.DataContract.Transact.Order
{
    using System;

    public class UserCommentProduct
    {
        public int CommentID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int EmployeeID { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string Path { get; set; }
        public DateTime TransactTime { get; set; }
    }
}