using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public abstract class DiscountStrategy
    {
        public abstract List<OrderModel> ApplyDiscount(List<OrderModel> orderList);
    }
}
