using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 點餐程式_v2
{
    public class NameQuantityPair
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public NameQuantityPair(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

    }
}
