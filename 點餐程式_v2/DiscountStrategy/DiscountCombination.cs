using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountCombination : DiscountStrategy
    {
        readonly int combinationPrice;
        readonly string combinationName;
        readonly List<NameQuantityPair> buyList;


        public DiscountCombination(int price, string combinationName, List<NameQuantityPair> buyList)
        {
            this.buyList = buyList;
            this.combinationName = combinationName;
            this.combinationPrice = price;
        }

        public override List<OrderModel> ApplyDiscount(List<OrderModel> orderList)
        {
            var combinationList = new List<OrderModel>();

            // 檢查套餐內的所有餐點是否都有買齊
            foreach (NameQuantityPair buyItem in buyList)
            {
                OrderModel order = orderList.FirstOrDefault(x => x.Name == buyItem.Name);

                if (order == null)
                    return orderList;
                else if (order.TotalQuantity < buyItem.Quantity)
                    return orderList;
                else
                    combinationList.Add(order);
            }

            // 計算套餐數量
            int combinationQuantity = combinationList.Min(x => x.PaidQuantity);

            // 將單點組合成套餐
            orderList.Add(new OrderModel
            {
                Name = combinationName,
                Price = combinationPrice,
                PaidQuantity = combinationQuantity,
                FreeQuantity = 0
            });

            // 將被組成套餐的單點從orderList中移除
            foreach (OrderModel order in combinationList)
            {
                order.PaidQuantity -= combinationQuantity;

                if (order.PaidQuantity == 0)
                    orderList.Remove(order);
            }

            return orderList;
        }
    }
}
