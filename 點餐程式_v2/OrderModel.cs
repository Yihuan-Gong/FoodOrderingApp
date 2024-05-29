using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class OrderModel : ICloneable
    {
        public string Name { get; set; }

        public int TotalQuantity
        {
            get { return PaidQuantity + FreeQuantity; }
        }

        public int PaidQuantity { get; set; }

        public int FreeQuantity { get; set; }

        public int Price { get; set; }

        public int SubTotal
        {
            get { return PaidQuantity * Price; }
        }

        public string Note { get; set; }

        public object Clone()
        {
            return new OrderModel
            {
                Name = Name,
                PaidQuantity = PaidQuantity,
                FreeQuantity = FreeQuantity,
                Price = Price,
            };
        }
    }


}
