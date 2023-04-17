using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities
{
    public class OrderItem: Product
    {
        /// <summary>
        /// Счётчик. 
        /// </summary>
        public int Count { get; set; }
    }
}
