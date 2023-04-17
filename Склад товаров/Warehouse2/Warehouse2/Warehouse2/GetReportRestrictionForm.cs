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
    public partial class GetReportRestrictionForm : Form
    {
        public int MinCount = 0;
        public string ReportPath;
        public GetReportRestrictionForm()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Report(*.csv)|*.csv";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            this.ReportPath = saveFileDialog1.FileName;
            this.label2.Text = this.ReportPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReportPath))
            {
                MessageBox.Show("Please set report path.");
                return;
            }
            this.MinCount = (int)this.numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
