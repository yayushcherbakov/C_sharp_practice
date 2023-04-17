using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataAnalysis2
{
    public partial class GraphForm : Form
    {
        private readonly List<DataPoint> data;
        public GraphForm(List<DataPoint> data, string abscissaName, string ordinateName)
        {
            this.data = data;

            InitializeComponent();

            // Добавление серии.
            var series = this.chart1.Series.Add($"{abscissaName}->{ordinateName}");
            series.ChartType = SeriesChartType.Line;

            // Добавление точек.
            this.data = this.data.OrderBy(x => x.XValue).ToList();

            // Запись точек.
            foreach (var point in this.data)
            {
                series.Points.Add(point);
            }

            Axis ax = new Axis();
            ax.Title = abscissaName;
            chart1.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = ordinateName;
            chart1.ChartAreas[0].AxisY = ay;
        }

        // Событие открыти панели инструментов для сохранения.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File | *.png";
            saveFileDialog.ShowDialog();
            saveFileDialog.FileOk += Save;
        }

        // Сохранение изображени в формате png.
        private void Save(object sender, CancelEventArgs e)
        {
            var dialog = (SaveFileDialog)sender;
            try
            {
                chart1.SaveImage(dialog.FileName, ChartImageFormat.Png);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить.");
            }
        }
    }
}

