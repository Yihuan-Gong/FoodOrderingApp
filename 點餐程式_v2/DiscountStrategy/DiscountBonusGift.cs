using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountBonusGift : DiscountStrategy
    {
        readonly List<NameQuantityPair> buyList;
        readonly List<NameQuantityPair> giftList;

        public DiscountBonusGift(List<NameQuantityPair> buyList, List<NameQuantityPair> giftList)
        {
            this.buyList = buyList;
            this.giftList = giftList;
        }

        public override List<OrderModel> ApplyDiscount(List<OrderModel> orderList)
        {
            var giftQuantityFactorList = new List<int>();
            
            // 檢查該買的有沒有齊全
            foreach (var buyItem in buyList)
            {
                OrderModel order = orderList.FirstOrDefault(x => x.Name == buyItem.Name);

                if (order == null)
                    return orderList;
                else if (order.TotalQuantity < buyItem.Quantity)
                    return orderList;
                else
                    giftQuantityFactorList.Add(order.PaidQuantity / buyItem.Quantity);
            }

            // 計算贈品數量
            int giftQuantityFactor = giftQuantityFactorList.Min();

            // 將贈品放到orderList上面
            foreach (var giftItem in giftList)
            {
                orderList.Add(new OrderModel
                {
                    Name = giftItem.Name,
                    PaidQuantity = 0,
                    FreeQuantity = giftQuantityFactor * giftItem.Quantity,
                    Price = 0,
                    Note = "贈送"
                });
            }

            return orderList;
        }
    }
}
