using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class DiscountStrategyFactory
    {
        public static DiscountStrategy CreateStrategy(StrategyListModel.Strategy strategy)
        {
            switch (strategy.Type)
            {
                case "DiscountBonusGift":
                    return new DiscountBonusGift
                    (
                        strategy.Condition.Require.Select(x => new NameQuantityPair(x.Name, x.Quantity)).ToList(),
                        strategy.Condition.BonusGift.Select(x => new NameQuantityPair(x.Name, x.Quantity)).ToList()
                    );

                case "DiscountCombination":
                    return new DiscountCombination
                    (
                        (int)strategy.Condition.CombinationPrice,
                        strategy.Condition.CombinationName,
                        strategy.Condition.Require.Select(x => new NameQuantityPair(x.Name, x.Quantity)).ToList()
                    );

                case "DiscountReturn":
                    return new DiscountReturn
                    (
                        (int)strategy.Condition.RequiredTotalPrice,
                        (int)strategy.Condition.DiscountValue
                    );

                case "DiscountRebate":
                    return new DiscountRebate((double)strategy.Condition.PriceFactor);

                default:
                    return new DiscountNone();
            }
        }
    }
}
