namespace V5.Portal.Models
{
    using System;

    public class CartProduct
    {
        /*
          <span class="cb_r2">单价</span>
    <span class="cb_r3">优惠</span>
    <span class="cb_r4">数量</span>
    <span class="cb_r5">小计</span>
    <span class="cb_r6">操作</span>
         */

        public int ID { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductPic { get; set; }

        public double GoujiuPrice { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public double TotalMoney {
            get
            {
                return Quantity * GoujiuPrice;
            }
        }

        public DateTime UpdateTime { get; set; }
    }
}