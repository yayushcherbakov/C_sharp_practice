using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataAnalysis2
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Значение для ячейки по умолчанию.
        /// </summary>
        private readonly string defaultValue = string.Empty;

        /// <summary>
        /// Запуск основной формы для работы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// События нажатия на элементы панели инстурментов. 
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Информация о событии.</param>
        private void openNewTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание нового файлового  диалога.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Фильтр на для открытия *.csv файлов. 
            openFileDialog.Filter = "Файлы CSV|*.csv";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string[] rows = new string[0];

            try
            {
                // Попытка считать информацию из файла.
                rows = File.ReadAllLines(openFileDialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось отккрыть файл.");

                return;
            }


            if (rows.Length == 0)
                return;

            // Очистка столбцов и колонок.
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Замена проблемных символов для успешного парса.
            foreach (string header in rows[0].Replace(", ", "@@@@@@").Split(",".ToCharArray()))
            {
                var hdr = RemoveQuotes(header.Replace("@@@@@@", ", "));
                dataGridView1.Columns.Add(hdr, hdr);
            }

            // Парс данных в таблицу и обратная замена проблемных символов.
            for (int i = 1; i < rows.Length; i++)
            {
                var currentStrings = rows[i].Replace(", ", "@@@@@@").Split(",".ToCharArray());

                for (int j = 0; j < currentStrings.Length; j++)
                {
                    currentStrings[j] = RemoveQuotes(currentStrings[j].Replace("@@@@@@", ", "));
                }

                dataGridView1.Rows.Add(currentStrings);
            }
        }

        /// <summary>
        /// Удаление лишних кавычек.
        /// </summary>
        /// <param name="str">Строчка для удаления кавычек.</param>
        /// <returns>Строчка в результате удаления кавычек.</returns>
        private string RemoveQuotes(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;

            return str[0] == '"' && str[str.Length - 1] == '"' ? str.Substring(1, str.Length - 2) : str;
        }

        /// <summary>
        /// События нажатия на ячейки панели инстурментов.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Информация о событии.</param>
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Создание и конструирование ячеей к панели инструментов.
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Создать гистограмм"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));

            }
        }

        /// <summary>
        /// Событие нажатия на элемент для отображения статистики.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Информация о событии.</param>
        private void statisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Считывание выбранных колонок.
            var selectedColumns = dataGridView1.SelectedCells.Cast<DataGridViewCell>().Select(x => x.ColumnIndex).Distinct().ToList();

            if (selectedColumns.Count() != 1)
            {
                MessageBox.Show("Должна быть выделена только одна колонка!");
                return;
            }

            var columnIndex = selectedColumns[0];

            var data = new List<double>();

            // Считывание, проверка и попытка записи данных.
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var xAsString = row.Cells[columnIndex].Value as string;

                if (xAsString == defaultValue)
                {
                    continue;
                }

                if (double.TryParse(xAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double x))
                {
                    data.Add(x);

                    continue;
                }

                MessageBox.Show("Не все ячейки имеют числовое значение!");

                return;
            }

            var xName = dataGridView1.Columns[columnIndex].HeaderText;

            var statisticForm = new StatisticForm(data, xName);

            statisticForm.Show();
        }

        /// <summary>
        /// Событие нажатия на элемент для отображения графика.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Информация о событии.</param>
        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedColumns = dataGridView1.SelectedCells.Cast<DataGridViewCell>().Select(x => x.ColumnIndex).Distinct().ToList();

            if (selectedColumns.Count() != 2)
            {
                MessageBox.Show("Должны быть выделены только две колонки!");

                return;
            }

            var xColumnIndex = selectedColumns[0];
            var yColumnIndex = selectedColumns[1];

            var data = new List<DataPoint>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var xAsString = row.Cells[xColumnIndex].Value as string;
                var yAsString = row.Cells[yColumnIndex].Value as string;

                if (xAsString == defaultValue || yAsString == defaultValue)
                {
                    continue;
                }

                if (double.TryParse(xAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double x) && double.TryParse(yAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
                {
                    data.Add(new DataPoint(x, y));

                    continue;
                };

                MessageBox.Show("Не все ячейки имеют числовое значение!");

                return;
            }

            var xName = dataGridView1.Columns[xColumnIndex].HeaderText;
            var yName = dataGridView1.Columns[yColumnIndex].HeaderText;

            var graphForm = new GraphForm(data, xName, yName);

            graphForm.Show();
        }

        /// <summary>
        /// Событие нажатия на элемент для отображения гистограммы.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Информация о событии.</param>
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedColumns = dataGridView1.SelectedCells.Cast<DataGridViewCell>().Select(x => x.ColumnIndex).Distinct().ToList();

            if (selectedColumns.Count() != 1)
            {
                MessageBox.Show("Должна быть выделена только одна колонка!");

                return;
            }

            var columnIndex = selectedColumns[0];

            var data = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
                data.Add(row.Cells[columnIndex].Value.ToString());

            var numericData = new List<double>();

            foreach (var cell in data)
            {

                if (cell == defaultValue)
                {
                    continue;
                }

                if (double.TryParse(cell, NumberStyles.Any, CultureInfo.InvariantCulture, out double x))
                {
                    numericData.Add(x);

                    continue;
                };

                var histogram = new HistogramForm(data);

                histogram.Show();

                return;
            }

            var numericHistogram = new NumericHistogramForm(numericData);

            numericHistogram.Show();

        }
    }
}
