using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities
{
    public class Product
    {
        /// <summary>
        /// Название продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Артикул продукта.
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Стоимость продукта.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Проверка на удаление.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
