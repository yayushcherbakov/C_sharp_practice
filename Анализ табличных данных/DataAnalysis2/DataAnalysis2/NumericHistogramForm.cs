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
    public partial class NumericHistogramForm : Form
    {
        private readonly List<double> data;

        public NumericHistogramForm(List<double> data)
        {
            this.data = data;

            InitializeComponent();

            // Массив данных.
            this.numericUpDown1.ValueChanged += GetNumericData;
            var fullRange = data.Max() - data.Min();
            this.numericUpDown1.Minimum = Convert.ToDecimal(fullRange * 0.1);
            this.numericUpDown1.Maximum = Convert.ToDecimal(fullRange);
            var wide = fullRange * 0.2;

            // Отрисовка диаграммы.
            DrawChart(wide);
        }

        /// <summary>
        /// Отрисовка диаграммы.
        /// </summary>
        /// <param name="wide">Шаг отрисовки.</param>
        private void DrawChart(double wide)
        {
            this.chart1.Series.Clear();


            for (double i = data.Min(); i < data.Max(); i += wide)
            {
                // Add series.
                var series = this.chart1.Series.Add($"{i}-{i + wide}");
                var count = data.Count(dataItem => dataItem >= i && dataItem < i + wide);
                // Add point.
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

        // Получить числовы данные и отрисовать диаграмму.
        private void GetNumericData(object sender, EventArgs e)
        {
            DrawChart(Convert.ToDouble(numericUpDown1.Value));
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
