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
    public partial class EditClassifierForm : Form
    {
        public string ClassifierName;

        private List<string> existingClassifiers;
        public int SortCode = 0;
        public EditClassifierForm(string oldName, int sortCode, List<string> existingClassifiers)
        {
            this.existingClassifiers = existingClassifiers.Where(x => x != oldName).ToList();
            InitializeComponent();
            this.textBox1.Text = oldName;
            this.numericUpDown1.Value = sortCode;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Name can not be empty");
                return;
            }
            if (existingClassifiers.Any(x => x == this.textBox1.Text))
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
