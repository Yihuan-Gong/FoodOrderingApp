using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class RecommendationProcessor
    {
        readonly IndicationUnit indicationUnit;

        public RecommendationProcessor()
        {
            indicationUnit = new IndicationUnit();
        }

        // 在所有折扣方案中，找到價錢最低的方案，推薦給使用者
        public void GiveRecommendation(List<OrderModel> orderList)
        {
            int minTotalPrice = int.MaxValue;
            string minTotalPriceStrategy = string.Empty;
            var minTotalPriceOrderList = new List<OrderModel>();

            foreach (var strategy in Form1.strategyListModel.StrategyList)
            {
                List<OrderModel> newOrderList = orderList.Clone();
                DiscountStrategy discountStrategy = DiscountStrategyFactory.CreateStrategy(strategy);

                discountStrategy.ApplyDiscount(newOrderList);

                int totalPrice = newOrderList.Sum(x => x.SubTotal);

                if (totalPrice < minTotalPrice)
                {
                    minTotalPrice = totalPrice;
                    minTotalPriceStrategy = strategy.Name;
                    minTotalPriceOrderList = newOrderList;
                }
            }

            indicationUnit.ShowOrderList(minTotalPriceOrderList);
            indicationUnit.ShowDiscountMode(minTotalPriceStrategy);
        }
    }
}
