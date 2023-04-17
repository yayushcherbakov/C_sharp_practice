using Carts.Managers;
using Orders.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders.Forms.Buyer
{
    public partial class AddProductToCartForm : Form
    {
        private Product product;
        private string userId;

        /// <summary>
        /// Добавляет продукт в форму карзины заказов.
        /// </summary>
        /// <param name="product">Продукт.</param>
        /// <param name="userId">Айди пользователя.</param>
        public AddProductToCartForm(Product product, string userId)
        {
            InitializeComponent();
            this.product = product;
            this.userId = userId;
            this.label1.Text = product.Name;
            this.label2.Text = product.Article;
            this.label3.Text = product.Price.ToString("C", CultureInfo.CreateSpecificCulture("RU-ru"));

            var cartManager = new CartManager();
            try
            {
                var cart = cartManager.GetUserCart(userId);
                var productItem = cart.Items.FirstOrDefault(x => x.Article == product.Article);
                if (productItem is null)
                {
                    this.numericUpDownCount.Value = 1;
                }
                else
                {
                    this.numericUpDownCount.Value = productItem.Count;
                }
            }
            catch (Exception)
            {
                this.numericUpDownCount.Value = 1;
            }
        }

        /// <summary>
        /// Событие при добавлении в корзину. 
        /// </summary>
        private void AddToCartClick(object sender, EventArgs e)
        {
            var cartManager = new CartManager();
            try
            {
                var cart = cartManager.GetUserCart(userId);
                var productItem = cart.Items.FirstOrDefault(x => x.Article == product.Article);
                if (productItem is null)
                {
                    productItem = new OrderItem()
                    {
                        Article = product.Article,
                        Count = 0
                    };
                    cart.Items.Add(productItem);
                }
                productItem.Name = product.Name;
                productItem.Price = product.Price;
                productItem.Count = (int)this.numericUpDownCount.Value;

                cartManager.SaveCart(cart);
                this.DialogResult = DialogResult.OK;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't add product");
            }
        }
    }
}
