using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse2
{
    public partial class AddNewClassifierForm : Form
    {
        public string ClassifierName;
        public int SortCode = 0;
        private List<string> existingClassifiers;
        public AddNewClassifierForm(List<string> existingClassifiers)
        {
            this.existingClassifiers = existingClassifiers;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Name can not be empty");
                return;
            }
            if (existingClassifiers.Any(x=>x==this.textBox1.Text))
            {
                MessageBox.Show("This classifier already exists");
                return;
            }
            this.ClassifierName = this.textBox1.Text;
            this.SortCode = (int)this.numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
