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
    public partial class HistogramForm : Form
    {
        private readonly List<string> data;
        public HistogramForm(List<string> data)
        {
            this.data = data;

            InitializeComponent();

            // Массив данных.
            var seriesArray = this.data.Distinct().ToArray();

            for (int i = 0; i < seriesArray.Length; i++)
            {
                // Добавление серии.
                var series = this.chart1.Series.Add(seriesArray[i]);

                var count = data.Count(dataItem => dataItem == seriesArray[i]);

                // Добавление точки.
                series.Points.Add(count);
            }
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

        // Изменение цвета у столбца гистограммы.
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            var result = this.chart1.HitTest(e.X, e.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                var obj = (DataPoint)result.Object;
                var colorDialog = new ColorDialog();
                colorDialog.ShowDialog();
                result.Series.Color = colorDialog.Color;
            }
        }
    }
}
