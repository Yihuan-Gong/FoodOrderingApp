using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class StrategyListModel
    {

        public Strategy[] StrategyList { get; set; }

        public class Strategy
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public Condition Condition { get; set; }
        }

        public class Condition
        {
            public Require[] Require { get; set; }
            public BonusGift[] BonusGift { get; set; }
            public int? CombinationPrice { get; set; }
            public string CombinationName { get; set; }
            public int? RequiredTotalPrice { get; set; }
            public int? DiscountValue { get; set; }
            public float? PriceFactor { get; set; }
        }

        public class Require
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        public class BonusGift
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }



    }
}
