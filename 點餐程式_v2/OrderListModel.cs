using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class OrderListModel
    {
        public List<OrderModel> orderList;
        public int totalPrice;

        public OrderListModel(List<OrderModel> orderList)
        {
            this.orderList = orderList;
            this.totalPrice = CalculateTotalPrice();
        }

        private int CalculateTotalPrice()
        {
            int totalPrice = 0;

            foreach (OrderModel order in orderList)
            {
                totalPrice += order.SubTotal;
            }

            return totalPrice;
        }
    }
}
