using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountProcessor
    {
        private IndicationUnit indicationUnit;
        private DiscountStrategy discountStrategy;

        public DiscountProcessor()
        {
            indicationUnit = new IndicationUnit();
            discountStrategy = new DiscountNone(); // Default: 無折扣
        }

        public void ChooseDiscount(string discountMode)
        {
            StrategyListModel strategyListModel = Form1.strategyListModel;

            foreach (StrategyListModel.Strategy strategy in strategyListModel.StrategyList)
            {
                if (strategy.Name == discountMode)
                {
                    discountStrategy = DiscountStrategyFactory.CreateStrategy(strategy);
                }
            }
        }

        public void ApplyDiscount(List<OrderModel> orderList)
        {
            List<OrderModel> newOrderList = orderList.Clone();

            discountStrategy.ApplyDiscount(newOrderList);
            indicationUnit.ShowOrderList(newOrderList);
        }
    }
}
