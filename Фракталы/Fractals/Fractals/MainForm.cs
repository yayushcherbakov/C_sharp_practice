using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals
{
    /// <summary>
    /// Главная форма приложения.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки итераций для KochCurve.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyKochCurveButtonClick(object sender, EventArgs e)
        {
            var fractal = new KochCurve(this.trackBarKochCurve.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик ползунка итераций для KochCurve.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarKochCurveValueChanged(object sender, EventArgs e)
        {
            this.labelKochCurve.Text = this.trackBarKochCurve.Value.ToString();
            var fractal = new KochCurve(this.trackBarKochCurve.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик кнопки итераций для PythagorasTree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyPythagorasTreeButtonClick(object sender, EventArgs e)
        {
            var fractal = new PythagorasTree(this.trackBarPythagorasTree5.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик ползунка итераций для PythagorasTree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarPythagorasTreeValueChanged(object sender, EventArgs e)
        {
            this.labelPythagorasTree.Text = this.trackBarPythagorasTree5.Value.ToString();
            var fractal = new PythagorasTree(this.trackBarPythagorasTree5.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик кнопки итераций для SerpinskiTriangle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerpinskiTriangleButtonClick(object sender, EventArgs e)
        {
            var fractal = new SerpinskiTriangle(this.trackBarSerpinskiTriangle.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик ползунка итераций для SerpinskiTriangle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarSerpinskiTriangleValueChanged(object sender, EventArgs e)
        {
            this.labelSerpinskiTriangle.Text = this.trackBarSerpinskiTriangle.Value.ToString();
            var fractal = new SerpinskiTriangle(this.trackBarSerpinskiTriangle.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик кнопки итераций для SerpinskiCarpet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerpinskiCarpetButtonClick(object sender, EventArgs e)
        {
            var fractal = new SerpinskiCarpet(this.trackBarSerpinskiCarpet.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик ползунка итераций для SerpinskiCarpet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarSerpinskiCarpetValueChanged(object sender, EventArgs e)
        {
            this.labelSerpinskiCarpet.Text = this.trackBarSerpinskiCarpet.Value.ToString();
            var fractal = new SerpinskiCarpet(this.trackBarSerpinskiCarpet.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик кнопки итераций для CantorSet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CantorSetButtonClick(object sender, EventArgs e)
        {
            var fractal = new CantorSet(this.trackBarCantorSet.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }

        /// <summary>
        /// Обработчик ползунка итераций для CantorSet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarCantorSetValueChanged(object sender, EventArgs e)
        {
            this.labelCantorSet.Text = this.trackBarCantorSet.Value.ToString();
            var fractal = new CantorSet(this.trackBarCantorSet.Value, pictureBox.CreateGraphics(), Color.Black, Color.White, pictureBox.Width);
            fractal.Draw();
        }
    }
}
