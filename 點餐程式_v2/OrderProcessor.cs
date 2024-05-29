using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 點餐程式_v2
{
    public class OrderProcessor
    {

        private List<OrderModel> orderList;
        private readonly RecommendationProcessor recommendationProcessor;
        private readonly DiscountProcessor discountProcessor;

        public OrderProcessor()
        {
            orderList = new List<OrderModel>();
            discountProcessor = new DiscountProcessor();
            recommendationProcessor = new RecommendationProcessor();
        }

        public void Order(OrderModel newOrder)
        {
            var currentOrder = orderList.FirstOrDefault(order => order.Name == newOrder.Name);

            if (currentOrder != null)
            {
                if (newOrder.TotalQuantity == 0)
                    orderList.Remove(currentOrder);
                else
                {
                    currentOrder.PaidQuantity = newOrder.PaidQuantity;
                    currentOrder.FreeQuantity = newOrder.FreeQuantity;
                }
            }
            else
            {
                if (newOrder.TotalQuantity > 0)
                    orderList.Add(newOrder);
            }

            recommendationProcessor.GiveRecommendation(orderList);
        }

        public void ApplyDiscountMode(string discountMode)
        {
            discountProcessor.ChooseDiscount(discountMode);
            discountProcessor.ApplyDiscount(orderList);
        }
    }
}
