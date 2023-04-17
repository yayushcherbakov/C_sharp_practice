using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Enums
{
    /// <summary>
    /// Перечисление статусов заказа.  
    /// </summary>
    [Flags]
    public enum OrderStatus
    {
        None = 0,
        Processed = 0b0_0001,
        Paid = 0b0_0010,
        Shipped = 0b0_0100,
        Completed = 0b0_1000,
    }
}
