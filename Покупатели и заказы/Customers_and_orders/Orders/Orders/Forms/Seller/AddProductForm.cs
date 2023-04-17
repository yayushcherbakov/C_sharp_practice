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
    public partial class AddProductForm : Form
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public AddProductForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие нажатия на кнопку. 
        /// </summary>
        private void Button1Click(object sender, EventArgs e)
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
                productManager.CreateProduct(newProduct);
                this.DialogResult = DialogResult.OK;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't create product");
            }
        }
    }
}
