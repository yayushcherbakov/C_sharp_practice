using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    /// <summary>
    /// Базовый класс для фракталов.
    /// </summary>
    public abstract class Fractal
    {
        private int itarationCount;
        protected int ItarationCount 
        {
            get
            {
                return itarationCount;
            }
            set
            {
                itarationCount = value;
                Gr.Clear(Color.White);
                Draw();
            } 
        }

        protected int ViewBoxSize { get; set; }
        protected Pen DrawPen { get; set; }
        protected Pen BackgroundPen { get; set; }
        protected Graphics Gr { get; set; }
        public Fractal(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize)
        {
            this.itarationCount = itarationCount;
            this.Gr = gr;
            this.DrawPen = new Pen(drawColor, 1);
            this.BackgroundPen = new Pen(backgroundColor, 1);
            this.ViewBoxSize = viewBoxSize;
        }

        /// <summary>
        /// Абстрактный метод отрисовки фрактала
        /// </summary>
        public abstract void Draw();
    }
}
