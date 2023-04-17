using Newtonsoft.Json;
using Orders.Entities;
using Orders.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carts.Managers
{
    public class CartManager
    {
        private static readonly string cartStorageFileName = "carts.json";

        /// <summary>
        /// Получает корзину заказов пользователя.
        /// </summary>
        /// <param name="userId">Айди пользователя.</param>
        /// <returns>Корзина заказов.</returns>
        public Cart GetUserCart(string userId)
        {
            var carts = GetCartsFromFile();
            var cart = carts.FirstOrDefault(c => c.UserId.ToLower() == userId.ToLower());
            if (cart is null)
            {
                cart = new Cart()
                {
                    Items = new List<OrderItem>(),
                    UserId = userId
                };
                SaveCart(cart);
            }
            if (cart.Items is null)
            {
                cart.Items = new List<OrderItem>();
            }
            RefreshPriceCart(cart);
            return cart;
        }

        /// <summary>
        /// Изменяет стоимость корзины заказов.
        /// </summary>
        /// <param name="cart">Корзина заказов.</param>
        /// <returns>Корзина заказов.</returns>
        public Cart RefreshPriceCart(Cart cart)
        {
            var productManager = new ProductManager();
            var existingProduct = productManager.AllProducts;
            foreach (var item in cart.Items)
            {
                var product = existingProduct.FirstOrDefault(p => p.Article == item.Article);
                if (product is null) continue;
                item.Price = product.Price;
            };
            SaveCart(cart);

            return cart;
        }

        /// <summary>
        /// Сохраняет корзину заказов.
        /// </summary>
        /// <param name="cart">Корзина заказов.</param>
        public void SaveCart(Cart cart)
        {
            var carts = GetCartsFromFile();

            var cartToUpdate = carts.FirstOrDefault(c => c.UserId.ToLower() == cart.UserId.ToLower());
            if (!(cartToUpdate is null))
            {
                carts.Remove(cartToUpdate);
            }
            
            carts.Add(cart);
            try
            {
                string output = JsonConvert.SerializeObject(carts);
                File.WriteAllText(cartStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save cart");
            }
        }

        /// <summary>
        /// Удаляет корзину заказов.
        /// </summary>
        /// <param name="cart">Корзина заказов.</param>
        public void DeleteCart(Cart cart)
        {
            var carts = GetCartsFromFile();

            var cartToUpdate = carts.FirstOrDefault(c => c.UserId.ToLower() == cart.UserId.ToLower());
            if (cartToUpdate is null)
            {
                return;
            }
            carts.Remove(cartToUpdate);
            try
            {
                string output = JsonConvert.SerializeObject(carts);
                File.WriteAllText(cartStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't delete cart");
            }
        }

        /// <summary>
        /// Получает корзины заказов из файла.
        /// </summary>
        /// <returns>Список корзин.</returns>
        private List<Cart> GetCartsFromFile()
        {
            var carts = new List<Cart>();
            if (File.Exists(cartStorageFileName))
            {
                try
                {
                    var content = File.ReadAllText(cartStorageFileName, Encoding.UTF8);
                    carts = JsonConvert.DeserializeObject<List<Cart>>(content);
                }
                catch
                {
                    throw new ApplicationException("Can't get carts");
                }
            }
            return carts;
        }
    }
}