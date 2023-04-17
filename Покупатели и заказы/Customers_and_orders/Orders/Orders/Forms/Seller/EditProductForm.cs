using Orders.Entities;
using Orders.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders.Forms.Seller
{
    public partial class EditProductForm : Form
    {
        private Product product;
        public EditProductForm(Product product)
        {
            InitializeComponent();
            this.product = product;
            this.textBox1.Text = product.Name;
            this.textBox2.Text = product.Article;
            this.numericUpDown1.Value = product.Price;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Name can not be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                MessageBox.Show("Article can not be empty");
                return;
            }

            var newProduct = new Product()
            {
                Name = this.textBox1.Text,
                Article = this.textBox2.Text,
                Price = this.numericUpDown1.Value
            };
            var productManager = new ProductManager();
            try
            {
                productManager.EditProduct(newProduct, product.Article);
                this.DialogResult = DialogResult.OK;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save product");
            }
        }
    }
}
