using Orders.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities
{
    public class Order
    {
        /// <summary>
        /// Список предметов.
        /// </summary>
        public List<OrderItem> Items { get; set; }

        /// <summary>
        /// Номер заказа.
        /// </summary>
        public ulong OrderNumber { get; set; }
        
        /// <summary>
        /// Дата и время создания заказа.
        /// </summary>
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// Статус заказа.
        /// </summary>
        public OrderStatus Status { get; set; }
        
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Конструктор заказа.
        /// </summary>
        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
