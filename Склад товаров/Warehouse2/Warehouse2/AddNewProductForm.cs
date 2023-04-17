using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse2.Entities;

namespace Warehouse2
{
    public partial class AddNewProductForm : Form
    {
        public Product NewProduct;
        public AddNewProductForm()
        {
            InitializeComponent();
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

            NewProduct = new Product()
            {
                Name = this.textBox1.Text,
                Article = this.textBox1.Text,
                Price = this.numericUpDown1.Value,
                Count = (int)this.numericUpDown1.Value
            };
            this.DialogResult = DialogResult.OK;
        }
    }
}
