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
    public partial class GenerateClassifiersForm : Form
    {
        public List<Classifier> Classifiers = new List<Classifier>();
        private List<string> existingClassifiers = new List<string>();
        public GenerateClassifiersForm(List<string> existingClassifiers)
        {
            this.existingClassifiers = existingClassifiers;
            InitializeComponent();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var prefix = this.textBox1.Text;
            var count = (int)this.numericUpDown1.Value;
            var sortCode = (int)this.numericUpDown2.Value;
            var random = new Random();
            for (int i=0; i < count; i++)
            {
                var name = $"{prefix}{random.Next(0, int.MaxValue)}";
                if(this.existingClassifiers.Any(x=>x == name))
                {
                    i--;
                    continue;
                }
                Classifiers.Add(new Classifier()
                {
                    Name = name,
                    SortCode = sortCode
                });
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
