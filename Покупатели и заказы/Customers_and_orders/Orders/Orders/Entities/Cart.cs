using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities
{
    public class Cart
    {
        /// <summary>
        /// Список заказов. 
        /// </summary>
        public List<OrderItem> Items { get; set; }

        /// <summary>
        /// Номер пользоватлея. 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Конструктор заказа. 
        /// </summary>
        public Cart()
        {
            Items = new List<OrderItem>();
        }
    }
}
