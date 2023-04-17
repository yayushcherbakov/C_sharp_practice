using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAnalysis2
{
    public partial class StatisticForm : Form
    {
        List<double> data;
        string xName;

        // Конструктор формы со статистикой.
        public StatisticForm(List<double> data, string xName)
        {
            this.data = data;
            this.xName = xName;

            InitializeComponent();

            ShowInfo(GetAverageValue(), GetMedianValue(), GetSquareDeviationValue(), GetDisperseValue());
        }

        // Выводит на экран статистические данные.
        void ShowInfo(double average, double median, double squareDeviation, double disperse)
        {
            richTextBox1.Font = new Font("Tahoma", 14, FontStyle.Bold);
            richTextBox1.AppendText($"Cреднее значение: {average:0.000}\n\nМедиана: {median:0.000}\n\nСреднеквадратичное отклонение: {squareDeviation:0.000}\n\nДисперсия: {disperse:0.000}");
        }

        // Вычисляет среднее арифметическое.
        double GetAverageValue()
        {
            return data.Average();
        }

        //// Вычисляет медиану.
        double GetMedianValue()
        {

            var count = data.Count();
            var sortedData = data.OrderBy(x => x).ToList<double>();

            return count % 2 == 1 ? sortedData[count / 2] : (sortedData[count / 2] + sortedData[(count / 2) - 1]) / 2;
        }

        // Вычисляет среднеквадратичное отклонение.
        double GetSquareDeviationValue()
        {
            return Math.Sqrt(GetDisperseValue());
        }

        // Вычисляет дисперсию.
        double GetDisperseValue()
        {
            var uniqueValues = data.Distinct().ToList<double>();

            var probabilities = new List<double>();
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                probabilities.Add(data.Count(x => x == uniqueValues[i]) / (double)data.Count);
            }

            var math1 = new List<double>();

            for (int i = 0; i < uniqueValues.Count; i++)
            {
                math1.Add(uniqueValues[i] * uniqueValues[i] * probabilities[i]);
            }

            // Математическое ожидание от (х*х).
            var sumMath1 = math1.Sum();

            var math2 = new List<double>();
            for (int i = 0; i < uniqueValues.Count; i++)
            {
                math2.Add(uniqueValues[i] * probabilities[i]);
            }

            // Квадрат математического ожидания от (x).
            var sumMath2 = math2.Sum() * math2.Sum();

            // дисперсия = матожидание(х*х) - мфтожидание^2(х).
            var disperse = sumMath1 - sumMath2;

            return disperse;
        }
    }
}
