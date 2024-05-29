using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountRebate : DiscountStrategy
    {
        double priceFactor;

        public DiscountRebate(double priceFactor)
        {
            this.priceFactor = priceFactor;
        }

        public override List<OrderModel> ApplyDiscount(List<OrderModel> orderList)
        {
            orderList.ForEach(x => x.Price = (int)(x.Price * priceFactor));

            return orderList;
        }
    }
}
