using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class IndicationUnit
    {
        public static event EventHandler<OrderListModel> OrderChanged;
        public static event EventHandler<string> DiscountModeChanged;

        public void ShowOrderList(List<OrderModel> orderList)
        {
            OrderListModel orderListModel = new OrderListModel(orderList);

            OrderChanged.Invoke(this, orderListModel);
        }

        public void ShowDiscountMode(string discountMode)
        {
            DiscountModeChanged.Invoke(this, discountMode);
        }
    }
}
