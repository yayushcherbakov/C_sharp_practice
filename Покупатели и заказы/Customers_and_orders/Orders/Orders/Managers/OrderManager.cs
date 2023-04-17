using Newtonsoft.Json;
using Orders.Entities;
using Orders.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Managers
{
    public class OrderManager
    {
        private static readonly string orderStorageFileName = "orders.json";

        /// <summary>
        /// Список заказов.
        /// </summary>
        public List<Order> Orders
        {
            get
            {
                return GetOrdersFromFile();
            }
        }


        /// <summary>
        /// Получает заказы из файла.
        /// </summary>
        /// <returns>Список заказов.</returns>
        private List<Order> GetOrdersFromFile()
        {
            var orders = new List<Order>();
            if (File.Exists(orderStorageFileName))
            {
                try
                {
                    var content = File.ReadAllText(orderStorageFileName, Encoding.UTF8);
                    orders = JsonConvert.DeserializeObject<List<Order>>(content);
                }
                catch
                {
                    throw new ApplicationException("Can't get orders");
                }
            }
            return orders;
        }

        /// <summary>
        /// Создает заказ.
        /// </summary>
        /// <param name="cart">Корзина заказа.</param>
        public void CreateOrder(Cart cart)
        {
            if(cart.Items.Count == 0) throw new ApplicationException("Cart is empty");
            var newOrder = new Order();

            var orders = GetOrdersFromFile();
            newOrder.OrderNumber = orders.Count == 0 ? 1 : (orders.Select(o => o.OrderNumber).Max() + 1);
            newOrder.CreationDate = DateTime.Now;
            newOrder.UserId = cart.UserId;
            newOrder.Status = OrderStatus.None;
            newOrder.Items = cart.Items;

            try
            {
                orders.Add(newOrder);
                string output = JsonConvert.SerializeObject(orders);
                File.WriteAllText(orderStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save order");
            }
        }

        /// <summary>
        /// Изменяет статус заказа.
        /// </summary>
        /// <param name="order">Заказ.</param>
        /// <param name="status">Статусы.</param>
        public void ChangeStatusOrder(Order order, OrderStatus status)
        {
            var orders = GetOrdersFromFile();

            var currentTocheck = orders.FirstOrDefault(p => p.OrderNumber == order.OrderNumber);
            if (currentTocheck is null)
            {
                throw new ApplicationException("Can't find order");
            }

            currentTocheck.Status = currentTocheck.Status | status;

            try
            {
                string output = JsonConvert.SerializeObject(orders);
                File.WriteAllText(orderStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save order");
            }
        }

        /// <summary>
        /// Получает заказ покупателя.
        /// </summary>
        /// <param name="userId">Айди пользователя.</param>
        /// <returns>Список заказов.</returns>
        public List<Order> GetOrdersByBuyer(string userId)
        {
            return GetOrdersFromFile().Where(o => o.UserId == userId).ToList();
        }
    }
}
