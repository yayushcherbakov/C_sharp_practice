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
    public partial class GeterateProductsForm : Form
    {
        public List<Product> Products= new List<Product>();
        public GeterateProductsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var namePrefix = this.textBox1.Text;
            var articlePrefix = this.textBox2.Text;
            var count = (int)this.numericUpDown1.Value;
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                Products.Add(new Product()
                {
                    Name = $"{namePrefix}{random.Next(0, int.MaxValue)}",
                    Article = $"{articlePrefix}{random.Next(0, int.MaxValue)}",
                    Count = random.Next(0, 99999),
                    Price = random.Next(0, 99999)/100m
                });
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
