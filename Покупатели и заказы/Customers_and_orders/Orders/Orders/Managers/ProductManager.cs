using Newtonsoft.Json;
using Orders.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Managers
{
    public class ProductManager
    {
        private static readonly string productStorageFileName = "products.json";

        /// <summary>
        /// Список продуктов.
        /// </summary>
        public List<Product> AllProducts
        {
            get
            {
                return GetProductsFromFile();
            }
        }

        /// <summary>
        /// Список активных продуктов.
        /// </summary>
        public List<Product> ActiveProducts
        {
            get
            {
                return GetProductsFromFile().Where(p => !p.IsDeleted).ToList();
            }
        }

        /// <summary>
        /// Список удалённых продуктов.
        /// </summary>
        public List<Product> DeletedProducts
        {
            get
            {
                return GetProductsFromFile().Where(p => p.IsDeleted).ToList();
            }
        }

        /// <summary>
        /// Получает продукты из файла.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        private List<Product> GetProductsFromFile()
        {
            var products = new List<Product>();
            if (File.Exists(productStorageFileName))
            {
                try
                {
                    var content = File.ReadAllText(productStorageFileName, Encoding.UTF8);
                    products = JsonConvert.DeserializeObject<List<Product>>(content);
                }
                catch
                {
                    throw new ApplicationException("Can't get products");
                }
            }
            return products;
        }

        /// <summary>
        /// Создает позицию продукта.
        /// </summary>
        /// <param name="newProduct">Продукт.</param>
        public void CreateProduct(Product newProduct)
        {
            var products = GetProductsFromFile();
            CheckArticle(newProduct, products);

            try
            {
                products.Add(newProduct);
                string output = JsonConvert.SerializeObject(products);
                File.WriteAllText(productStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save product");
            }
        }

        /// <summary>
        /// Проверяет артикул.
        /// </summary>
        /// <param name="newProduct">Продукт.</param>
        /// <param name="products">Список продуктов.</param>
        private static void CheckArticle(Product newProduct, List<Product> products)
        {
            if (products.Any(user => user.Article.ToLower() == newProduct.Article.ToLower()))
            {
                throw new ApplicationException("Product with this article alredy exists");
            }
        }

        /// <summary>
        /// Редактирует продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <param name="oldArticle">Старый артикул.</param>
        public void EditProduct(Product product, string oldArticle)
        {
            var products = GetProductsFromFile();

            var currentTocheck = products.FirstOrDefault(p => p.Article.ToLower() == oldArticle.ToLower());
            if (currentTocheck is null)
            {
                throw new ApplicationException("Can't find product");
            }

            CheckArticle(product, products.Where(p => p != currentTocheck).ToList());
            currentTocheck.Article = product.Article;
            currentTocheck.Name = product.Name;
            currentTocheck.Price = product.Price;

            try
            {
                string output = JsonConvert.SerializeObject(products);
                File.WriteAllText(productStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save product");
            }
        }

        /// <summary>
        /// Удаляет продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        public void DeleteProduct(Product product)
        {
            var products = GetProductsFromFile();

            var currentTocheck = products.FirstOrDefault(p => p.Article.ToLower() == product.Article.ToLower());
            if (currentTocheck is null)
            {
                throw new ApplicationException("Can't find product");
            }
            currentTocheck.IsDeleted = true;

            try
            {
                string output = JsonConvert.SerializeObject(products);
                File.WriteAllText(productStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save product");
            }
        }

        /// <summary>
        /// Восстанавливает продукт.
        /// </summary>
        /// <param name="product">Продукт.</param>
        public void RestoreProduct(Product product)
        {
            var products = GetProductsFromFile();

            var currentTocheck = products.FirstOrDefault(p => p.Article.ToLower() == product.Article.ToLower());
            if (currentTocheck is null)
            {
                throw new ApplicationException("Can't find product");
            }
            currentTocheck.IsDeleted = false;

            try
            {
                string output = JsonConvert.SerializeObject(products);
                File.WriteAllText(productStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save product");
            }
        }
    }
}
