using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountReturn : DiscountStrategy
    {
        int requiredTotalPrice;
        int discountValue;
        string discountName = "折扣";

        public DiscountReturn(int requiredTotalPrice, int discountValue)
        {
            this.requiredTotalPrice = requiredTotalPrice;
            this.discountValue = discountValue;
        }

        public override List<OrderModel> ApplyDiscount(List<OrderModel> orderList)
        {
            // 檢查是否達折扣標準
            int totalPrice = orderList.Select(order => order.SubTotal).Sum();
            if (totalPrice < requiredTotalPrice)
                return orderList;

            // 計算折扣數量
            int discountQuantity = (int)Math.Floor((float)totalPrice / requiredTotalPrice);

            // 將折扣加入orderList
            orderList.Add(new OrderModel
            {
                Name = discountName,
                Price = -1 * discountValue,
                PaidQuantity = discountQuantity,
                FreeQuantity = 0
            });

            return orderList;
        }
    }
}
