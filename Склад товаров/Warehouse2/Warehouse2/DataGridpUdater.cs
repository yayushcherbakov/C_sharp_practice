using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse2.Entities;

namespace Warehouse2
{
    public class DataGridpUdater
    {
        private DataGridView dataGridView1;
        public DataGridpUdater(DataGridView dataGridView1)
        {
            this.dataGridView1 = dataGridView1;
            InitDataGridView();
        }
        private void InitDataGridView()
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Article";
            column.Name = "Article";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Price";
            column.Name = "Price";
            column.DefaultCellStyle.Format = "N2";
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Count";
            column.Name = "Count";
            dataGridView1.Columns.Add(column);

            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(DataGridView1CellValidating);
            this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(DataGridView1CellEndEdit);
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void DataGridView1CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            if (!(headerText.Equals("Price") || headerText.Equals("Count"))) return;

            decimal price = -1m;
            if (headerText.Equals("Price") && !decimal.TryParse(e.FormattedValue.ToString(), out price))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText =
                    "Price must be decimal type";
                e.Cancel = true;
            }

            if (headerText.Equals("Price") && price < 0m)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText =
                    "Price mustn`t be negative";
                e.Cancel = true;
            }

            int count = -1;

            if (headerText.Equals("Count") && !int.TryParse(e.FormattedValue.ToString(), out count))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText =
                    "Count must be integer type";
                e.Cancel = true;
            }

            if (headerText.Equals("Count") && count < 0)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText =
                    "Count mustn`t be negative";
                e.Cancel = true;
            }
        }
        void DataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.
            dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        public void UpdateDataGridViewSource(Classifier classifer)
        {
            BindingSource source = new BindingSource();
            source.DataSource = classifer.Products;
            dataGridView1.DataSource = source;
            dataGridView1.AutoGenerateColumns = true;
        }
    }
}
