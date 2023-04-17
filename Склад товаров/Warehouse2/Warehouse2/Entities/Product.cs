using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse2.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
